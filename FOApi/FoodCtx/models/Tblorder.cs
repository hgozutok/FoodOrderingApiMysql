﻿// <auto-generated> This file has been auto generated by EF Core Power Tools. </auto-generated>
#nullable disable
using System;
using System.Collections.Generic;

namespace FOApi
{
    public partial class Tblorder
    {
        public Tblorder()
        {
            Tblorderdetails = new HashSet<Tblorderdetail>();
        }

        public int OrderId { get; set; }
        public int CustomerId { get; set; }
        public DateTime OrderDate { get; set; }
        public double TotalAmount { get; set; }
        public int OrderStatus { get; set; }
        public int ProcessedBy { get; set; }

        public virtual Tblcustomer Customer { get; set; }
        public virtual ICollection<Tblorderdetail> Tblorderdetails { get; set; }
    }
}