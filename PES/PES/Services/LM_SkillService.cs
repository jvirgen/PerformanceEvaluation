using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using PES.Models;
using PES.DBContext;
using Oracle.ManagedDataAccess.Client;

namespace PES.Services
{
    public class LM_SkillService
    {
        private PESDBContext dbContext;

        public bool InsertLM_Skill(LM_Skill lm_skill)
        {
            bool status = false;

            try
            {
                using(OracleConnection db = dbContext.GetDBConnection())
                {
                    string InsertQuery = "INSTER INTO LM_SKILL (ID_SKILL," +
                                                                "ID_PE," +
                                                                "CHECK_EMPLOYEE," +
                                                                "CHECK_EVALUATOR)" +
                                         "VALUES (" + lm_skill.SkillId + "," +
                                                     lm_skill.PEId + "," +
                                                     lm_skill.CheckEmployee + "," +
                                                     lm_skill.CheckEvaluator + ")";
                    OracleCommand Command = new OracleCommand(InsertQuery, db);
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