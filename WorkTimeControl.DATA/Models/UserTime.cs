using System;

namespace WorkTimeControl.DATA.Models
{
   public class UserTime
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public DateTime DateTime { get; set; }
        public string Descript { get; set; }
        public byte[] Image { get; set; }
    }
}
