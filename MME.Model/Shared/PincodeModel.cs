using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MME.Model.Shared
{
    [Table("tblPinCodes")]
    public class PincodeModel
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int PinCodeId { get; set; }
        public string PinCode { get; set; }
        public int State { get; set; }
        public int DisplayOrder { get; set; }
        public bool IsActive { get; set; }
    }
}
