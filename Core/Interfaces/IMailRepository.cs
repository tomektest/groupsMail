using Core.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Core.Interfaces
{
    public interface IMailRepository
    {
        List<MailsType> GetAll(int groupId, string userID);

        bool Delete(int mailId, string userID);

        void Create(MailsType mail, string userID);

        int GetGroupIdByEmailId(int mailId, string userID);

        MailsType GetEmailById(int mailId, string userID);

        void UpdateEmail(MailsType mail, string userID);
    }
}