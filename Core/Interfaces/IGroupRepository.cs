using Core.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Core.Interfaces
{
    public interface IGroupRepository
    {
        IEnumerable<GroupType> GroupAll(string userID);
        bool Delete(int groupId, string UserId);

        void UpdateGroup(int id, string groupName, string userID);

        bool ValidGroupName(GroupType group, string userID);

        void AddGroup(string groupName, string userID);

        GroupType GetGroupById(int groupId, string userID);

        bool VerifyGroupName(string name, string userID);
    }
}