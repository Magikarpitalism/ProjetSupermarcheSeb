using ProjetSupermarcheSeb.Data;
using ProjetSupermarcheSeb.Models;

namespace ProjetSupermarcheSeb.Views
{
    public partial class GestionPage : ContentPage
    {
        // On récupère DatabaseService via injection
        private readonly DatabaseService _db;

        // Le constructeur reçoit DatabaseService
        // grâce à l'injection de dépendances
        public GestionPage(DatabaseService db)
        {
            InitializeComponent(); // Initialise le XAML
            _db = db;             // On stocke la référence
        }

        private async void OnAddEmployeeClicked(object sender, EventArgs e)
        {
            string name = EntryEmployeeName.Text?.Trim();
            if (!string.IsNullOrEmpty(name))
            {
                await _db.AddEmployeeAsync(new Employee { Name = name });
                await DisplayAlert("Succès", "Employé ajouté !", "OK");
            }
            else
            {
                await DisplayAlert("Erreur", "Veuillez saisir un nom", "OK");
            }
        }

        private async void OnAddRayonClicked(object sender, EventArgs e)
        {
            string rayonName = EntryRayonName.Text?.Trim();
            string sectorIdText = EntryRayonSectorId.Text?.Trim();

            if (!string.IsNullOrEmpty(rayonName) &&
                int.TryParse(sectorIdText, out int sectorId))
            {
                await _db.AddRayonAsync(new Rayon { Name = rayonName, SectorId = sectorId });
                await DisplayAlert("Succès", "Rayon ajouté !", "OK");
            }
            else
            {
                await DisplayAlert("Erreur", "Champs invalides", "OK");
            }
        }

        private async void OnAddSectorClicked(object sender, EventArgs e)
        {
            string sectorName = EntrySectorName.Text?.Trim();
            if (!string.IsNullOrEmpty(sectorName))
            {
                await _db.AddSectorAsync(new Sector { Name = sectorName });
                await DisplayAlert("Succès", "Secteur ajouté !", "OK");
            }
            else
            {
                await DisplayAlert("Erreur", "Veuillez saisir un nom de secteur", "OK");
            }
        }
    }
}