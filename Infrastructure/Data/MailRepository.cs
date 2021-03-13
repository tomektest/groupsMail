using Core.Entities;
using Core.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Infrastructure.Data
{
    public class MailRepository : EntityFrameworkRepository<MailsType>, IMailRepository
    {
        private readonly ApplicationDbContext _db;

        public MailRepository(ApplicationDbContext db) : base(db)
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

        public bool Delete(int mailId, string userID)
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

        public void Create(MailsType mail, string userID)
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

        public int GetGroupIdByEmailId(int mailId, string userID)
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

        public void UpdateEmail(MailsType mail, string userID)
        {
            try
            {
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
