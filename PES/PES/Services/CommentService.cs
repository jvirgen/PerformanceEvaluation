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


        public bool InsertComment(Comment comment)
        {
            bool status = false ;
            try
            {
                using (OracleConnection db = dbContext.GetDBConnection()) 
                {
                    string InsertComment = "INSERT INTO COMMENT (ID_PE," +
                                                                 "TRAINING_EMPLOYEE," +
                                                                 "TRAINING_EVALUATOR," +
                                                                 "AKNOWLEDGE_EVALUATOR," +
                                                                 "comm/recomm_employee," +
                                                                 "comm/recomm_evaluator)" +
                                            "VALUES (" + comment.PEId + "," +
                                                       comment.TrainningEmployee + "," +
                                                       comment.TrainningEvaluator + "," +
                                                       comment.CommRecommEmployee + "," +
                                                       comment.CommRecommEvaluator + ")";
                    OracleCommand Command = new OracleCommand(InsertComment, db);
                    Command.ExecuteNonQuery();

                    status = true;
                }
                    
            }
            catch
            {
                status = false;
            }
            return status;
        }
    }
}