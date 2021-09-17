using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WorkTimeControl.DATA.Models
{
    [Table("UserTimes")]
    public class UserTime
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public int UserId { get; set; }
        public DateTime DateTimes { get; set; }
        public string Descript { get; set; }
        public byte[] Photo { get; set; }
    }
}
