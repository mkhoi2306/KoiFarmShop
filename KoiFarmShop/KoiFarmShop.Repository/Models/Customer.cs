﻿// <auto-generated> This file has been auto generated by EF Core Power Tools. </auto-generated>
#nullable disable
using System;
using System.Collections.Generic;

namespace KoiFarmShop.Repository.Models;

public partial class Customer
{
    public long CustomerId { get; set; }

    public long? UserId { get; set; }

    public string Email { get; set; }

    public string Phone { get; set; }

    public DateTime? CreatedDate { get; set; }

    public string CreatedBy { get; set; }

    public DateTime? UpdatedDate { get; set; }

    public string UpdatedBy { get; set; }

    public bool? IsDeleted { get; set; }

    public virtual ICollection<KoiOrder> KoiOrders { get; set; } = new List<KoiOrder>();

    public virtual User User { get; set; }
}