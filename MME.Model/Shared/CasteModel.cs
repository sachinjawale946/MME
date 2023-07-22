using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MME.Model.Shared
{
    [Table("tblCastes")]
    public class CasteModel
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int CasteId { get; set; }
        public string Caste { get; set; }
        public int DisplayOrder { get; set; }
        public bool IsActive { get; set; }
    }
}
