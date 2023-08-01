using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MME.Model.Shared
{
    [Table("tblLanguages")]
    public class LanguageModel
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int LanguageId { get; set; }
        public string Language { get; set; }
        public string LanguageCode { get; set; }
        public int DisplayOrder { get; set; }
        public bool IsActive { get; set; }
    }
}
