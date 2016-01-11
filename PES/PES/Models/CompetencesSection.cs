using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PES.Models
{
    public static class CompetencesSection
    {
        public static Title Title
        {
            get 
            {
                Title title = new Title { TitleId = 2, Name = "Peformance" };
                return title;
            }
        }
        
        public static Subtitle Subtitle1
        {
            get
            {
                Subtitle subtitle1 = new Subtitle { SubtitleId = 3, TitleId = 2, Name = "Skills and Knowledge" };
                return subtitle1;
            }
        }

        public static Subtitle Subtitle2
        {
            get
            {
                Subtitle subtitle2 = new Subtitle { SubtitleId = 4, TitleId = 2, Name = " INTERPERSONAL SKILLS: Effectiveness of the team member's interaction with others and as a team participant" };
                return subtitle2;
            }
        }

        public static Subtitle Subtitle3
        {
            get
            {
                Subtitle subtitle3 = new Subtitle { SubtitleId = 5, TitleId = 2, Name = "Growth and development: Learns new concepts and techniques, investigates and explores new work processes and/or new tools." };
                return subtitle3;
            }
        }

        public static Subtitle Subtitle4
        {
            get
            {
                Subtitle subtitle4 = new Subtitle { SubtitleId = 6, TitleId = 2, Name = "Policies Compliance: (non-disclosure, dress code )" };
                return subtitle4;
            }
        }

        public static List<Description> Subtitle1Descriptions
        {
            get
            {
                List<Description> listDescription = new List<Description>
                {
                    new Description{DescriptionId= 13, SubtitleId = 3, DescriptionText = "10. Job Knowledge" },
                    new Description{DescriptionId= 14, SubtitleId = 3, DescriptionText = "11. Analyzes Problems" },
                    new Description{DescriptionId= 15, SubtitleId = 3, DescriptionText = "12. Flexible / Adaptable" },
                    new Description{DescriptionId= 16, SubtitleId = 3, DescriptionText = "13. Planning and Organization" },
                    new Description{DescriptionId= 17, SubtitleId = 3, DescriptionText = "14. Competent/proper usage of work tools" },
                    new Description{DescriptionId= 18, SubtitleId = 3, DescriptionText = "15. Follows proper procedures, standards and requirements" },
                    new Description{DescriptionId= 19, SubtitleId = 3, DescriptionText = "Subtotal" }
                };

                return listDescription;
            }
        }
        
        public static List<Description> Subtitle2Descriptions
        {
            get
            {
                List<Description> listDescription = new List<Description>
                {
                    new Description{DescriptionId= 20, SubtitleId = 4, DescriptionText = "16. With Supervisors" },
                    new Description{DescriptionId= 21, SubtitleId = 4, DescriptionText = "17. With other team members / across teams" },
                    new Description{DescriptionId= 22, SubtitleId = 4, DescriptionText = "18. With client(s)" },
                    new Description{DescriptionId= 23, SubtitleId = 4, DescriptionText = "19. Commitment to Team Success" },
                    new Description{DescriptionId= 24, SubtitleId = 4, DescriptionText = "Subtotal" },
                };

                return listDescription;
            }
        }

        public static List<Description> Subtitle3Descriptions
        {
            get
            {
                List<Description> listDescription = new List<Description>
                {
                    new Description{DescriptionId= 25, SubtitleId = 5, DescriptionText = "20. Actively seeks ways to streamline processes" },
                    new Description{DescriptionId= 26, SubtitleId = 5, DescriptionText = "21. Open to new ideas and approaches" },
                    new Description{DescriptionId= 27, SubtitleId = 5, DescriptionText = "22. Involvement/commitment in activities for work/company improvement" },
                    new Description{DescriptionId= 28, SubtitleId = 5, DescriptionText = "23. Challenges Status Quo processes in appropriate ways" },
                    new Description{DescriptionId= 29, SubtitleId = 5, DescriptionText = "24. Seeks additional training and development" },
                    new Description{DescriptionId= 30, SubtitleId = 5, DescriptionText = "Subtotal" }
                };

                return listDescription;
            }
        }

        public static List<Description> Subtitle4Descriptions
        {
            get
            {
                List<Description> listDescription = new List<Description>
                {
                    new Description{DescriptionId= 31, SubtitleId = 6, DescriptionText = "Punctuality: Fulfillment of the company's established schedules for attendance, meetings, etc (within the company and with clients). " },
                    new Description{DescriptionId= 32, SubtitleId = 6, DescriptionText = "Policies Compliance: (non-disclosure, dress code )" },
                    new Description{DescriptionId= 33, SubtitleId = 6, DescriptionText = "Values: Acts according to the company values " },
                    new Description{DescriptionId= 34, SubtitleId = 6, DescriptionText = "Subtotal" },
                    new Description{DescriptionId= 35, SubtitleId = 6, DescriptionText = "Total  Competences" }
                };

                return listDescription;
            }
        }
    }
}