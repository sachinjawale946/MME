﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace MME.Model.Response
{
     public class ModelBase : INotifyPropertyChanged
    {
       

        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged([CallerMemberName] string name = "") =>
   PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
    }
}
