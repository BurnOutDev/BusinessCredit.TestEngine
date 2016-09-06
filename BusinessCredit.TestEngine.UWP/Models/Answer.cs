using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessCredit.TestEngine.Domain
{
    public class Answer
    {
        public int AnswerID { get; set; }

        public string Prefix { get; set; }

        public string TextContent { get; set; }
        public string ImageUrl { get; set; }
        public string AudioUrl { get; set; }
        public string VideoUrl { get; set; }
        public string HtmlContent { get; set; }

        public AnswerContentType ContentType { get; set; }

        public bool IsCorrect { get; set; }
    }

    public enum AnswerContentType
    {
        Text,
        Picture,
        Audio,
        Video,
        Html
    }
}
