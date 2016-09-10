using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessCredit.TestEngine.Domain
{
    public class Question
    {
        public Question()
        {
            if (Answers == null)
                Answers = new List<Answer>();
        }

        public string QuestionID { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public QuestionContentType ContentType { get; set; }
        public string TextContent { get; set; }
        public string ImageUrl { get; set; }

        public ICollection<Answer> Answers { get; set; }
    }

    public enum QuestionContentType
    {
        Text,
        Picture,
        Dual
    }
}
