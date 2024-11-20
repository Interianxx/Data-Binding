using System.Collections.ObjectModel;
using System.Text.Json;

namespace Data_Binding
{

    public partial class MainPage : ContentPage
    {

        public MainPage()
        {
            InitializeComponent();
            BindingContext = this;
            GetData();

        }

        private readonly HttpClient _httpClient = new HttpClient();

        private ObservableCollection<PersonaModel> _personas;

        public ObservableCollection<PersonaModel> Personas
        {
            get => _personas;

            set
            {
                _personas = value;
                OnPropertyChanged();
            }
        }

        private async void GetData()
        {
            try
            {
                var response = await _httpClient.GetStringAsync("https://fi.jcaguilar.dev/v1/escuela/persona");
                Console.WriteLine(response); // Imprime la respuesta para verificarla

                var personas = JsonSerializer.Deserialize<ObservableCollection<PersonaModel>>(response);
                Personas = personas ?? new ObservableCollection<PersonaModel>();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error al obtener los datos: {ex.Message}");
            }
        }


        private async void Button_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new FormPage());
        }




    }

}
