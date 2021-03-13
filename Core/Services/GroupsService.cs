using Core.Entities;
using Core.Exceptions;
using Core.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace Core.Services
{
    public class GroupsService : IGroupsService
    {
        private readonly IGroupRepository _groupsRepository;
        private readonly IMailRepository _mailRepository;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public GroupsService(IHttpContextAccessor httpContextAccessor, IGroupRepository groupsRepository, IMailRepository mailRepository)
        {
            _groupsRepository = groupsRepository ?? throw new ArgumentNullException(nameof(groupsRepository));
            _mailRepository = mailRepository ?? throw new ArgumentNullException(nameof(mailRepository));
            _httpContextAccessor = httpContextAccessor;
        }

        public IEnumerable<GroupType> GetAllGroups()
        {
            string userID = "";
            if (_httpContextAccessor != null && _httpContextAccessor.HttpContext != null && _httpContextAccessor.HttpContext.User != null &&
                _httpContextAccessor.HttpContext.User.FindFirst(ClaimTypes.NameIdentifier) != null && !String.IsNullOrEmpty(_httpContextAccessor.HttpContext.User.FindFirst(ClaimTypes.NameIdentifier).Value))
                userID = _httpContextAccessor.HttpContext.User.FindFirst(ClaimTypes.NameIdentifier).Value;
            else
                return null;

            var result = _groupsRepository.GroupAll(userID);
            return result;
        }

        public bool DeleteGroups(int groupId)
        {
            string userID = "";
            if (_httpContextAccessor != null && _httpContextAccessor.HttpContext != null && _httpContextAccessor.HttpContext.User != null &&
                _httpContextAccessor.HttpContext.User.FindFirst(ClaimTypes.NameIdentifier) != null && !String.IsNullOrEmpty(_httpContextAccessor.HttpContext.User.FindFirst(ClaimTypes.NameIdentifier).Value))
                userID = _httpContextAccessor.HttpContext.User.FindFirst(ClaimTypes.NameIdentifier).Value;
            else
                return false;

            bool result = _groupsRepository.Delete(groupId, userID);
            return result;
        }

        public bool AddGroup(GroupType group)
        {
            string userID = "";
            if (_httpContextAccessor != null && _httpContextAccessor.HttpContext != null && _httpContextAccessor.HttpContext.User != null &&
                _httpContextAccessor.HttpContext.User.FindFirst(ClaimTypes.NameIdentifier) != null && !String.IsNullOrEmpty(_httpContextAccessor.HttpContext.User.FindFirst(ClaimTypes.NameIdentifier).Value))
                userID = _httpContextAccessor.HttpContext.User.FindFirst(ClaimTypes.NameIdentifier).Value;
            else
                return false;

            var groups = _groupsRepository.GroupAll(userID);
            if (groups.Any(x => x.Name == group.Name))
                return false;
            else
                _groupsRepository.AddGroup(group, userID);
            return true;
        }

        public bool EditGroup(GroupType group)
        {
            string userID = "";
            if (_httpContextAccessor != null && _httpContextAccessor.HttpContext != null && _httpContextAccessor.HttpContext.User != null &&
                _httpContextAccessor.HttpContext.User.FindFirst(ClaimTypes.NameIdentifier) != null && !String.IsNullOrEmpty(_httpContextAccessor.HttpContext.User.FindFirst(ClaimTypes.NameIdentifier).Value))
                userID = _httpContextAccessor.HttpContext.User.FindFirst(ClaimTypes.NameIdentifier).Value;
            else
                return false;

            var groups = _groupsRepository.GroupAll(userID);
            if (groups.Any(x => x.Name == group.Name))
                return false;
            else
                _groupsRepository.UpdateGroup(group, userID);
            return true;
        }

        public GroupType GetGroupById(int groupId)
        {
            string userID = "";
            if (_httpContextAccessor != null && _httpContextAccessor.HttpContext != null && _httpContextAccessor.HttpContext.User != null &&
                _httpContextAccessor.HttpContext.User.FindFirst(ClaimTypes.NameIdentifier) != null && !String.IsNullOrEmpty(_httpContextAccessor.HttpContext.User.FindFirst(ClaimTypes.NameIdentifier).Value))
                userID = _httpContextAccessor.HttpContext.User.FindFirst(ClaimTypes.NameIdentifier).Value;
            else
                return null;

            var group = _groupsRepository.GetGroupById(groupId, userID);
            return group;
        }
    }
}
