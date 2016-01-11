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

        // Get Comments of the PE to the DB by peID
        public List<Comment> GetCommentByPE(int peId) 
        {
            List<Comment> Comments = new List<Comment>();
            Comment Comment;
            try
            {
                using (OracleConnection db = dbContext.GetDBConnection())
                {
                    db.Open();
                    string SelectComments = "SELECT ID_COMMENT, " +
                                                    "ID_PE, " +
                                                    "TRAINNING_EMPLOYEE, " +
                                                    "TRAINNING_EVALUATOR, " +
                                                    "ACKNOWLEDGE_EVALUATOR, " +
                                                    "\"comm/recomm_employee\", " +
                                                    "\"comm/recomm_evaluator\" " +
                                                    "FROM \"COMMENT\" WHERE ID_PE = " + peId;
                    OracleCommand Command = new OracleCommand(SelectComments, db);
                    Command.ExecuteReader();
                    OracleDataReader Reader = Command.ExecuteReader();
                    while (Reader.Read())
                    {
                        Comment = new Comment();
                        Comment.CommentId = Convert.ToInt32(Reader["ID_COMMENT"]);
                        Comment.PEId = Convert.ToInt32(Reader["ID_PE"]);
                        Comment.TrainningEmployee = Convert.ToString(Reader["TRAINNING_EMPLOYEE"]);
                        Comment.TrainningEvaluator = Convert.ToString(Reader["TRAINNING_EVALUATOR"]);
                        Comment.AcknowledgeEvaluator = Convert.ToString(Reader["ACKNOWLEDGE_EVALUATOR"]);
                        Comment.CommRecommEmployee = Convert.ToString(Reader["comm/recomm_employee"]);
                        Comment.CommRecommEvaluator = Convert.ToString(Reader["comm/recomm_evaluator"]);
                        Comments.Add(Comment);
                    }
                    db.Close();
                }
            }
            catch
            {
                Comments = null;
            }
            return Comments;
        }

        public bool InsertComment(Comment comment)
        {
            bool status = false ;
            try
            {
                using (OracleConnection db = dbContext.GetDBConnection()) 
                {
                    db.Open();
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