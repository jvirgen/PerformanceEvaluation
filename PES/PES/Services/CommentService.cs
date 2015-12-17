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
        private PESDBContext dbContext = new PESDBContext();


        public bool InsertComment(Comment comment)
        {
            bool status = false ;
            try
            {
                using (OracleConnection db = dbContext.GetDBConnection()) 
                {
                    db.Open();
                    string commillas = "\"commias\" ";
                    string InsertComment = "INSERT INTO "+ "\""+"COMMENT"+"\" "+" (ID_PE," +
                                                                 "TRAINNING_EMPLOYEE," +
                                                                 "TRAINNING_EVALUATOR," +
                                                                 "ACKNOWLEDGE_EVALUATOR," +
                                                                 "\"comm/recomm_employee\"," +
                                                                 " \"comm/recomm_evaluator\" )" +
                                            "VALUES (" + comment.PEId + ", '" +
                                                       comment.TrainningEmployee + "', '" +
                                                       comment.TrainningEvaluator + "', '" +
                                                       comment.AcknowledgeEvaluator+"', '"+
                                                       comment.CommRecommEmployee + "', '" +
                                                       comment.CommRecommEvaluator + "')";
                    OracleCommand Command = new OracleCommand(InsertComment, db);
                    Command.ExecuteNonQuery();

                    status = true;
                    db.Close();
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