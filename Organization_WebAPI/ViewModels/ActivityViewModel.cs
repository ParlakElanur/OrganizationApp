namespace Organization_WebAPI.ViewModels
{
    public class ActivityViewModel
    {
        public int ActivityID { get; set; }
        public string Name { get; set; }
        public string Detail { get; set; }
        public string CategoryName { get; set; }
        public DateTime Date { get; set; }
        public DateTime ActivityDeadline { get; set; }
        public string City { get; set; }
        public string Address { get; set; }
        public int Quota { get; set; }
        public string? Status { get; set; }
    }
}
