using MailingGroups.Types;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MailingGroups.Data
{
    public interface IGroupData
    {
        List<GroupType> GetAll(string UserId);
        bool Delete(int groupId, string UserId);

        void UpdateGroup(GroupType group);

        bool ValidGroupName(GroupType group);

        void AddGroup(GroupType group);

        GroupType GetGroupById(int groupId, string userID);

        bool VerifyGroupName(string name, string userID);
    }
}