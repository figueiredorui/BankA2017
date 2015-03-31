using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Owin;
using Owin;

[assembly: OwinStartup(typeof(BankA.WebHost.Startup))]

namespace BankA.WebHost
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
        }
    }
}
