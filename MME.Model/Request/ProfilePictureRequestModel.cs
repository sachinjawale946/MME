using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MME.Model.Request
{
    public class ProfilePictureRequestModel
    {
        public Guid userid { get; set; }
        public byte[]? picture { get; set; }
        public string? pictureextenstion { get; set; }
    }
}
