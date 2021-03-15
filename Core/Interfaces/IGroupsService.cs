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
        bool AddGroup(string groupName);
        bool EditGroup(int id, string groupName);
        GroupType GetGroupById(int groupId);
    }
}
