using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Organization_Model.Models
{
    public class User
    {
        public User()
        {
            Activities = new HashSet<Activity>();
        }
        public int UserID { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string Role { get; set; }
        public UserDetail UserDetail { get; set; }
        public ICollection<Activity> Activities { get; set; }
    }
}
