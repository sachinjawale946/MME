using Microsoft.AspNetCore.Http;
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
        [Display(Name = "Title / Header")]
        [Required]
        [StringLength(200)]
        public string Event { get; set; }
        [Display(Name = "Description")]
        [Required]
        [StringLength(5000)]
        public string Description { get; set; }
        public string? Banner { get; set; }
        [Display(Name = "Location")]
        [StringLength(200)]
        public string? Location { get; set; }
        [Display(Name = "Event Type")]
        [Required]
        [ForeignKey("EventType")]
        public int EventTypeId { get; set; }
        [Display(Name = "Event Date")]
        [Required]
        public DateTime EventDate { get; set; }
        [Display(Name = "Activate On")]
        [Required]
        public DateTime ActivationDate { get; set; }
        public DateTime CreatedDate { get; set; }
        public Guid CreatedBy { get; set; }
        public DateTime? LastUpdatedDate { get; set; }
        public Guid? LastUpdatedBy { get; set; }
        public bool IsActive { get; set; }
        public virtual EventTypeModel? EventType { get; set; }

        [NotMapped]
        public List<EventTypeModel>? EventTypes { get; set; }
        [NotMapped]
        public IFormFile? BannerImage { get; set; }
        [NotMapped]
        public string? BannerUrl { get; set; }
    }
}
