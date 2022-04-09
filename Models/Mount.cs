using Umbra_C.Enums;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace Umbra_C.Models
{
    public class Mount
    {
        public Mount ()
        {
            Users = new HashSet<User>();
            Sources = new HashSet<Source>();
        }
        [Key]
        public int Id {get; set;}
        public string Name {get; set;}
        public string Description {get; set;}
        public string Enhanced_Description {get; set;}
        public string Tooltip {get; set;}
        public string Movement {get; set;}
        public int Seats {get; set;}
        public int Order {get; set;}
        public int Order_group {get; set;}
        public string Patch {get; set;}
        public int? Item_id {get; set;}
        public string Owned {get; set;}
        public string Image {get; set;}
        public string Icon {get; set;}
        public string? Bgm {get; set;}
        public virtual HashSet<Source> Sources {get; set;}
       
        public virtual HashSet<User> Users {get; set;}

        public override int GetHashCode()
            => Id.GetHashCode();
    }

    public class Source
    {
        public int ID {get; set;}
        public string? Type {get; set;}
        public string? Text {get; set;}
        public string? Related_Type {get; set;}
        public int? Related_ID {get; set;}
        public override int GetHashCode()
            => ID.GetHashCode();
    }
    public class Mounts 
    {
        public object Query {get; set;}
        public int Count {get;set;}
        public HashSet<Mount> Results {get; set;}
    }
}