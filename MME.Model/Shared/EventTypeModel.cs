using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MME.Model.Shared
{
    [Table("tblEventTypes")]
    public class EventTypeModel
    {
        [Key]
        public int EventTypeId { get; set; }
        public string EventType { get; set; }
        public int DisplayOrder { get; set; }
        public bool IsActive { get; set; }
    }
}
