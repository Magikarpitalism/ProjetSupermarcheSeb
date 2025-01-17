using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SQLite;

namespace ProjetSupermarcheSeb.Models
{
    // Représente la table "Rayon"
    public class Rayon
    {
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }

        public string Name { get; set; }

        // Lien vers "Sector"
        public int SectorId { get; set; }
    }
}