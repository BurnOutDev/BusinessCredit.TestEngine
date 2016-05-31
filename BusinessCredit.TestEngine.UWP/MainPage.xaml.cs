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
            this.InitializeComponent();

            List<Question> questions = new List<Question>();

            var q1 = new Question() { Name = "IQ ტესტი", ImageUrl = @"http://www.quickiqtest.net/questions/firstq.gif", ContentType = QuestionContentType.Picture, QuestionID = 1 };
            q1.Answers.Add(new Answer() { AnswerID = 1, ContentType = AnswerContentType.Picture, IsCorrect = true, ImageUrl = @"http://www.quickiqtest.net/answers/firstqa.gif" });
            q1.Answers.Add(new Answer() { AnswerID = 2, ContentType = AnswerContentType.Picture, IsCorrect = false, ImageUrl = @"http://www.quickiqtest.net/answers/firstqb.gif" });
            q1.Answers.Add(new Answer() { AnswerID = 3, ContentType = AnswerContentType.Picture, IsCorrect = false, ImageUrl = @"http://www.quickiqtest.net/answers/firstqc.gif" });
            q1.Answers.Add(new Answer() { AnswerID = 4, ContentType = AnswerContentType.Picture, IsCorrect = false, ImageUrl = @"http://www.quickiqtest.net/answers/firstqd.gif" });

            questions.Add(q1);

            foreach (var q in questions)
            {
                var pivot = new PivotItem() { Name = "pivotItemQ" + q.QuestionID, Header = q.Name };
                var qc = new QuestionControl();
                var v1 = ((Viewbox)((Grid)(qc.Content)).Children.ToList().First());
                var v2 = ((Viewbox)((Grid)(qc.Content)).Children.ToList()[1]);
                var v3 = ((Viewbox)((Grid)(qc.Content)).Children.ToList().Last());

                ((TextBlock)v1.Child).Text = q.Name;

                v2.Child = new Image() { Source = new BitmapImage(new Uri(q.ImageUrl)) };

                foreach (var answer in q.Answers)
                {
                    var btn = new Button() { Content = new Image() { Source = new BitmapImage(new Uri(answer.ImageUrl)) } };
                    //btn.Click += (sender, e) => new ContentDialog() { Content = sender };
                    ((StackPanel)((RelativePanel)v3.Child).Children[0]).Children.Add(btn);
                }

                pivot.Content = qc;
                //Add Item
                pivotQuestions.Items.Add(pivot);
            }

            var item = (Viewbox)((Grid)((QuestionControl)(pivotItemQ1.Content)).Content).Children.ToList().Last();

            var button = new Button() { Margin = new Thickness(10), Content = "Axali" };
            button.Click += Button_Click;
            item.Child = button;

            
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            var d = new MessageDialog("Pressed! " + ((Button)sender).Content);
            d.ShowAsync();
        }
    }
}
