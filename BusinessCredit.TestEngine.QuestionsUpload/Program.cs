using BusinessCredit.TestEngine.QuestionsUpload;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Serialization;

namespace BusinessCredit.TestEngine.QuestionsUpload
{
    static class Program
    {
        static void Main(string[] args)
        {
            var path = @"C:\Users\Irakl\Desktop\TESTS\Math Test Print.xml";
            var obj = new XmlSerializer(typeof(objs)).Deserialize(new FileStream(path, FileMode.Open));
        }

        static public string Beautify(this XmlDocument doc)
        {
            StringBuilder sb = new StringBuilder();
            XmlWriterSettings settings = new XmlWriterSettings
            {
                Indent = true,
                IndentChars = "  ",
                NewLineChars = "\r\n",
                NewLineHandling = NewLineHandling.Replace
            };
            using (XmlWriter writer = XmlWriter.Create(sb, settings))
            {
                doc.Save(writer);
            }
            return sb.ToString();
        }
    }

    public class objs
    {
        public BindingList<QuestionObject> Objects { get; set; }
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
