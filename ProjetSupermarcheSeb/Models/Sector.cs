using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SQLite;

namespace ProjetSupermarcheSeb.Models
{
    // Représente la table "Sector"
    public class Sector
    {
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }

        // Nom du secteur
        public string Name { get; set; }
    }
}
