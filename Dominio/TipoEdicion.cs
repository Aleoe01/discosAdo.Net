﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace dominio
{
    public class TipoEdicion
    {
        public int id { get; set; }
        public string formato { get; set; }

        public override string ToString()
        {
            return formato;
        }
    }
}
