using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SQLite;

namespace ProjetSupermarcheSeb.Models
{
    // Représente la table "WorkTime"
    // (Heures travaillées par un Employé dans un Rayon, à une Date précise)
    public class WorkTime
    {
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }

        // Clés étrangères
        public int EmployeeId { get; set; }
        public int RayonId { get; set; }

        public DateTime Date { get; set; }
        public int Hours { get; set; }
    }
}
