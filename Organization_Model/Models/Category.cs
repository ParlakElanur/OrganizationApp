using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Organization_Model.Models
{
    public class Category
    {
        public Category()
        {
            Activities = new HashSet<Activity>();
        }
        public int CategoryID { get; set; }
        public string CategoryName { get; set; }
        public ICollection<Activity> Activities { get; set; }
    }
}
