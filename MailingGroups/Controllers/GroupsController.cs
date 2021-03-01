using MailingGroups.Areas.Identity.Data;
using MailingGroups.Data;
using MailingGroups.Types;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MailingGroups.Controllers
{
    [ApiController]
    [Authorize]
    public class GroupsController : Controller
    {
        private readonly UserManager<IdentityUser> _userManager;
        private string UserID = null;
        private readonly IGroupData _GroupData;
        [BindProperty]
        public GroupType group { get; set; } 

        public GroupsController(UserManager<IdentityUser> userManager, IGroupData GroupData)
        {
            _GroupData = GroupData;
            _userManager = userManager;
            object user;
            if (HttpContext != null && HttpContext.User != null)
                user = _userManager.GetUserAsync(HttpContext.User);

            if (User != null)
                UserID = _userManager.GetUserId(User);
        }

        [Route("Groups/Index")]
        public IActionResult Index()
        {
            if (User != null)
                UserID = _userManager.GetUserId(User);

            return View();
        }

        /*  public IActionResult Authenticate()
          {
              var claims = new[]
              {
                  new Claim(JwtRegisteredClaimNames.Sub, UserID),
                  new Claim("test", "test2")
              };

              var secretBytes = Encoding.UTF8.GetBytes(JwtConstants.Secret);
              var key = new SymmetricSecurityKey(secretBytes);
              var algorithm = SecurityAlgorithms.HmacSha256;

              var signingCredentials = new SigningCredentials(key, algorithm);

              var token = new JwtSecurityToken(
                  JwtConstants.Issuer,
                  JwtConstants.Audience, 
                  claims, 
                  notBefore: DateTime.Now, 
                  expires: DateTime.Now.AddHours(2),
                  signingCredentials);

              var tokenJson = new JwtSecurityTokenHandler().WriteToken(token);

              return Ok(new { access_token = tokenJson });
          }*/

        [Route("Groups/Upsert/{id:int?}")]
        public IActionResult Upsert(int? id)
        {
            if (User != null)
                UserID = _userManager.GetUserId(User);

            group = new GroupType();
            if (id == null)
            {
                return View(group);
            }
            group = _GroupData.GetGroupById(id.Value, UserID).Result;
            if (group == null)
            {
                return NotFound();
            }
            return View(group);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Route("Groups/Upsert")]
        public IActionResult Upsert()
        {
            if (User != null)
                UserID = _userManager.GetUserId(User);

            group.UserId = UserID;
            if (ModelState.IsValid)
            {
                if (group.Id == 0)
                {
                    _GroupData.AddGroup(group);
                }
                else
                {
                    _GroupData.UpdateGroup(group);

                }
                return RedirectToAction("Index");
            }

            return View(group);
        }

        //GET Groups/GetAll
        /// <summary>
        /// Description of Groups/GetAll
        /// </summary>
        /// <returns>Json</returns>
        [HttpGet]
        [Route("Groups/GetAll")]
        public async Task<IActionResult> GetAll()
        {
            if (User != null)
                UserID = _userManager.GetUserId(User);

            List<GroupType> _data = new List<GroupType>();

            try
            {
                _data = await _GroupData.GetAll(UserID);
            }
            catch(Exception ex)
            {
                throw new NotImplementedException("Not implemented.");
            }

            return Json(new { data = _data });
        }

        //GET Groups/Delete
        /// <summary>
        /// Description of Groups/Delete
        /// </summary>
        /// <returns></returns>
        [HttpDelete]
        [Route("Groups/Delete/{groupId:int}")]
        public async Task<IActionResult> Delete(int groupId)
        {
            try
            {
                if (User != null)
                  UserID = _userManager.GetUserId(User);
 
                bool result = await _GroupData.Delete(groupId, UserID);
                if (result)
                    return Json(new { success = true, message = "Delete successful" });
            }
            catch(Exception ex)
            {
                throw new NotImplementedException("Not implemented.");
            }
            return Json(new { success = false, message = "Error while Deleting" });
        }

       /* [AcceptVerbs("GET", "POST")]
        public IActionResult VerifyGroupName(string Name)
        {
            if (User != null)
                UserID = _userManager.GetUserId(User);

            if (_GroupData.VerifyGroupName(Name, UserID).Result)
            {
                return Json($"Group {Name} is already in use.");
            }

            return Json(true);
        }*/
    }
}
