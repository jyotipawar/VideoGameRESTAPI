using System;
using Microsoft.AspNetCore.Mvc;

namespace VideoGame.RESTAPI.Helpers.BasicAuth
{
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method)]
    public class BasicAuthAttribute : TypeFilterAttribute
    {
        public BasicAuthAttribute() : base(typeof(BasicAuthFilter))
        {
           
        }
    }
}
