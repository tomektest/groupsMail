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
        bool EditEmail(MailsType mail);
        bool AddEmail(MailsType mail);
        MailsType GetEmailById(int mailId);
        int GetGroupIdByEmailId(int mailId);
    }
}
