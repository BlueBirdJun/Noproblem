using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Nop.Domain
{
    public class BaseModel
    {
        public int idx { get; set; }
        [JsonIgnore]
        public DateTime UpdateDate { get; set; }
        [JsonIgnore]
        public DateTime CreateDate { get; set; }
    }
}
