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

        public static Response SaveChanges(ECommerceContext db)
        {
            try
            {
                db.SaveChanges();
                return new Response { Succeeded = true, };
            }
            catch (Exception ex)
            {
                var response = new Response { Succeeded = false, };
                if (ex.InnerException != null &&
                    ex.InnerException.InnerException != null &&
                    ex.InnerException.InnerException.Message.Contains("_Index"))
                {
                    response.Message = "There is a record with the same value";
                }
                else if (ex.InnerException != null &&
                    ex.InnerException.InnerException != null &&
                    ex.InnerException.InnerException.Message.Contains("REFERENCE"))
                {
                    response.Message = "The record can't be delete because it has related records";
                }
                else
                {
                    response.Message = ex.Message;
                }

                return response;
            }
        }

    }
}