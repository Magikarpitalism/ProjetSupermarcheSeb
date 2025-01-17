using ProjetSupermarcheSeb.Data;
using ProjetSupermarcheSeb.Models;

namespace ProjetSupermarcheSeb.Views
{
    public partial class SaisirTempsPage : ContentPage
    {
        private readonly DatabaseService _db;

        // On re�oit le DatabaseService en injection
        public SaisirTempsPage(DatabaseService db)
        {
            InitializeComponent();
            _db = db;

            LoadPickers();
            DatePickerWork.Date = DateTime.Today; // Date par d�faut
        }

        private async void LoadPickers()
        {
            var employees = await _db.GetEmployeesAsync();
            PickerEmployee.ItemsSource = employees;
            PickerEmployee.ItemDisplayBinding = new Binding("Name");

            var rayons = await _db.GetRayonsAsync();
            PickerRayon.ItemsSource = rayons;
            PickerRayon.ItemDisplayBinding = new Binding("Name");
        }

        private async void OnValiderClicked(object sender, EventArgs e)
        {
            var selEmployee = (Employee)PickerEmployee.SelectedItem;
            var selRayon = (Rayon)PickerRayon.SelectedItem;
            var selDate = DatePickerWork.Date;

            if (int.TryParse(EntryHours.Text, out int hours) &&
                selEmployee != null && selRayon != null)
            {
                var newWork = new WorkTime
                {
                    EmployeeId = selEmployee.Id,
                    RayonId = selRayon.Id,
                    Date = selDate,
                    Hours = hours
                };

                try
                {
                    await _db.AddWorkTimeAsync(newWork);
                    await DisplayAlert("Succ�s", "Temps ajout� !", "OK");
                }
                catch (Exception ex)
                {
                    await DisplayAlert("Erreur", ex.Message, "OK");
                }
            }
            else
            {
                await DisplayAlert("Erreur", "Champs invalides", "OK");
            }
        }
    }
}
