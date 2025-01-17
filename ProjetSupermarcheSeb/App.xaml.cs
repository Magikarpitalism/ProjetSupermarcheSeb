namespace ProjetSupermarcheSeb
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();
        }

        public static IServiceProvider Services
    => Current is App app
        ? app.Handler?.MauiContext?.Services
        : throw new InvalidOperationException("App not initialized correctly");

        protected override Window CreateWindow(IActivationState? activationState)
        {
            return new Window(new AppShell());
        }
    }
}