using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text.RegularExpressions;
using System.Windows.Forms;
using System.Net;
using System.Threading;
using System.IO;
using System.Runtime.InteropServices;

namespace bt
{
    public partial class frmMain : Form
    {
        
        string sVersion = "0.9.7.0";
        bool bDebug = true;

        string sTmpFilePath = System.IO.Path.GetTempPath() + "bt.tmp";

        [DllImport("user32.dll")]
        static extern Int32 FlashWindowEx(ref FLASHWINFO pwfi);

        [StructLayout(LayoutKind.Sequential)]
        public struct FLASHWINFO
        {
            public UInt32 cbSize;
            public IntPtr hwnd;
            public Int32 dwFlags;
            public UInt32 uCount;
            public Int32 dwTimeout;
        }

        Random rnd = new Random();

        List<string> friends;
        List<string> vips;
        string sUrFkt="";
        int minuteTick = 0, loginCounter = 0, bankCounter = 0;
        string sLatestVersion = "";
        int iCurVer = 0;
        Stats stats = null;
        DateTime dtWait;
        
        DateTime dtGang;
        bool bVice = true;
        string sGangURL = "";
        string sGang = "";
        string sMyID = "";
        int iGangLvl = 0;

        long lCash = 0;
        long lTotCash = 0;
        long lOilCost = 0;
        int iHealth = 0, iCarProg = 0, iCarDmg = 0;
        int iWait = 500;

        bool pause = false;
        bool gangOK = false;
        bool bNotified = false;
        bool bImBoss = false;
        bool bFriendsLoaded = false;
        bool isDick = false;
        bool bGetTF = false;
        bool bTransfer = false;
        string[] dicks;

        int iRepeatRUS = 0;
        int iEnergy = 100;
        int iTmpPhase = 0;
        int iErrCount = 0;
        int iRaceTrackInterval = 75;
        int iUserRobberiesInterval = 5 * 60; // 3 för fas 4? usa???
        int iRobUserInterval = 20 * 60;
        int iRobUserPhase4 = 10 * 60;
        int iRaceUserInterval = 15 * 60;
        int iRUSRobInterval = 15 * 60;
        DateTime dtRaceTrack = DateTime.Now;
        DateTime dtRobRace = DateTime.Now; //.AddMinutes(2);
        DateTime dtRobUser = DateTime.Now; //.AddMinutes(2);
        DateTime dtRUSRob = DateTime.Now;
        DateTime dtRobEnd = DateTime.Now.AddYears(-1);
        DateTime dtRaceEnd = DateTime.Now.AddYears(-1);
        DateTime dtRusEnd = DateTime.Now.AddYears(-1);
        DateTime dtSpam = DateTime.Now;
        bool bRaceUser = false;

        int iRobInterval = 75;
        DateTime dtRob = DateTime.Now;

        int iStealInterval = 75;
        DateTime dtSteal = DateTime.Now;

        int iBankInterval = 900;
        DateTime dtBank = DateTime.Now;

        RobRace robrace;
        User activeUser;


        public frmMain()
        {
            InitializeComponent();
        }

        private bool isAwesome()
        {
            if (bDebug) debug("isAwesome");
            return (new string[] { "wayland", "sway_" }.Contains(txtUser.Text));
        }

        private bool isAwesome(string user)
        {
            if (bDebug) debug("isAwesome(" + user + ")");
            return (new string[] { "wayland", "sway_" }.Contains(user.ToLower()));
        }

        private void webBrowser1_DocumentCompleted(object sender, WebBrowserDocumentCompletedEventArgs e)
        {
            if (bDebug) debug("webBrowser1_DocumentCompleted: " + webBrowser1.Url.ToString());
            
            updateVars();

            System.Threading.Thread.Sleep(iWait);
            dtWait = DateTime.Now.AddMinutes(5);
            
            if (pause) return;

            string location = getLocation();
            switch (location) {
                case "hospital": heal(); break;
                case "sell": sell(); break;
                case "login": login(); break;
                case "bank": bank(); break;
                case "crime": idle.Enabled = true; break;
                case "gangprofile": setGangVars(); break;
                case "profile": handleUser(); break;
                case "robresults": robresults(); break;
                case "raceresults": raceresults(); break;
                case "gang": gang(); break;
                case "racetrack": raceTrack(); break;
                case "infobox": checkMail(); break;
                case "olja": olja(); break;
                case "online": updateUsers(); break;
                case "inbox": bNotified = false; break;
                case "friends": updateFriends(); break;
                case "sms": sms(); break;
                case "msg": msg(); break;
                case "status": 
                    if(webBrowser1.Document.GetElementById("lblInfo").InnerText.Contains("Felaktigt användarnamn"))
                    { 
                        txtPass.Text="";
                        lblIvnalidLogin.Visible = true;
                        webBrowser1.Visible = false;
                        gbLogin.Visible = true; 
                    }
                    webBrowser1.Navigate("http://biltjuv.se"); 
                    break;
                //case "armory": checkWeaponStatus(); break;
                default:                                    //FIXA!!!
                    if (stats == null) 
                    {
                        webBrowser1.Navigate("http://biltjuv.se/?p=crime&rnd=" + rnd.Next().ToString());
                    }
                    else
                    {
                        sGangURL = ""; //inte gängledare längre?
                    }
                    idle.Enabled = true;
                    break;
            }
        }

        private void msg()
        {
            //if (txtUser.Text != "wayland") return;
            /*
            if (bDebug) debug("btnInfo_Click");

            getCaptcha().Save("d:\\img\\" + rnd.Next().ToString() + ".png", ImageFormat.Png);

            System.Threading.Thread.Sleep(500);

            webBrowser1.Navigate(webBrowser1.Url);
            */
            /*string f = Application.StartupPath + "\\captcha.dat";
            Captcha c = new Captcha(f, getCaptcha());
            string s = c.solve();
            s = s;*/
        }

        private Bitmap getCaptcha()
        {
            mshtml.HTMLWindow2Class w2 = webBrowser1.Document.Window.DomWindow as mshtml.HTMLWindow2Class;
            w2.execScript("var ctrlRange=document.body.createControlRange();var imgs=document.getElementsByTagName('img');ctrlRange.add(imgs[imgs.length-1]);ctrlRange.execCommand('Copy');", "javascript");
            return new Bitmap(Clipboard.GetImage());
        }

        private void sms()
        {
            debug("sms");
            if (!bGetTF) { idle.Enabled = true; return; }

            HtmlDocument doc = webBrowser1.Document;
            HtmlElement el1 = doc.GetElementById("ctl08_tblInformation");
            //HtmlElement el2 = doc.GetElementById("ctl08_ctrlNewMessage_imgInformation");

            if (el1 != null && el1.InnerText.Contains("sms tills tiden"))
            {
                bGetTF = false;
                el1 = doc.GetElementById("ctl10_lbSMS_1");
                el1.SetAttribute("onclick", "");
                el1.InvokeMember("click");
            }
            else
            {
                el1 = doc.GetElementById("ctl10_lbSMS_2");
                el1.SetAttribute("onclick", "");
                el1.InvokeMember("click");
            }
        }


        /*
        private void checkWeaponStatus()
        {
            HtmlElement el = webBrowser1.Document.GetElementById("ctl08_tblInformation");
            if (!elOK(el)) return;
            if(el.InnerText.Contains("Grattis till ditt inköp")
            {
                robrace.resetRob(stats.getPhase());
            }
        }*/

        private void updateFriends()
        {
            if (bDebug) debug("updateFriends");
            HtmlElementCollection links = webBrowser1.Document.GetElementById("ctl10_tblFriends").GetElementsByTagName("A");
            string sUser;
            for (int i = 0; i < links.Count; i += 2)
            { 
                sUser = links[i].InnerText.ToLower();
                if (!isFriend(sUser)) friends.Add(sUser);
            }
            bFriendsLoaded = true;
            idle.Enabled = true;
        }

        private void robresults()
        {
            if (bDebug) debug("robresults");
            HtmlElement el = webBrowser1.Document.GetElementById("ctl10_lblResults");
            if (!elOK(el)) return;
            el = el.GetElementsByTagName("B")[1];
            if (!elOK(el)) return;

            string res = el.InnerText;
            if(stats.getPhase() == 4)
                activeUser.setRobTime(DateTime.Now.AddSeconds(iRobUserPhase4));
            else
                activeUser.setRobTime(DateTime.Now.AddSeconds(iRobUserInterval));
            dtRobUser = DateTime.Now.AddSeconds(iUserRobberiesInterval);
            if (res.Contains("Du vann"))
            {
                activeUser.incrementRobWon();
                activeUser.incrementRobInRow();
                debug("Successfull robbery (" + activeUser.getName() + ")");

            }
            else if (res.Contains("Du förlorade") || res.Contains("Du är död!"))
            {
                activeUser.incrementRobLost();
                activeUser.decrementRobInRow();
                if (activeUser.stopRobbing())
                {
                    activeUser.setRob(false);// setRobTime(DateTime.Now.AddDays(1));
                }
                debug("Failed robbery (" + activeUser.getName() + ")");
            }
            robrace.updateUser(activeUser);
            idle.Enabled = true;
            
        }
        
