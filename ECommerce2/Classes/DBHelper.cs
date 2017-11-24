using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ECommerce2.Models;

namespace ECommerce2.Classes
{
    public class DBHelper
    {
        public static int GetStatus(string description, ECommerceContext db)
        {
            var status = db.Status.Where(s =>s.Description == description ).FirstOrDefault();
            if (status == null)
            {
                status = new Status {

                    Description = description

                };

                db.Status.Add(status);
                db.SaveChanges();
            }
            return status.StatusId;
        }
    }
}