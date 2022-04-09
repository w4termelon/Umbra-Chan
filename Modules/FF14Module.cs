using Discord;
using Discord.Interactions;
using Discord.WebSocket;
using Umbra_C.Enums;
using Umbra_C.utils;
using Umbra_C.Attributes;
namespace Umbra_C.Modules
{
    public class FF14Module : InteractionModuleBase<SocketInteractionContext>
    {
        // Dependencies can be accessed through Property injection, public properties with public setters will be set by the service provider
        public InteractionService Commands { get; set; }

        private InteractionHandler _handler;
        private readonly ApplicationDbContext _db;

        public FF14Module(InteractionHandler handler, ApplicationDbContext db)
        {
            _handler = handler;
            _db = db;
        }

        [SlashCommand("add", "Füge dein FF14 Charakter zum Bot hinzu, z.B: Suika Wintermoon")]
        public async Task FF14Char([Summary(description: "FF14 Charakter Vorname, z.B: Suika")]string firstName,
        [Summary(description: "FF14 Charakter Nachname, z.B: Wintermoon")]string lastName,
        [Summary(description: "Füge ein FF14 Charakter für einen anderen Benutzer zum Bot hinzu")]IUser? targetUser = null )
        {
            targetUser = targetUser == null ? Context.User : targetUser;
            await RespondAsync("**"+targetUser.Username + "** wurde erfolgreich mit: **" + firstName + " "+ lastName+ "** verknüpft!", ephemeral: true);
            var id = LodeStoneCrawler.getLodestoneID(firstName.Trim() + "+"+lastName.Trim());
            var dbUser = _db.Users.Find(targetUser.Id);
            if(dbUser == null)
                _db.Users.Add(
                    new Models.User{
                        UserId = targetUser.Id,
                        IsAdmin = false,
                        Lodestone = id,
                        DiscordMention = targetUser.Mention,
                        Mounts = new HashSet<Models.Mount>()
                    });
            else
                dbUser.Lodestone = id;
            _db.SaveChanges();
        }

        [RequireAdmin]
        [SlashCommand("mount_list", "Erstellt eine Liste mit Mounts für alle Mitglieder im Discord")]
        public async Task MountList([Summary(description: "Auswahl an möglichen Listen")]MountFarmList list)
        {
            var manager = new MountFarmListManager(list, _db);
            await ReplyAsync(embed: manager.createEmbed(), components: manager.createButtons());
        }

        [SlashCommand("update", "Aktualisiert die Info zu deinem oder dem angegebenen Benutzer")]
        public async Task UpdateUserMountInfo([Summary(description: "Benutzer wessen Info aktualisiert werden sollen")]IUser? targetUser = null)
        {
            targetUser = targetUser == null ? Context.User : targetUser;
            try{
                int numOfChanges = await new XIVManager(_db).updateUserMounts(targetUser.Id);
                await RespondAsync(targetUser.Username + " wurde " +  (numOfChanges == 0 ? "nicht" : "erfolgreich") + " aktualiasiert!", ephemeral: true);
            }
            catch(HttpRequestException e)
            {
                await RespondAsync(targetUser.Username + " ist auf ffxivcollect.com auf privat gestellt!", ephemeral: true);
            }
            catch(Exception e)
            {
                await RespondAsync("Ein Fehler ist unterlaufen!", ephemeral: true);
            }
            
        }

        [RequireAdmin]
        [ComponentInteraction("Delete_*")]
        public async Task DeleteMounList(string id)
        {
            SocketMessageComponent inter =  (SocketMessageComponent)Context.Interaction;
            await inter.Message.DeleteAsync();
        }
        
        [RequireAdmin]
        [ComponentInteraction("Update_*")]
        public async Task UpdateMountList(string id)
        { 
            MountFarmList list = EnumConverter.GetMountFarmList(id);
            var newEmbed = new MountFarmListManager(list, _db).createEmbed();
            SocketMessageComponent inter = (SocketMessageComponent)Context.Interaction;
            await inter.Message.ModifyAsync(a => {
                a.Embed = newEmbed;
            });
            await Context.Interaction.DeferAsync(true);
        }
    }

}