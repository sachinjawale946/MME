using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MME.Model.Shared
{
    [Table("tblStates")]
    public class StateModel
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int StateId { get; set; }
        public string State { get; set; }
        public int DisplayOrder { get; set; }
        public bool IsActive { get; set; }
    }
}
