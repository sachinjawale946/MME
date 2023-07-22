using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MME.Model.Shared
{
    [Table("tblOccupations")]
    public class Occupation
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int OccupationId { get; set; }
        public string Occupation { get; set; }
        public int DisplayOrder { get; set; }
        public bool IsActive { get; set; }
    }
}
