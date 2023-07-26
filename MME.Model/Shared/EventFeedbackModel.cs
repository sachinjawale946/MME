using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MME.Model.Shared
{
    [Table("tblEventFeedbacks")]
    public class EventFeedbackModel
    {
        [Key]
        public Guid Id { get; set; }
        public bool? Liked { get; set; }
        public bool? DisLiked { get; set; }
        public bool? ReportAbuse { get; set; }
        public bool? Participation { get;}
        public decimal? Donation { get; set; }
        public string? Suggestion { get; set; }
        public string? Feedback { get; set; }
        [ForeignKey("Event")]
        public Guid EventId { get; set;}
        [ForeignKey("User")]
        public Guid UserId { get; set; }
        public DateTime Created { get; set; }

        public virtual EventModel? Event { get; set; }
        public virtual UserModel? User { get; set; }
    }
}
