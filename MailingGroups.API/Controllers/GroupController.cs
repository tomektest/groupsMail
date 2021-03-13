using Core.Entities;
using Core.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace MailingGroups.API.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    public class GroupController : ControllerBase
    {
        private readonly IGroupsService _groupsService;

        public GroupController(IGroupsService groupsService)
        {
            _groupsService = groupsService;
        }

        /// <summary>
        /// </summary>
        /// <remarks>
        /// </remarks>
        /// <response code="200">OK</response>
        [HttpGet("[action]")]
        public IActionResult GetAll()
        {
            var groups = _groupsService.GetAllGroups();
            if (groups == null)
                return NotFound();
            return Ok(groups);
        }

        /// <summary>
        /// </summary>
        /// <remarks>
        /// </remarks>
        /// <param name="groupId"></param>
        /// <response code="200">OK</response>
        [HttpDelete("[action]/{groupID}")]
        public IActionResult DeleteGroups(int groupId)
        {
            var groups = _groupsService.DeleteGroups(groupId);
            if (!groups)
                return NotFound();
            return Ok(groups);
        }

        /// <summary>
        /// </summary>
        /// <remarks>
        /// </remarks>
        /// <param name="group"></param>
        /// <response code="200">OK</response>
        [HttpPut("[action]/{group}")]
        public IActionResult AddGroup(GroupType group)
        {
            var groups = _groupsService.AddGroup(group);
            if (!groups)
                return NotFound();
            return Ok(groups);
        }

        /// <summary>
        /// </summary>
        /// <remarks>
        /// </remarks>
        /// <param name="group"></param>
        /// <response code="200">OK</response>
        [HttpPut("[action]/{group}")]
        public IActionResult EditGroup(GroupType group)
        {
            var groups = _groupsService.EditGroup(group);
            if (!groups)
                return NotFound();
            return Ok(groups);
        }

        /// <summary>
        /// </summary>
        /// <remarks>
        /// </remarks>
        /// <param name="groupId"></param>
        /// <response code="200">OK</response>
        [HttpGet("[action]{groupId}")]
        public IActionResult GetGroupById(int groupId)
        {
            var groups = _groupsService.GetGroupById(groupId);
            if (groups == null)
                return NotFound();
            return Ok(groups);
        }
    }
}
