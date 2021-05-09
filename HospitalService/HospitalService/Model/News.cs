using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model
{
    public class News
    {
        public String Title { get; set; }
        public DateTime PublishingDate { get; set; }
        public String Content { get; set; }
        public Role Roles { get; set; }
        public Patient specificPatient { get; set; }


        public String FullNews { get { return Title + "\n" + Roles; } }
    }
}
