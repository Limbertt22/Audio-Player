using System.Diagnostics;
using Plugin.Maui.Audio;

namespace MauiApp12.ClassStend
{
    internal class SliderControll
    {
        private readonly Slider slider;
        private readonly IAudioPlayer player;
        private readonly Label label;
        private bool timerRunning = true;

        public SliderControll(Slider slider, IAudioPlayer player, Label label)
        {
            this.slider = slider;
            this.player = player;
            this.label = label;
        }

        public void StartSliderTimer()
        {
            if (timerRunning)
            {
                Device.StartTimer(TimeSpan.FromMilliseconds(100), () =>
                {
                    if (player != null && player.IsPlaying)
                    {
                        slider.Maximum = player.Duration;
                        slider.Value = player.CurrentPosition;

                        var time = TimeSpan.FromSeconds(player.CurrentPosition);
                        label.Text = $"{time.Minutes:D2}:{time.Seconds:D2}";
                    }

                    return player != null && player.IsPlaying;
                });
            }
        }

        public void ValueChanged(double e)
        {
             player.Seek(e);
             var time = TimeSpan.FromSeconds(e);
             label.Text = $"{time.Minutes:D2}:{time.Seconds:D2}";
        }
    }
}
