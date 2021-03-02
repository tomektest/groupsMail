using MailingGroups.Data;
using MailingGroups.Types;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;

namespace MailingGroups.Controllers
{
    [Authorize]
    public class MailsController : Controller
    {
        private readonly UserManager<IdentityUser> _userManager;
        private string UserID = null;
        private readonly IGroupData _GroupData;
        private readonly IMailData _MailData;
        private readonly ApiController _APIController;
        [BindProperty]
        public MailsType mail { get; set; }

        public MailsController(UserManager<IdentityUser> userManager, IGroupData GroupData, IMailData MailData, ApiController apiController)
        {
            _GroupData = GroupData;
            _MailData = MailData;
            _userManager = userManager;
            _APIController = apiController;

            if (User != null)
                UserID = _userManager.GetUserId(User);
        }

        [ApiExplorerSettings(IgnoreApi = true)]
        [Route("Mails/Index/{groupId:int}")]
        public IActionResult Index(int groupId)
        {
            return View(groupId);
        }

        [ApiExplorerSettings(IgnoreApi = true)]
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

        [ApiExplorerSettings(IgnoreApi = true)]
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

                    var resultJson = _APIController.AddEmail(mail);

                    return RedirectToAction("Index", "Mails", new { id = groupId });
                }

            }catch(Exception ex) {
                throw new NotImplementedException("Not implemented.");
            }
            return View(mail);
        }

        [ApiExplorerSettings(IgnoreApi = true)]
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

                var mailJson = _APIController.GetEmailById(mailId.Value).Result;
                mail = (MailsType)mailJson.Value;

                if (mail == null)
                {
                    return NotFound();
                }

                var getGroupEmail = _APIController.GetGroupEmail(mailId.Value).Result;

                ViewBag.groupId = (int)getGroupEmail.Value;
            }
            catch (Exception ex)
            {
                throw new NotImplementedException("Not implemented.");
            }

            return View(mail);
        }

        [ApiExplorerSettings(IgnoreApi = true)]
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
                    var getGroupEmail = _APIController.GetGroupEmail(mailId).Result;
                    int groupId = (int)getGroupEmail.Value;
                    mail.GroupId = groupId;
                    mail.GroupModelId = groupId;
                    var resultJson = _APIController.EditEmail(mail);

                    return RedirectToAction("Index", "Mails", new { id = groupId });
                }

            }catch (Exception ex){
                throw new NotImplementedException("Not implemented.");
            }

            return View(mail);
        }
    }
}
