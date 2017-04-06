using AMC.CORE.Enumerations;
using Microsoft.AspNetCore.Authorization;
using System.Threading.Tasks;

namespace AMC.WEB.Authorization
{
    public class ClientRequirement : AuthorizationHandler<ClientRequirement>, IAuthorizationRequirement
    {
        protected override Task HandleRequirementAsync(AuthorizationHandlerContext context, ClientRequirement requirement)
        {
            if(!context.User.HasClaim(claim => claim.Type == "Role" && (claim.Value == UserRole.Client.ToClaimString() || claim.Value == UserRole.Admin.ToClaimString())))
            {
                return Task.CompletedTask;
            }

            context.Succeed(requirement);
            return Task.CompletedTask;
        }
    }

    public class ContractorRequirement : AuthorizationHandler<ContractorRequirement>, IAuthorizationRequirement
    {
        protected override Task HandleRequirementAsync(AuthorizationHandlerContext context, ContractorRequirement requirement)
        {
            if (!context.User.HasClaim(claim => claim.Type == "Role" && (claim.Value == UserRole.Contractor.ToClaimString() || claim.Value == UserRole.Admin.ToClaimString())))
            {
                return Task.CompletedTask;
            }

            context.Succeed(requirement);
            return Task.CompletedTask;
        }
    }

    public class NotaryRequirement : AuthorizationHandler<NotaryRequirement>, IAuthorizationRequirement
    {
        protected override Task HandleRequirementAsync(AuthorizationHandlerContext context, NotaryRequirement requirement)
        {
            if (!context.User.HasClaim(claim => claim.Type == "Role" && (claim.Value == UserRole.Notary.ToClaimString() || claim.Value == UserRole.Admin.ToClaimString())))
            {
                return Task.CompletedTask;
            }

            context.Succeed(requirement);
            return Task.CompletedTask;
        }
    }

    public class AdminRequirement : AuthorizationHandler<AdminRequirement>, IAuthorizationRequirement
    {
        protected override Task HandleRequirementAsync(AuthorizationHandlerContext context, AdminRequirement requirement)
        {
            if (!context.User.HasClaim(claim => claim.Type == "Role" && claim.Value == UserRole.Admin.ToClaimString()))
            {
                return Task.CompletedTask;
            }

            context.Succeed(requirement);
            return Task.CompletedTask;
        }
    }
}
