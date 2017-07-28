using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.Storage;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;
using Newtonsoft.Json;
using System.Threading.Tasks;
using System.Security.Cryptography;
using System.Text;
using System.Collections;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=234238

namespace SongQuiz
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class PlayPage : Page
    {
        StorageFile gameFile;
        public Quiz gameMaterial;
        public int count = 1;
        public int interval;
        public int TotalScore = 0;
        public bool IsRunning = true;
        public PlayPage()
        {
            this.InitializeComponent();
            gameMaterial = new Quiz();
        }
        

        protected async override void OnNavigatedTo(NavigationEventArgs e)
        {
            gameFile = (StorageFile)e.Parameter;

            using (var zipMemoryStream = await gameFile.OpenStreamForWriteAsync())
            using (var zipArchive = new ZipArchive(zipMemoryStream, ZipArchiveMode.Read))
            {
                var folder = await ApplicationData.Current.LocalFolder.TryGetItemAsync("quiz");
                if(folder!=null)
                    await folder.DeleteAsync();
                zipArchive.ExtractToDirectory(ApplicationData.Current.LocalFolder.Path + "\\quiz");
                var file = await ApplicationData.Current.LocalFolder.GetFileAsync("quiz\\quiz.json");
                var content = await FileIO.ReadTextAsync(file);
                gameMaterial = JsonConvert.DeserializeObject<Quiz>(content);
                
            }
            PlayRound(count);
        }

        private async void PlayRound(int index)
        {
            
            var file = await ApplicationData.Current.LocalFolder.GetFileAsync($"quiz\\Song{count}.mp3");
            mediaElement.Source = new Uri(file.Path);
            await Task.Delay(1000);
            mediaElement.Play();
            IsRunning = true;
            interval = gameMaterial.Songs[index - 1].Interval;
            Timer.Text = interval.ToString();
            AnswerA.Content = gameMaterial.Songs[index - 1].Answers[0];
            AnswerB.Content = gameMaterial.Songs[index - 1].Answers[1];
            AnswerC.Content = gameMaterial.Songs[index - 1].Answers[2];
            AnswerD.Content = gameMaterial.Songs[index - 1].Answers[3];
            for (int i = interval; i >= 0 && IsRunning == true; i--)
            {
                interval = i;
                Timer.Text = interval.ToString();
                await Task.Delay(1000);
            }
            if(interval == 0)
                CalculateScore("0");
        }

        private async void CalculateScore(string answer)
        {
            IsRunning = false;
            MD5 md = MD5.Create();
            var result = FindControl<TextBlock>(this, typeof(TextBlock), $"Result{count}");
            if (answer == "0")
            {
                result.Foreground = new SolidColorBrush(Windows.UI.Colors.White);
                result.Text = "0";
            }
            else if (((IStructuralEquatable)md.ComputeHash(Encoding.ASCII.GetBytes(answer))).Equals(gameMaterial.Songs[count - 1].RightAnswer, StructuralComparisons.StructuralEqualityComparer))
            {
                result.Foreground = new SolidColorBrush(Windows.UI.Colors.LightGreen);
                result.Text = interval.ToString();
                TotalScore = TotalScore +  interval;
                mediaElement.Stop();
            }
            else
            {
                result.Foreground = new SolidColorBrush(Windows.UI.Colors.Red);
                result.Text = "X";
                mediaElement.Stop();
            }
            count++;
            if(count > 10)
            {
                Timer.Text = TotalScore.ToString();
                Timer.Foreground = new SolidColorBrush(Windows.UI.Colors.DeepPink);
                AnswerA.Visibility = Visibility.Collapsed;
                AnswerB.Visibility = Visibility.Collapsed;
                AnswerC.Visibility = Visibility.Collapsed;
                AnswerD.Visibility = Visibility.Collapsed;
            }
            else
                PlayRound(count);
            await Task.Delay(1000);
        }
        public void ClickA()
        {
            CalculateScore(gameMaterial.Songs[count - 1].Answers[0]);
        }
        public void ClickB()
        {
            CalculateScore(gameMaterial.Songs[count - 1].Answers[1]);
        }
        public void ClickC()
        {
            CalculateScore(gameMaterial.Songs[count - 1].Answers[2]);
        }
        public void ClickD()
        {
            CalculateScore(gameMaterial.Songs[count - 1].Answers[3]);
        }
        public void CancelClick()
        {
            mediaElement.Stop();
            Frame.Navigate(typeof(MainPage));
        }

        public static T FindControl<T>(UIElement parent, Type targetType, string ControlName) where T : FrameworkElement
        {

            if (parent == null) return null;

            if (parent.GetType() == targetType && ((T)parent).Name == ControlName)
            {
                return (T)parent;
            }
            T result = null;
            int count = VisualTreeHelper.GetChildrenCount(parent);
            for (int i = 0; i < count; i++)
            {
                UIElement child = (UIElement)VisualTreeHelper.GetChild(parent, i);

                if (FindControl<T>(child, targetType, ControlName) != null)
                {
                    result = FindControl<T>(child, targetType, ControlName);
                    break;
                }
            }
            return result;
        }
    }
}
