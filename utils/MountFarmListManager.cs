using Discord;
using Umbra_C.Enums;
using Umbra_C.Models;
using Microsoft.EntityFrameworkCore;
namespace Umbra_C.utils
{
    public class MountFarmListManager
    {
        private ApplicationDbContext _db {get; set;}
        public MountFarmListManager(MountFarmList list, ApplicationDbContext db )
        {
            _list = list;
            _db = db;
        }
        private MountFarmList _list;
        public Embed createEmbed()
        {
            var sad = _db.Users.ToList();
            var allUsers = new User[sad.Count()];
            sad.CopyTo(0, allUsers,0,sad.Count());

            var builder = new EmbedBuilder();
            builder.Color = new Color(0xff0000);
            builder.Title = getTitle();
            var mounts = getMounts();
            foreach(var m in mounts)
            {
                var value = getUserMentionsForMount(m, allUsers.ToList());
                builder.AddField(m.Name,  string.IsNullOrEmpty(value) ? "\u200B" : value);
            }
            return builder.Build();
        }
        public  MessageComponent createButtons()
        {

            var builder = new ComponentBuilder();
            builder.WithButton("Update","Update_" + _list.ToString(),ButtonStyle.Primary);
            builder.WithButton("Delete","Delete_" + _list.ToString(),ButtonStyle.Danger);
            return builder.Build();
        }
        
        private string getTitle()
        {
            switch(_list)
            {
                case MountFarmList.Prüfung50: 
                return "Mounts - Prüfung - Stufe50";
                case MountFarmList.Prüfung60: 
                return "Mounts - Prüfung - Stufe60";
                case MountFarmList.Prüfung70: 
                return "Mounts - Prüfung - Stufe70";
                case MountFarmList.Prüfung80: 
                return "Mounts - Prüfung - Stufe80";
                case MountFarmList.Prüfung90: 
                return "Mounts - Prüfung - Stufe90";
                case MountFarmList.Raid: 
                return "Mounts - Raid";
                default:
                return "Mounts";
            }
        }
        private IEnumerable<Mount> getMounts()
        {
            switch(_list)
            {
                case MountFarmList.Prüfung50: 
                return _db.Mounts.Where(m => m.Sources.OrderBy(s => s.ID).Last().Type == "Trial" ).Include(m => m.Users).ToList().Where(l => l.Patch.StartsWith('2'));
                case MountFarmList.Prüfung60: 
                return _db.Mounts.Where(m => m.Sources.OrderBy(s => s.ID).Last().Type == "Trial" ).Include(m => m.Users).ToList().Where(l => l.Patch.StartsWith('3'));
                case MountFarmList.Prüfung70: 
                return _db.Mounts.Where(m => m.Sources.OrderBy(s => s.ID).Last().Type == "Trial" ).Include(m => m.Users).ToList().Where(l => l.Patch.StartsWith('4'));
                case MountFarmList.Prüfung80: 
                return _db.Mounts.Where(m => m.Sources.OrderBy(s => s.ID).Last().Type == "Trial" ).Include(m => m.Users).ToList().Where(l => l.Patch.StartsWith('5'));
                case MountFarmList.Prüfung90: 
                return _db.Mounts.Where(m => m.Sources.OrderBy(s => s.ID).Last().Type == "Trial" ).Include(m => m.Users).ToList().Where(l => l.Patch.StartsWith('6'));
                case MountFarmList.Raid: 
                return _db.Mounts.Where(m => m.Sources.First().Type == "Raid").Include(m => m.Users).ToList();
                default:
                return new HashSet<Mount>();
            }
        }

        private string getUserMentionsForMount(Mount m, List<User>allUsers)
        {
            if(m == null) return "";
            foreach(var user in m.Users)
                allUsers.Remove(user);
            var sss = string.Join(" ", allUsers.Select(a => a.DiscordMention));
            return  sss;
        }
    }
}