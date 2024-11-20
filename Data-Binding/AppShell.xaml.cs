namespace Data_Binding
{
    public partial class AppShell : Shell
    {
        public AppShell()
        {
            InitializeComponent();

            // Registra las rutas de navegacion
            Routing.RegisterRoute(nameof(FormPage), typeof(FormPage));
        }
    }
}
