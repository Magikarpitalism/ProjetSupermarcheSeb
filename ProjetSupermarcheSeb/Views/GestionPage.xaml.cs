using ProjetSupermarcheSeb.Data;
using ProjetSupermarcheSeb.Models;

namespace ProjetSupermarcheSeb.Views
{
    public partial class GestionPage : ContentPage
    {
        // On r�cup�re DatabaseService via injection
        private readonly DatabaseService _db;

        // Le constructeur re�oit DatabaseService
        // gr�ce � l'injection de d�pendances
        public GestionPage(DatabaseService db)
        {
            InitializeComponent(); // Initialise le XAML
            _db = db;             // On stocke la r�f�rence
        }

        private async void OnAddEmployeeClicked(object sender, EventArgs e)
        {
            string name = EntryEmployeeName.Text?.Trim();
            if (!string.IsNullOrEmpty(name))
            {
                await _db.AddEmployeeAsync(new Employee { Name = name });
                await DisplayAlert("Succ�s", "Employ� ajout� !", "OK");
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
                await DisplayAlert("Succ�s", "Rayon ajout� !", "OK");
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
                await DisplayAlert("Succ�s", "Secteur ajout� !", "OK");
            }
            else
            {
                await DisplayAlert("Erreur", "Veuillez saisir un nom de secteur", "OK");
            }
        }
    }
}