namespace WorkTimeControl.BLL.Models
{
  public  class UserTimeReport
    {
        public int UserID { get; set; }
        public string UserName { get; set; }
        public string Date { get; set; }
        public string StartTime { get; set; }
        public string StopTime { get; set; }
        public byte[] StartPhoto { get; set; }
        public byte[] StopPhoto { get; set; }
    }
}
