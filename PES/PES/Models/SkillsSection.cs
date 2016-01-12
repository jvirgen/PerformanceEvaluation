using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PES.Models
{
    public static class SkillsSection
    {
        public static List<Skill> SkillDescriptions
        {
            get
            {
                List<Skill> skillList = new List<Skill>
                {
                    new Skill{SkillId = 1, Description = "Supervises personnel"},
                    new Skill{SkillId = 2, Description = "Coordinates activities with the client or is the main contact"},
                    new Skill{SkillId = 3, Description = "Defines the tech approach and/ or project plan"},
                    new Skill{SkillId = 4, Description = "Supports and observes company policies"},
                    new Skill{SkillId = 5, Description = "Keeps control and follows up for the plan"},
                    new Skill{SkillId = 6, Description = "Generates business opportunities"},
                    new Skill{SkillId = 7, Description = "Trains and develops team members"},
                    new Skill{SkillId = 8, Description = "Supports experimentation and brainstorming that leads to innovation and learning"},
                    new Skill{SkillId = 9, Description = "Evaluates team regularly"},
                    new Skill{SkillId = 10, Description = "Faces performance problems in an honest, straightforward manner"},
                    new Skill{SkillId = 11, Description = "Supports responsible risk taking"},
                    new Skill{SkillId = 12, Description = "Helps control costs and maximize resources"},
                    new Skill{SkillId = 13, Description = "Instills pride, service, innovation and quality"},
                    new Skill{SkillId = 14, Description = "Sets high standards for self, as well as others"},
                    new Skill{SkillId = 15, Description = "Supports useful debate and disagreement"},
                    new Skill{SkillId = 16, Description = "Welcomes constructive criticism"},
                    new Skill{SkillId = 17, Description = "Sets specific goals for simplicity, productivity and process improvements"}
                };

                return skillList;
            }
        }

    }
}