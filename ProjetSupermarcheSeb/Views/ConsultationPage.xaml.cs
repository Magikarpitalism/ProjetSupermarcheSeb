using ProjetSupermarcheSeb.Data;
using ProjetSupermarcheSeb.Models;

namespace ProjetSupermarcheSeb.Views
{
    public partial class ConsultationPage : ContentPage
    {
        private readonly DatabaseService _db;

        public ConsultationPage(DatabaseService db)
        {
            InitializeComponent();
            _db = db;

            LoadEmployees();
        }

        private async void LoadEmployees()
        {
            var employees = await _db.GetEmployeesAsync();
            PickerEmployee.ItemsSource = employees;
            PickerEmployee.ItemDisplayBinding = new Binding("Name");
        }

        private async void OnEmployeeSelected(object sender, EventArgs e)
        {
            if (PickerEmployee.SelectedItem is Employee emp)
            {
                await LoadWorkTimes(emp.Id);
            }
        }

        private async Task LoadWorkTimes(int employeeId)
        {
            var allTimes = await _db.GetWorkTimesAsync();
            var rayons = await _db.GetRayonsAsync();

            // Filtrer les WorkTime de cet employé
            var filtered = allTimes.Where(x => x.EmployeeId == employeeId).ToList();

            // Préparer la liste d'affichage
            var displayList = new List<WorkTimeDisplay>();
            foreach (var t in filtered)
            {
                var r = rayons.FirstOrDefault(rn => rn.Id == t.RayonId);
                displayList.Add(new WorkTimeDisplay
                {
                    RayonName = r?.Name ?? "Inconnu",
                    DateString = t.Date.ToString("dd/MM/yyyy"),
                    Hours = t.Hours
                });
            }

            CollectionViewWorkTimes.ItemsSource = displayList;
            int total = displayList.Sum(d => d.Hours);
            LabelTotalHours.Text = $"Total : {total}h";
        }

        private class WorkTimeDisplay
        {
            public string RayonName { get; set; }
            public string DateString { get; set; }
            public int Hours { get; set; }
        }
    }
}
