using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nop.Repository.NopModels
{
    public class CommonCodes : BaseModel
    {
        [MaxLength(10)]
        [Required]        
        public string Code { get; set; }
        [MaxLength(50)]
        [Required]
        public string Group { get; set; }
        [MaxLength(50)]
        public string Name { get; set; }
        [MaxLength(200)]
        public string Description { get; set; }
        [Required]
        [MaxLength(50)]
        public string UserId { get; set; }
    }
}
