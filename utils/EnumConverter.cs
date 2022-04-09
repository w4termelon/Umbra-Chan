using Umbra_C.Enums;
namespace Umbra_C.utils
{
    public static class EnumConverter
    {
        public static MountFarmList GetMountFarmList(string enumString)
        {
            switch(enumString){
                case "Prüfung50":
                return MountFarmList.Prüfung50;
                case "Prüfung60":
                return MountFarmList.Prüfung60;
                case "Prüfung70":
                return MountFarmList.Prüfung70;
                case "Prüfung80":
                return MountFarmList.Prüfung80;
                case "Prüfung90":
                return MountFarmList.Prüfung90;
                case "Raid":
                return MountFarmList.Raid;
                default:
                return MountFarmList.Prüfung50;
            }
        }
    }
}