namespace Umbra_C.Models
{
    public class XIVCharakter
    {
        public int ID {get;set;}
        public string Name {get; set;}
        public string Server {get; set;}
        public string Portrait {get; set;}
        public string Avatar {get; set;}
        public string Last_Parsed {get; set;}
        public bool Verified {get; set;}
        public CharakterAchievementInfo Achievements {get; set;}
        public CharakterMountInfo Mounts {get; set;}
        public CharakterMinionInfo Minions {get; set;}
        public CharakterOrchestrionInfo Orchestrions {get; set;}
        public CharakterBlueSpellInfo BlueSpells {get; set;}
        public CharakterEmoteInfo Emotes {get; set;}
        public CharakterBardingInfo Bardings {get; set;}
        public CharakterHairstyleInfo Hairstyles {get; set;}
        public CharakterArmoireInfo Armoires {get; set;}
        public CharakterFashionInfo Fashions {get; set;}
        public CharakterRecordInfo Records {get; set;}
        public CharakterTriadInfo Triads {get; set;}
    }

    public class CharakterAchievementInfo{
        public int Count {get;set;}
        public int Total {get;set;}
        public int Points {get;set;}
        public int Points_Total {get;set;}
        public bool Public {get;set;}
        public int[] IDs {get;set;}
    }
    public class CharakterMountInfo{
        public int Count {get;set;}
        public int Total {get;set;}
        public int[] IDs {get;set;}
    }
    public class CharakterMinionInfo{
        public int Count {get;set;}
        public int Total {get;set;}
        public int[] IDs {get;set;}
    }
    public class CharakterOrchestrionInfo{
        public int Count {get;set;}
        public int Total {get;set;}
        public int[] IDs {get;set;}
    }
    public class CharakterBlueSpellInfo{
        public int Count {get;set;}
        public int Total {get;set;}
        public int[] IDs {get;set;}
    }
    public class CharakterEmoteInfo{
        public int Count {get;set;}
        public int Total {get;set;}
        public int[] IDs {get;set;}
    }
    public class CharakterBardingInfo{
        public int Count {get;set;}
        public int Total {get;set;}
        public int[] IDs {get;set;}
    }
    public class CharakterHairstyleInfo{
        public int Count {get;set;}
        public int Total {get;set;}
        public int[] IDs {get;set;}
    }
    public class CharakterArmoireInfo{
        public int Count {get;set;}
        public int Total {get;set;}
        public int[] IDs {get;set;}
    }
    public class CharakterFashionInfo{
        public int Count {get;set;}
        public int Total {get;set;}
        public int[] IDs {get;set;}
    }
    public class CharakterRecordInfo{
        public int Count {get;set;}
        public int Total {get;set;}
        public int[] IDs {get;set;}
    }
    public class CharakterTriadInfo{
        public int Count {get;set;}
        public int Total {get;set;}
        public int[] IDs {get;set;}
    }

}