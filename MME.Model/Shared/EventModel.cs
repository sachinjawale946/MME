using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MME.Model.Shared
{
    [Table("tblEvents")]
    public class EventModel
    {
        [Key]
        public Guid EventId { get; set; }
        public string Event { get; set; }
        public string Description { get; set; }
        public string? Banner { get; set; }
        public string? Location { get; set; }
        [ForeignKey("EventType")]
        public int EventTypeId { get; set; }
        public DateTime EventDate { get; set; }
        public DateTime ActivationDate { get; set; }
        public DateTime CreatedDate { get; set;}
        public Guid CreatedBy { get; set;}
        public DateTime? LastUpdatedDate { get; set; }
        public Guid? LastUpdatedBy { get; set; }
        public bool IsActive { get; set; }
        public virtual EventTypeModel? EventType { get; set; }
    }
}
