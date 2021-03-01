using MailingGroups.Types;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MailingGroups.Data
{
    public class GroupData : IGroupData
    {
        private readonly ApplicationDbContext _db;

        public GroupData(ApplicationDbContext db)
        {
            _db = db;
        }

        public async Task<List<GroupType>> GetAll(string UserId)
        {
            List<GroupType> _data = new List<GroupType>();

            try
            {
                _data = await _db.Groups.Where(x => x.UserId == UserId).ToListAsync();
                for (int i = 0; i < _data.Count; i++)
                {
                    _data[i].Lp = i + 1;
                }

            }
            catch (Exception ex)
            {
                throw new NotImplementedException("Not implemented.");
            }

            return _data;
        }

        public async Task<bool> Delete(int groupId, string UserId)
        {
            try
            {
                var groupsFromDb = await _db.Groups.Where(x => x.UserId == UserId).FirstOrDefaultAsync(u => u.Id == groupId);
                if (groupsFromDb == null)
                {
                    return false;
                }

                _db.Groups.Remove(groupsFromDb);
                await _db.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                throw new NotImplementedException("Not implemented.");
            }

            return true;
        }

        public async Task UpdateGroup(GroupType group)
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

        public async Task<bool> ValidGroupName(GroupType group)
        {
            try
            {
                int count = _db.Groups.Where(x => x.Name.ToLower() == group.Name.ToLower()).Count();
                if (count > 0)
                    return false;
            }
            catch (Exception ex)
            {
                throw new NotImplementedException("Not implemented.");
            }
            return true;
        }

        public async Task AddGroup(GroupType group)
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

        public async Task<GroupType> GetGroupById(int groupId, string userID)
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

        public async Task<bool> VerifyGroupName(string name, string userID)
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
