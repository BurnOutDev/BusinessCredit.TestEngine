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
        public long MaxSeconds { get; set; } = 10;
        public long Ticks { get; set; }
        public DispatcherTimer Timer { get; set; }
        public objs AnsweredTest { get; set; }
        public objs ExportedQuestions { get; set; }

        public MainPage()
        {
            InitializeComponent();
            AnsweredTest = new objs();
            AnsweredTest.Objects = new List<QuestionObject>();
            #region Comments
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


            //var item = (Viewbox)((Grid)((QuestionControl)(pivotItemQ1.Content)).Content).Children.ToList().Last();

            //var button = new Button() { Margin = new Thickness(10), Content = "Axali" };
            //button.Click += Button_Click;
            //item.Child = button; 
            #endregion

            Timer = new DispatcherTimer() { Interval = new TimeSpan(TimeSpan.TicksPerSecond) };
            Timer.Tick += Timer_Tick;
        }

        private void Timer_Tick(object sender, object e)
        {
            if (MaxSeconds < Ticks)
                Finish();

            Ticks++;

            var t = TimeSpan.FromSeconds(Ticks);

            if (t.Hours == 0)
                Hour.Text = "00";
            else
                Hour.Text = t.Hours.ToString();

            if (t.Minutes == 0)
                Minute.Text = "00";
            else
                Minute.Text = t.Minutes.ToString();

            if (t.Seconds == 0)
                Seconds.Text = "00";
            else
                Seconds.Text = t.Seconds.ToString();
        }

        private async void Finish()
        {
            Timer.Stop();

            int max = 0;
            int score = 0;

            foreach (var qs in AnsweredTest.Objects)
            {
                max++;
                var eq = ExportedQuestions.Objects.FirstOrDefault(x => x.QID == qs.QID);
                if (eq.CorrectAnswerIndex == qs.CorrectAnswerIndex)
                    score++;
            }

            var m = new MessageDialog($"{score} out of {max} SHOOTING FIREWORKS !!!");
            await m.ShowAsync();
        }

        private List<Question> GetQuestionCollection(objs tests)
        {
            var i = 0;
            var list = new List<Question>();

            foreach (var item in tests.Objects)
            {
                #region AnsweredTest
                AnsweredTest.Objects.Add(new QuestionObject()
                {
                    QID = item.QID,
                    Answer1 = item.Answer1,
                    Answer2 = item.Answer2,
                    Answer3 = item.Answer3,
                    Answer4 = item.Answer4,
                    ImageUrl = item.ImageUrl,
                    Question = item.Question,
                    CorrectAnswerIndex = -1
                });
                #endregion

                i++;

                var q = new Question();
                q.QuestionID = item.QID;
                q.Name = i.ToString();
                q.ContentType = string.IsNullOrWhiteSpace(item.ImageUrl) ? QuestionContentType.Text : QuestionContentType.Picture;

                if (!string.IsNullOrWhiteSpace(item.ImageUrl) && !string.IsNullOrWhiteSpace(item.Question))
                    q.ContentType = QuestionContentType.Dual;

                //q.ContentType = QuestionContentType.Text;

                switch (q.ContentType)
                {
                    case QuestionContentType.Text:
                        q.TextContent = item.Question;
                        break;
                    case QuestionContentType.Picture:
                        q.ImageUrl = item.ImageUrl;
                        break;
                    case QuestionContentType.Dual:
                        q.TextContent = item.Question;
                        q.ImageUrl = item.ImageUrl;
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
                var v3 = ((Viewbox)((Grid)(qc.Content)).Children.ToList()[2]);



                ((TextBlock)v1.Child).Text = $"{q.Name}/{questions.Count}";

                switch (q.ContentType)
                {
                    case QuestionContentType.Text:
                        v2.Child = new TextBlock() { Width = v2.Width * 0.9, TextWrapping = TextWrapping.Wrap, Text = q.TextContent };
                        break;
                    case QuestionContentType.Picture:
                        v2.Child = new Image() { Source = new BitmapImage(new Uri(q.ImageUrl)) };
                        break;
                    case QuestionContentType.Dual:

                        Grid g = new Grid();

                        g.ColumnDefinitions.Add(new ColumnDefinition() { Width = new GridLength(5, GridUnitType.Star) });
                        g.ColumnDefinitions.Add(new ColumnDefinition() { Width = new GridLength(5, GridUnitType.Star) });

                        var txt = new TextBlock() { Width = v2.Width * 0.9, TextWrapping = TextWrapping.Wrap, Text = q.TextContent };
                        Grid.SetColumn(txt, 1);

                        var pic = new Image() { Source = new BitmapImage(new Uri(q.ImageUrl)) };
                        Grid.SetColumn(pic, 0);

                        g.Children.Add(pic);
                        g.Children.Add(txt);

                        v2.Child = g;
                        break;
                    default:
                        break;
                }

                var c = 'A';

                foreach (var answer in q.Answers)
                {
                    UIElement el = null;
                    switch (answer.ContentType)
                    {
                        case AnswerContentType.Text:
                            var cb = new CheckBox()
                            {
                                HorizontalAlignment = HorizontalAlignment.Left,
                                VerticalContentAlignment = VerticalAlignment.Top,
                                Tag = q.QuestionID
                            };

                            cb.Content = answer.TextContent;

                            cb.Checked += AnswerClicked;

                            el = cb;
                            break;
                        case AnswerContentType.Picture:
                            el = new Button() { Content = new Image() { Source = new BitmapImage(new Uri(answer.ImageUrl)) }, Background = new SolidColorBrush(Color.FromArgb(255, 255, 255, 255)) };
                            var b = (Button)el;
                            b.Click += AnswerClickedButton;
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
                    c++;
                    //btn.Click += (sender, e) => new ContentDialog() { Content = sender };
                    ((StackPanel)((RelativePanel)v3.Child).Children[0]).Children.Add(el);
                }

                foreach (var item in ((StackPanel)((RelativePanel)v3.Child).Children[0]).Children)
                {
                    //var max = ((StackPanel)((RelativePanel)v3.Child).Children[0]).Children.Where(x => x is StackPanel).Max(x => ((StackPanel)x).Width);
                    //if (item is StackPanel)
                    //{
                    //    ((StackPanel)item).Width = max;
                    //}
                }

                pivot.Content = qc;
                //Add Item
                pivotQuestions.Items.Add(pivot);

                v2.SizeChanged += V2_SizeChanged;
            }
        }

        private void AnswerClickedButton(object sender, RoutedEventArgs e)
        {
            throw new NotImplementedException();
        }

        private void AnswerClicked(object sender, RoutedEventArgs e)
        {
            //var m = new MessageDialog($"Clicked: Q = {((CheckBox)e.OriginalSource).Tag} A = {((CheckBox)e.OriginalSource).Content}");
            //await m.ShowAsync();

            var aobj = AnsweredTest.Objects.FirstOrDefault(x => x.QID == ((CheckBox)e.OriginalSource).Tag.ToString());

            if ((string)((CheckBox)e.OriginalSource).Content == aobj.Answer1)
                aobj.CorrectAnswerIndex = 1;
            else if ((string)((CheckBox)e.OriginalSource).Content == aobj.Answer2)
                aobj.CorrectAnswerIndex = 2;
            else if ((string)((CheckBox)e.OriginalSource).Content == aobj.Answer3)
                aobj.CorrectAnswerIndex = 3;
            else if ((string)((CheckBox)e.OriginalSource).Content == aobj.Answer4)
                aobj.CorrectAnswerIndex = 4;
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

            if (t is StackPanel)
            {
                var s = (StackPanel)t;

                var txt = (TextBlock)s.Children.Where(x => x is TextBlock).FirstOrDefault();

                txt.Width = e.NewSize.Width;
                txt.FontSize = 22;
                txt.TextWrapping = TextWrapping.Wrap;
            }

            var v3 = ((Viewbox)((Grid)((((Viewbox)sender).Parent))).Children.ToList()[2]);

            //foreach (var item in ((StackPanel)((RelativePanel)v3.Child).Children[0]).Children)
            //{
            //    ((TextBlock)((CheckBox)((StackPanel)item).Children[0]).Content).FontSize = 22;
            //    ((TextBlock)((CheckBox)((StackPanel)item).Children[0]).Content).TextWrapping = TextWrapping.Wrap;
            //}
        }

        private async void Button_Click(object sender, RoutedEventArgs e)
        {
            var d = new MessageDialog("Pressed! " + ((Button)sender).Content);
            await d.ShowAsync();
        }

        private void Page_Loaded(object sender, RoutedEventArgs e)
        {

            var task = Task.Run(GetFile<objs>);
            task.Wait();
            var tests = task.Result;

            ExportedQuestions = tests;
            List<Question> questions = GetQuestionCollection(tests);

            AddQuestions(questions);
        }

        private void LeftButton_Click(object sender, RoutedEventArgs e)
        {
            if (pivotQuestions.SelectedIndex > 0)
                pivotQuestions.SelectedIndex--;
        }

        private void RightButton_Click(object sender, RoutedEventArgs e)
        {
            if (pivotQuestions.SelectedIndex < pivotQuestions.Items.Count - 1)
                pivotQuestions.SelectedIndex++;
        }

        private void StartConversation_Click(object sender, RoutedEventArgs e)
        {
            Timer.Start();
        }

        //private void Button_Click_1(object sender, RoutedEventArgs e)
        //{
        //    var b = (Button)sender;

        //    b.Visibility = Visibility.Collapsed;

        //    Timer.Start();
        //}
    }
}
