using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using PES.DBContext;
using PES.Models;
using Oracle.ManagedDataAccess.Client;

namespace PES.Services
{
    public class CommentService
    {
        private PESDBContext dbContext;

        public Comment GetById(int id)
        {
            Comment comment = null;
            using (OracleConnection db = dbContext.GetDBConnection())
            {
                db.Open();


            }

            return comment;
        }
    }
}