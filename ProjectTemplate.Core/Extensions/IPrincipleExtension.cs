using System;
using System.Security.Claims;
using System.Security.Principal;

namespace ProjectTemplate.Core.Extensions
{
    public static class IPrincipleExtension
    {
        public static string GetUserId(this IPrincipal principal)
        {
            string result = "";

            if (principal == null)
                return null;

            if (principal is ClaimsPrincipal identity)
            {
                var found = identity.FindFirst(a => a.Type == ClaimTypes.NameIdentifier);

                if (found != null && !String.IsNullOrEmpty(found.Value))
                {
                    result = found.Value;

                    return result;
                }

                return null;

            }
            else
                return null;
        }
        
        public static string GetFullName(this IPrincipal principal)
        {
            string result = "";

            if (principal == null)
                return null;

            if (principal is ClaimsPrincipal identity)
            {
                var found = identity.FindFirst(a => a.Type == ClaimTypes.GivenName);

                if (found != null && !String.IsNullOrEmpty(found.Value))
                {
                    result = found.Value;

                    return result;
                }

                return null;

            }
            else
                return null;
        }
    }
}