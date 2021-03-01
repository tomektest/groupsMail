using MailingGroups.Areas.Identity.Data;
using MailingGroups.Data;
using MailingGroups.Types;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

/* migracja BD - typy/modele, oas */

namespace MailingGroups.Controllers
{
    [ApiController]
    [Authorize]
    public class MailsController : Controller
    {
        private readonly UserManager<IdentityUser> _userManager;
        private string UserID = null;
        private readonly IGroupData _GroupData;
        private readonly IMailData _MailData;
        [BindProperty]
        public MailsType mail { get; set; }

        public MailsController(UserManager<IdentityUser> userManager, IGroupData GroupData, IMailData MailData)
        {
            _GroupData = GroupData;
            _MailData = MailData;
            _userManager = userManager;

            if (User != null)
                UserID = _userManager.GetUserId(User);
        }

        [Route("Mails/Index/{groupId:int}")]
        public IActionResult Index(int groupId)
        {
            return View(groupId);
        }

        [HttpGet]
        [Route("Mails/Create/{groupId:int?}")]
        public IActionResult Create(int? groupId)
        {
            try
            {
                if (User != null)
                    UserID = _userManager.GetUserId(User);

                mail = new MailsType();

                if (groupId == null)
                {
                    return View(mail);
                }

                ViewBag.groupId = groupId;

            }catch(Exception ex) {
                throw new NotImplementedException("Not implemented.");
            }

            return View(mail);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Route("Mails/Create/{groupId:int}")]
        public IActionResult Create(int groupId)
        {
            try
            {
                if (User != null)
                    UserID = _userManager.GetUserId(User);

                mail.GroupModelId = groupId;
                mail.GroupId = groupId;
                ViewBag.groupId = groupId;

                if (ModelState.IsValid)
                {
                    mail.Id = 0;

                    _MailData.Create(mail);

                    return RedirectToAction("Index", "Mails", new { id = groupId });
                }

            }catch(Exception ex) {
                throw new NotImplementedException("Not implemented.");
            }
            return View(mail);
        }

        [HttpGet]
        [Route("Mails/Edit/{mailId:int}")]
        public IActionResult Edit(int? mailId)
        {
            try
            {
                if (User != null)
                    UserID = _userManager.GetUserId(User);

                mail = new MailsType();

                if (mailId == null)
                {
                    return View(mail);
                }
                mail = _MailData.GetEmailById(mailId.Value).Result;

                if (mail == null)
                {
                    return NotFound();
                }
                int groupId = _MailData.GetGroupEmail(mailId.Value).Result;

                ViewBag.groupId = groupId;

            }catch(Exception ex) {
                throw new NotImplementedException("Not implemented.");
            }

            return View(mail);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Route("Mails/Edit/{mailId:int}")]
        public IActionResult Edit(int mailId)
        {
            try
            {

                if (User != null)
                    UserID = _userManager.GetUserId(User);

                if (ModelState.IsValid)
                {
                    ViewBag.groupId = 0;
                    var mailVal = _MailData.GetEmailById(mailId).Result;
                    if (mailVal != null)
                        ViewBag.groupId = mailVal.GroupId;
                    mail.GroupId = ViewBag.groupId;
                    mail.GroupModelId = ViewBag.groupId;

                    _MailData.UpdateEmail(mail);

                    return RedirectToAction("Index", "Mails", new { id = ViewBag.groupId });
                }

            }catch (Exception ex){
                throw new NotImplementedException("Not implemented.");
            }

            return View(mail);
        }

        //GET Mails/GetAll
        /// <summary>
        /// Description of Mails/GetAll
        /// </summary>
        /// <returns>Json</returns>
        [HttpGet]
        [Route("Mails/GetAll/{groupId:int}")]
        public async Task<IActionResult> GetAll(int groupId)
        {
            List<MailsType> _data = new List<MailsType>();
            try
            {
                   if (User != null)
                      UserID = _userManager.GetUserId(User);

                    _data = await _MailData.GetAll(groupId, UserID);
            }
            catch (Exception ex)
            {
                throw new NotImplementedException("Not implemented.");
            }

            return Json(new { data = _data });
        }

        //GET Mails/Delete
        /// <summary>
        /// Description of Mails/Delete
        /// </summary>
        /// <returns></returns>
        [HttpDelete]
        [Route("Mails/Delete/{mailId:int}")]
        public async Task<IActionResult> Delete(int mailId)
        {
            try {
                
                bool del = await _MailData.Delete(mailId);
                if (!del)
                    return Json(new { success = false, message = "Error while Deleting" });

            }
            catch (Exception ex)
            {
                throw new NotImplementedException("Not implemented.");
            }
            return Json(new { success = true, message = "Delete successful" });
        }
    }
}
