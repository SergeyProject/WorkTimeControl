﻿using System.Collections.Generic;
using System.Linq;
using WorkTimeControl.DATA.Models;
using WorkTimeControl.DATA.Repositories.Abstract;

namespace WorkTimeControl.DATA.Repositories
{
    public class UserRepository : IUserRepository
    {
        public int Create(User user)
        {
            using(DataContext db=new DataContext())
            {
                db.Users.Add(user);
                db.SaveChanges();
                return user.Id;
            }
        }

        public bool Delete(int Id)
        {
           using(DataContext db=new DataContext())
            {
                User user = db.Users.Find(Id);
                if (user != null)
                {
                    db.Users.Remove(user);
                    db.SaveChanges();
                    return true;
                }
                else
                    return false;
            }
        }

        public IEnumerable<User> GetAllUsers()
        {
            using(DataContext db=new DataContext())
            {
                return db.Users.ToList();
            }
        }
    }
}