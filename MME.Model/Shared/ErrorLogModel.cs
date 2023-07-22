using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MME.Model.Shared
{
    [Table("tblErrorLogs")]
    public class ErrorLogModel
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Key]
        public int Id { get; set; }
        public string Message { get; set; }
        public string ErrorDetails { get; set; }
        public string Controller { get; set; }
        public string Action { get; set; }
        public string Query { get; set; }
        public string Username { get; set; }
        public DateTime CreatedOn { get; set; }
    }
}
