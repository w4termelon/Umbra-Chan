using Discord;
using Discord.Interactions;
using Microsoft.Extensions.DependencyInjection;

namespace Umbra_C.Attributes
{
    public class RequireAdminAttribute : PreconditionAttribute
    {
        public override Task<PreconditionResult> CheckRequirementsAsync(IInteractionContext context, ICommandInfo commandInfo, IServiceProvider services)
        {       
            var db = services.GetRequiredService<ApplicationDbContext>();
            return (db.Users.Find(context.User.Id)?.IsAdmin ?? false)
                ? Task.FromResult(PreconditionResult.FromSuccess())
                : Task.FromResult(PreconditionResult.FromError("User is not an Admin!" + context.Interaction.DeferAsync(true)));
        }
    }
}