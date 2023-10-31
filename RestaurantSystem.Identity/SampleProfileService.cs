using Duende.IdentityServer.Extensions;
using Duende.IdentityServer.Models;
using Duende.IdentityServer.Services;
using IdentityModel;
using Microsoft.AspNetCore.Identity;
using RestaurantSystem.Models.Models;
using System.Security.Claims;

namespace RestaurantSystem.Identity
{
    public class SampleProfileService : IProfileService
    {
        private readonly IUserClaimsPrincipalFactory<WebAppUser> _userClaimsPrincipalFactory;
        private readonly UserManager<WebAppUser> _userMgr;
        public SampleProfileService(
            UserManager<WebAppUser> userMgr,
            IUserClaimsPrincipalFactory<WebAppUser> userClaimsPrincipalFactory)
        {
            _userMgr = userMgr;
            _userClaimsPrincipalFactory = userClaimsPrincipalFactory;
        }

        public virtual async Task GetProfileDataAsync(ProfileDataRequestContext context)
        {
            var sub = context.Subject.GetSubjectId();
            WebAppUser user = await _userMgr.FindByIdAsync(sub);
            ClaimsPrincipal userClaims = await _userClaimsPrincipalFactory.CreateAsync(user);
            List<Claim> claims = userClaims.Claims.ToList();


            if (_userMgr.SupportsUserRole)
            {
                IList<string> roles = await _userMgr.GetRolesAsync(user);
                foreach (var rolename in roles)
                {
                    claims.Add(new Claim(JwtClaimTypes.Role, rolename));
                }
            }

            context.IssuedClaims = claims;
        }
      
        // this method allows to check if the user is still "enabled" per token request
        public virtual async Task IsActiveAsync(IsActiveContext context)
        {
            string sub = context.Subject.GetSubjectId();
            WebAppUser user = await _userMgr.FindByIdAsync(sub);
            context.IsActive = user != null;
        }
    }
}
