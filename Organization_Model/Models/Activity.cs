using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Organization_Model.Models
{
    public class Activity
    {
        public Activity()
        {
            ActivityUsers = new HashSet<ActivityUser>();
        }
        public int ActivityID { get; set; }
        public int CategoryID { get; set; }
        public string Name { get; set; }
        public string Detail { get; set; }
        public DateTime Date { get; set; }
        public DateTime ActivityDeadline { get; set; }
        public string City { get; set; }
        public string Address { get; set; }
        public int Quota { get; set; }
        public string Status { get; set; }
        public ICollection<ActivityUser> ActivityUsers { get; set; }
        public Category Category { get; set; }
    }
}
