﻿using System;
using System.Collections.Generic;

namespace AlamedasAPI.Models
{
    public partial class ProductoGasto
    {
        public int IdEntity { get; set; }
        public string Concepto { get; set; } = null!;
        public double Valor { get; set; }
    }
}