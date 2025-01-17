using ProjetSupermarcheSeb.Data;
using ProjetSupermarcheSeb.Models;

namespace ProjetSupermarcheSeb.Views
{
    public partial class StatistiquesPage : ContentPage
    {
        private readonly DatabaseService _db;

        public StatistiquesPage(DatabaseService db)
        {
            InitializeComponent();
            _db = db;
        }

        private async void OnCalculerClicked(object sender, EventArgs e)
        {
            var emps = await _db.GetEmployeesAsync();
            var rays = await _db.GetRayonsAsync();
            var secs = await _db.GetSectorsAsync();
            var works = await _db.GetWorkTimesAsync();

            // 1) Heures par employé
            var groupEmp = from w in works
                           group w by w.EmployeeId into g
                           select new { EmpId = g.Key, Total = g.Sum(x => x.Hours) };

            string txtEmp = "Heures par employé :\n";
            foreach (var ge in groupEmp)
            {
                var empName = emps.FirstOrDefault(e2 => e2.Id == ge.EmpId)?.Name;
                txtEmp += $"{empName} : {ge.Total}h\n";
            }
            LabelNbHeuresParEmploye.Text = txtEmp;

            // 2) Heures par rayon
            var groupRay = from w in works
                           group w by w.RayonId into g
                           select new { RayId = g.Key, Total = g.Sum(x => x.Hours) };

            string txtRay = "Heures par rayon :\n";
            foreach (var gr in groupRay)
            {
                var rName = rays.FirstOrDefault(rn => rn.Id == gr.RayId)?.Name;
                txtRay += $"{rName} : {gr.Total}h\n";
            }
            LabelHeuresParRayon.Text = txtRay;

            // 3) Total général
            int totalGeneral = works.Sum(x => x.Hours);
            LabelTotalGeneralHeures.Text = $"Total général : {totalGeneral}h";

            // 4) Rayons par secteur
            var groupSect = from r in rays
                            group r by r.SectorId into g
                            select new { SectorId = g.Key, Count = g.Count() };

            string txtSect = "Nombre de rayons par secteur :\n";
            foreach (var gs in groupSect)
            {
                var secName = secs.FirstOrDefault(s2 => s2.Id == gs.SectorId)?.Name;
                txtSect += $"{secName} : {gs.Count}\n";
            }
            LabelRayonsParSecteur.Text = txtSect;

            // 5) Heures par mois
            var groupMonth = from w in works
                             group w by new { w.Date.Year, w.Date.Month } into g
                             select new
                             {
                                 g.Key.Year,
                                 g.Key.Month,
                                 SumHours = g.Sum(x => x.Hours)
                             };

            string txtMonth = "Heures par mois :\n";
            foreach (var gm in groupMonth)
            {
                txtMonth += $"{gm.Month:00}/{gm.Year} : {gm.SumHours}h\n";
            }
            LabelHeuresParMois.Text = txtMonth;
        }
    }
}
