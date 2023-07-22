using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MME.Model.Shared
{
    [Table("tblRoles")]
    public  class RoleModel
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int RoleId { get;set; }
        public string Role { get;set; }
        public string Description { get;set; }
        public int DisplayOrder { get;set; }
        public bool IsActive { get; set; }
    }
}
