using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SQLite;

namespace ProjetSupermarcheSeb.Models
{
    // Représente la table "Employee"
    public class Employee
    {
        [PrimaryKey, AutoIncrement] // Clé primaire auto-incrémentée
        public int Id { get; set; }

        // Nom de l'employé
        public string Name { get; set; }
    }
}