        private void raceresults()
        {
            if (bDebug) debug("raceresults");
            HtmlElement el = webBrowser1.Document.GetElementById("ctl10_lblResults");
            if (!elOK(el)) return;
            el = el.GetElementsByTagName("B")[0];
            if (!elOK(el)) return;

            string res = el.InnerText;
            if (res.Contains("Du vann"))
            {
                activeUser.incrementRaceWon();
                activeUser.incrementRaceInRow();
                debug("Won race (" + activeUser.getName() + ")");
            }
            else if (res.Contains("Du förlorade"))
            {
                activeUser.incrementRaceLost();
                activeUser.decrementRaceInRow();
                if(activeUser.stopRacing() || getIntBetween(res, "med ", " meter") > 100)
                {
                    activeUser.setRace(false);
                }
                debug("Lost race (" + activeUser.getName() + ")");
            }
            activeUser.setRaceTime(DateTime.Now.AddSeconds(iRaceUserInterval));
            robrace.updateUser(activeUser);
            idle.Enabled = true;
        }

  
        private void handleUser()
        {
            if (bDebug) debug("handleUsers");

            if (iHealth == 0) { idle.Enabled = true; return; }

            HtmlDocument doc = webBrowser1.Document;
            stats.update(doc);

            //Hemmaprofilen
            HtmlElement el = doc.GetElementById("ctl10_lblNumberOfVisitors");
            if (el != null)
            {
                //double check username
                if (txtUser.Text.ToLower() != doc.GetElementById("ctl10_lblUsername").InnerText.ToLower())
                {
                    MessageBox.Show("Ditt användarnamn stämmer inte övernes med dina inloggningsuppgifter!", "Duuude!", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                    Application.Exit();
                }

                //Ryssland
                if (cbRus.Checked && dtRUSRob < DateTime.Now || stats.getPhase() == 20)
                {
                    if (stats.getPhase() == 20) //ru
                    {
                        if (bDebug) debug("handleUsers - ryssland");
                        if (iEnergy > 2) //nyankommen
                        {
                            webBrowser1.Navigate("http://biltjuv.se/default.aspx?p=crime");
                            return;
                        }
                        else
                        {
                            iEnergy = 100;
                            dtRUSRob = DateTime.Now.AddSeconds(iRUSRobInterval);
                            if (iTmpPhase < 10)
                                doc.GetElementById("ctl10_ctrlChangePlace_rptrChangePlace_ctl00_lbChangePlace").InvokeMember("click");
                            else if (iTmpPhase == 10)
                                doc.GetElementById("ctl10_ctrlChangePlace_rptrChangePlace_ctl01_lbChangePlace").InvokeMember("click");
                            else if (iTmpPhase == 15)
                                doc.GetElementById("ctl10_ctrlChangePlace_rptrChangePlace_ctl02_lbChangePlace").InvokeMember("click");
                            else
                                MessageBox.Show("LOL WUT? Fas=" + iTmpPhase.ToString());
                            return;
                        }
                    }
                    else
                    {
                        iTmpPhase = stats.getPhase();
                        doc.GetElementById("ctl10_ctrlChangePlace_rptrChangePlace_ctl03_lbChangePlace").InvokeMember("click");
                        return;
                    }
                }
                else if (!cbRace.Checked && !cbRob.Checked) 
                { 
                    idle.Enabled = true; 
                    return; 
                }
                else if (!bFriendsLoaded)
                {
                    string sUrl = doc.GetElementById("ctl10_hlFriends").GetAttribute("HREF");
                    sMyID = sUrl.Substring(sUrl.IndexOf("id=") + 3);
                    webBrowser1.Navigate(sUrl);
                    return;
                }
            }

            el = doc.GetElementById("ctl08_tblInformation");
            int s = 0;
            if (el != null)
            {
                el = el.GetElementsByTagName("FONT")[0];
                if (!elOK(el)) return;
                if (el.InnerText.Contains("måste vänta"))
                {
                    int m;
                    m = Convert.ToInt16(getBetween(el.InnerText, "nta ", " min"));
                    s = Convert.ToInt16(getBetween(el.InnerText, "och ", " sek"));
                    s += m * 60;
                }
                else
                {
                    if (el.InnerText.Contains("inte online") || el.InnerText.Contains("inte inloggad"))
                    {
                        activeUser.setOffline();
                    }
                    else if (el.InnerText.Contains("är död"))
                    {
                        activeUser.setRobTime(DateTime.Now.AddMinutes(5));
                        activeUser.setRaceTime(DateTime.Now.AddMinutes(5));
                    }
                    else if (el.InnerText.Contains("offer"))
                    {
                        activeUser.setRobTime(DateTime.Now.AddHours(2));
                    }
                    else if (el.InnerText.Contains("beskyddar"))
                    {
                        activeUser.setRobTime(DateTime.Now.AddMinutes(20));
                    }
                    else 
                    {
                        debug("Something inconvenient is happening! (" + activeUser.getName() + ")");
                    }
                    robrace.updateUser(activeUser);
                    idle.Enabled = true;
                    return;
                }
                                
            }

            if (cbRob.Checked && activeUser.getRob() && activeUser.getRobTime() < DateTime.Now && dtRobUser < DateTime.Now)
            {
                if (s > 0)
                {
                    activeUser.setRobTime(DateTime.Now.AddSeconds(s));
                    robrace.updateUser(activeUser);
                    idle.Enabled = true;
                }
                else
                {
                    el = doc.GetElementById("ctl10_hlRob");
                    if (!elOK(el)) return;
                    el.SetAttribute("onclick", "");
                    el.InvokeMember("click");
                }
            }
            else if (cbRace.Checked && activeUser.getRace() && activeUser.getRaceTime() < DateTime.Now)
            {
                if (s > 0)
                {
                    if (s > 600 && stats.getPhase() == 4) s -= 600; //skrivs ut fel tid på bt
                    activeUser.setRaceTime(DateTime.Now.AddSeconds(s));
                    robrace.updateUser(activeUser);
                    idle.Enabled = true;
                }
                else
                {
                    el = doc.GetElementById("ctl10_hlRace");
                    if (el == null)
                    {
                        activeUser.setRace(false);
                        robrace.updateUser(activeUser);
                        idle.Enabled = true;
                    }
                    else
                    {
                        el.SetAttribute("onclick", "");
                        el.InvokeMember("click");
                    }
                }
            }
            else
            {
                if((object) activeUser != null)
                {
                    activeUser.setRobTime(DateTime.Now.AddMinutes(15));
                    activeUser.setRaceTime(DateTime.Now.AddMinutes(15));
                    robrace.updateUser(activeUser);
                }
                idle.Enabled = true;
            }
        }

        private void robRaceUser()
        {
            if (bDebug) debug("robRaceUser");
            
            if (cbRace.Checked && cbRob.Checked && dtRobUser < DateTime.Now) // rob/race
            {
                activeUser = robrace.getNextRobRace();
                if ((object)activeUser == null) { idle.Enabled = true; return; }
                if ((object)activeUser != null && activeUser.getRobRaceTime() > DateTime.Now)
                {
                    dtRobRace = activeUser.getRobRaceTime();
                    idle.Enabled = true;
                    return;
                }
            }
            else if (cbRob.Checked && dtRobUser < DateTime.Now) // rob/race, rob
            {
                activeUser = robrace.getNextRob();
                if ((object)activeUser == null) { idle.Enabled = true; return; }
                if (activeUser.getRobTime() > DateTime.Now)
                {
                    dtRobRace = activeUser.getRaceTime();
                    idle.Enabled = true;
                    return;
                }
                else
                {
                    dtRobRace = dtRobUser;
                }
            }
            else if (cbRace.Checked) // race
            {
                activeUser = robrace.getNextRace();
                if ((object)activeUser == null) { idle.Enabled = true; return; }
                if (activeUser.getRaceTime() > DateTime.Now)
                {
                    dtRobRace = activeUser.getRaceTime();
                    idle.Enabled = true;
                    return;
                }
            }
            else 
            {
                dtRobRace = dtRobUser;
                idle.Enabled = true;
                return;
            }
            webBrowser1.Navigate(activeUser.getUrl());
        }

        private void raceTrack() 
        {
            if (bDebug) debug("raceTrack");
            if (dtRaceTrack < DateTime.Now)
            {
                HtmlElement el = webBrowser1.Document.GetElementById("ctl10_btnGetSponsor");
                if (!elOK(el)) return;
                setDDLValGE("ctl10_ddlGetSponsor", 30);
                el.InvokeMember("click");
                dtRaceTrack = DateTime.Now.AddSeconds(iRaceTrackInterval + 2);
            }
            else
            {
                idle.Enabled = true;
            }
        }

        //sverige...
        private void updateUsers() 
        {
            if (bDebug) debug("updateUsers");
            if (!cbRace.Checked && !cbRob.Checked) return;
            HtmlDocument doc = webBrowser1.Document;
            HtmlElement elTable = doc.GetElementById("ctl10_tblOnline");
            if (!elOK(elTable)) return;
            HtmlElementCollection elsA = elTable.GetElementsByTagName("A");
            byte bTmp;
            byte bPage = 1;
            string sNextPage = "";
            string sHref = "";
            int iID = 0;
            string sName, sTmp;
            byte iPhase;
            foreach (HtmlElement el in elsA)
            { 
                sHref= el.GetAttribute("HREF");
                if (sHref.Contains("online"))
                {
                    if (sNextPage != "") continue;
                    bTmp = Convert.ToByte(el.InnerText);
                    if (bTmp > bPage)
                        sNextPage = sHref;
                    else
                        ++bPage;
                }
                else 
                {
                    sTmp = el.Parent.NextSibling.InnerText;
                    sTmp = sTmp.Substring(sTmp.IndexOf("s")+2, 1);
                    iPhase = Convert.ToByte(sTmp);
                    iID = Convert.ToInt32(sHref.Substring(sHref.IndexOf("i=") + 2));
                    sName = el.InnerText;
                    
                    if (!robrace.contains(sName) && !isFriend(sName))
                    {
                        robrace.addUser(sName, iID, (iPhase == stats.getPhase() || (iPhase >= 2 && stats.getPhase() >= 10)), true);    //(iPhase <= stats.getPhase() + 1));
                    }
                    else
                    {
                        if (isFriend(sName))
                            robrace.remove(iID);
                        else
                        {
                            robrace.update(iID, (iPhase == stats.getPhase() || (iPhase >= 2 && stats.getPhase() >= 10)), false);
                        }
                    }
                    
                }
            }

            if (sNextPage == "")
            {
                robrace.setNextUpdate(30);
                sNextPage = "http://biltjuv.se/?p=crime";
            }
            webBrowser1.Navigate(sNextPage);
        }



        private void olja() 
        {
            if (bDebug) debug("olja");

            long lTmpOilCost = 0;
            HtmlDocument doc = webBrowser1.Document;
            HtmlElement els = doc.GetElementById("ctl10_tblIncoming");
            HtmlElement a;
            if(!elOK(els)) return;

            lOilCost = 0;
            foreach (HtmlElement el in els.GetElementsByTagName("TR"))
            { 
                if(el.InnerHtml.Contains(" kr.</TD>"))
                {
                    //köp det man har råd med
                    lTmpOilCost = getIntBetween(el.InnerHtml, "Liter</TD>\r\n<TD vAlign=top>", " kr.");
                    if (lTmpOilCost <= lCash + Convert.ToInt64(txtCash.Text)) 
                    {
                        lOilCost = 0; lTmpOilCost = 0;
                        a = el.GetElementsByTagName("A")[1];
                        a.SetAttribute("onclick", "");
                        a.InvokeMember("click");
                        return;
                    }
                    //pengar att ta ut... gör ett kappsäckproblem av det om vill :p
                    if (lOilCost + lTmpOilCost < lTotCash + Convert.ToInt64(txtCash.Text))
                        lOilCost += lTmpOilCost;
                }
            }
            idle.Enabled = true;
        }

        private void checkMail()
        {
            if (bDebug) debug("checkMail");
            HtmlDocument doc = webBrowser1.Document;
            HtmlElementCollection els = getElementsByClass(doc, "tr-third-row", "tr");
            foreach (HtmlElement el in els) 
            {
                if(el.InnerText.Contains("Du har fått ett nytt erbjudande"))
                {
                    webBrowser1.Navigate("http://biltjuv.se/?p=sms/trade");
                    return;
                }
                else if (el.InnerText.Contains("Tid sms"))
                {
                    iRobInterval = 75;
                    iStealInterval = 75;
                }
                else if (txtUser.Text == "wayland" && el.InnerText.Contains("kr. till dig"))
                {
                    long amount = getIntBetween(el.InnerText, "överförde", "kr");
                    string user = el.GetElementsByTagName("A")[0].InnerText;
                    int transCode = Convert.ToInt32(amount % 1000);
                    if (!isTransCode(transCode)) { translog(DateTime.Now.ToString() + " NO TRANSCODE: " + user + " (" + amount.ToString() + ")"); continue; }
                    if (amount < 10000000000) { translog(DateTime.Now.ToString() + " PETTY TRANSFER: " + user + " (" + amount.ToString() + ")"); continue; }
                    handleUserPayment(user, amount, transCode);
                }
            }
            idle.Enabled = true;
        }

        void translog(string msg)
        {
            try
            {
                TextWriter tw = File.AppendText(System.IO.Path.GetTempPath() + "bt_transactions.log");
                tw.WriteLine(msg);
                tw.Close();
            }
            catch (Exception ex)
            {
                err("Error in translog(" + msg + ")\n" + ex.ToString());
            }
        }

        private void handleUserPayment(string user, long amount, int code)
        {
            /*
            DateTime dtNow = DateTime.Now;
            DateTime rob, race, rus, sms;
            rob = race = rus = sms = new DateTime(2001,01,01);
            try
            {
                long cost = 0;
                switch (code)
                {
                    case 205:
                    case 206: cost = 69400000; break;
                    case 239: cost = 34700000; break;
                    case 920: cost = 104100000; break;
                    default: return;
                }
                long minutes = amount / cost;

                SqlConnection con = new SqlConnection(@"Server=localhost\web;Database=BT;Trusted_Connection=True;");
                con.Open();
                SqlCommand com = new SqlCommand("SELECT * FROM Users WHERE nick = '" + user + "'", con);
                SqlDataReader dr = com.ExecuteReader();
                dr.Read();
                rob = Convert.ToDateTime(dr["lastRob"]);
                race = Convert.ToDateTime(dr["lastRace"]);
                rus = Convert.ToDateTime(dr["lastRus"]);
                sms = Convert.ToDateTime(dr["lastSMS"]);
                dr.Close();

                translog(dtNow.ToString() + "\t" + user + "\t" + code.ToString() + "\t" + amount.ToString() + "\t"
                    + rob.ToString() + "\t" + race.ToString() + "\t" + rus.ToString() + "\t" + sms.ToString() + "\t");

                sms = (sms < dtNow) ? dtNow.AddMinutes(minutes) : sms.AddMinutes(minutes);
                switch (code)
                {
                    case 205: //rus
                        rus = (rus < dtNow) ? dtNow.AddMinutes(minutes) : rus.AddMinutes(minutes);
                        com = new SqlCommand("UPDATE Users SET lastRus='" + rus.ToString()
                            + "', lastSMS='" + sms.ToString() + "' WHERE nick = '" + user + "'", con);
                        break;
                    case 239: //race
                        race = (race < dtNow) ? dtNow.AddMinutes(minutes) : race.AddMinutes(minutes);
                        com = new SqlCommand("UPDATE Users SET lastRace='" + race.ToString()
                            + "', lastSMS='" + sms.ToString() + "' WHERE nick = '" + user + "'", con);
                        break;
                    case 206: //rob
                        rob = (rob < dtNow) ? dtNow.AddMinutes(minutes) : rob.AddMinutes(minutes);
                        com = new SqlCommand("UPDATE Users SET lastRob='" + rob.ToString()
                            + "', lastSMS='" + sms.ToString() + "' WHERE nick = '" + user + "'", con);
                        break;
                    case 920: //pro
                        rus = (rus < dtNow) ? dtNow.AddMinutes(minutes) : rus.AddMinutes(minutes);
                        race = (race < dtNow) ? dtNow.AddMinutes(minutes) : race.AddMinutes(minutes);
                        rob = (rob < dtNow) ? dtNow.AddMinutes(minutes) : rob.AddMinutes(minutes);

                        com = new SqlCommand("UPDATE Users SET lastRus='" + rus.ToString()
                            + "', lastRace='" + race.ToString() + "', lastRob='" + rob.ToString()
                            + "', lastSMS='" + sms.ToString() + "' WHERE nick = '" + user + "'", con);
                        break;
                }
                com.ExecuteNonQuery();
                con.Close();
            }
            catch (Exception ex)
            {
                string msg = "TRANSACTION FAILED FOR " + user + " (" + amount.ToString() + ")\n" + ex.ToString();
                translog(msg);
                err(msg);
            }
            finally 
            {
                translog(dtNow.ToString() + "\t" + user + "\t" + code.ToString() + "\t" + amount.ToString() + "\t"
                        + rob.ToString() + "\t" + race.ToString() + "\t" + rus.ToString() + "\t" + sms.ToString() + "\t");
            }
            */
        }

        private bool isTransCode(int tc)
        {
            return (tc == 205   //rus
                || tc == 206    //rob
                || tc == 239    //rejs
                || tc == 535    //sms/tf
                || tc == 920);  //pro
        }

        private void updateVars() 
        {
            if (bDebug) debug("updateVars");
            HtmlDocument doc = webBrowser1.Document;
            HtmlElement el = null;
            //inloggad?
            if (doc.GetElementById("ctl09_lblLive") == null)
            {
                if (doc.GetElementById("ctl00_ctl00_ctrlSpam_lblLive") == null) return;
            } 

            //gängmedlem?
            if (stats != null && stats.getPhase() != 20 && doc.GetElementById("ctl08_hlMyGang") == null) bVice = false;
            
            //nytt meddelande?
            if (doc.GetElementById("ctl08_ctrlNewMessage_hlNormal") != null && !bNotified)
            {
                bNotified = true;
                Flash();
            }

            iHealth = getProgress("ctl08_ctrlHealth_divProgressText", "ctl00_ctl00_cph1_cph2_ctrlMainMenu_ctrlUserMenuData_ProgressHealth_divProgressText");

            //lagg? död?
            if (doc.GetElementById("ctl10_lblStatus") != null)
            {
                string txt = doc.GetElementById("ctl10_lblStatus").InnerText;
                if (txt.Contains("Du är död")) iHealth = 0;
                int min = Convert.ToInt32(getIntBetween(txt, "vänta", "min"));
                int sec = min*60 + Convert.ToInt32(getIntBetween(txt, "och", "sek"));
                if (sec > 0)
                {
                    debug("Lagg: " + sec);

                    if (sec < 5)
                    {
                        iRobInterval += sec;
                        iStealInterval += sec;

                        DateTime dtNow = DateTime.Now;
                        if((dtRob - dtNow).TotalSeconds > iRobInterval - 10) dtRob = DateTime.Now.AddSeconds(sec);
                        if((dtSteal - dtNow).TotalSeconds > iStealInterval - 10) dtSteal = DateTime.Now.AddSeconds(sec); 

                    }
                    if (sec > iRobInterval)
                    {
                        if (cbTF.Checked)
                        {
                            if (stats.getOil() >= 25) bGetTF = true;
                        }
                        else
                        {
                            iRobInterval = 180;
                            iStealInterval = 180;
                        }
                    }
                }
            }
            if (doc.GetElementById("ctl08_ctrlNewMessage_imgDead") != null) iHealth = 0;

            //stats
            el = doc.GetElementById("ctl08_tblInformation");
            if (el != null)
            {
                if(stats != null)  stats.update(el.GetElementsByTagName("FONT")[0].InnerText);
                if (bTransfer && el.InnerText.Contains("Beloppet du vill")) dtBank = DateTime.Now.AddSeconds(iBankInterval); //crap :'(
            }

            lCash = getIntBetween(doc.Body.InnerText, "Pengar:", "Banken:");
            lTotCash = lCash + getIntBetween(doc.Body.InnerText, "Banken:", "Lokal:");

            iCarProg = getProgress("ctl08_ctrlCars_divProgressText", "ctl00_ctl00_cph1_cph2_ctrlMainMenu_ctrlUserMenuData_ProgressCars_divProgressText");
            iCarDmg = getProgress("ctl08_ctrlCarDamage_divProgressText", "ctl00_ctl00_cph1_cph2_ctrlMainMenu_ctrlUserMenuData_ProgressCarHealth_divProgressText");
        }

        private void checkVersion()
        {
            //if (bDebug) debug("checkVersion");
            if (bImBoss) return;
            if (File.Exists(sTmpFilePath))
            {
                TextReader tr = new StreamReader(sTmpFilePath);
                string input = tr.ReadLine();
                tr.Close();
                if(Convert.ToInt32(Regex.Replace(input, "[^0-9]", "")) > iCurVer)
                {
                    if (File.Exists(input))
                    {
                        string args = "";
                        if (txtUser.Text != "") args += "-u " + txtUser.Text;
                        if (txtPass.Text != "") args += " -p " + txtPass.Text;

                        Process.Start(input, "-u " + txtUser.Text + " -p " + txtPass.Text);
                        Application.Exit();
                    }
                }
            }
        }

        private void rus()
        {
            if(bDebug) debug("rusJob");
            if (!cbRus.Checked) return;
            try
            {
                iEnergy = getProgress("ctl00_ctl00_cph1_cph2_ctrlMainMenu_ctrlUserMenuData_ProgressEnergy_divProgressText");
                if (iEnergy < 2) { webBrowser1.Navigate("http://www.biltjuv.se/?p=profile"); return; }
                if (iCarProg > 90) { webBrowser1.Navigate("http://biltjuv.se/Sell.aspx"); return; }

                --iRepeatRUS;
                HtmlElement el = webBrowser1.Document.GetElementById("ctl00_ctl00_cph1_cph1_ctrlStealArea_cbAlert_btnOK");
                if (el != null && el.Parent.InnerText.Contains("och du har inga")) iRepeatRUS = (iRepeatRUS > 0)? 0: 10;
                HtmlElementCollection els = webBrowser1.Document.GetElementsByTagName("INPUT");
                int extra = (iRepeatRUS > 0) ? 1 : 0;
                el = els[els.Count - 4 - extra];

                el.SetAttribute("onclick", "");
                el.InvokeMember("click");
            }
            catch (Exception ex)
            {
                err("Error in rus()\n\n" + ex.ToString());
            }
        }

        private void err(string msg)
        {
            MessageBox.Show(msg, "Error! :(", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }

        private bool doDeposit() 
		{
			long lSaveCash = Convert.ToInt64(txtCash.Text);

            return ((stats != null && lSaveCash > 0)
                    && ((stats.getPhase() >= 10 && lCash > lSaveCash * 5)
                        || (dtBank < DateTime.Now.AddMonths(-5))
                        || ((dtBank < DateTime.Now && cbCaptcha.Checked )
                            && ((lCash > 18000000000 && iCarProg < 70)
                                || (lOilCost > 0 && lOilCost <= lTotCash - lSaveCash)))));
			
		}

        private void idle_Tick(object sender, EventArgs e)
        {
            idle.Enabled = false;
            
            if (stats == null) { if (webBrowser1.Document == null) return; else stats = new Stats(webBrowser1.Document); }
            checkVersion();

            HtmlDocument doc = webBrowser1.Document;
            DateTime dtNow = DateTime.Now;
            
            int iPhase = stats.getPhase();
            if (iPhase == 0)
            {
                stats.update(doc);
                if (iPhase == 0)
                {
                    idle.Enabled = true;
                    return;
                }
            }

            if (doc.GetElementById("ctl08_ctrlNewMessage_hlDeadHealer") != null) iHealth = 0;
            long lSaveCash = Convert.ToInt64(txtCash.Text);

            if (iHealth < 6 || (cbRob.Checked && iHealth < 70))
            {
                stats.incrHospital();
                webBrowser1.Navigate("http://www.biltjuv.se/?p=shop/healer_ex");
            }
            else if (iPhase == 20)
            {
                rus();
            }
            else if (iCarProg >= 90)
            {
                stats.incrSell();
                webBrowser1.Navigate("http://www.biltjuv.se/?p=fence");
            }
            else if ((iPhase < 5 && iCarDmg >= 15 && iCarDmg <= 30)
                    || (iPhase == 10 && iCarDmg >= 20 && iCarDmg <= 35)
                    || (iPhase == 15 && iCarDmg >= 30 && iCarDmg <= 45))
            {
                webBrowser1.Navigate("http://www.biltjuv.se/?p=fence");
            }
            else if (doDeposit())
            {
                webBrowser1.Navigate("http://biltjuv.se/?p=bank");
            }
            else if (dtSteal < dtNow) { steal(); }
            else if (dtRob < dtNow) { rob(); }
            else if (dtGang < dtNow && bVice && (txtGang.Text != "0" || sGang == ""))
            {
                if (sGangURL != "")
                    webBrowser1.Navigate(sGangURL);
                else if (doc.GetElementById("ctl08_hlMyGang") != null)
                    webBrowser1.Navigate(doc.GetElementById("ctl08_hlMyGang").GetAttribute("HREF"));
            }
            else if ((iPhase < 5 || txtUser.Text == "wayland") 
                && (doc.GetElementById("ctl08_trInformation") != null 
                    || doc.GetElementById("ctl08_ctrlNewMessage_hlInformation") != null))   // olja / taskerköp
            {
                webBrowser1.Navigate("http://www.biltjuv.se/?p=messages/inbox&type=2");
            }
            else if (cbRace.Checked && dtRaceTrack < dtNow)
            {
                webBrowser1.Navigate("http://biltjuv.se/default.aspx?p=dragrace/sponsor");
            }
            else if (!bFriendsLoaded && (bRaceUser || cbRob.Checked))
            {
                webBrowser1.Navigate("http://biltjuv.se/default.aspx?p=profile");
            }
            else if (dtRobRace < dtNow && ((bRaceUser && dtRobUser < dtNow) || cbRob.Checked))
            {
                if (robrace.doUpdate())
                    webBrowser1.Navigate("http://biltjuv.se/default.aspx?p=online");
                else
                    robRaceUser();
            }
            else if (cbRus.Checked && dtRUSRob < DateTime.Now && dtSteal > dtNow.AddMinutes(1))
            {
                if (stats.getTools() != 40) { cbRus.Checked = false; idle.Enabled = true; return; }
                webBrowser1.Navigate("http://biltjuv.se/default.aspx?p=profile");
            }
            else if (bGetTF)
            {
                webBrowser1.Navigate("http://biltjuv.se/default.aspx?p=sms/buysms");
            }
            else { idle.Enabled = true; }
        }

        private void gang() 
        {
            if (bDebug) debug("gang");
            DateTime dtNow = DateTime.Now;
            HtmlDocument doc = webBrowser1.Document;
            if (dtGang < dtNow)
            {
                setDDLValGE("ctl10_ddlGangSteal", iGangLvl);
                HtmlElement el = doc.GetElementById("ctl10_btnGangSteal");
                if (!elOK(el)) return;
                el.SetAttribute("onclick", "");
                el.InvokeMember("click");
            }
            else 
            {
                HtmlElement el = doc.GetElementById("ctl08_tblInformation");
                if (el == null)
                {
                    idle.Enabled = true;
                }
                else
                {
                    if (el.InnerText.Contains("tagna") || el.InnerText.Contains("snodde") || el.InnerText.Contains("samla"))
                    {
                        idle.Enabled = true;
                    }
                    else
                    {
                        //full lokal
                        dtGang = DateTime.Now.AddSeconds(-1);
                        el = doc.GetElementById("ctl10_btnGangSell");
                        if (!elOK(el)) return;
                        el.InvokeMember("click");
                        return;
                    }
                }
            }
            dtGang = DateTime.Now.AddSeconds(Convert.ToInt32(txtGang.Text));
        }

        private bool elOK(HtmlElement el) {
            if (bDebug) debug("elOK");
            if (el == null)
            {
                webBrowser1.Navigate("http://biltjuv.se/?p=crime&rnd=" + rnd.Next().ToString());
                return false;
            }
            else
            {
                return true;
            }
        }

        private void setGangVars()
        {
            if (bDebug) debug("setGangVars");
            if (stats.getPhase() == 20) return;

            HtmlDocument doc = webBrowser1.Document;
            string sHref, sUrl; 
            sHref = (doc.GetElementById("ctl08_hlMyGang") != null)? doc.GetElementById("ctl08_hlMyGang").GetAttribute("HREF") : "";
            sUrl = webBrowser1.Url.ToString();
            if (sHref != sUrl) return;

            HtmlElement el = doc.GetElementById("ctl10_lblName");
            
            if (el != null)
            {
                string sOldGang = sGang;
                sGang = el.InnerText.ToLower();
               
                if (sOldGang != sGang)
                {
                    bgwServerCom.RunWorkerAsync();
                    //serverCom();
                }

                el = doc.GetElementById("ctl10_hlSteal");
                if (el == null)
                {
                    bVice = false;
                    idle.Enabled = true;
                }
                else
                {
                    bVice = true;
                    sGangURL = el.GetAttribute("HREF");
                    el = doc.GetElementById("ctl10_lblLevel");
                    if (elOK(el)) iGangLvl = Convert.ToInt32(Regex.Replace(el.InnerText, "[^0-9]", ""));
                    idle.Enabled = true;
                }
            }
        }

        private void clearTrans()
        {
            txtTrans.Text = "";
            txtTransName.Text = "";
            txtTransPin.Text = "";
            bTransfer = false;
        }

        private void bank() 
        {
            if (bDebug) debug("bank");

            debug("BANK COUNTER: " + bankCounter.ToString());
            if (txtCash.Text == "0") { idle.Enabled = true; return; }

            if (lCash <= Convert.ToInt64(txtCash.Text))
            {
                dtBank = DateTime.Now.AddSeconds(iBankInterval);
                idle.Enabled = true;
                return;
            }

            if (cbCaptcha.Checked && ++bankCounter > 5)
            {
                cbCaptcha.Checked = false;
                idle.Enabled = true;
                Flash();
                return;
            }

            DateTime dtNow = DateTime.Now;
            HtmlDocument doc = webBrowser1.Document;
            if (doc.GetElementById("ctl10_ctrlDepositCaptcha_txtMessageLabel").InnerHtml != "") dtBank = dtNow.AddMinutes(-1);

            if (stats.getPhase() == 3 && Convert.ToInt64(txtCash.Text) < 100000000) txtCash.Text = "200000000";
            if (stats.getPhase() == 4 && Convert.ToInt64(txtCash.Text) < 200000000) txtCash.Text = "400000000";
            if (stats.getPhase() == 10 && Convert.ToInt64(txtCash.Text) < 1000000) txtCash.Text = "5000000"; //us
            if (stats.getPhase() == 15 && Convert.ToInt64(txtCash.Text) < 1000000) txtCash.Text = "5000000"; //jp 

            if (dtBank < dtNow || (stats.getPhase() >= 10  && lCash > Convert.ToInt64(txtCash.Text)*2))
            {
               
                HtmlElement el = doc.GetElementById("ctl10_txtDeposit");
                if (!elOK(el)) return;

                if (cbCaptcha.Checked)
                {
                    string f = Application.StartupPath + "\\captcha.dat";
                    if (!File.Exists(f))
                    {
                        MessageBox.Show("Kan inte hitta datafilen:\n" + f);
                        cbCaptcha.Checked = false;
                    }
                    else
                    {
                        Captcha c = new Captcha(f, getCaptcha());
                        string s = c.solve(); //kolla 3 >= s <= 4
                        doc.GetElementById("ctl10_ctrlDepositCaptcha_txtCode").SetAttribute("value", s);
                        doc.GetElementById("ctl10_ctrlTransferCaptcha_txtCode").SetAttribute("value", s);
                    }
                }

                if (lOilCost > 0 && lOilCost < lTotCash - Convert.ToInt64(txtCash.Text))
                {
                    el.SetAttribute("value", (lOilCost + Convert.ToInt64(txtCash.Text) - lCash).ToString());
                    el = doc.GetElementById("ctl10_btnWithdraw");
                }

                //TRANSFER
                else if (bTransfer && stats.getPhase() < 10)
                {
                    if (txtTransName.Text == "" || txtTransPin.Text.Length != 4 
                        ||txtTrans.Text == "" || txtTrans.Text == "0") 
                    {
                        clearTrans();
                        idle.Enabled = true; 
                        return; 
                    }
                    long transCap = 999999999999;
                    long transTot = Convert.ToInt64(txtTrans.Text);
                    long saveInBank = 1000000000;
                    long saveOnHand = Convert.ToInt64(txtCash.Text);
                    long bank = getIntBetween(doc.GetElementById("ctl10_lblMoney").InnerText, "har", "kr");
                    
                    if (lCash >= transTot + saveOnHand 
                        || lCash >= transCap + saveOnHand
                        || (bank <= saveInBank && lCash > saveOnHand)
                        || (doc.GetElementById("ctl08_tblInformation") != null 
                            && doc.GetElementById("ctl08_tblInformation").InnerText.Contains("måste vänta")))
                    {
                        long trans = Math.Min(transTot, transCap);
                        trans = Math.Min(trans, lCash - saveOnHand);

                        doc.GetElementById("ctl10_txtUsername").SetAttribute("value", txtTransName.Text);
                        doc.GetElementById("ctl10_txtMoney").SetAttribute("value", trans.ToString());
                        doc.GetElementById("ctl10_txtPin").SetAttribute("value", txtTransPin.Text);
                        el = doc.GetElementById("ctl10_btnTransfer");
                        el.SetAttribute("onclick", "");
                        if (cbCaptcha.Checked) el.InvokeMember("click");

                        if (trans == transTot || (transTot - trans) < 10)
                        {
                            clearTrans();
                        }
                        else
                        {
                            txtTrans.Text = (transTot - trans).ToString();
                        }

                    }
                    else if (bank > saveInBank)
                    {
                        long withdraw = Math.Min(transTot - (lCash - saveOnHand), transCap - (lCash - saveOnHand));
                        withdraw = Math.Min(withdraw, bank - saveInBank);

                        el.SetAttribute("value", withdraw.ToString());
                        el = doc.GetElementById("ctl10_btnWithdraw");
                        el.SetAttribute("onclick", "");
                        if (cbCaptcha.Checked) el.InvokeMember("click");
                        return;
                    }

                }
                //DEPOSIT
                else
                {
                    el.SetAttribute("value", (lCash - Convert.ToInt64(txtCash.Text)).ToString());
                    el = doc.GetElementById("ctl10_btnDeposit");
                }
                el.SetAttribute("onclick", "");
                if (cbCaptcha.Checked) el.InvokeMember("click");
            }
            else if (lOilCost > 0 && lOilCost < lTotCash)
            {
                webBrowser1.Navigate("http://biltjuv.se/?p=sms/trade");
            }
            else
            {
                idle.Enabled = true;
            }
            dtBank = DateTime.Now.AddSeconds(iBankInterval);
        }

       
        
        private void heal() 
        {
            if (bDebug) debug("heal");

            if (getProgress("ctl08_ctrlHealth_divProgressText") < 100)
            {
                HtmlDocument doc = webBrowser1.Document;
                HtmlElement el = doc.GetElementById("ctl10_btnHeal");
                if (!elOK(el)) return;
                el.SetAttribute("onclick", "");
                el.InvokeMember("click");
            }
            else
            {
                idle.Enabled = true;
            }
        }

        private void sell()
        {
            if (bDebug) debug("sell");

            HtmlDocument doc = webBrowser1.Document;
            HtmlElement el = null;

            //RUS
            if (stats.getPhase() == 20) 
            {
                if (iCarProg == 0) { webBrowser1.Navigate("http://biltjuv.se/?p=crime"); return; }
                if (iCarDmg >= 90)
                    el = doc.GetElementById("ctl00_ctl00_cph1_cph1_ctrlSellArea_lvSellAreas_ctrl0_ctrlSellAreaItem_btnSell");
                else if (iCarDmg >= 80)
                    el = doc.GetElementById("ctl00_ctl00_cph1_cph1_ctrlSellArea_lvSellAreas_ctrl2_ctrlSellAreaItem_btnSell");
                else if (iCarDmg >= 70)
                    el = doc.GetElementById("ctl00_ctl00_cph1_cph1_ctrlSellArea_lvSellAreas_ctrl4_ctrlSellAreaItem_btnSell");
                else if (iCarDmg >= 60)
                    el = doc.GetElementById("ctl00_ctl00_cph1_cph1_ctrlSellArea_lvSellAreas_ctrl6_ctrlSellAreaItem_btnSell");
                else
                    el = doc.GetElementById("ctl00_ctl00_cph1_cph1_ctrlSellArea_lvSellAreas_ctrl8_ctrlSellAreaItem_btnSell");
                el.InvokeMember("click");
                return;
            }
            
            bool bPoor = false;
            if(iCarDmg > 0)
            {
                if (doc.GetElementById("ctl08_tblInformation") == null
                    || !doc.GetElementById("ctl08_tblInformation").InnerText.Contains("du har inte så mycket pengar"))
                {
                    if (setDDLValGERange("ctl10_ddlRepair", iCarDmg))
                    {
                        doc.GetElementById("ctl10_btnRepair").InvokeMember("click");
                        return;
                    }
                }
                else if (doc.GetElementById("ctl08_tblInformation") != null
                    && doc.GetElementById("ctl08_tblInformation").InnerText.Contains("du har inte så mycket pengar"))
                {
                    bPoor = true;
                }
            }

            if (getProgress("ctl08_ctrlCars_divProgressText") >= 90 
                || getProgress("ctl08_ctrlCars_divProgressText") > 0 && bPoor)
            {
                setDDLValGERange("ctl10_ddlSell", iCarDmg);
                doc.GetElementById("ctl10_btnSell").InvokeMember("click");
            }
            else
            {
                idle.Enabled = true;
            }
        }

        private void steal() 
        {
            if (bDebug) debug("steal");
            try
            {
                if (getLocation() != "crime")
                {
                    webBrowser1.Navigate("http://biltjuv.se/?p=crime");
                }
                else
                {
                    HtmlDocument doc = webBrowser1.Document;
                    setDDLValGE("ctl10_ddlSteal", getVar("Verktyg"));
                    HtmlElement el = doc.GetElementById("ctl10_btnSteal");
                    if (!elOK(el)) return;
                    el.InvokeMember("click");
                    dtSteal = DateTime.Now.AddSeconds(iStealInterval);
                }
            }
            catch (Exception ex)
            {
                err("Error in steal()\n\n" + ex.ToString());
            }
        }

        private void rob()
        {
            if (bDebug) debug("rob");
            try{
                if (getLocation() != "crime")
                {
                    webBrowser1.Navigate("http://biltjuv.se/?p=crime");
                }
                else
                {
                    HtmlDocument doc = webBrowser1.Document;
                    setDDLValGE("ctl10_ddlRob", getVar("Level"));
                    HtmlElement el = doc.GetElementById("ctl10_btnRob");
                    if (!elOK(el)) return;
                    el.InvokeMember("click");
                    dtRob = DateTime.Now.AddSeconds(iRobInterval);
                }
            }
            catch (Exception ex)
            {
                err("Error in steal()\n\n" + ex.ToString());
            }
        }

        string decrypt(string sCipher)
        {
            if (bDebug) debug("decrypt");

            int iPos, iChar, iTmpChar, iLastChar = 0;
            string sPlain = "";
            for (iPos = 0; iPos < sCipher.Length; iPos++)
            {
                iChar = Convert.ToByte(sCipher.ElementAt(iPos));
                iTmpChar = iChar;
                if (iChar >= 32 && iChar <= 126) iChar = (iChar + 203 + iLastChar) % 94 + 32;
                sPlain += Convert.ToChar(iChar);
                iLastChar = -((Math.Abs(iLastChar) + iChar) % 94);
            }
            sPlain = sPlain.Substring(sPlain.LastIndexOf(' ')+1);
            return sPlain.Replace("\r", "");
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            
            if (bDebug) debug("Form_Load");
            this.Text = "BTT|" + sVersion.Replace(".", "");

            toolTip1.InitialDelay = 1500;
            toolTip1.SetToolTip(this.lblCash, "Visa/dölj överföringsdialog.");
            toolTip1.SetToolTip(this.txtCash, "Belopp kvar efter bankbesök (USA/fas3+).\n 0 inaktiverar automatiska insättningar/överföringar.");
            toolTip1.SetToolTip(this.txtGang, "Sekunder mellan gängstötar (ledare/vice).\n 0 inaktiverar automatiska gängstötar.");
            toolTip1.SetToolTip(this.cbRob, "Råna användare.");
            toolTip1.SetToolTip(this.cbRace, "Rejsa användare och besök rejsbanan.");
            toolTip1.SetToolTip(this.cbTF, "Köp tid- och fordon-SMS automatiskt.");
            toolTip1.SetToolTip(this.cbRus, "Besök Ryssland var 15:e min (40 verktyg, fas3+).");
            toolTip1.SetToolTip(this.btnInfo, "Information och nedladdningar.");
            toolTip1.SetToolTip(this.btnUpdate, "Ladda ner senaste versionen av taskern.");
            toolTip1.SetToolTip(this.btnPause, "Pausa taskern så du kan browsa fritt.");
            toolTip1.SetToolTip(this.btnBank, "Nollställ bank-timern för direkt insättning/överföring.");

            robrace = new RobRace();
            iCurVer = Convert.ToInt32(sVersion.Replace(".", ""));

            string[] args = Environment.GetCommandLineArgs();
            if (args.Length > 2)
            {
                for (int i = 1; i < args.Length; i++)
                {
                    if (args[i] == "-u") txtUser.Text = args[i + 1];
                    else if (args[i] == "-p") txtPass.Text = args[i + 1];
                    else if (args[i] == "-k")
                    {
                        System.Threading.Thread.Sleep(1000);
                        try { File.Delete(args[i + 1]); }
                        catch { }
                    }
                    
                }
            }
            if (txtUser.Text.Length > 0 && txtPass.Text.Length > 3)
            {
                logon();
            }

        }
        
        public int Flash()
        {
            if (bDebug) debug("Flash");
            FLASHWINFO fw = new FLASHWINFO();
            const int FLASHW_TIMERNOFG = 0x0000000C;
            const int FLASHW_ALL = 0x00000003;
            fw.cbSize = Convert.ToUInt32(Marshal.SizeOf(typeof(FLASHWINFO)));
            fw.hwnd = this.Handle;
            fw.dwFlags = FLASHW_TIMERNOFG | FLASHW_ALL; //0 = stop, 1 = caption, 2 = taskbar, 3 = all, 4  = continously, 12 = until focus
            fw.uCount = UInt32.MaxValue;

            return FlashWindowEx(ref fw);
        }
        

        private string getLocation() {
            if (bDebug) debug("getLocation");
            if (webBrowser1.Url == null) return "unknown";

            string sUrl = webBrowser1.Url.ToString();
            if (sUrl.Contains("login.aspx")) { return "login"; }
            else if (sUrl.Contains("statusinfo")) { return "status"; }
            else if (sUrl.Contains("=crime") || sUrl.Contains("Steal.")) { return "crime"; }
            else if (sUrl.Contains("healer")) { return "hospital"; }
            else if (sUrl.Contains("bank")) { return "bank"; }
            else if (sUrl.Contains("fence") || sUrl.Contains("Sell.")) { return "sell"; }
            else if (sUrl.Contains("gangprofile")) { return "gangprofile"; }
            else if (sUrl.Contains("contacts&type=0&id=" + sMyID)) { return "friends"; }
            else if (sUrl.Contains("p=profile")) { return "profile"; }
            else if (sUrl.Contains("robuser")) { return "robresults"; }
            else if (sUrl.Contains("crime")) { return "gang"; }
            else if (sUrl.Contains("sponsor")) { return "racetrack"; }
            else if (sUrl.Contains("dragrace/race")) { return "raceresults"; }
            else if (sUrl.Contains("online")) { return "online"; }
            else if (sUrl.Contains("inbox&type=2")) { return "infobox"; }
            else if (sUrl.Contains("sms/trade")) { return "olja"; }
            else if (sUrl.Contains("inbox")) { return "inbox"; }
            else if (sUrl.Contains("buysms")) { return "sms"; }
            else if (sUrl.Contains("writenew")) { return "msg"; }
            //else if (sUrl.Contains("weapons") || sUrl.Contains("protections")) { return "armpory"; }
            else { return "unknown"; }
        }

        private int getProgress(string sID)
        {
            if (bDebug) debug("getProgress(" + sID + ")");

            HtmlDocument doc = webBrowser1.Document;
            HtmlElement el = doc.GetElementById(sID);
            if (el == null) return 0;
            return Convert.ToInt16(el.InnerText.Replace("%", "").Trim());
        }

        private int getProgress(string sID1, string sID2)
        {
            if (bDebug) debug("getProgress(" + sID1 + ", " + sID2 + ")");

            HtmlDocument doc = webBrowser1.Document;
            HtmlElement el = doc.GetElementById(sID1);
            if (el == null) el = doc.GetElementById(sID2); 
            if (el == null) return 0;
            return Convert.ToInt16(el.InnerText.Replace("%", "").Trim());
        }

        public int getVar(string sVar)
        {
            if (bDebug) debug("getVar(" + sVar + ")");

            HtmlDocument doc = webBrowser1.Document;
            HtmlElementCollection els = doc.GetElementsByTagName("td");

            foreach (HtmlElement el in els)
            {
                if (el.InnerText == sVar + ":")
                {
                    return Convert.ToInt16(el.NextSibling.InnerText.Split('/')[0].Replace(" ", ""));
                }
            }
            return 0;
        }

        private bool setDDLValGERange(String id, int target)
        {
            if (bDebug) debug("setDDLValGERange(" + id + ", " + target.ToString() + ")");
            HtmlDocument doc = webBrowser1.Document;
            HtmlElement el = doc.GetElementById(id);
            if (!elOK(el)) return false;
            HtmlElementCollection els = el.Children;
            string sVal = "";

            int min, max;
            foreach (HtmlElement elm in els)
            {
                min = Convert.ToInt16(getBetween(elm.InnerText, "(", ")").Split('-').ElementAt(0).Trim());
                max = Convert.ToInt16(getBetween(elm.InnerText, "(", ")").Split('-').ElementAt(1).Trim());
                if (target >= min && target <= max)
                {
                    sVal = elm.GetAttribute("value");
                    break;
                }
            }
            if (sVal == "")
                return false;
            else
            {
                doc.GetElementById(id).SetAttribute("value", sVal);
                return true;
            }
        }

        private bool isFriend(string name)
        {
            if (bDebug) debug("isFriend(" + name + ")");
            return friends.Contains(name.ToLower());
        }

        private bool setDDLValGE(String id, int target)
        {
            if (bDebug) debug("setDDLValGE");
            HtmlDocument doc = webBrowser1.Document;
            HtmlElement el = doc.GetElementById(id);
            if (!elOK(el)) return false;
            HtmlElementCollection els = el.Children;
            string sVal = "";

            foreach (HtmlElement elm in els)
            {
                int i = Convert.ToInt16(getBetween(elm.InnerText, "(", ")"));
                if (target >= i)
                {
                    sVal = elm.GetAttribute("value");
                }
            }
            if (sVal == "")
                return false;
            else
            {
                doc.GetElementById(id).SetAttribute("value", sVal);
                return true;
            }
        }

        private string getBetween(string str, string first, string last)
        {
            if (bDebug) debug("getBetween");
            int iStart = str.IndexOf(first) + first.Length;
            int iEnd = str.IndexOf(last, iStart);
            if (iStart >= iEnd) return "0";
            return str.Substring(iStart, iEnd - iStart);
        }

        private long getIntBetween(string str, string first, string last)
        {
            if (bDebug) debug("getIntBetween");
            int iStart = str.IndexOf(first) + first.Length;
            int iEnd = str.IndexOf(last, iStart);
            if (iStart - first.Length < 0 || iEnd < 0) return 0;
            return Convert.ToInt64(Regex.Replace(str.Substring(iStart, iEnd - iStart), "[^0-9]", ""));
        }

        private void serverCom()
        {
            return;

            if (bDebug) debug("serverCom");
            
            try
            {
                string sData = " " + txtUser.Text + ";" + sVersion + ";" + cbRob.Checked.ToString() + ";" + cbRace.Checked.ToString()
                    + ";" + cbRus.Checked.ToString() + ";" + cbTF.Checked.ToString();
                if(sGang != "") sData += ";" + sGang;    
                sData = enc(sData, 64);

                sData = Convert.ToBase64String(System.Text.Encoding.UTF8.GetBytes(sData));

                string html = getHTML("http://---" + sData);
                
                sLatestVersion = getBetween(html, "id=\"div_version\">", "</span>");
                int iNewVer = Convert.ToInt32(Regex.Replace(sLatestVersion, "[^0-9]", ""));
                if(iCurVer < iNewVer) btnUpdate.Enabled = true;

                Int64 ticks = Convert.ToInt64(getBetween(html, "id=\"div_date\">", "</span>"));
                if (ticks > DateTime.Now.AddDays(1).Ticks || ticks < DateTime.Now.AddDays(-1).Ticks)
                {
                    if (iCurVer < iNewVer)
                        sUrFkt = "Dags att uppdatera!";
                    else
                        sUrFkt = "Datumknas?";
                }

                if (getBetween(html, "id=\"div_error\">", "</span>") != "ok") return;
                dicks = decrypt(getBetween(html, "id=\"div_dicks\">", "</span>")).Split(';');
                string[] robbers = decrypt(getBetween(html, "id=\"div_robbers\">", "</span>")).Split(';');
                string[] racers = decrypt(getBetween(html, "id=\"div_racers\">", "</span>")).Split(';');
                string[] captcha = decrypt(getBetween(html, "id=\"div_captcha\">", "</span>")).Split(';');
                string[] russians = decrypt(getBetween(html, "id=\"div_russians\">", "</span>")).Split(';');
                string[] newFriends = decrypt(getBetween(html, "id=\"div_friends\">", "</span>")).Split(';');

            /*
                StreamWriter sw = File.CreateText("d:\\conf" + DateTime.Now.Minute + ".txt");
                sw.WriteLine(string.Join(" ", robbers));
                sw.WriteLine(string.Join(" ", racers));
                sw.WriteLine(string.Join(" ", captcha));
                sw.WriteLine(string.Join(" ", russians));
                sw.WriteLine(string.Join(" ", newFriends));
                sw.Close();
            */

                isDick = (dicks.Contains("g:" + sGang)  || dicks.Contains(txtUser.Text.ToLower()));//sUrFkt = "*host* *host*";
                
                
                if (userMatch(robbers))
                {
                    Invoke((MethodInvoker)(() =>
                    {
                        cbRob.Enabled = true;
                    }));
                }
                else
                {
                    Invoke((MethodInvoker)(() =>
                    {
                        cbRob.Enabled = false;
                        cbRob.Checked = false;
                    }));
                }

                if (userMatch(racers))
                {
                    Invoke((MethodInvoker)(() =>
                    {
                        cbRace.Enabled = true;
                    }));
                }
                else
                {
                    Invoke((MethodInvoker)(() =>
                    {
                        cbRace.Enabled = false;
                        cbRace.Checked = false;
                    }));
                }

                if (userMatch(captcha))
                {
                    Invoke((MethodInvoker)(() => {
                        cbCaptcha.Visible = true;
                        cbCaptcha.Enabled = true;
                    }));
                }
                else
                {
                    Invoke((MethodInvoker)(() =>
                    {
                        cbCaptcha.Enabled = false;
                        cbCaptcha.Checked = false;
                    }));
                }

                if (userMatch(russians))
                {
                    Invoke((MethodInvoker)(() =>
                    {
                        cbRus.Enabled = true;
                    }));
                    
                }
                else
                {
                    Invoke((MethodInvoker)(() =>
                    {
                        cbRus.Enabled = false;
                        cbRus.Checked = false;
                    }));
                }

                if (cbRace.Enabled || cbRob.Enabled)
                {
                    foreach (string friend in newFriends)
                    {
                        if (!isFriend(friend)) friends.Add(friend);
                    }
                }
                iErrCount = 0;
             }
           
            catch(Exception ex)
            {
                debug("Error in serverCom()\n" + ex.ToString() + "\n");
                /*
                if (++iErrCount < 3)
                {
                    System.Threading.Thread.Sleep(100);
                    serverCom();
                }
                else
                {
                    iErrCount = 0;
                }*/
            }
    
        }

        private bool userMatch(string[] users)
        {
            if(isDick) return false;
            if(isAwesome()
                || users.Contains("g:" + sGang) 
                || users.Contains("all")
                || users.Contains(txtUser.Text.ToLower())) return true;
            
            return false;
        }

        private string getHTML(string url)
        {
            // error handling in calling methods
            if (bDebug) debug("getHTML");
            string html;
            WebRequest request = WebRequest.Create(url);
            WebResponse response = request.GetResponse();
            StreamReader stream = new StreamReader(response.GetResponseStream());
            html = stream.ReadToEnd();
            stream.Close();
            response.Close();
            return html;
            
        }

        private void urFkt(string why) 
        {
            if (bDebug) debug("urFkt");
            lockaAll("ACCESS DENIED");
            MessageBox.Show(why);
        }

        private void lockaAll(string msg)
        {
            if (bDebug) debug("lockaAll");
            label5.Text = msg;
            label5.Visible = true;
            webBrowser1.Visible = false;
            idle.Enabled = false;
            updateStats.Enabled = false;
            wait.Enabled = false;
            updateTime.Enabled = false;
            pause = true;
            btnPause.Enabled = false;
            txtUser.Enabled = false;
            txtPass.Enabled = false;
            txtCash.Enabled = false;
            txtGang.Enabled = false;
            cbRace.Checked = false;
            cbRace.Enabled = false;
            cbRob.Checked = false;
            cbRob.Enabled = false;
            cbRus.Checked = false;
            cbRus.Enabled = false;
        }

        private void txtPass_KeyUp(object sender, KeyEventArgs e)
        {
             
            if (bDebug) debug("txtPass_KeyUp");
            if (e.KeyCode == Keys.Enter && txtUser.Text != "" && txtPass.Text.Length > 3) {
                loginCounter = 0;

                //if (isAwesome()) { cbCaptcha.Checked = true; cbCaptcha.Visible = true; cbCaptcha.Enabled = true; } 
                //else { cbCaptcha.Enabled = false; cbCaptcha.Checked = false; }
                loadData(System.IO.Path.GetTempPath() + "bt_tasker_" + txtUser.Text.ToLower() + ".cfg");
                cbCaptcha.Checked = true; cbCaptcha.Visible = true; cbCaptcha.Enabled = true;

                logon();
            }
        }

        private void logon()
        {
            if (bDebug) debug("logon");
            
            gbLogin.Visible = false;
            webBrowser1.Visible = true;

            friends = new List<string> { txtUser.Text.ToLower() };
            vips = new List<string> { };

            gangOK = isAwesome();
            if (gangOK) { iWait = 100; }


            this.Text = "[" + txtUser.Text + "] BTT|" + sVersion.Replace(".","");

            minuteTick = 23;
            updateStats.Enabled = true;

            login();

            if (!cbRace.Enabled && !cbRob.Enabled) bFriendsLoaded = true;
        }

        private void login()
        {
            if (bDebug) debug("login");

            //loopprotection
            if (cbCaptcha.Checked && loginCounter > 4)
            {
                cbCaptcha.Checked = false;
            }

            if (getLocation() == "login" && txtUser.Text != "" && txtPass.Text != "")
            {
                wait.Enabled = updateStats.Enabled = true;

                HtmlDocument doc = webBrowser1.Document;

                //if (cbCaptcha.Checked)
                //{
                //    string f = Application.StartupPath + "\\captcha.dat";
                //    if (!File.Exists(f))
                //    {
                //        MessageBox.Show("Kan inte hitta datafilen:\n" + f);
                //        cbCaptcha.Checked = false;
                //    }
                //    else
                //    {
                //        Captcha c = new Captcha(f, getCaptcha());
                //        string s = c.solve(); //kolla 3 >= s <= 4 
                //        doc.GetElementById("ctrlLoginCaptcha_txtCode").SetAttribute("value", s);
                //    }
                //}
                doc.All["txtEmail"].SetAttribute("Value", txtUser.Text);
                doc.All["txtPassword"].SetAttribute("Value", txtPass.Text);
                //if (cbCaptcha.Checked) { doc.All["btnLogin"].InvokeMember("Click"); }
                doc.All["btnLogin"].InvokeMember("Click");
                ++loginCounter;
            }
        }

        private void loadData(string file)
        {
            if (bDebug) debug("loadData");

            if (!File.Exists(file)) return;

            string sName, sNextRace, sNextRob, sCreated;
            bool bRace, bRob, bOnline;
            int iID, iRaceWon, iRaceLost, iRobWon, iRobLost, iRobInRow, iRaceInRow, iCountry;
            DateTime dtUpdated;
            TextReader tr = new StreamReader(file);
            string[] lines = tr.ReadToEnd().Split('\n');
            
            //settings
            if(lines[0].Substring(0,4) == "ver:")
            {
                string[] settings = decrypt(lines[0]).Split(';');
                
                foreach (string item in settings)
                {
                    string[] setting = item.Split(':');
                    switch (setting[0])
                    {
                        case "rob":
                            cbRob.Enabled = Convert.ToBoolean(setting[1]);
                            cbRob.Checked = Convert.ToBoolean(setting[2]);
                            break;
                        case "race":
                            cbRace.Enabled = Convert.ToBoolean(setting[1]);
                            cbRace.Checked = Convert.ToBoolean(setting[2]);
                            break;
                        case "rus":
                            cbRus.Enabled = Convert.ToBoolean(setting[1]);
                            cbRus.Checked = Convert.ToBoolean(setting[2]);
                            break;
                        case "captcha":
                            cbCaptcha.Visible = Convert.ToBoolean(setting[1]);
                            cbCaptcha.Enabled = Convert.ToBoolean(setting[1]);
                            cbCaptcha.Checked = Convert.ToBoolean(setting[2]);
                            break;
                        case "tf": cbTF.Checked = Convert.ToBoolean(setting[1]); break;
                        case "gang": txtGang.Text = setting[1]; break;
                        case "cash": txtCash.Text = setting[1]; break;
                    }
                }

                lines[0] = "";
            }

            //statistics
            try
            {
                foreach (string line in lines)
                {
                    if (line == "") continue;
                    string[] u = line.Split(';');
                    sName = u[0];
                    iID = Convert.ToInt32(u[1]);
                    dtUpdated = Convert.ToDateTime(u[2]);
                    bRace = Convert.ToBoolean(u[3]);
                    bRob = Convert.ToBoolean(u[4]);
                    iRaceWon = Convert.ToInt32(u[5]);
                    iRaceLost = Convert.ToInt32(u[6]);
                    iRobWon = Convert.ToInt32(u[7]);
                    iRobLost = Convert.ToInt32(u[8]);
                    iRaceInRow = Convert.ToInt32(u[9]);
                    iRobInRow = Convert.ToInt32(u[10]);
                    iCountry = Convert.ToInt32(u[11]);
                    bOnline = Convert.ToBoolean(u[12]);
                    sNextRace = u[13];
                    sNextRob = u[14];
                    sCreated = u[15];
                    if (isDick) if(!dicks.Contains(sName)) { bRace = false; bRob = false; } else { bRace = false; bRob = true; }
                    robrace.addUser(new User(sName, iID, dtUpdated, bRob, bRace, iRaceWon, iRaceLost, iRobWon, iRobLost, iRaceInRow, iRobInRow, iCountry, bOnline, sNextRace, sNextRob, sCreated));
                }
            }
            catch (Exception ex)
            {
                debug(ex.ToString());
                tr.Close();
                File.Delete(file);
            }
            tr.Close();
        }

        private void btnPause_Click(object sender, EventArgs e)
        {
            if (bDebug) debug("btnPause_Click");

            pause = !pause;
            if (pause) {
                idle.Enabled = false;
                btnPause.Text = "►";
            }
            else {
                btnPause.Text = "ıı";
                idle.Enabled = true;
            }
        }

        private void Form1_Resize(object sender, EventArgs e)
        {
            webBrowser1.Height = this.Height - 60;
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (bDebug) debug("Form1_FormClosing");
            
            string sPath = System.IO.Path.GetTempPath();
            string sFile = sPath + "bt_tasker_" + txtUser.Text.ToLower() + ".cfg";
            TextWriter tw = new StreamWriter(sFile);
            string sEnc = " rob:" + cbRob.Enabled + ":" + cbRob.Checked + ";race:" + cbRace.Enabled 
                + ":" + cbRace.Checked + ";rus:" + cbRus.Enabled + ":" + cbRus.Checked + ";cash:" 
                + txtCash.Text + ";captcha:" + cbCaptcha.Enabled + ":" + cbCaptcha.Checked + ";gang:" 
                + txtGang.Text + ";tf:" + cbTF.Checked; 
            tw.WriteLine("ver:01" + enc(sEnc));
            if(robrace != null && robrace.Count() > 0) robrace.save(tw);
            tw.Close();
            
        }

        string enc(string sPlain, int padding = 200)
        {
            sPlain = pad(sPlain, padding + rnd.Next(10));
            int iPos, iChar, iTmpChar, iLastChar = 0;
            string sCipher = "";
            for (iPos = 0; iPos < sPlain.Length; iPos++)
            {
                iChar = Convert.ToByte(sPlain.ElementAt(iPos));
                iTmpChar = iChar;
                if (iChar >= 32 && iChar <= 126) iChar = (iChar + 203 + iLastChar) % 94 + 32;
                sCipher += Convert.ToChar(iChar);
                iLastChar = (iLastChar + iTmpChar) % 94;
            }
            return sCipher;
        }

        string pad(string s, int len)
        {
            string padding = "";
            padding += Convert.ToChar((rnd.Next(94) + 32));
            for (int i = 0; i < len - s.Length; i++)
                padding += Convert.ToChar((rnd.Next(94) + 32));
            return padding + s;
        }

        private void wait_Tick(object sender, EventArgs e)
        {
            if (dtWait < DateTime.Now)
            {
                if (bDebug) debug("wait_Tick - (dtWait < DateTime.Now)");
                webBrowser1.Navigate("http://biltjuv.se/?p=crime&rnd=" + rnd.Next().ToString()); //idle funkar inte om sidan vart offline
                btnPause.Text = "ıı";
                pause = false;
            }
        }

        private void updateTime_Tick(object sender, EventArgs e)
        {
            DateTime dtNow = DateTime.Now;
            long lMin = long.MaxValue; 
            long lDiff;
            string sOut = "";

            lDiff = (dtSteal - dtNow).Ticks;
            if (lDiff < 0) lDiff = 0;
            if(lDiff < lMin && lDiff > 0) lMin = lDiff;
            sOut += "Stjäl    " + new DateTime(lDiff).ToLongTimeString().Substring(3) + "\n";

            lDiff = (dtRob - dtNow).Ticks;
            if (lDiff < 0) lDiff = 0;
            if (lDiff < lMin && lDiff > 0) lMin = lDiff;
            sOut += "Råna     " + new DateTime(lDiff).ToLongTimeString().Substring(3);

            if (true)
            {
                lDiff = (dtBank - dtNow).Ticks;
                if (lDiff < 0) lDiff = 0;
                if (lDiff < lMin && lDiff > 0) lMin = lDiff;
                sOut += "\nBank     " + new DateTime(lDiff).ToLongTimeString().Substring(3);
            }
            if (bVice)
            {
                lDiff = (dtGang - dtNow).Ticks;
                if (lDiff < 0) lDiff = 0;
                if (lDiff < lMin && lDiff > 0) lMin = lDiff;
                sOut += "\nGäng     " + new DateTime(lDiff).ToLongTimeString().Substring(3);
            }
            if (cbRace.Checked)
            {
                lDiff = (dtRaceTrack - dtNow).Ticks;
                if (lDiff < 0) lDiff = 0;
                if (lDiff < lMin && lDiff > 0) lMin = lDiff;
                sOut += "\nSpons    " + new DateTime(lDiff).ToLongTimeString().Substring(3);
            }

            if (cbRob.Checked && !cbRace.Checked)
            {
                lDiff = (dtRobUser - dtNow).Ticks;
                if (lDiff < 0) lDiff = 0;
                if (lDiff < lMin && lDiff > 0) lMin = lDiff;
                sOut += "\nRåna anv " + new DateTime(lDiff).ToLongTimeString().Substring(3);
            }

            if (cbRob.Checked && cbRace.Checked)
            {
                lDiff = (dtRobRace - dtNow).Ticks;
                if (lDiff < 0) lDiff = 0;
                if (lDiff < lMin && lDiff > 0) lMin = lDiff;
                sOut += "\nR/R      " + new DateTime(lDiff).ToLongTimeString().Substring(3);
            }

            if (cbRus.Checked)
            {
                lDiff = (dtRUSRob - dtNow).Ticks;
                if (lDiff < 0) lDiff = 0;
                if (lDiff < lMin && lDiff > 0) lMin = lDiff;
                sOut += "\nRyssland " + new DateTime(lDiff).ToLongTimeString().Substring(3);
            }

            if (lMin == long.MaxValue) lMin = 0;
            sOut = "Nästa    " + new DateTime(lMin).ToLongTimeString().Substring(3) + " \n" + sOut;
                        
            lblStatus.Text = sOut;
        }


        private void lblStatus_MouseHover(object sender, EventArgs e)
        {
            lblStatus.AutoSize = true;
        }

        private void lblStatus_MouseLeave(object sender, EventArgs e)
        {
            lblStatus.AutoSize = false;
        }

        private void lblStats_MouseHover(object sender, EventArgs e)
        {
            if (stats != null)
            {
                stats.update(webBrowser1.Document);
                lblStats.Text = stats.getStats();
                lblStats.AutoSize = true;
            }
        }

        private void lblStats_MouseLeave(object sender, EventArgs e)
        {
            lblStats.AutoSize = false;
        }

        private void updateStats_Tick(object sender, EventArgs e)
        {
            if (bDebug) debug("updateStats_Tick");

            if ((iStealInterval > 75 && iStealInterval < 85) || (iStealInterval > 180 && iStealInterval < 190)) --iStealInterval;
            if ((iRobInterval > 75 && iRobInterval < 85) || (iRobInterval > 180 && iRobInterval < 190)) --iRobInterval;

            if (++minuteTick % 15 == 0) {

                bVice = true;
                loginCounter = 0;
                bankCounter = 0;
                if (cbRob.Enabled || cbRace.Enabled) bFriendsLoaded = false;
            }
            
            if (minuteTick % 25 == 0)
            {
                ThreadStart job = new ThreadStart(serverCom);
                Thread thread = new Thread(job);
                thread.Start();
            }

            if (sUrFkt != "") urFkt(sUrFkt);
            
            if (stats != null)
            {
                stats.update(webBrowser1.Document);
                lblStats.Text = stats.getStats();
                if (stats.getPhase() == 4) { iUserRobberiesInterval = 180;  }
                else { iUserRobberiesInterval = 5 * 60; }
            }
        }


        HtmlElementCollection getElementsByClass(HtmlDocument doc, string cls, string tag = "*")
        {
            if (bDebug) debug("getElementByClass(doc, " + cls + ", " + tag + ")");

            HtmlElementCollection elsByTag = doc.GetElementsByTagName(tag);
            HtmlElement container = doc.CreateElement("DIV");
            if (elsByTag == null) return null;
            foreach (HtmlElement el in elsByTag)
            {
                if (el.GetAttribute("className").ToLower() == cls.ToLower())
                    container.AppendChild(el);
            }
            HtmlElementCollection elsByClass = container.Children;
            return elsByClass;
        }

        private void debug(string msg)
        {
            System.Diagnostics.Debug.WriteLine(DateTime.Now.ToShortTimeString() + " | " + msg);
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            /*
            if (bDebug) debug("btnUpdate_Click");
            try
            {
                WebClient webClient = new WebClient();
                webClient.DownloadFileCompleted += new AsyncCompletedEventHandler(downloadCompleted);
                webClient.DownloadProgressChanged += new DownloadProgressChangedEventHandler(downloadProgressChanged);
                webClient.DownloadFileAsync(new Uri("http://---"), sLatestVersion);
                btnUpdate.Visible = false;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
            */
        }

        private void downloadProgressChanged(object sender, DownloadProgressChangedEventArgs e)
        {
            progressBar1.Value = e.ProgressPercentage;
        }

        private void downloadCompleted(object sender, AsyncCompletedEventArgs e)
        {

            if (bDebug) debug("downloadCompleted");
            lockaAll("Uppdaterar...");

            bImBoss = true;

            TextWriter tw = new StreamWriter(sTmpFilePath);
            tw.WriteLine(sLatestVersion);
            tw.Close();


            string newProcName = sLatestVersion.Substring(0, sLatestVersion.LastIndexOf('.'));
            int pid = Process.GetCurrentProcess().Id;
            int btCount = 0;
            do
            {
                btCount = 0;
                System.Threading.Thread.Sleep(500);
                Process[] runningProcs = Process.GetProcesses(".");
                foreach (Process p in runningProcs)
                {
                    if (p.ProcessName.Contains("bt") && p.Id != pid && p.ProcessName != newProcName)
                    {
                        ++btCount;
                    }
                }
                Application.DoEvents();
            } while (btCount > 1);
            File.Delete(sTmpFilePath);
            
            string args = "-u " + txtUser.Text;
            if (txtPass.Text != "") args += " -p " + txtPass.Text;
            args += " -k " + Application.ExecutablePath;

            Process.Start(sLatestVersion, args);
            Application.Exit();
        }

        private void cbRace_CheckedChanged(object sender, EventArgs e)
        {
            if (bDebug) debug("cbRace_CheckedChanged");
            bRaceUser = !bRaceUser;
            if (cbRob.Checked == false && cbRace.Checked == true) bFriendsLoaded = false;
        }

        private void cbRob_CheckedChanged(object sender, EventArgs e)
        {
            if (bDebug) debug("cbRob_CheckedChanged");
            if (cbRob.Checked == true && cbRace.Checked == false) bFriendsLoaded = false;
        }

        private void btnInfo_Click(object sender, EventArgs e)
        {            
            // CC Attribution ShareAlike !!
            // https://creativecommons.org/licenses/by-sa/3.0/
            Process.Start("https://github.com/tallberg/BT");
        }

        private void txtGang_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar)) e.Handled = true;
        }

