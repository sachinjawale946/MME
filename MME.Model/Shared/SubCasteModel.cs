using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MME.Model.Shared
{
    [Table("tblSubCastes")]
    public class SubCasteModel
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int SubCasteId { get; set; }
        public string SubCaste { get; set; }
        public int DisplayOrder { get; set; }
        public bool IsActive { get; set; }
    }
}
