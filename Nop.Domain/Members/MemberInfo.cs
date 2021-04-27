using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nop.Domain.Members
{
    public class MemberInfo :BaseModel
    {
        public string UID { get; set; }
        public string Name { get; set; }

        public string BirthDay { get; set; }

        public string Address1 { get; set; }
        public string Address2 { get; set; }

        public string Memo { get; set; }
        public string Password { get; set; }

    }
}
