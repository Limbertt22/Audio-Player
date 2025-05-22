using MauiApp12.ClassStend;
using Plugin.Maui.Audio;

namespace MauiApp12
{
    class Volumen
    {
        private readonly Label bottomVolumen;
        private readonly IAudioPlayer player;
        private readonly Slider slider;
        private readonly StackLayout stack;
        public Volumen(Label bottomVolumen, IAudioPlayer player, Slider slider, StackLayout stack)
        {
            this.bottomVolumen = bottomVolumen;
            this.player = player;
            this.slider = slider;
            this.stack = stack;
        }

        public void pressbottom() 
        {
            if (slider.IsVisible)
            {
                slider.IsVisible = false;
                stack.TranslationX = 280;
            }
            else 
            {
                slider.IsVisible = true;
                stack.TranslationX = 180;
            }
        }

        public void ValueChanged(double e)
        {
            player.Volume = e;
            if (player.Volume == 0.0) 
            { 
                bottomVolumen.Text = "🔇";
            }
            else if (player.Volume >= 0.1 && player.Volume <= 0.5)
            {
                bottomVolumen.Text = "🔉";
            }
            else if(player.Volume > 0.5)
            { 
                bottomVolumen.Text = "🔊";
            }
        }
    }
}