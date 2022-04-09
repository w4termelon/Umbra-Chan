using System.ComponentModel.DataAnnotations;
namespace Umbra_C.Models
{
    public class User
    {
        public User()
        {
            Mounts = new HashSet<Mount>();
        }
        [Key]
        public ulong UserId {get; set;}
        public string DiscordMention {get; set;}
        public string Lodestone {get; set;}
        public bool IsAdmin {get; set;} = false;
        public virtual HashSet<Mount> Mounts {get; set;}

        public override int GetHashCode()
            => UserId.GetHashCode();

    }

}