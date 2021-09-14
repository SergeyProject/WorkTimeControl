using System.Data.Entity;
using WorkTimeControl.DATA.Models;

namespace WorkTimeControl.DATA
{
   public class DataContext: DbContext
    {
        public DataContext() : base("MyBase") { }
        public DbSet<User> Users { get; set; }
        public DbSet<UserTime> UserTimes { get; set; }
    }
}
