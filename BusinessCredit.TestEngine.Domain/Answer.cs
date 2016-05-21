using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessCredit.TestEngine.Domain
{
    class Answer
    {
        public int AnswerID { get; set; }
        public object Content { get; set; }
        public AnswerContentType ContentType { get; set; }


        public enum AnswerContentType
        {
            Text,
            Picture,
            Audio,
            Video,
            Html
        }
    }
}
