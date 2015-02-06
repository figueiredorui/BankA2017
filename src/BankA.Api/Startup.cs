using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Owin;
using Owin;

[assembly: OwinStartup(typeof(BankA.Api.Startup))]

namespace BankA.Api
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
        }
    }
}
