using Core.Entities;
using Core.Interfaces;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace Infrastructure.Data
{
    public class GroupRepository : IGroupRepository
    {
        private readonly ApplicationDbContext _db;

        public GroupRepository(ApplicationDbContext db)
        {
            _db = db;
        }

        public IEnumerable<GroupType> GroupAll(string userID)
        {
            List<GroupType> _data = new List<GroupType>();

            try
            {
                _data = _db.Groups.Where(x => x.UserId == userID).ToList();
                for (int i = 0; i < _data.Count; i++)
                    _data[i].Lp = i + 1;
            }
            catch (Exception ex)
            {
                throw new NotImplementedException("Not implemented.");
            }

            return _data;
        }

        public bool Delete(int groupId, string userID)
        {
            try
            {
                var groupsFromDb = _db.Groups.Where(x => x.UserId == userID).FirstOrDefault(u => u.Id == groupId);
                if (groupsFromDb == null)
                    return false;

                _db.Groups.Remove(groupsFromDb);
                _db.SaveChanges();
            }
            catch (Exception ex)
            {
                throw new NotImplementedException("Not implemented.");
            }

            return true;
        }

        public void UpdateGroup(GroupType group, string userID)
        {
            try
            {
                _db.Groups.Update(group);
                _db.SaveChanges();
            }
            catch (Exception ex)
            {
                throw new NotImplementedException("Not implemented.");
            }
        }

        public bool ValidGroupName(GroupType group, string userID)
        {
            try
            {
                int count = _db.Groups.Where(x => x.Name.ToLower() == group.Name.ToLower() && x.UserId == userID).Count();
                if (count > 0)
                    return false;
            }
            catch (Exception ex)
            {
                throw new NotImplementedException("Not implemented.");
            }
            return true;
        }

        public void AddGroup(GroupType group, string userID)
        {
            try
            {
                _db.Groups.Add(group);
                _db.SaveChanges();
            }
            catch (Exception ex)
            {
                throw new NotImplementedException("Not implemented.");
            }
        }

        public GroupType GetGroupById(int groupId, string userID)
        {
            GroupType _group = new GroupType();
            try
            {
                _group = _db.Groups.Where(x => x.UserId == userID).FirstOrDefault(u => u.Id == groupId);
            }
            catch (Exception ex)
            {
                throw new NotImplementedException("Not implemented.");
            }

            return _group;
        }

        public bool VerifyGroupName(string name, string userID)
        {
            GroupType _group = new GroupType();
            try
            {
                if (_db.Groups.Where(x => x.UserId == userID).FirstOrDefault(x => x.Name.ToLower() == name.ToLower()) != null)
                    return true;
            }
            catch (Exception ex)
            {
                throw new NotImplementedException("Not implemented.");
            }

            return false;
        }
    }
}
