﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MME.Model.Request
{
    public class FCMRequestModel
    {
        public Guid userid { get; set; }
        public string token { get; set; }
    }
}
