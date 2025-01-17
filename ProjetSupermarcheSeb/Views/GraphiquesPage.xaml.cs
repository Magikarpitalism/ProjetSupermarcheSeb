using ProjetSupermarcheSeb.Data;
using OxyPlot;
using OxyPlot.Axes;
using OxyPlot.Series;

namespace ProjetSupermarcheSeb.Views
{
    public partial class GraphiquesPage : ContentPage
    {
        private readonly DatabaseService _db;

        public GraphiquesPage(DatabaseService db)
        {
            InitializeComponent();
            _db = db;
        }

        private async void OnAfficherGraphiquesClicked(object sender, EventArgs e)
        {
            // R�cup�re tous les WorkTimes (temps de travail) et la liste d'employ�s
            var works = await _db.GetWorkTimesAsync();
            var employees = await _db.GetEmployeesAsync();

            // Regrouper par employ� et sommer les heures
            var groupEmp = from w in works
                           group w by w.EmployeeId into g
                           select new
                           {
                               EmpId = g.Key,
                               TotalHours = g.Sum(x => x.Hours)
                           };

            // 1) Construire le camembert/pie avec OxyPlot

            // Cr�e la s�rie PieSeries
            var pieSeries = new PieSeries
            {
                StrokeThickness = 0.25,   // �paisseur du contour
                InsideLabelPosition = 0.8,
                AngleSpan = 360,          // Angle total
                StartAngle = 0            // D�marrage � 0�
            };

            // Ajouter les tranches (PieSlice)
            foreach (var item in groupEmp)
            {
                // Nom de l'employ�
                var empName = employees.FirstOrDefault(e => e.Id == item.EmpId)?.Name ?? "Inconnu";
                // On ajoute une portion correspondant � item.TotalHours
                pieSeries.Slices.Add(new PieSlice(empName, item.TotalHours));
            }

            // Construire un mod�le pour le PieChart
            var pieModel = new PlotModel { Title = "R�partition des heures" };

            pieModel.TextColor = OxyColors.White;        // Couleur de base pour le texte
            pieModel.TitleColor = OxyColors.White;              // Couleur du titre

            pieModel.Series.Add(pieSeries);

            PieChart.Model = pieModel;

            // 2) Construire l'histogramme (BarSeries)

            //PlotModel pour les barres
            var barModel = new PlotModel { Title = "Heures par Employ�" };

            barModel.TextColor = OxyColors.White;
            barModel.TitleColor = OxyColors.White;

            var valAxis = new LinearAxis
            {
                Position = AxisPosition.Bottom,
                MinimumPadding = 0  // Pour �viter un gap important en bas
            };

            valAxis.TextColor = OxyColors.White;
            valAxis.TitleColor = OxyColors.White;
            valAxis.AxislineColor = OxyColors.White;
            valAxis.TicklineColor = OxyColors.White;

            var catAxis = new CategoryAxis
            {
                Position = AxisPosition.Left,
                
            };

            catAxis.TextColor = OxyColors.White;
            catAxis.TitleColor = OxyColors.White;
            catAxis.AxislineColor = OxyColors.White;
            catAxis.TicklineColor = OxyColors.White;

            barModel.Axes.Add(valAxis); 
            barModel.Axes.Add(catAxis);

            // Cr�e la BarSeries
            var barSeries = new BarSeries
            {
                StrokeColor = OxyColors.Black,
                StrokeThickness = 1
            };

            // Remplir la BarSeries, en ajoutant 1 item par employ�
            foreach (var item in groupEmp)
            {
                var empName = employees.FirstOrDefault(e => e.Id == item.EmpId)?.Name ?? "Inconnu";
                // Ajoute l'�tiquette de cat�gorie
                catAxis.Labels.Add(empName);
                // Ajoute la valeur
                barSeries.Items.Add(new BarItem(item.TotalHours));
            }

            // Ajouter la s�rie de barres au mod�le
            barModel.Series.Add(barSeries);

            BarChart.Model = barModel;
        }
    }
}