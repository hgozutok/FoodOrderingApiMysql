﻿// <auto-generated> This file has been auto generated by EF Core Power Tools. </auto-generated>
#nullable disable
using System;
using System.Collections.Generic;

namespace FOApi
{
    public partial class Tblorderdetail
    {
        public int OrderDetailsId { get; set; }
        public int OrderId { get; set; }
        public int MenuId { get; set; }
        public double Amount { get; set; }
        public int NoOfServing { get; set; }
        public double TotalAmount { get; set; }

        public virtual Tblorder Order { get; set; }
    }
}