using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Windows.Forms;

namespace bt
{
    class Stats
    {
        DateTime dtStart;
        int iMeanCars, iCountCars, iSumCars;
        //int iMeanCash, iCountCash, iSumCash;
        int iCountRob; long lMeanRob, lSumRob;
        int iLvl, iStartLvl, iPhase, iHospital, iSell, iTools, iOil;
        public enum countries { se=0, us=10, jp=15, ru=20 }
        long lHospital;

        public Stats(HtmlDocument doc)
        {
            dtStart = DateTime.Now;
            iMeanCars = iCountCars = iSumCars = 0;
            //iMeanCash = iCountCash = iSumCash = 0;
            iCountRob = 0;
            lMeanRob = lSumRob = 0;
            iHospital = iSell = 0;
            lHospital = 0;
            iStartLvl = iLvl = getLevel(doc);
            iTools = getTools(doc);
            iPhase = getPhase(doc);
        }

        public int getLevel() { return iLvl; }
        public int getTools() { return iTools;  }
        public int getOil() { return iOil; }
        public int getStartLevel() { return iStartLvl; }
        public int getPhase() { return iPhase; } //0 == usa/japan BUT LE WHY?!?!?
        public void incrHospital() { ++iHospital; }
        public void incrSell() { ++iSell; }

        private int getLevel(HtmlDocument doc) { return getVar(doc, "Level");  }
        private int getTools(HtmlDocument doc) { return getVar(doc, "Verktyg"); }
        private int getOil(HtmlDocument doc) { return getVar(doc, "Olja"); }

        private int getPhase(HtmlDocument doc) { 
            int fas = getVar(doc, "Fas");
            if(fas == 0) fas = getCountry(doc);
            return fas;
        }

        public void update(string info) 
        {
            long lTmp;
            //Steal
            lTmp = getIntBetween(info, "stal ", " st");
            if (lTmp > 0)
            {
                ++iCountCars;
                iSumCars += (int)lTmp;
                iMeanCars = Convert.ToInt32(Math.Round(1.0 * iSumCars / iCountCars));
                return;
            }
            //HospitalCost
            lTmp = getIntBetween(info, "sjukhuset", "kr");
            if (lTmp > 0) { lHospital = lTmp; return; }
            //Råna
            lTmp = getIntBetween(info, "gav dig ", " kr");
            if (lTmp > 0)
            {
                ++iCountRob;
                lSumRob += lTmp;
                lMeanRob = lSumRob / iCountRob;
                return;
            }
            lTmp = getIntBetween(info, "kostade dig ", " kr");
            if (lTmp > 0)
            {
                ++iCountRob;
                lSumRob -= lTmp;
                lMeanRob = lSumRob / iCountRob;
                return;
            }
        
        }

        public void update(HtmlDocument doc) 
        {
            iLvl = getLevel(doc);
            iPhase = getPhase(doc);
            iTools = getTools(doc);
            iOil = getOil(doc);
        }

        public string getStats() 
        {
            double dHours = (DateTime.Now - dtStart).TotalHours;
            string sOut = "";
            sOut += "Levels/h      " + Math.Round((iLvl - iStartLvl) / dHours, 1).ToString() + " \n";
            sOut += "Levels totalt " + (iLvl - iStartLvl).ToString() + " \n";
            sOut += "Stulna bilar  " + iSumCars.ToString() + "\n";
            sOut += "Bilar/stöt    " + iMeanCars.ToString() + "\n";
            sOut += "Råneinkomst   " + toCashString(lSumRob) + "\n";
            sOut += "Inkomst/Rån   " + toCashString(lMeanRob) + "\n";
            sOut += "Sjukhusbesök  " + iHospital + "\n";
            sOut += "Heal/h        " + Math.Round(iHospital / dHours, 1).ToString() + "\n";
            sOut += "Fulla lokaler " + iSell + "\n";
            sOut += "Lokaler/h     " + Math.Round(iSell / dHours, 1).ToString() + "\n";

            string sRuntime = (DateTime.Now - dtStart).ToString();
            sOut += "Runtime       " + sRuntime.Substring(0,sRuntime.IndexOf("."));
            
            return sOut;
        }

        public string toCashString(long lCash) 
        {
            double dCash = lCash;
            string sCash;
            if (lCash >= 1000000000)
                sCash = Math.Round(dCash /= 1000000000, 2).ToString() + " md";
            else if (lCash >= 1000000)
                sCash = Math.Round(dCash /= 1000000, 1).ToString() + " mn";
            else if (lCash >= 10000)
                sCash = Math.Round(dCash /= 1000).ToString() + " k";
            else
                sCash = lCash.ToString();
            return sCash;
        }

        private int getCountry(HtmlDocument doc) {
            HtmlElement el = doc.GetElementById("ctl04_imgPlace");
            if (el == null) {
                el = doc.GetElementById("ctl00_ctl00_cph1_cph2_ctrlMainMenu_ctrlUserMenuData_imgPlace");
                if (el == null) return 0;
            } 
            int country = 0;
            switch (el.GetAttribute("ALT"))
            {
                case "USA": country = Convert.ToInt16(countries.us); break;
                case "Japan": country = Convert.ToInt16(countries.jp); break;
                case "Ryssland": country = Convert.ToInt16(countries.ru); break;
            }
            return country;
        }

        private int getVar(HtmlDocument doc, string sVar)
        {
            HtmlElementCollection els = doc.GetElementsByTagName("td");
            foreach (HtmlElement el in els)
            {
                if (el.InnerText == sVar + ":")
                {
                    return Convert.ToInt16(Regex.Replace(el.NextSibling.InnerText.Split('/')[0], "[^0-9]", ""));
                }
            }
            return 0;
        }

        private string getBetween(string str, string first, string last)
        {
            int iStart = str.IndexOf(first) + first.Length;
            int iEnd = str.IndexOf(last, iStart);
            if (iStart-first.Length < 0 || iEnd < 0) return "0";
            return str.Substring(iStart, iEnd - iStart);
        }

        private long getIntBetween(string str, string first, string last)
        {
            int iStart = str.IndexOf(first) + first.Length;
            int iEnd = str.IndexOf(last, iStart);
            if (iStart - first.Length < 0 || iEnd < 0) return 0;
            return Convert.ToInt64(Regex.Replace(str.Substring(iStart, iEnd - iStart),"[^0-9]", ""));
        }


    }
}
