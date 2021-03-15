using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Core.Entities;


namespace Core.Interfaces
{
    public interface IMailsService
    {
        IEnumerable<MailsType> GetAllEmails(int groupId);
        bool DeleteEmail(int mailId);
        bool EditEmail(int id, string mail, int groupID);
        bool AddEmail(string mail, int groupID);
        MailsType GetEmailById(int mailId);
        int GetGroupIdByEmailId(int mailId);
    }
}
