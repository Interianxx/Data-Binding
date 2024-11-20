using System.Text;
using System.Text.Json;

namespace Data_Binding;

public partial class FormPage : ContentPage
{
    private readonly HttpClient _httpClient = new HttpClient();
    public FormPage()
    {
        InitializeComponent();
    }

    private readonly CancellationTokenSource _cts = new CancellationTokenSource();


    private async void OnGuardarClicked(object sender, EventArgs e)
    {
        var nuevaPersona = new PersonaModel
        {
            nombre = NombreEntry.Text,
            apellido = ApellidoEntry.Text,
            sexo = GetSelectedSexo(),
            fh_nac = FechaNacimientoDatePicker.Date.ToString("yyyy-MM-dd"),
            id_rol = int.TryParse(idRol.Text, out int idRolValue) ? idRolValue : 0
        };

        var json = JsonSerializer.Serialize(nuevaPersona);
        var content = new StringContent(json, Encoding.UTF8, "application/json");




        var response = await _httpClient.PostAsync("https://fi.jcaguilar.dev/v1/escuela/persona",
            content,
            _cts.Token
            );


        if (response.IsSuccessStatusCode)
        {
            await DisplayAlert("Éxito", "La persona fue guardada exitosamente.", "OK");
            await Navigation.PushAsync(new MainPage());
        }
        else
        {
            await DisplayAlert("Error", "Hubo un problema al guardar la persona.", "OK");
        }
    }

    private string GetSelectedSexo()
    {
        if (SexoPicker.SelectedIndex >= 0) // Verifica si hay una selección válida
        {
            return SexoPicker.SelectedItem.ToString();
        }
        else
        {
            return null; // No hay selección
        }
    }
}