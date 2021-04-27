using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Nop.Repository
{
    public class BaseModel
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int idx { get; set; }
        public DateTime UpdateDate { get; set; }
        
        public DateTime CreateDate { get; set; }
    }
}
