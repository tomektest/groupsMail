using MailingGroups.Types;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MailingGroups.Data
{
    public interface IGroupData
    {
        Task<List<GroupType>> GetAll(string UserId);
        Task<bool> Delete(int groupId, string UserId);

        Task UpdateGroup(GroupType group);

        Task<bool> ValidGroupName(GroupType group);

        Task AddGroup(GroupType group);

        Task<GroupType> GetGroupById(int groupId, string userID);

        Task<bool> VerifyGroupName(string name, string userID);
    }
}