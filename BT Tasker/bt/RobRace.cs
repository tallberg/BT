using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace bt
{
    class RobRace
    {
        List<User> users;
        DateTime nextUpdate;

        public RobRace() 
        { 
            users = new List<User>();
            nextUpdate = DateTime.Now;
        }

        public int Count()
        {
            return users.Count;
        }

        public bool doUpdate()
        {
            return (nextUpdate <= DateTime.Now);
        }

        public void setNextUpdate(int m)
        {
            nextUpdate = DateTime.Now.AddMinutes(m);
        }

        public void addUser(User usr)
        {
            users.Add(usr);
        }
        
        /*
        public void resetRob(int phase)
        {
            for (int i = 0; i < users.Count; i++)
            {
                if (users[i].setRob(true)) ;
            }
        }*/

        public void update(int uid, bool rob, bool force)
        {
            int idx = getIndex(uid);
            if (idx < 0) return;
            users[idx].update(rob, force);
        }

        public void addUser(string un, int uid, bool rob, bool race)
        {
            users.Add(new User(un, uid, DateTime.Now, rob, race));
        }

        public void remove(int id)
        {
            int idx = getIndex(id);
            if (idx < 0) return;
            users.RemoveAt(idx);
        }

        public int getIndex(int id)
        {
            for (int i = 0; i < users.Count; i++)
            {
                if (users[i].getId() == id)
                {
                    return i;
                }
            }
            return -1;
        }

        public bool contains(string name)
        { 
            foreach (User u in users)
            {
                if (u.getName() == name) return true;
            }
            return false;
        }

        public bool setOnline(int iID, int iCntry = 0)
        {
            foreach (User u in users)
            {
                if (u.getId() == iID)
                {
                    u.setOnline();
                    u.setCountry(iCntry);
                }
            }
            return false;
        }
        
        public void updateUser(User u)
        {
            for (int i = 0; i < users.Count; i++)
            {
                if (users[i] == u)
                {
                    users[i] = u;
                    return;
                }
            }
        }

        public User getNextRob()
        {
            DateTime dtMin = DateTime.Now.AddMinutes(20);
            DateTime dtNow = DateTime.Now;
            DateTime dtTmp;
            int iBest = -1;
            for (int i = users.Count - 1; i >= 0; i--)
            {
                if (users[i].isOnline())
                {
                    dtTmp = users[i].getRobTime();
                    if (dtTmp <= dtNow)
                    {
                        return users[i];
                    }
                    else if (dtTmp < dtMin)
                    {
                        dtMin = dtTmp;
                        iBest = i;
                    }
                }
            }
            if (iBest != -1)
                return users[iBest];
            else
                return null;
        }

        public User getNextRace()
        {
            DateTime dtMin = DateTime.Now.AddMinutes(15);
            DateTime dtNow = DateTime.Now;
            DateTime dtTmp;
            int iBest = -1;
            for (int i = users.Count - 1; i >= 0; i--)
            {
                if (users[i].isOnline())
                {
                    dtTmp = users[i].getRaceTime();
                    if (dtTmp <= dtNow)
                    {
                        return users[i];
                    }
                    else if (dtTmp < dtMin)
                    {
                        dtMin = dtTmp;
                        iBest = i;
                    }
                }
            }
            if (iBest != -1)
                return users[iBest];
            else
                return null;
        }

        public void save(TextWriter tw)
        {
            foreach (User u in users)
            {
                tw.WriteLine(u.getAll());
            }
        }

        public User getNextRobRace()
        {
            DateTime dtMin = DateTime.Now.AddMinutes(15);
            DateTime dtNow = DateTime.Now;
            DateTime dtTmp;
            int iBest = -1;
            for (int i = users.Count - 1; i >= 0; i--)
            {
                if (users[i].isOnline())
                {
                    dtTmp = users[i].getRobRaceTime();
                    if (dtTmp <= dtNow)
                    {
                        return users[i];
                    }
                    else if (dtTmp < dtMin)
                    {
                        dtMin = dtTmp;
                        iBest = i;
                    }
                }
            }
            if (iBest != -1) 
                return users[iBest]; 
            else 
                return null;
        }

    }
}
