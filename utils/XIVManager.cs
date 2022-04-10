using Umbra_C.Models;
using RestSharp;
using Umbra_C;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using Microsoft.Data.SqlClient;
using System.Collections;
namespace Umbra_C.utils
{
    public class XIVManager
    {
        ApplicationDbContext _db;
        public XIVManager(ApplicationDbContext db)
        {
            _db = db;
        }
        private async Task<HashSet<Mount>> getMounts()
        {
            var options = new RestClientOptions("https://ffxivcollect.com/api/mounts") {
            ThrowOnAnyError = true,
            Timeout = 5000
            };
            var client = new RestClient(options);
            var request = new RestRequest();
            var response =  await client.GetAsync<Mounts>(request);
            return response.Results;  
        }

        public async Task getMountsForUser(ulong userID, List<Mount> allMounts)
        {
            var user = await _db.Users
                .Where(u => u.UserId == userID)
                .Include(u => u.Mounts)
                .FirstOrDefaultAsync();
            if(user == null) return;
            var options = new RestClientOptions($"https://ffxivcollect.com/api/characters/{user.Lodestone}?ids=show") {
                ThrowOnAnyError = true,
                Timeout = 1000
            };
            var client = new RestClient(options);
            var request = new RestRequest();
            var response =  await client.GetAsync<XIVCharakter>(request);
            if(response == null) return;
            if(response.Mounts == null) throw new ArgumentNullException();
            foreach(var id in response.Mounts.IDs)
            {
                var mount = allMounts.Find(am => am.Id == id);
                if(mount == null) continue;
                user.Mounts.Add(mount);
            }
            _db.SaveChanges();
        }
        // public async void updateAllUserMounts()
        // {
        //     HashSet<User> allUsers = _db.Users.ToHashSet();
        //     Task[] tasks = new Task[allUsers.Count];
        //     int i = 0;
        //     foreach(var user in allUsers)
        //     {
        //         tasks[i++] = getMountsForUser(user);
        //     }
        //     Task.WaitAll(tasks);
        //     await _db.SaveChangesAsync();
        // }
        public async Task<int> updateUserMounts(ulong discordID)
        {
            var allMounts = _db.Mounts.ToList();
            await getMountsForUser(discordID, allMounts);
            return 1;
        }
        
        public async void updateMounts()
        {
            do{
                var mountList = await getMounts();
                foreach(var mount in mountList)
                {
                    var ent = _db.Mounts.Find(mount.Id);
                    if(ent == null)
                        _db.Mounts.Add(mount);
                    else
                    {
                        ent.Owned = mount.Owned;
                    }
                }
                _db.SaveChangesAsync();
                await Task.Delay(86400000);
            }while(true);
        }
    }
}