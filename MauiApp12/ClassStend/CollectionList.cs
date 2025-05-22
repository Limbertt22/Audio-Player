using System.Collections.ObjectModel;
using Plugin.Maui.Audio;

namespace MauiApp12.ClassStend
{
    public class CollectionList
    {
        private readonly IAudioPlayer Player;
        private readonly Entry entry;
        public readonly Page page;
        private ObservableCollection<Date> lista { get; set; } = new();
        public CollectionList(IAudioPlayer player, Entry entry, ObservableCollection<Date> lista, Page page)
        {
            this.Player = player;
            this.entry = entry;
            this.lista = lista;
            this.page = page;
        }

        public void DeleteIteam(object sender, EventArgs e)
        {
            var label = sender as Label;
            var dateSeleccionado = label?.BindingContext as Date;

            if (dateSeleccionado != null)
            {
                lista.Remove(dateSeleccionado);
            }
        }

        public async Task AddIteam(string name, string path) 
        {
            if (!lista.Any(d => d.Name == name))
            {
                lista.Add(new Date
                {
                    Name = name,
                    Path = path,
                    date = DateTime.Now.ToString("dd/MM/yyyy")
                });
            }
            else 
            {
              await  page.DisplayAlert("Archivo repetido", "Este Archivo ya existe", "Ok");
            }
        }

        public string SelectIteam(SelectionChangedEventArgs e) 
        {
              Player.Stop();
              if (e.CurrentSelection.FirstOrDefault() is Date Song)
              {
                entry.Text = $"Music-{Song.Name}";
                return Song.Path;
              }
            return null;
        }

    }
}
