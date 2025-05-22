using Plugin.Maui.Audio;

namespace MauiApp12.ClassStend
{
    internal class Speed
    {
        private readonly IAudioPlayer player;
        public Speed(IAudioPlayer player) 
        {
            this.player = player;
        }

        public void SpeedChanged(object sender) 
        {
            var picker = (Picker)sender;
            int value = picker.SelectedIndex;

            switch (value) 
            {
                case 1:
                    player.Speed = 1.0f;
                    break;
                case 2:
                    player.Speed = 0.5f;
                    break;
                case 3:
                    player.Speed = 1.5f;
                    break;
                case 4:
                    player.Speed = 2.0f;
                    break;
                default:
                    break;
            }
        }
    }
}