        private void txtCash_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar)) e.Handled = true;
        }

        private void frmMain_KeyDown(object sender, KeyEventArgs e)
        {
            hotkeys(e.KeyCode, e.Control);
        }

        private void webBrowser1_PreviewKeyDown(object sender, PreviewKeyDownEventArgs e)
        {
            hotkeys(e.KeyCode, e.Control);
        }

        private void hotkeys(Keys key, bool ctrl = false, bool alt = false, bool shift = false)
        {
            debug("in");
            if (dtSpam > DateTime.Now.AddSeconds(-5) || key == Keys.ControlKey) return;
            dtSpam = DateTime.Now;
            debug("gogogogogo" + key + " ctrl: " + ctrl.ToString() );
            if (key == Keys.F5)
            {
                webBrowser1.Refresh();
            }
            else if (ctrl && key == Keys.R)
            {
                DialogResult dr = MessageBox.Show("Nollställ all rån och rejs-data?", "BTT [" + txtUser.Text + "]", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (dr == DialogResult.Yes) robrace = new RobRace();
            }
            else if (ctrl && key == Keys.T)
            {
                gbTransfer.Visible = !gbTransfer.Visible;
            }
            else if (ctrl && key == Keys.U)
            {
                try{
                    bgwServerCom.RunWorkerAsync();
                }
                catch(Exception err){
                    if(bDebug) debug("hotkeys: ctrl+u\n" + err.ToString());    
                }
                    //serverCom();
            }
            
        }

        private void label6_MouseEnter(object sender, EventArgs e)
        {
            label6.Text = webBrowser1.Url.ToString();
        }

        private void label6_MouseLeave(object sender, EventArgs e)
        {
            label6.Text = "  ";
        }

        private void txtTransPin_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                gbTransfer.Visible = false;
                bTransfer = true;
            }
        }

        private void lblCash_Click(object sender, EventArgs e)
        {
            gbTransfer.Visible = !gbTransfer.Visible;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            dtBank = DateTime.Now.AddYears(-1);
        }

        private void bgwServerCom_DoWork(object sender, DoWorkEventArgs e)
        {
            serverCom();
        }

    }
}
