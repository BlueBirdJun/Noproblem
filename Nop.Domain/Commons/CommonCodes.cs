﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nop.Domain.Commons
{
    public class CommonCodes : BaseModel
    {
        public string Code { get; set; }
        public string Group { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
    }
}
