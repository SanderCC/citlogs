using System;
using System.Collections.Generic;


namespace Manager
{
    public class Acclog
    {
        public string Error { get; set; } = null;
        public double Duration { get; set; } = 0;
        public List<string> Local { get; set; }
        public List<string> Emergency { get; set; }
        public List<string> Main { get; set; }
        public List<string> Team { get; set; }
        public List<string> Country { get; set; }
        public List<string> Advert { get; set; }
        public List<string> Occupation { get; set; }
        public List<string> TTransactions { get; set; }
        public List<string> GTransactions { get; set; }
        public List<string> PlayerTransactions { get; set; }
        public List<string> KillDeaths { get; set; }
        public List<string> JoinQuit { get; set; }
        public List<string> Fmsg { get; set; }
        public List<string> Sms { get; set; }
        public List<string> Group { get; set; }
        public List<string> Squad { get; set; }
        public List<string> Unit { get; set; }
        public List<string> Cad { get; set; }
        public List<string> Support { get; set; }
        public List<string> Hit { get; set; }
        public List<string> Inventory { get; set; }
        public List<string> Trading { get; set; }
        public List<string> Other { get; set; }

        public Acclog()
        {
            Emergency = new List<string>();
            Local = new List<string>();
            Main = new List<string>();
            Team = new List<string>();
            Country = new List<string>();
            Advert = new List<string>();
            Occupation = new List<string>();
            TTransactions = new List<string>();
            GTransactions = new List<string>();
            PlayerTransactions = new List<string>();
            KillDeaths = new List<string>();
            JoinQuit = new List<string>();
            Fmsg = new List<string>();
            Sms = new List<string>();
            Group = new List<string>();
            Squad = new List<string>();
            Unit = new List<string>();
            Other = new List<string>();
            Cad = new List<string>();
            Support = new List<string>();
            Hit = new List<string>();
            Inventory = new List<string>();
            Trading = new List<string>();
            Other = new List<string>();
        }

        public void Divide(string content)
        {
            DateTime start = DateTime.Now;
            try
            {
                foreach (string line in content.Split("\n"))
                {
                    if (line.Length < 5) Console.WriteLine("Line too small, skipped.");
                    else if (line.Contains(" TC: ")) Team.Add(line);
                    else if (line.Contains(" MC LS:")) Main.Add(line);
                    else if (line.Contains(" MC LV:")) Main.Add(line);
                    else if (line.Contains(" MC SF:")) Main.Add(line);
                    else if (line.Contains(" (ADVERT) ")) Advert.Add(line);
                    else if (line.Contains(" (MYC ")) Country.Add(line);

                    else if (line.Contains(" (sup) ")) Support.Add(line);
                    else if (line.Contains(" (cad) ")) Cad.Add(line);

                    else if (line.Contains(" KILL: ")) KillDeaths.Add(line);
                    else if (line.Contains(" DEATH: ")) KillDeaths.Add(line);
                    else if (line.Contains(" killed themselves via command")) KillDeaths.Add(line);

                    else if (line.Contains(" GrC (")) Group.Add(line);
                    else if (line.Contains(" SC (")) Squad.Add(line);
                    else if (line.Contains(" UC (")) Unit.Add(line);
                    else if (line.Contains(" (alliance) ")) Group.Add(line);

                    else if (line.Contains(" SMS from ")) Sms.Add(line);
                    else if (line.Contains(" SMS to ")) Sms.Add(line);

                    else if (line.Contains(" T$ "))
                    {
                        TTransactions.Add(line);
                        if(line.Contains("(CITphoneTran")) PlayerTransactions.Add(line);
                        else if (line.Contains("CIThit")) Hit.Add(line);
                    }
                    else if (line.Contains(" G$ "))
                    {
                        GTransactions.Add(line);
                        if(line.Contains("(CITphoneTran")) PlayerTransactions.Add(line);
                        else if (line.Contains("CIThit")) Hit.Add(line);
                    }
                    else if(line.Contains(" BT: ")) PlayerTransactions.Add(line);
                    
                    else if (line.Contains(" (FMSG) ")) Fmsg.Add(line);
                    else if (line.Contains(" (LOCF)[")) Fmsg.Add(line);

                    else if (line.Contains(" (LOC)[")) Local.Add(line);
                    else if (line.Contains(" (LOC)[")) Local.Add(line);
                    else if (line.Contains(" (LOC)[")) Local.Add(line);
                    
                    else if (line.Contains(" LC ")) Emergency.Add(line);
                    
                    else if (line.Contains(" GroupPromotion: ")) Group.Add(line);
                    
                    else if (line.Contains(" modify ")) Inventory.Add(line);
                    else if (line.Contains(") sold ")) Trading.Add(line);
                    else if (line.Contains(" (Bought ")) Trading.Add(line);

                    else if (line.Contains(" NC ")) JoinQuit.Add(line);
                    else if (line.Contains(" LOGIN MISC: ")) JoinQuit.Add(line);
                    else if (line.Contains(" LOGIN: ")) JoinQuit.Add(line);
                    else if (line.Contains(" LOGIN WEPS: ")) JoinQuit.Add(line);
                    else if (line.Contains(" QUIT: ")) JoinQuit.Add(line);
                    else if (line.Contains(" QUIT MISC: ")) JoinQuit.Add(line);
                    else if (line.Contains(" QUIT WEPS: ")) JoinQuit.Add(line);
                    else if (line.Contains(" QUIT WEPS: ")) JoinQuit.Add(line);
                    
                    else Other.Add(line);
                }
            }
            catch (Exception e)
            {
                Error = e.Message;
            }
            Duration = (DateTime.Now - start).TotalSeconds;
        }
    }
}