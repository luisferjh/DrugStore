﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DrugStore.Web.Models.Store.Losses
{
    public class UpdateViewModel
    {
        public int IdLosses { get; set; }       
        public string Cause { get; set; }
        public string State { get; set; }      
    }
}
