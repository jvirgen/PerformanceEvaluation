using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using PES.Models;
using PES.DBContext;
using Oracle.ManagedDataAccess.Client;
using OfficeOpenXml;
using System.IO;

namespace PES.Services
{
    public class ScoreService
    {
        private PESDBContext dbContext;
        public ScoreService()
        {
            dbContext = new PESDBContext();
        }
        public bool InsertScore (Score score)
        {
            bool status= false;
            
                try
                {
                    using (OracleConnection db = dbContext.GetDBConnection())
                    {

                        db.Open();
                        string Insertquery = "INSERT INTO SCORE (ID_DESCRIPTION, " +
                                                                 "ID_PE, " +
                                                                 "SCORE_EMPLOYEE, " +
                                                                 "SCORE_EVAULATOR, " +
                                                                 "COMMENTS, " +
                                                                 "CALCULATION) " +
                                            "VALUES (" + score.DescriptionId + ", " +
                                                      score.PEId + ", " +
                                                      score.ScoreEmployee + ", " +
                                                      score.ScoreEvaluator + ", " +
                                                      score.Comments + ", " +
                                                      score.Calculation + ")";
                        OracleCommand Command = new OracleCommand(Insertquery, db);
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

        public List<Score> GetPEScoresbyPEID(int peID)
        {
            List<Score> Scores = new List<Score>();
            Score Score = new Score();

            try
            {
                using (OracleConnection db = dbContext.GetDBConnection())
                {
                    db.Open();
                    string SelectScores = "SELECT ID_SCORE, " +
                                                  "ID_DESCRIPTION, " +
                                                  "ID_PE, " +
                                                  "SCORE_EMPLOYEE, " +
                                                  "SCORE_EVALUATOR, " +
                                                  "COMMENTS, " +
                                                  "CALCULATION " +
                                                  "FROM SCORE WHERE ID_PE = " + peID;
                    OracleCommand Command = new OracleCommand(SelectScores, db);
                    Command.ExecuteReader();
                    OracleDataReader Reader = Command.ExecuteReader();
                    while (Reader.Read())
                    {
                        Score = new Score();
                        Score.ScoreId = Convert.ToInt32(Reader["ID_SCORE"]);
                        Score.DescriptionId = Convert.ToInt32(Reader["ID_DESCRIPTION"]);
                        Score.PEId = Convert.ToInt32(Reader["ID_PE"]);
                        Score.ScoreEmployee = Convert.ToInt32(Reader["SCORE_EMPLOYEE"]);
                        Score.ScoreEvaluator = Convert.ToInt32(Reader["SCORE_EVALUATOR"]);
                        Score.Comments = Convert.ToString(Reader["COMMENTS"]);
                        Score.Calculation = Convert.ToInt32(Reader["CALCULATION"]);
                        Scores.Add(Score);
                    }
                    db.Close();
                }
            }
            catch
            {
                Scores = null;
            }
            return Scores;
        }

        public Score GetPEScorebyPEIdDescId(int peID, int DescID)
        {
            Score score = new Score();

            try
            {
                using (OracleConnection db = dbContext.GetDBConnection())
                {
                    db.Open();
                    string SelectScores = "SELECT ID_SCORE, " +
                                                   "ID_DESCRIPTION, " +
                                                   "ID_PE, " +
                                                   "SCORE_EMPLOYEE, " +
                                                   "SCORE_EVALUATOR, " +
                                                   "COMMENTS, " +
                                                   "CALCULATION " +
                                                   "FROM SCORE WHERE ID_PE = " + peID + "AND ID_DESCRIPTION = " + DescID;
                    OracleCommand Command = new OracleCommand(SelectScores, db);
                    Command.ExecuteReader();
                    OracleDataReader Reader = Command.ExecuteReader();
                    while (Reader.Read())
                    {
                        score = new Score();
                        score.ScoreId = Convert.ToInt32(Reader["ID_SCORE"]);
                        score.DescriptionId = Convert.ToInt32(Reader["ID_DESCRIPTION"]);
                        score.PEId = Convert.ToInt32(Reader["ID_PE"]);
                        score.ScoreEmployee = Convert.ToInt32(Reader["SCORE_EMPLOYEE"]);
                        score.ScoreEvaluator = Convert.ToInt32(Reader["SCORE_EVALUATOR"]);
                        score.Comments = Convert.ToString(Reader["COMMENTS"]);
                        score.Calculation = Convert.ToInt32(Reader["CALCULATION"]);
                    }
                    db.Close();
                }
            }
            catch
            {
                score = null;
            }
            return score;
        }
    }
}