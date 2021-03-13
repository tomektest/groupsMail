using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Core.Entities;

namespace Core.Interfaces
{
    public interface IGroupsService
    {
        IEnumerable<GroupType> GetAllGroups();
        bool DeleteGroups(int groupId);
        bool AddGroup(GroupType group);
        bool EditGroup(GroupType group);
        GroupType GetGroupById(int groupId);
    }
}
