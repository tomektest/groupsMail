using MailingGroups.Types;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MailingGroups.Data
{
    public interface IMailData
    {
        Task<List<MailsType>> GetAll(int groupId, string userID);
        Task<bool> Delete(int mailId);

        Task Create(MailsType mail);

        Task<int> GetGroupEmail(int mailId);

        Task<MailsType> GetEmailById(int mailId);

        Task UpdateEmail(MailsType mail);
    }
}