using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MME.Model.Shared
{
    [Table("tblReligions")]
    public class ReligionModel
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ReligionId { get; set; }
        public string Religion { get; set; }
        public int DisplayOrder { get; set; }
        public bool IsActive { get; set; }
    }
}
