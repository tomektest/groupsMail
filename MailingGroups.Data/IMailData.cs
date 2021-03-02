using MailingGroups.Types;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MailingGroups.Data
{
    public interface IMailData
    {
        List<MailsType> GetAll(int groupId, string userID);
        bool Delete(int mailId);

        void Create(MailsType mail);

        int GetGroupEmail(int mailId, string userID);

        MailsType GetEmailById(int mailId, string userID);

        void UpdateEmail(MailsType mail);
    }
}