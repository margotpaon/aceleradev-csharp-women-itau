using Codenation.Challenge.Models;
using IdentityServer4.Models;
using IdentityServer4.Validation;
using System.Threading.Tasks;

namespace Codenation.Challenge.Services
{
    public class PasswordValidatorService : IResourceOwnerPasswordValidator
    {
        private readonly CodenationContext _context;
        public PasswordValidatorService(CodenationContext dbContext)
        {
            _context = dbContext;
        }

        public Task ValidateAsync(ResourceOwnerPasswordValidationContext context)
        {
            
            UserService userService = new UserService(_context);

            User user = userService.FindByEmail(context.UserName);

            
            if (user != null && user.Password == context.Password)
            {
                
                context.Result = new GrantValidationResult(
                    
                    subject: user.Id.ToString(),
                     "custom",
                     UserProfileService.GetUserClaims(user)); 

                return Task.CompletedTask;
            }
            
            context.Result = new GrantValidationResult(
                TokenRequestErrors.InvalidGrant, "Invalid username or password");
            return Task.FromResult(context.Result);
        }
    }
}