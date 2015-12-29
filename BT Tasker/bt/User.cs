using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace bt
{
    public class User
    {
        string sName;
        int iID, iCountry;
        bool bRace, bRob;
        bool bOnline;
        int iRaceWon, iRaceLost, iRobWon, iRobLost, iRobInRow, iRaceInRow;
        DateTime dtCreated, dtUpdated, dtNextRace, dtNextRob;
        
        public User(string un, int uid, DateTime ud, bool rob = true, bool race = true, int raw = 0, int ral = 0, int row = 0, int rol = 0, int rair = 0,
            int roir = 0, int cntry = 0, bool online = true, string nro = "2010-01-01", string nra = "2010-01-01", string cr = "2010-10-10")
        {
            sName = un;
            iID = uid;
            dtUpdated = ud;
            bRace = race;
            bRob = rob;
            iRaceWon = raw;
            iRaceLost = ral;
            iRobWon = row;
            iRobLost = rol;
            iRaceInRow = rair;
            iRobInRow = roir;
            iCountry = cntry;
            bOnline = online;
            dtNextRace = Convert.ToDateTime(nro);
            dtNextRob = Convert.ToDateTime(nra);
            dtCreated = Convert.ToDateTime(cr);

        }

        public string getAll()
        {
            return sName+";"+iID.ToString()+";"+dtUpdated.ToString()+";"+bRace.ToString()+";"+bRob.ToString()+";"
                   + iRaceWon.ToString()+";"+iRaceLost.ToString()+";"+iRobWon.ToString()+";"+iRobLost.ToString()+";"
                   + iRaceInRow.ToString()+";"+iRobInRow.ToString()+";"+iCountry.ToString()+";"+bOnline.ToString()+";"+dtNextRace.ToString()+";"
                   + dtNextRob.ToString()+";"+dtCreated.ToString();
            
        }

        public User(User u)
        {
            sName = u.sName;
            iID = u.iID;
            dtUpdated = u.dtUpdated;
            bRace = u.bRace;
            bRob = u.bRob;
            iRaceWon = u.iRaceWon;
            iRaceLost = u.iRaceLost;
            iRobWon = u.iRobWon;
            iRobLost = u.iRobLost;
            bOnline = u.bOnline;
            dtNextRace = u.dtNextRace;
            dtNextRob = u.dtNextRob;
        }

        public bool stopRacing() {
            return (iRaceInRow < -2); //(iRaceLost > 2 && iRaceWon / iRaceLost < 0.2);
        }

        public bool stopRobbing()
        {
            return (iRobInRow < -1); //(iRobLost > 2 && iRobWon / iRobLost < 0.2);
        }


        public void incrementRaceWon()
        {
            ++iRaceWon;
        }

        public void incrementRaceLost()
        {
            ++iRaceLost;
        }

        public void incrementRaceInRow()
        {
            if (iRaceInRow < 0)
                iRaceInRow = 1;
            else
                ++iRaceInRow;
        }

        public void decrementRaceInRow()
        {
            if (iRaceInRow > 0)
                iRaceInRow = -1;
            else
                --iRaceInRow;
        }

        public void incrementRobWon()
        {
            ++iRobWon;
        }

        public void incrementRobLost()
        {
            ++iRobLost;
        }

        public int incrementRobInRow()
        {
            if (iRobInRow < 0)
                iRobInRow = 1;
            else
                ++iRobInRow;
            return iRobInRow;
        }

        public int decrementRobInRow()
        {
            if (iRobInRow > 0) 
                iRobInRow = -1;
            else
                --iRobInRow;
            return iRobInRow;
        }

        public int getId()
        {
            return iID;
        }

        public static  bool operator ==(User x,User y)
        {
            if ((object)x == null) return false;
            return (x.iID == y.iID);
        }
        
        public static  bool operator !=(User x, User y)
        {
            if ((object)x == null) return true;
            return (x.iID != y.iID);
        }

        public bool getRob()
        {
            return bRob;
        }

        public bool getRace()
        {
            return bRace;
        }

        public void setRob(bool rob)
        {
            bRob = rob;
        }

        public void setRace(bool race)
        {
            bRace = race;
        }
    
        public void setOnline()
        {
            bOnline = true;
        }

        public void setOffline()
        {
            bOnline = false;
        }

        public void setCountry(int c)
        {
            iCountry = c;
        }

        public int getCountry()
        {
            return iCountry;
        }

        public bool isOnline()
        {
            return bOnline;
        }

        public string getName()
        {
            return sName;
        }

        public void update(bool rob, bool force)
        {
            bool old = (dtUpdated < DateTime.Now.AddDays(-5));
                
            if(old || force) setRob(rob);
            if(old && !stopRacing()) setRace(true);
            if(old) dtUpdated = DateTime.Now;

            setOnline();
        }

        public void setRobTime(DateTime dt)
        {
            dtNextRob = dt;
        }

        public void setRaceTime(DateTime dt)
        {
            dtNextRace = dt; 
        }

        public DateTime getRobTime()
        {
            if(bRob)
                return dtNextRob;
            else
                return DateTime.Now.AddYears(1);
        }

        public DateTime getRaceTime()
        {
            if(bRace)
                return dtNextRace;
            else
                return DateTime.Now.AddYears(1);
        }

        public DateTime getRobRaceTime()
        {
            if (bRob && bRace)
                if (dtNextRace < dtNextRob)
                    return dtNextRace;
                else
                    return dtNextRob;
            else if (bRob)
                return dtNextRob;
            else if (bRace)
                return dtNextRace;
            else
                return DateTime.Now.AddYears(1);
        }

        public string getUrl()
        {
            return "http://biltjuv.se/default.aspx?p=profile&i=" + iID.ToString();
        }

    }
}
