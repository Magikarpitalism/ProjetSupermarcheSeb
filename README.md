# ProjetSupermarcheSeb - Application .NET MAUI

## Description
**ProjetSupermarcheSeb** est une application **mobile** et **desktop** (via .NET MAUI) permettant de gérer les **heures travaillées** par des employés dans différents rayons d’un supermarché. Elle s’appuie sur une **base de données SQLite** (via `sqlite-net-pcl`), propose un **converter** personnalisé (pour colorer les heures) et affiche des **graphiques** (camembert, barres) via **OxyPlot**.

---

## Fonctionnalités

1. **Gestion des Données**
   - Création / consultation / suppression des employés  
   - Création / consultation / suppression des rayons  
   - Création / consultation / suppression des secteurs  

2. **Saisie du Temps de Travail**
   - Association employé ↔ rayon ↔ date  
   - Validation (un employé ne peut pas travailler dans le même rayon à la même date)  
   - Date du jour proposée par défaut  

3. **Consultation**
   - Visualisation du temps de travail par employé  
   - Affichage détaillé : nom du rayon, date, nombre d’heures  
   - Total des heures travaillées pour chaque employé  

4. **Statistiques**
   - Nombre d’heures par employé  
   - Total des heures par rayon  
   - Total général des heures  
   - Total des heures par mois  

5. **Visualisations (Graphiques)**
   - Affichage camembert / donut (via OxyPlot)  
   - Affichage barres (histogramme)  
   - Synthèse visuelle des données  

6. **Converter (Heures → Couleur)**
   - Un converter qui colorie un nombre d’heures en **vert** (si > 6) ou **Rouge** (si ≤ 6)  
   - Permet de repérer rapidement les gros volumes d’heures  

---

## Prérequis Techniques

### Packages NuGet
- **sqlite-net-pcl** (v. 1.9.172)  
  > Gère la base de données SQLite dans .NET MAUI  
- **OxyPlot.Maui.Skia** (v. 1.0.2)  
  > Bibliothèque de graphiques (camembert, barres) via SkiaSharp  
- **SkiaSharp** et **SkiaSharp.Views.Maui.Controls.Compatibility**  
  > Moteur 2D cross-plateforme pour OxyPlot  
- **Microsoft.Maui.Controls** (v. 9.0.30)  
  > Base du framework .NET MAUI  
- **Microsoft.Extensions.Logging.Debug** (v. 9.0.1)  
  > Logging simplifié pour .NET MAUI  

### Base de Données
- **SQLite** (via `sqlite-net-pcl`)
- Tables :
  - `Employee` : (Id, Name)  
  - `Sector` : (Id, Name)  
  - `Rayon` : (Id, Name, SectorId)  
  - `WorkTime` : (Id, EmployeeId, RayonId, Date, Hours)

---

## Installation et Lancement

1. **Cloner** ce repo (ProjetSupermarcheSeb) depuis GitHub.
2. **Ouvrir** la solution (`.sln`) dans **Visual Studio 2022** (ou plus récent).
3. **Restaurer** les packages NuGet si nécessaire.
4. **Compiler** et **Exécuter** :
   - Choisir **“Windows Machine”** pour lancer en mode Desktop.
   - Ou configurer un **émulateur Android** (ou iOS) pour mobile.

---

## Architecture du Projet

```plaintext
ProjetSupermarcheSeb/
├── Converters/
│   └── HoursToColorConverter.cs       // Converter pour colorer le texte selon nb d’heures
├── Data/
│   └── DatabaseService.cs             // Accès à la base SQLite
├── Models/
│   ├── Employee.cs
│   ├── Sector.cs
│   ├── Rayon.cs
│   └── WorkTime.cs
├── Views/
│   ├── GestionPage.xaml / .cs         // Gérer employés, rayons, secteurs
│   ├── SaisirTempsPage.xaml / .cs     // Saisir heures (employé, rayon, date)
│   ├── ConsultationPage.xaml / .cs    // Voir liste + total des heures
│   ├── StatistiquesPage.xaml / .cs    // Calculs (heures/employé, total général, etc.)
│   └── GraphiquesPage.xaml / .cs      // OxyPlot (camembert, barres)
├── App.xaml / App.xaml.cs             // Ressources (dont converter)
├── MauiProgram.cs                     // Configuration .NET MAUI
├── ProjetSupermarcheSeb.csproj
└── ...
```

## Aperçu Fonctionnel

GestionPage
Ajout/Suppression d’employés, rayons, secteurs
SaisirTempsPage
Sélection d’un employé, d’un rayon, saisie des heures + date
Validation doublon (employé/rayon/date)
ConsultationPage
Choix d’un employé, liste des temps (Rayon, Date, Heures), total
Couleur du texte “Heures” (rouge/vert)
StatistiquesPage
Bouton “Calculer” => nombre d’heures par employé, total global, etc.
GraphiquesPage
OxyPlot : camembert (répartition par employé) + barres (histogramme)