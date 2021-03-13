using Core.Entities;
using Core.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MailingGroups.API.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    public class EmailController : ControllerBase
    {
        private readonly IGroupsService _groupsService;
        private readonly IMailsService _mailService;

        public EmailController(IGroupsService groupsService, IMailsService mailsService)
        {
            _groupsService = groupsService;
            _mailService = mailsService;
        }

        /// <summary>
        /// </summary>
        /// <remarks>
        /// </remarks>
        /// <param name="groupId"></param>
        /// <response code="200">OK</response>
        [HttpGet("[action]/{groupId}")]
        public IActionResult GetAllEmails(int groupId)
        {
            var mails = _mailService.GetAllEmails(groupId);
            if (mails == null)
                return NotFound();
            return Ok(mails);
        }

        /// <summary>
        /// </summary>
        /// <remarks>
        /// </remarks>
        /// <param name="mailId"></param>
        /// <response code="200">OK</response>
        [HttpDelete("[action]/{mailId}")]
        public IActionResult DeleteEmail(int mailId)
        {
            var mail = _mailService.DeleteEmail(mailId);
            if (!mail)
                return NotFound();
            return Ok(mail);
        }

        /// <summary>
        /// </summary>
        /// <remarks>
        /// </remarks>
        /// <param name="mail"></param>
        /// <response code="200">OK</response>
        [HttpPut("[action]/{group}")]
        public IActionResult AddEmail(MailsType mail)
        {
            var result = _mailService.AddEmail(mail);
            if (!result)
                return NotFound();
            return Ok(result);
        }

        /// <summary>
        /// </summary>
        /// <remarks>
        /// </remarks>
        /// <param name="mail"></param>
        /// <response code="200">OK</response>
        [HttpPut("[action]/{mail}")]
        public IActionResult EditEmail(MailsType mail)
        {
            var result = _mailService.EditEmail(mail);
            if (!result)
                return NotFound();
            return Ok(result);
        }

        /// <summary>
        /// </summary>
        /// <remarks>
        /// </remarks>
        /// <param name="mailId"></param>
        /// <response code="200">OK</response>
        [HttpGet("[action]/{mailId}")]
        public IActionResult GetEmailById(int mailId)
        {
            var result = _mailService.GetEmailById(mailId);
            if (result == null)
                return NotFound();
            return Ok(result);
        }

        /// <summary>
        /// </summary>
        /// <remarks>
        /// </remarks>
        /// <param name="mailId"></param>
        /// <response code="200">OK</response>
        [HttpGet("[action]/{mailId}")]
        public IActionResult GetGroupIdByEmailId(int mailId)
        {
            var result = _mailService.GetGroupIdByEmailId(mailId);
            if (result == -1)
                return NotFound();
            return Ok(result);
        }
    }
}
