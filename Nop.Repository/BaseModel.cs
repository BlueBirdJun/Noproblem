using System;

namespace Nop.Repository
{
    public class BaseModel
    {
        public int idx { get; set; }
        public DateTime UpdateDate { get; set; }
        public DateTime CreateDate { get; set; }
    }
}
