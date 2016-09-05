using BusinessCredit.TestEngine.UWP.Controls;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Popups;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;
using BusinessCredit.TestEngine.Domain;
using Windows.UI.Xaml.Media.Imaging;
using Windows.UI;
using System.Xml.Serialization;
using BusinessCredit.TestEngine.UWP.Models;
using Windows.Storage;
using System.Threading.Tasks;

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x409

namespace BusinessCredit.TestEngine.UWP
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainPage : Page
    {
        public MainPage()
        {
            InitializeComponent();

            var task = Task.Run(GetFile<objs>);
            task.Wait();
            var tests = task.Result;

            List<Question> questions = GetQuestionCollection(tests);

            //var q1 = new Question() { Name = "IQ", ImageUrl = @"http://www.quickiqtest.net/questions/firstq.gif", ContentType = QuestionContentType.Picture, QuestionID = 1 };
            //q1.Answers.Add(new Answer() { AnswerID = 1, ContentType = AnswerContentType.Picture, IsCorrect = true, ImageUrl = @"http://www.quickiqtest.net/answers/firstqa.gif" });
            //q1.Answers.Add(new Answer() { AnswerID = 2, ContentType = AnswerContentType.Picture, IsCorrect = false, ImageUrl = @"http://www.quickiqtest.net/answers/firstqb.gif" });
            //q1.Answers.Add(new Answer() { AnswerID = 3, ContentType = AnswerContentType.Picture, IsCorrect = false, ImageUrl = @"http://www.quickiqtest.net/answers/firstqc.gif" });
            //q1.Answers.Add(new Answer() { AnswerID = 4, ContentType = AnswerContentType.Picture, IsCorrect = false, ImageUrl = @"http://www.quickiqtest.net/answers/firstqd.gif" });

            ////////////////////

            //var q2 = new Question() { Name = "Math", ImageUrl = @"https://i.ytimg.com/vi/tvjJppmx9Hk/hqdefault.jpg", ContentType = QuestionContentType.Picture, QuestionID = 1 };
            //q2.Answers.Add(new Answer() { AnswerID = 1, ContentType = AnswerContentType.Text, IsCorrect = true, TextContent = "10" });
            //q2.Answers.Add(new Answer() { AnswerID = 2, ContentType = AnswerContentType.Text, IsCorrect = false, TextContent = "14" });
            //q2.Answers.Add(new Answer() { AnswerID = 3, ContentType = AnswerContentType.Text, IsCorrect = false, TextContent = "18" });
            //q2.Answers.Add(new Answer() { AnswerID = 4, ContentType = AnswerContentType.Text, IsCorrect = false, TextContent = "8" });

            ////////////////////

            //var q3 = new Question() { Name = "Companies", TextContent = "Business __________", ContentType = QuestionContentType.Text, QuestionID = 1 };
            //q3.Answers.Add(new Answer() { AnswerID = 1, ContentType = AnswerContentType.Text, IsCorrect = true, TextContent = "Carry" });
            //q3.Answers.Add(new Answer() { AnswerID = 2, ContentType = AnswerContentType.Text, IsCorrect = false, TextContent = "Case" });
            //q3.Answers.Add(new Answer() { AnswerID = 3, ContentType = AnswerContentType.Text, IsCorrect = false, TextContent = "Credit" });
            //q3.Answers.Add(new Answer() { AnswerID = 4, ContentType = AnswerContentType.Text, IsCorrect = false, TextContent = "Rabbit" });

            ////////////////////

            //questions.Add(q1);
            //questions.Add(q2);
            //questions.Add(q3);
            //questions.Add(q1);
            //questions.Add(q2);
            //questions.Add(q3);

            AddQuestions(questions);

            //var item = (Viewbox)((Grid)((QuestionControl)(pivotItemQ1.Content)).Content).Children.ToList().Last();

            //var button = new Button() { Margin = new Thickness(10), Content = "Axali" };
            //button.Click += Button_Click;
            //item.Child = button;


        }

        private List<Question> GetQuestionCollection(objs tests)
        {
            var i = 0;
            var list = new List<Question>();

            foreach (var item in tests.Objects)
            {
                i++;
                var q = new Question();
                q.Name = i.ToString();
                q.ContentType = string.IsNullOrWhiteSpace(item.ImageUrl) ? QuestionContentType.Text : QuestionContentType.Picture;

                switch (q.ContentType)
                {
                    case QuestionContentType.Text:
                        q.TextContent = item.Question;
                        break;
                    case QuestionContentType.Picture:
                        q.ImageUrl = item.ImageUrl;
                        break;
                    case QuestionContentType.Audio:
                        break;
                    case QuestionContentType.Video:
                        break;
                    case QuestionContentType.Html:
                        break;
                    default:
                        break;
                }

                q.Answers.Add(new Answer()
                {
                    ContentType = AnswerContentType.Text,
                    TextContent = item.Answer1,
                    IsCorrect = item.CorrectAnswerIndex == 1 ? true : false,
                    AnswerID = 1
                });

                q.Answers.Add(new Answer()
                {
                    ContentType = AnswerContentType.Text,
                    TextContent = item.Answer2,
                    IsCorrect = item.CorrectAnswerIndex == 2 ? true : false,
                    AnswerID = 2
                });

                q.Answers.Add(new Answer()
                {
                    ContentType = AnswerContentType.Text,
                    TextContent = item.Answer3,
                    IsCorrect = item.CorrectAnswerIndex == 3 ? true : false,
                    AnswerID = 3
                });

                q.Answers.Add(new Answer()
                {
                    ContentType = AnswerContentType.Text,
                    TextContent = item.Answer4,
                    IsCorrect = item.CorrectAnswerIndex == 4 ? true : false,
                    AnswerID = 4
                });

                list.Add(q);
            }

            return list;
        }

        public async Task<T> GetFile<T>()
        {
            StorageFolder InstallationFolder = Windows.ApplicationModel.Package.Current.InstalledLocation;
            StorageFolder AssetsFolder = await InstallationFolder.GetFolderAsync("Assets");
            StorageFolder TestsFolder = await AssetsFolder.GetFolderAsync("XmlTests");
            StorageFile file = await TestsFolder.GetFileAsync("Math.xml");

            return (T)new XmlSerializer(typeof(objs)).Deserialize(file.OpenStreamForReadAsync().Result);
        }

        private void AddQuestions(List<Question> questions)
        {
            foreach (var q in questions)
            {
                var pivot = new PivotItem() { Name = "pivotItemQ" + q.QuestionID, Header = q.Name };
                var qc = new QuestionControl();
                var v1 = ((Viewbox)((Grid)(qc.Content)).Children.ToList().First());
                var v2 = ((Viewbox)((Grid)(qc.Content)).Children.ToList()[1]);
                var v3 = ((Viewbox)((Grid)(qc.Content)).Children.ToList().Last());

                

                ((TextBlock)v1.Child).Text = q.Name;

                switch (q.ContentType)
                {
                    case QuestionContentType.Text:
                        v2.Child = new TextBlock() { Width = v2.Width*0.9, TextWrapping = TextWrapping.Wrap, Text = q.TextContent};
                        break;
                    case QuestionContentType.Picture:
                        v2.Child = new Image() { Source = new BitmapImage(new Uri(q.ImageUrl)) };
                        break;
                    case QuestionContentType.Audio:
                        break;
                    case QuestionContentType.Video:
                        break;
                    case QuestionContentType.Html:
                        break;
                    default:
                        break;
                }

                foreach (var answer in q.Answers)
                {
                    UIElement el = null;
                    switch (answer.ContentType)
                    {
                        case AnswerContentType.Text:
                            el = new Button() { Content = new TextBlock() { Width = v3.Width * 0.4, TextWrapping = TextWrapping.Wrap, Text = answer.TextContent }, Background = new SolidColorBrush(Color.FromArgb(255, 255, 255, 255)) };
                            break;
                        case AnswerContentType.Picture:
                            el = new Button() { Content = new Image() { Source = new BitmapImage(new Uri(answer.ImageUrl)) }, Background = new SolidColorBrush(Color.FromArgb(255, 255, 255, 255)) };
                            break;
                        case AnswerContentType.Audio:
                            break;
                        case AnswerContentType.Video:
                            break;
                        case AnswerContentType.Html:
                            break;
                        default:
                            break;
                    }

                    //btn.Click += (sender, e) => new ContentDialog() { Content = sender };
                    ((StackPanel)((RelativePanel)v3.Child).Children[0]).Children.Add(el);
                }

                pivot.Content = qc;
                //Add Item
                pivotQuestions.Items.Add(pivot);

                v2.SizeChanged += V2_SizeChanged;
            }
        }

        private void V2_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            var t = ((Viewbox)sender).Child;

            if (t is TextBlock)
            {
                ((TextBlock)t).Width = e.NewSize.Width;
                ((TextBlock)t).FontSize = 22;
                ((TextBlock)t).TextWrapping = TextWrapping.Wrap;
            }

            

            var v3 = ((Viewbox)((Grid)((((Viewbox)sender).Parent))).Children.ToList().Last());
            
            foreach (var item in ((StackPanel)((RelativePanel)v3.Child).Children[0]).Children)
            {
                ((TextBlock)((Button)item).Content).FontSize = 22;
                ((TextBlock)((Button)item).Content).TextWrapping = TextWrapping.Wrap;
            }
        }

        private async void Button_Click(object sender, RoutedEventArgs e)
        {
            var d = new MessageDialog("Pressed! " + ((Button)sender).Content);
            await d.ShowAsync();
        }
    }
}
