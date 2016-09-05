using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessCredit.TestEngine.UWP.Models
{
    public class objs
    {
        public List<QuestionObject> Objects { get; set; }
    }

    public class QuestionObject
    {
        public string Question { get; set; }

        public string ImageUrl { get; set; }

        public string Answer1 { get; set; }

        public string Answer2 { get; set; }

        public string Answer3 { get; set; }

        public string Answer4 { get; set; }

        public int CorrectAnswerIndex { get; set; }
    }
}
