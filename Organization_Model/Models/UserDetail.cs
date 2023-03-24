using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Organization_Model.Models
{
    public class UserDetail
    {
        public int UserID { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public User User { get; set; }
    }
}
