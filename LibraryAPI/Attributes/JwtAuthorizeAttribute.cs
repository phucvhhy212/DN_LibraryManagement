using LibraryAPI.Filters;
using Microsoft.AspNetCore.Mvc;

namespace LibraryAPI.Attributes
{
    public class JwtAuthorizeAttribute : TypeFilterAttribute
    {
        public JwtAuthorizeAttribute() : base(typeof(JwtAuthorizeFilter))
        {
        }

    }
}
