using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nop.Domain.Members
{
    public class LoginModel
    {
        public string UserId { get; set; }
        public string PassWord { get; set; }

        public string UserIP { get; set; }
        public DateTime ReqeustTime { get; set; }
    }
}
