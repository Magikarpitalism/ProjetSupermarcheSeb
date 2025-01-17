using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ProjetSupermarcheSeb.Models;
using SQLite;

namespace ProjetSupermarcheSeb.Data
{ 
// Gère la connexion et les opérations CRUD sur la base SQLite
    public class DatabaseService
{
    private readonly SQLiteAsyncConnection _database;

    // Constructeur
    public DatabaseService()
    {
        // Chemin où est stockée la base .db3
        string dbPath = Path.Combine(
            Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData),
            "supermarche.db3");

        // Crée la connexion SQLite en asynchrone
        _database = new SQLiteAsyncConnection(dbPath);

        // Crée les tables si elles n'existe pas
        _database.CreateTableAsync<Employee>().Wait();
        _database.CreateTableAsync<Sector>().Wait();
        _database.CreateTableAsync<Rayon>().Wait();
        _database.CreateTableAsync<WorkTime>().Wait();

        // Insère des données d'exemple si la base est vide
        SeedData();
    }

    private async void SeedData()
    {
        // Si aucun employé n'existe, on en crée
        var existingEmps = await _database.Table<Employee>().ToListAsync();
        if (existingEmps.Count == 0)
        {
            await _database.InsertAllAsync(new List<Employee>
                {
                    new Employee { Name = "Fortin" },
                    new Employee { Name = "Alison" },
                    new Employee { Name = "Cousinot" }
                });
        }

        // pareille pour secteur
        var existingSectors = await _database.Table<Sector>().ToListAsync();
        if (existingSectors.Count == 0)
        {
            await _database.InsertAllAsync(new List<Sector>
                {
                    new Sector { Name = "Alimentation" },
                    new Sector { Name = "Maison" },
                    new Sector { Name = "Produits frais" }
                });
        }

        // pareille pour rayon
        var existingRayons = await _database.Table<Rayon>().ToListAsync();
        if (existingRayons.Count == 0)
        {
            await _database.InsertAllAsync(new List<Rayon>
                {
                    new Rayon { Name = "Gâteaux",   SectorId=1 },
                    new Rayon { Name = "Légumes",   SectorId=3 },
                    new Rayon { Name = "Viande",    SectorId=3 },
                    new Rayon { Name = "Luminaire", SectorId=2 }
                });
        }

        // pareille pour WorkTime
        var existingWorks = await _database.Table<WorkTime>().ToListAsync();
        if (existingWorks.Count == 0)
        {
            await _database.InsertAllAsync(new List<WorkTime>
                {
                    new WorkTime { EmployeeId=1, RayonId=1, Date=new DateTime(2025,1,5), Hours=5 },
                    new WorkTime { EmployeeId=1, RayonId=2, Date=new DateTime(2025,1,6), Hours=7 },
                    new WorkTime { EmployeeId=2, RayonId=1, Date=new DateTime(2025,1,5), Hours=6 },
                    new WorkTime { EmployeeId=3, RayonId=2, Date=new DateTime(2025,1,5), Hours=8 },
                    new WorkTime { EmployeeId=3, RayonId=2, Date=new DateTime(2025,1,6), Hours=5 }
                });
        }
    }

    // Récupérer liste employés
    public Task<List<Employee>> GetEmployeesAsync() => _database.Table<Employee>().ToListAsync();

    // Ajouter un employé
    public Task<int> AddEmployeeAsync(Employee e) => _database.InsertAsync(e);

    // Récupérer secteurs
    public Task<List<Sector>> GetSectorsAsync() => _database.Table<Sector>().ToListAsync();
    public Task<int> AddSectorAsync(Sector s) => _database.InsertAsync(s);

    // Récupérer rayons
    public Task<List<Rayon>> GetRayonsAsync() => _database.Table<Rayon>().ToListAsync();
    public Task<int> AddRayonAsync(Rayon r) => _database.InsertAsync(r);

    // Récupérer WorkTime
    public Task<List<WorkTime>> GetWorkTimesAsync() => _database.Table<WorkTime>().ToListAsync();

    // Ajouter un WorkTime (éviter doublon (Employé, Rayon, Date))
    public async Task<int> AddWorkTimeAsync(WorkTime w)
    {
        var existing = await _database.Table<WorkTime>()
            .Where(x => x.EmployeeId == w.EmployeeId
                     && x.RayonId == w.RayonId
                     && x.Date == w.Date)
            .FirstOrDefaultAsync();

        if (existing != null)
            throw new Exception("Cet employé a déjà travaillé dans ce rayon à cette date !");

        return await _database.InsertAsync(w);
    }
}
}
