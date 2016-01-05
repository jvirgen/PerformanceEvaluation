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
                Title title = new Title { TitleId = 0, Name = "Performance" };

                return title;
            }
        } 

        public static Subtitle Subtitle1
        {
            get 
            {   
                Subtitle subtitle1 = new Subtitle {SubtitleId = 0, Name = "Quality of the Developed Products: The products meet all the requirements, specifications and standards that the client requires?", TitleId = 0};
                return subtitle1;
            }
        }

        public static Subtitle Subtitle2
        {
            get
            {
                Subtitle subtitle2 = new Subtitle { SubtitleId = 1, Name = "Quality of the Developed Products: The products meet all the requirements, specifications and standards that the client requires?", TitleId = 0 };
                return subtitle2;
            }
        }

        public static List<Description> Subtitle1Descriptions 
        {
            get 
            {
                List<Description> listDescriptions = new List<Description>
                {
                    new Description{DescriptionId = 0, DescriptionText = "1. Accuracy or Precision", SubtitleId = 0},
                    new Description{DescriptionId = 1, DescriptionText = "2. Thoroughness (Content) and Neatness (Presentation)", SubtitleId = 0},
                    new Description{DescriptionId = 2, DescriptionText = "3. Reliability", SubtitleId = 0},
                    new Description{DescriptionId = 3, DescriptionText = "4. Responsiveness to requests for service", SubtitleId = 0},
                    new Description{DescriptionId = 4, DescriptionText = "5. Follow-through/Follow-up", SubtitleId = 0},
                    new Description{DescriptionId = 5, DescriptionText = "6. Judgment/Decision making", SubtitleId = 0},
                    new Description{DescriptionId = 6, DescriptionText = "Subtotal", SubtitleId = 0}
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
                    new Description{DescriptionId = 7, DescriptionText = "7.Priority Setting", SubtitleId = 1},
                    new Description{DescriptionId = 8, DescriptionText = "8.Amount of work completed", SubtitleId = 1},
                    new Description{DescriptionId = 9, DescriptionText = "9.Work completed on schedule", SubtitleId = 1},
                    new Description{DescriptionId = 10, DescriptionText = "Subtotal", SubtitleId = 1},
                    new Description{DescriptionId = 11, DescriptionText = "Total  Performance", SubtitleId = 1}
                };

                return listDescriptions;
            }
        }
    }
}