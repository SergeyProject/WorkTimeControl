using System;

namespace WorkTimeControl.BLL.Models
{
  public  class UserTimeDTO
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public DateTime DateTimes { get; set; }
        public string Descript { get; set; }
        public byte[] Photo { get; set; }
    }
}
