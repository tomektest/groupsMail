using Core.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MailingGroups.API.Controllers
{
    public class BaseController : Controller
    {
        protected RoleManager<IdentityRole> RoleManager { get; private set; }
        protected UserManager<ApplicationUser> UserManager { get; private set; }
        protected IConfiguration Configuration { get; private set; }

        public BaseController(RoleManager<IdentityRole> roleManager, UserManager<ApplicationUser> userManager,
                    IConfiguration configuration)
        {
            RoleManager = roleManager;
            UserManager = userManager;
            Configuration = configuration;
        }
    }
}
