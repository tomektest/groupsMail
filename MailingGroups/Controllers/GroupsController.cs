using MailingGroups.Data;
using MailingGroups.Types;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace MailingGroups.Controllers
{
    [Authorize]
    public class GroupsController : Controller
    {
        private readonly UserManager<IdentityUser> _userManager;
        private string UserID = null;
        private readonly IGroupData _GroupData;
        private readonly ApiController _APIController;
        [BindProperty]
        public GroupType group { get; set; } 

        public GroupsController(UserManager<IdentityUser> userManager, IGroupData GroupData, ApiController apiController)
        {
            _GroupData = GroupData;
            _userManager = userManager;
            _APIController = apiController;
            object user;
            if (HttpContext != null && HttpContext.User != null)
                user = _userManager.GetUserAsync(HttpContext.User);

            if (User != null)
                UserID = _userManager.GetUserId(User);
        }

        public IActionResult Index()
        {
            if (User != null)
                UserID = _userManager.GetUserId(User);

            return View();
        }

        //[Route("Groups/Upsert/{id:int?}")]

        [ApiExplorerSettings(IgnoreApi = true)]
        public IActionResult Upsert(int? id)
        {
            if (User != null)
                UserID = _userManager.GetUserId(User);

            group = new GroupType();
            if (id == null)
            {
                return View(group);
            }

            var groupJson = _APIController.GetGroupById(id.Value).Result;
            group = (GroupType)groupJson.Value;

            if (group == null)
            {
                return NotFound();
            }
            return View(group);
        }

        [ApiExplorerSettings(IgnoreApi = true)]
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Route("Groups/Upsert")]
        public IActionResult UpsertSubmit()
        {
            if (User != null)
                UserID = _userManager.GetUserId(User);

            group.UserId = UserID;
            if (ModelState.IsValid)
            {
                if (group.Id == 0)
                {
                    var resultJson = _APIController.AddGroup(group).Result;
                }
                else
                {
                    var resultJson = _APIController.EditGroup(group);
                }
                return RedirectToAction("Index");
            }

            return View(group);
        }
    }
}
