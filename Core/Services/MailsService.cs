using System;
using System.Collections.Generic;
using System.Text;
using Core.Interfaces;
using Core.Entities;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using System.Security.Claims;
using System.Linq;

namespace Core.Services
{
    public class MailsService : IMailsService
    {
        private readonly IMailRepository _mailRepository;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public MailsService(IHttpContextAccessor httpContextAccessor, IMailRepository mailRepository)
        {
            _mailRepository = mailRepository ?? throw new ArgumentNullException(nameof(mailRepository)); ;
            _httpContextAccessor = httpContextAccessor;
        }

        public IEnumerable<MailsType> GetAllEmails(int groupId)
        {
            string userID = "";
            if (_httpContextAccessor != null && _httpContextAccessor.HttpContext != null && _httpContextAccessor.HttpContext.User != null &&
                _httpContextAccessor.HttpContext.User.FindFirst(ClaimTypes.NameIdentifier) != null && !String.IsNullOrEmpty(_httpContextAccessor.HttpContext.User.FindFirst(ClaimTypes.NameIdentifier).Value))
                userID = _httpContextAccessor.HttpContext.User.FindFirst(ClaimTypes.NameIdentifier).Value;
            else
                return null;

            var result = _mailRepository.GetAll(groupId, userID);
            return result;
        }

        public bool DeleteEmail(int mailId)
        {
            string userID = "";
            if (_httpContextAccessor != null && _httpContextAccessor.HttpContext != null && _httpContextAccessor.HttpContext.User != null &&
                _httpContextAccessor.HttpContext.User.FindFirst(ClaimTypes.NameIdentifier) != null && !String.IsNullOrEmpty(_httpContextAccessor.HttpContext.User.FindFirst(ClaimTypes.NameIdentifier).Value))
                userID = _httpContextAccessor.HttpContext.User.FindFirst(ClaimTypes.NameIdentifier).Value;
            else
                return false;

            bool result = _mailRepository.Delete(mailId, userID);
            if (result)
                return false;

            return true;
        }

        public bool EditEmail(MailsType mail)
        {
            string userID = "";
            if (_httpContextAccessor != null && _httpContextAccessor.HttpContext != null && _httpContextAccessor.HttpContext.User != null &&
                _httpContextAccessor.HttpContext.User.FindFirst(ClaimTypes.NameIdentifier) != null && !String.IsNullOrEmpty(_httpContextAccessor.HttpContext.User.FindFirst(ClaimTypes.NameIdentifier).Value))
                userID = _httpContextAccessor.HttpContext.User.FindFirst(ClaimTypes.NameIdentifier).Value;
            else
                return false;

            var emails = _mailRepository.GetAll(mail.GroupId, userID);
            if (emails.Any(x => x.Email == mail.Email))
                return false;
            else
                _mailRepository.UpdateEmail(mail, userID);

            return true;
        }
        
        public bool AddEmail(MailsType mail)
        {
            string userID = "";
            if (_httpContextAccessor != null && _httpContextAccessor.HttpContext != null && _httpContextAccessor.HttpContext.User != null &&
                _httpContextAccessor.HttpContext.User.FindFirst(ClaimTypes.NameIdentifier) != null && !String.IsNullOrEmpty(_httpContextAccessor.HttpContext.User.FindFirst(ClaimTypes.NameIdentifier).Value))
                userID = _httpContextAccessor.HttpContext.User.FindFirst(ClaimTypes.NameIdentifier).Value;
            else
                return false;

            var emails = _mailRepository.GetAll(mail.GroupId, userID);
            if (emails.Any(x => x.Email == mail.Email))
                return false;
            else
                _mailRepository.Create(mail, userID);

            return true;
        }

        public MailsType GetEmailById(int mailId)
        {
            string userID = "";
            if (_httpContextAccessor != null && _httpContextAccessor.HttpContext != null && _httpContextAccessor.HttpContext.User != null &&
                _httpContextAccessor.HttpContext.User.FindFirst(ClaimTypes.NameIdentifier) != null && !String.IsNullOrEmpty(_httpContextAccessor.HttpContext.User.FindFirst(ClaimTypes.NameIdentifier).Value))
                userID = _httpContextAccessor.HttpContext.User.FindFirst(ClaimTypes.NameIdentifier).Value;
            else
                return null;

            var mail = _mailRepository.GetEmailById(mailId, userID);
            return mail;
        }

        public int GetGroupIdByEmailId(int mailId)
        {
            string userID = "";
            if (_httpContextAccessor != null && _httpContextAccessor.HttpContext != null && _httpContextAccessor.HttpContext.User != null &&
                _httpContextAccessor.HttpContext.User.FindFirst(ClaimTypes.NameIdentifier) != null && !String.IsNullOrEmpty(_httpContextAccessor.HttpContext.User.FindFirst(ClaimTypes.NameIdentifier).Value))
                userID = _httpContextAccessor.HttpContext.User.FindFirst(ClaimTypes.NameIdentifier).Value;
            else
                return -1;

            var mail = _mailRepository.GetGroupIdByEmailId(mailId, userID);
            return mail;
        }
    }
}
