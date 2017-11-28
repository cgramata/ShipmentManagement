using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ShipBobShipments.Models;
using System.Data.Entity;

namespace ShipBobShipments.DAL
{
    public class ShipmentDBContext : DbContext
    {
        public ShipmentDBContext()
        {

        }

        public DbSet<User> Users { get; set; }
        public DbSet<Orders> Orders { get; set; }
    }
}