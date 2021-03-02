using MailingGroups.Data;
using MailingGroups.Models;
using MailingGroups.Types;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text.Json;
using System.Threading.Tasks;

namespace MailingGroups.Controllers
{
    [Authorize]
    public class ApiController : Controller
    {
        private readonly IGroupData _GroupData;
        private readonly IMailData _MailData;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public ApiController(IHttpContextAccessor httpContextAccessor, IGroupData groupData, IMailData mailData)
        {
            _MailData = mailData;
            _GroupData = groupData;
            _httpContextAccessor = httpContextAccessor;
        }

        #region Group
        [HttpGet]
        [Route("Groups/GetAllGroups")]
        public async Task<JsonResult> GetAllGroups()
        {
            List<GroupViewModel> groupVM = new List<GroupViewModel>();
            try
            {
                var userID = _httpContextAccessor.HttpContext.User.FindFirst(ClaimTypes.NameIdentifier).Value;
                var r = _GroupData.GetAll(userID);

                foreach (var item in r)
                {
                    GroupViewModel gvw = new GroupViewModel();
                    gvw.Id = item.Id;
                    gvw.Lp = item.Lp;
                    gvw.Name = item.Name;
                    groupVM.Add(gvw);
                }
            }
            catch (Exception ex)
            {
                throw new NotImplementedException("Not implemented.");
            }
            return Json( new { data = groupVM });
        }

        [HttpDelete]
        [Route("Groups/DeleteGroups/{groupId:int}")]
        public async Task<JsonResult> DeleteGroups(int groupId)
        {
            try
            {
                var userID = _httpContextAccessor.HttpContext.User.FindFirst(ClaimTypes.NameIdentifier).Value;
                bool result = _GroupData.Delete(groupId, userID);
                if (result)
                    return Json(new { success = true, message = "Delete successful" });
            }
            catch (Exception ex)
            {
                throw new NotImplementedException("Not implemented.");
            }
            return Json(new { success = false, message = "Error while Deleting" });
        }

        [HttpGet]
        [Route("Groups/AddGroup")]
        public async Task<JsonResult> AddGroup([FromQuery] GroupType group)
        {
            try
            {
                var userID = _httpContextAccessor.HttpContext.User.FindFirst(ClaimTypes.NameIdentifier).Value;
                var groups = _GroupData.GetAll(userID);
                if (groups.Any(x => x.Name == group.Name))
                    return Json(new { success = false });
                else
                    _GroupData.AddGroup(group);
            }
            catch (Exception ex)
            {
                throw new NotImplementedException("Not implemented.");
            }
            return Json(new { success = true });
        }

        [HttpGet]
        [Route("Groups/EditGroup")]
        public async Task<JsonResult> EditGroup([FromQuery] GroupType group)
        {
            try
            {
                var userID = _httpContextAccessor.HttpContext.User.FindFirst(ClaimTypes.NameIdentifier).Value;
                var groups = _GroupData.GetAll(userID);
                if (groups.Any(x => x.Name == group.Name))
                    return Json(new { success = false });
                else
                    _GroupData.UpdateGroup(group);
            }
            catch (Exception ex)
            {
                throw new NotImplementedException("Not implemented.");
            }
            return Json(new { success = true });
        }

        [HttpGet]
        [Route("Groups/GetGroupById/{groupId:int}")]
        public async Task<JsonResult> GetGroupById(int groupId)
        {
            GroupType group = new GroupType();
            try
            {
                var userID = _httpContextAccessor.HttpContext.User.FindFirst(ClaimTypes.NameIdentifier).Value;
                group = _GroupData.GetGroupById(groupId, userID);
            }
            catch (Exception ex)
            {
                throw new NotImplementedException("Not implemented.");
            }
            return Json(group);
        }

        #endregion Group

        #region Email
        [HttpGet]
        [Route("Mails/GetAllEmails/{groupId:int}")]
        public async Task<JsonResult> GetAllEmails(int groupId)
        {
            List<MailsType> result = new List<MailsType>();
            try
            {
                var userID = _httpContextAccessor.HttpContext.User.FindFirst(ClaimTypes.NameIdentifier).Value;
                result = _MailData.GetAll(groupId, userID);
              //  result = JsonConvert.SerializeObject(r);
            }
            catch (Exception ex)
            {
                throw new NotImplementedException("Not implemented.");
            }
            return Json(new { data = result });
        }

        [HttpDelete]
        [Route("Mails/DeleteEmail/{mailId:int}")]
        public async Task<JsonResult> DeleteEmail(int mailId)
        {
            try
            {
                var userID = _httpContextAccessor.HttpContext.User.FindFirst(ClaimTypes.NameIdentifier).Value;
                bool result = _MailData.Delete(mailId);
                if (result)
                    return Json(new { success = true, message = "Delete successful" });
            }
            catch (Exception ex)
            {
                throw new NotImplementedException("Not implemented.");
            }
            return Json(new { success = false, message = "Error while Deleting" });
        }

        [HttpGet]
        [Route("Mails/EditEmail")]
        public async Task<JsonResult> EditEmail([FromQuery] MailsType mail)
        {
            try
            {
                var userID = _httpContextAccessor.HttpContext.User.FindFirst(ClaimTypes.NameIdentifier).Value;
                var emails = _MailData.GetAll(mail.GroupId, userID);
                if (emails.Any(x => x.Email == mail.Email))
                    return Json(new { success = false });
                else
                    _MailData.UpdateEmail(mail);
            }
            catch (Exception ex)
            {
                throw new NotImplementedException("Not implemented.");
            }
            return Json(new { success = true });
        }

        [HttpGet]
        [Route("Mails/AddEmail")]
        public async Task<JsonResult> AddEmail([FromQuery] MailsType mail)
        {
            try
            {
                var userID = _httpContextAccessor.HttpContext.User.FindFirst(ClaimTypes.NameIdentifier).Value;
                var emails = _MailData.GetAll(mail.GroupId, userID);
                if (emails.Any(x => x.Email == mail.Email))
                    return Json(new { success = false });
                else
                    _MailData.Create(mail);
            }
            catch (Exception ex)
            {
                throw new NotImplementedException("Not implemented.");
            }
            return Json(new { success = true });
        }

        [HttpGet]
        [Route("Mails/GetEmailById/{mailId:int}")]
        public async Task<JsonResult> GetEmailById(int mailId)
        {
            MailsType mail = new MailsType();
            try
            {
                var userID = _httpContextAccessor.HttpContext.User.FindFirst(ClaimTypes.NameIdentifier).Value;
                mail = _MailData.GetEmailById(mailId, userID);
            }
            catch (Exception ex)
            {
                throw new NotImplementedException("Not implemented.");
            }
            return Json(mail);
        }

        [HttpGet]
        [Route("Mails/GetGroupEmail/{mailId:int}")]
        public async Task<JsonResult> GetGroupEmail(int mailId)
        {
            int mail = -1;
            try
            {
                var userID = _httpContextAccessor.HttpContext.User.FindFirst(ClaimTypes.NameIdentifier).Value;
                mail = _MailData.GetGroupEmail(mailId, userID);
            }
            catch (Exception ex)
            {
                throw new NotImplementedException("Not implemented.");
            }
            return Json(mail);
        }
        
        #endregion Email
    }
}
