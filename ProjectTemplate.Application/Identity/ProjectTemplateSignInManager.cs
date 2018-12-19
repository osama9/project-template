using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using ProjectTemplate.Domain.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace ProjectTemplate.Application.Identity
{
    public class ProjectTemplateSignInManager : SignInManager<User>
    {
        public ProjectTemplateSignInManager(UserManager<User> userManager,
            IHttpContextAccessor contextAccessor,
            IUserClaimsPrincipalFactory<User> claimsFactory,
            IOptions<IdentityOptions> optionsAccessor,
            ILogger<SignInManager<User>> logger,
            IAuthenticationSchemeProvider schemes) : base(userManager, contextAccessor, claimsFactory, optionsAccessor, logger, schemes)
        {

        }
    }
}
