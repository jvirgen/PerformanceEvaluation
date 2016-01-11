using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PES.Models
{
    public static class PerformanceSection
    {
        public static Title Title
        {
            get 
            {
                Title title = new Title { TitleId = 1, Name = "Performance" };

                return title;
            }
        } 

        public static Subtitle Subtitle1
        {
            get 
            {   
                Subtitle subtitle1 = new Subtitle {SubtitleId = 1, Name = "Quality of the Developed Products: The products meet all the requirements, specifications and standards that the client requires?", TitleId = 1};
                return subtitle1;
            }
        }

        public static Subtitle Subtitle2
        {
            get
            {
                Subtitle subtitle2 = new Subtitle { SubtitleId = 2, Name = "Quality of the Developed Products: The products meet all the requirements, specifications and standards that the client requires?", TitleId = 1 };
                return subtitle2;
            }
        }

        public static List<Description> Subtitle1Descriptions 
        {
            get 
            {
                List<Description> listDescriptions = new List<Description>
                {
                    new Description{DescriptionId = 1, DescriptionText = "1. Accuracy or Precision", SubtitleId = 1},
                    new Description{DescriptionId = 2, DescriptionText = "2. Thoroughness (Content) and Neatness (Presentation)", SubtitleId = 1},
                    new Description{DescriptionId = 3, DescriptionText = "3. Reliability", SubtitleId = 1},
                    new Description{DescriptionId = 4, DescriptionText = "4. Responsiveness to requests for service", SubtitleId = 1},
                    new Description{DescriptionId = 5, DescriptionText = "5. Follow-through/Follow-up", SubtitleId = 1},
                    new Description{DescriptionId = 6, DescriptionText = "6. Judgment/Decision making", SubtitleId = 1},
                    new Description{DescriptionId = 7, DescriptionText = "Subtotal", SubtitleId = 1}
                };

                return listDescriptions;
            }

            
        }

        public static List<Description> Subtitle2Descriptions
        {
            get
            {
                List<Description> listDescriptions = new List<Description>
                {
                    new Description{DescriptionId = 8, DescriptionText = "7.Priority Setting", SubtitleId = 2},
                    new Description{DescriptionId = 9, DescriptionText = "8.Amount of work completed", SubtitleId = 2},
                    new Description{DescriptionId = 10, DescriptionText = "9.Work completed on schedule", SubtitleId = 2},
                    new Description{DescriptionId = 11, DescriptionText = "Subtotal", SubtitleId = 2},
                    new Description{DescriptionId = 12, DescriptionText = "Total  Performance", SubtitleId = 2}
                };

                return listDescriptions;
            }
        }
    }
}