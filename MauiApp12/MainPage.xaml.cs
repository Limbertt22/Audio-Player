using System.Collections.ObjectModel;
using System.Diagnostics;
using MauiApp12.ClassStend;
using Plugin.Maui.Audio;

namespace MauiApp12
{
    public partial class MainPage : ContentPage
    {
        public IAudioManager manager;
        public IAudioPlayer player;
        private SliderControll sliderControll;
        private Speed speed;
        private Volumen volumen;
        bool isplaying = false;
        private CollectionList collectionList;

        public ObservableCollection<Date> listaObservable { get; set; } = new();
        public MainPage()
        {
            InitializeComponent();
            manager = AudioManager.Current;
            BindingContext = this;
        }

        public void Start(object sender, TappedEventArgs e) 
        {
            if (player != null && isplaying) 
            {
                player.Pause();
                isplaying = false;
                bottomplay.Text = "▶";
            }
            else if(player != null)
            {
                player.Play();
                isplaying = true;
                bottomplay.Text = "🔲";
                sliderControll.StartSliderTimer();
            }
        }

        public async void SetArchive(string namepath) 
        {
            if (string.IsNullOrWhiteSpace(namepath) || !File.Exists(namepath))
            {
                await DisplayAlert("Error", "Ruta inválida o archivo no encontrado.", "OK");
                return;
            }


            var stream = File.OpenRead(namepath);
            player = manager.CreatePlayer(stream);
            sliderControll = new SliderControll(SliderVideo, player, LabelTime);
            volumen = new Volumen(BottomVolume, player, SliderVolume, speedMenuN);
            speed = new Speed(player);
            collectionList = new CollectionList(player, Entrada, listaObservable, this);
            isplaying = false;
            bottomplay.Text = "▶";
        }

        public async Task SelectFile()
        {
            try
            {
                if (player != null && player.IsPlaying) { player.Stop(); };

                var typeaArchivo = new FilePickerFileType(new Dictionary<DevicePlatform, IEnumerable<string>>
                {
                    {DevicePlatform.WinUI, new [] { ".mp3"} }
                });

                var result = new PickOptions
                {
                    PickerTitle = "Select File",
                    FileTypes = typeaArchivo
                };

                var file = await FilePicker.Default.PickAsync(result);

                if (file != null)
                {                   
                    Entrada.Text = $"Music-{file.FileName}";
                    SetArchive(file.FullPath);
                    await collectionList.AddIteam(file.FileName, file.FullPath);
                    await DisplayAlert("File find", $"Path: {file.FullPath}", "Ok");
                }
                else 
                {
                    await DisplayAlert("File not find", "file not find", "Ok");
                }
            }
            catch (Exception ex)
            {
                await DisplayAlert("Error", "Something is wrong", "Ok");
            }
        }

        private async void Button_Clicked_1(object sender, EventArgs e)
        {
           await SelectFile();
        }

        private void SliderVideo_ValueChanged(object sender, ValueChangedEventArgs e)
        {
            if (player != null)
            {
                sliderControll.ValueChanged(e.NewValue);
            }
        }

        private void TapGestureRecognizer_Tapped(object sender, TappedEventArgs e)
        {
            volumen.pressbottom();
        }

        private void SliderVolume_ValueChanged(object sender, ValueChangedEventArgs e)
        {
            volumen.ValueChanged(e.NewValue);
        }

        private void Picker_SelectedIndexChanged(object sender, EventArgs e)
        {
            speed.SpeedChanged(sender);
        }

        private void CollectionView_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            
            SetArchive(collectionList.SelectIteam(e));
        }

        private void TapGestureRecognizer_Tapped_1(object sender, EventArgs e)
        {
            collectionList.DeleteIteam(sender, e);
        }
    }
}
