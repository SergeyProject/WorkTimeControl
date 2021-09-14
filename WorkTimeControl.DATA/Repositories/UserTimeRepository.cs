using System.Collections;
using System.Linq;
using WorkTimeControl.DATA.Models;
using WorkTimeControl.DATA.Repositories.Abstract;

namespace WorkTimeControl.DATA.Repositories
{
    public class UserTimeRepository : IUserTimeRepository
    {
        public bool Delete(int userId)
        {
            using(DataContext db=new DataContext())
            {
                IEnumerable userTimes = db.UserTimes.Where(e => e.UserId == userId).ToList();
                foreach(UserTime user in userTimes)
                {
                    if (user != null)
                    {
                        db.UserTimes.Remove(user);
                        db.SaveChanges();
                    }
                }
                return true;
            }
        }

        public IEnumerable GetUserTimes(int id)
        {
            using (DataContext db = new DataContext())
            {
                return db.UserTimes.Where(e => e.UserId == id).ToList();
            }
        }

        public int StartTimeCreate(UserTime user)
        {
            using (DataContext db = new DataContext())
            {
                db.UserTimes.Add(user);
                db.SaveChanges();
                return user.Id;
            }
        }

        public int StopTimeCreate(UserTime user)
        {
            using (DataContext db = new DataContext())
            {
                db.UserTimes.Add(user);
                db.SaveChanges();
                return user.Id;
            }
        }
    }
}
