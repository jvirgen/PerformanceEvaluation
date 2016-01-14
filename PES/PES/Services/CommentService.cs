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
            List<Comment> Comments;
            Comment Comment;
            try
            {
                using (OracleConnection db = dbContext.GetDBConnection())
                {
                    db.Open();
                    string selectComments = @"SELECT ID_COMMENT, 
                                                    ID_PE, 
                                                    TRAINNING_EMPLOYEE, 
                                                    TRAINNING_EVALUATOR, 
                                                    ACKNOWLEDGE_EVALUATOR, 
                                                    ""comm/recomm_employee"",
                                                    ""comm/recomm_evaluator"" 
                                                    FROM ""COMMENT"" WHERE ID_PE = :peId";
                    using (OracleCommand command = new OracleCommand(selectComments, db))
                    {
                        command.Parameters.Add(new OracleParameter("peId", peId));
                        OracleDataReader reader = command.ExecuteReader();
                        Comments = new List<Comment>();
                        while (reader.Read())
                    {
                        Comment = new Comment();
                            Comment.CommentId = Convert.ToInt32(reader["ID_COMMENT"]);
                            Comment.PEId = Convert.ToInt32(reader["ID_PE"]);
                            Comment.TrainningEmployee = Convert.ToString(reader["TRAINNING_EMPLOYEE"]);
                            Comment.TrainningEvaluator = Convert.ToString(reader["TRAINNING_EVALUATOR"]);
                            Comment.AcknowledgeEvaluator = Convert.ToString(reader["ACKNOWLEDGE_EVALUATOR"]);
                            Comment.CommRecommEmployee = Convert.ToString(reader["comm/recomm_employee"]);
                            Comment.CommRecommEvaluator = Convert.ToString(reader["comm/recomm_evaluator"]);
                        Comments.Add(Comment);
                    }

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
                    string InsertComment = "INSERT INTO "+ "\""+"COMMENT"+"\" "+" (ID_PE, " +
                                                                 "TRAINNING_EMPLOYEE, " +
                                                                 "TRAINNING_EVALUATOR, " +
                                                                 "ACKNOWLEDGE_EVALUATOR, " +
                                                                 "\"comm/recomm_employee\", " +
                                                                 " \"comm/recomm_evaluator\" ) " +
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