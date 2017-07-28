using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Threading.Tasks;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.Media.Editing;
using Windows.Storage;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;
using Newtonsoft.Json;
using System.IO.Compression;
using Windows.Media.MediaProperties;
using System.Security.Cryptography;
using System.Text;
// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=234238

namespace SongQuiz
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class CreateGamePage : Page
    {
        private int count = 1;
        private Quiz quiz;
        private StorageFile pickedFile;
        private List<StorageFile> storageFiles;
        private int delay = 15; 
        public CreateGamePage()
        {
            this.InitializeComponent();
            Loaded += CreateGamePage_Loaded;
            quiz = new Quiz();
            storageFiles = new List<StorageFile>();
        }

        private async void CreateGamePage_Loaded(object sender, RoutedEventArgs e)
        { 
            await ApplicationData.Current.LocalFolder.CreateFolderAsync("quiz",CreationCollisionOption.ReplaceExisting);
        }

        private void generalSettingsGrid_GotFocus(object sender, RoutedEventArgs e)
        {
            generalSettingsGrid.Background = new SolidColorBrush(Windows.UI.Colors.DarkSlateGray);
        }

        private void generalSettingsGrid_LostFocus(object sender, RoutedEventArgs e)
        {
            generalSettingsGrid.Background = new SolidColorBrush(Windows.UI.Colors.Transparent);
        }

        private void songSettings_GotFocus(object sender, RoutedEventArgs e)
        {
            songSettingsGrid.Background = new SolidColorBrush(Windows.UI.Colors.DarkSlateGray);
        }

        private void songSettings_LostFocus(object sender, RoutedEventArgs e)
        {
            songSettingsGrid.Background = new SolidColorBrush(Windows.UI.Colors.Transparent);
        }     

        private async void timeSelector_ValueChanged(object sender, RangeBaseValueChangedEventArgs e)
        {
            if(pickedFile!=null)
            {
                
                timeSelector.Maximum = mediaElement.NaturalDuration.TimeSpan.TotalSeconds - 31;
                mediaElement.Position = TimeSpan.FromSeconds(e.NewValue);
                await Task.Delay(1000);
                mediaElement.Play(); 
                await Task.Delay(delay*1000);
                mediaElement.Stop();
            }
        }

        private void timeInterval_ValueChanged(object sender, RangeBaseValueChangedEventArgs e)
        {
            delay = (int)e.NewValue;
        }
        public async void SelectSongClick()
        {
            var openPicker = new Windows.Storage.Pickers.FileOpenPicker();
            openPicker.SuggestedStartLocation = Windows.Storage.Pickers.PickerLocationId.MusicLibrary;
            openPicker.FileTypeFilter.Add(".mp3");

            pickedFile = await openPicker.PickSingleFileAsync();
            var stream = await pickedFile.OpenAsync(FileAccessMode.Read);
            mediaElement.SetSource(stream, pickedFile.ContentType);
            songPicker.Content = pickedFile.DisplayName;
            await Task.Delay(1000);
            timeSelector.Maximum = mediaElement.NaturalDuration.TimeSpan.TotalSeconds -31; 
        }
        public void CancelClick()
        {
            Frame.Navigate(typeof(MainPage));
        }
        public void NextClick()
        {
            List<string> localList = new List<string>();
            localList.Add(rightAnswer.Text);
            localList.Add(wrongAnswer1.Text);
            localList.Add(wrongAnswer2.Text);
            localList.Add(wrongAnswer3.Text);
            if (pickedFile == null)
                songPicker.Foreground = new SolidColorBrush(Windows.UI.Colors.Red);
            else if (string.IsNullOrWhiteSpace(rightAnswer.Text))
                rightAnswer.Text = "Write an answer";
            else if (string.IsNullOrWhiteSpace(wrongAnswer1.Text))
                wrongAnswer1.Text = "Write an answer";
            else if (string.IsNullOrWhiteSpace(wrongAnswer2.Text))
                wrongAnswer2.Text = "Write an answer";
            else if (string.IsNullOrWhiteSpace(wrongAnswer2.Text))
                wrongAnswer3.Text = "Write an answer";
            else if (string.IsNullOrWhiteSpace(QuizName.Text))
                QuizName.Text = "Write a name";
            else if (localList.Distinct().ToList().Count < 4)
                wrongAnswer1.Text = wrongAnswer2.Text = wrongAnswer3.Text = "Write Different Answers";
            else
                CreateSong();
        }

        private async void CreateSong()
        {
            var encodingProfile = await MediaEncodingProfile.CreateFromFileAsync(pickedFile);
            var clip = await MediaClip.CreateFromFileAsync(pickedFile);
            clip.TrimTimeFromStart = TimeSpan.FromSeconds(timeSelector.Value);
            clip.TrimTimeFromEnd = clip.OriginalDuration - clip.TrimTimeFromStart - TimeSpan.FromSeconds(delay);
            var composition = new MediaComposition();
            composition.Clips.Add(clip);

            //var savePicker = new Windows.Storage.Pickers.FileSavePicker();
            //savePicker.FileTypeChoices.Add("MP3 files", new List<string>() { ".mp3" });
            //savePicker.SuggestedFileName = $"Song{count}.mp3";
            
            StorageFile file = await ApplicationData.Current.LocalFolder.CreateFileAsync($"Song{count}.mp3",CreationCollisionOption.ReplaceExisting);
            storageFiles.Add(file);
            if (file != null)
            {
                //Save to file using original encoding profile
                var result = await composition.RenderToFileAsync(file, MediaTrimmingPreference.Precise, encodingProfile);

                if (result != Windows.Media.Transcoding.TranscodeFailureReason.None)
                {
                    System.Diagnostics.Debug.WriteLine("Saving was unsuccessful");
                }
                else
                {
                    System.Diagnostics.Debug.WriteLine("Trimmed clip saved to file");
                }
            }

            MD5 md = MD5.Create();
            Random rnd = new Random();
            Song song = new Song();
            song.Interval = delay;
            song.RightAnswer = md.ComputeHash(Encoding.ASCII.GetBytes(rightAnswer.Text));
            song.Answers.Add(rightAnswer.Text);
            song.Answers.Add(wrongAnswer1.Text);
            song.Answers.Add(wrongAnswer2.Text);
            song.Answers.Add(wrongAnswer3.Text);
            song.Answers = song.Answers.OrderBy(x => rnd.Next(1, 10)).ToList();
            quiz.Songs.Add(song);
            count++;
            if (count > 10)
                SaveQuiz();
            else
                CleanUI();
        }
        private void CleanUI()
        {
            pickedFile = null;
            songPicker.Content = "Select Song";
            rightAnswer.Text = string.Empty;
            wrongAnswer1.Text = string.Empty;
            wrongAnswer2.Text = string.Empty;
            wrongAnswer3.Text = string.Empty;
            songCounter.Text = $"{count}/10";
        }

        private async void SaveQuiz()
        {

            var jsonFile = await ApplicationData.Current.LocalFolder.CreateFileAsync($"quiz.json", CreationCollisionOption.ReplaceExisting);
            quiz.Name = QuizName.Text;
            var json = JsonConvert.SerializeObject(quiz, Formatting.Indented);
            await FileIO.WriteTextAsync(jsonFile, json);

            

            var savePicker = new Windows.Storage.Pickers.FileSavePicker();
            savePicker.FileTypeChoices.Add("Zip files", new List<string>() { ".zip" });
            savePicker.SuggestedFileName = $"{quiz.Name}.zip";
            var storageZip = await savePicker.PickSaveFileAsync();

            storageFiles.Add(jsonFile);

            await SaveToZip(storageFiles, storageZip);

            Frame.Navigate(typeof(MainPage));
        }

        public async Task SaveToZip(IEnumerable<StorageFile> storageSongs, StorageFile file)
        {
            using (var zipMemoryStream = await file.OpenStreamForWriteAsync())
                using (var zipArchive = new ZipArchive(zipMemoryStream, ZipArchiveMode.Create))
                    await AddFileToZip(zipArchive, storageSongs);
        }

        private async Task AddFileToZip(ZipArchive zipArchive, IEnumerable<StorageFile> filesToCompress)
        {
            foreach (StorageFile fileToCompress in filesToCompress)
            {
                byte[] buffer = (await FileIO.ReadBufferAsync(fileToCompress)).ToArray();
                ZipArchiveEntry entry = zipArchive.CreateEntry(fileToCompress.Name);
                using (Stream entryStream = entry.Open())
                    await entryStream.WriteAsync(buffer, 0, buffer.Length);
            }
        }

        
    }


}
