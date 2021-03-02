using MailingGroups.Types;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MailingGroups.Data
{
    public class MailData : IMailData
    {
        private readonly ApplicationDbContext _db;

        public MailData(ApplicationDbContext db)
        {
            _db = db;
        }

        public List<MailsType> GetAll(int groupId, string userID)
        {
            List<MailsType> _data = new List<MailsType>();
            try
            {
                _data = _db.Mails.Where(x => x.GroupId == groupId).ToList();
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

        public bool Delete(int mailId)
        {
            try
            {
                var mailsFromDb = _db.Mails.FirstOrDefault(u => u.Id == mailId);
                if (mailsFromDb == null)
                    return false;

                _db.Mails.Remove(mailsFromDb);
                _db.SaveChanges();
            }
            catch (Exception ex)
            {
                throw new NotImplementedException("Not implemented.");
            }
            return true;
        }

        public void Create(MailsType mail)
        {
            try
            {
                _db.Mails.Add(mail);
                _db.SaveChanges();
            }
            catch (Exception ex)
            {
                throw new NotImplementedException("Not implemented.");
            }
        }

        public int GetGroupEmail(int mailId, string userID)
        {
            int groupId = -1;
            try
            {
                groupId = _db.Mails.Where(x => x.Id == mailId).Select(x => x.GroupId).FirstOrDefault();
            }
            catch (Exception ex)
            {
                throw new NotImplementedException("Not implemented.");
            }
            return groupId;
        }

        public MailsType GetEmailById(int mailId, string userID)
        {
            MailsType mail = new MailsType();
            try
            {
                mail = _db.Mails.FirstOrDefault(u => u.Id == mailId);
            }
            catch (Exception ex)
            {
                throw new NotImplementedException("Not implemented.");
            }
            return mail;
        }

        public void UpdateEmail(MailsType mail)
        {
            try
            {
               // _db.ChangeTracker.AutoDetectChangesEnabled = false;
               // _db.Entry<MailsType>(mail).State = EntityState.Detached;
                //The instance of entity type 'MailsType' cannot be tracked because another instance with the same key value for {'Id'} is already being tracked. When attaching existing entities, ensure that only one entity instance with a given key value is attached. Consider using 'DbContextOptionsBuilder.EnableSensitiveDataLogging' to see the conflicting key values.
               // _db.Mails.AsNoTracking<MailsType>();
                _db.Mails.Update(mail);
                _db.SaveChanges();
            }
            catch (Exception ex)
            {
                throw new NotImplementedException("Not implemented.");
            }
        }
    }
}
