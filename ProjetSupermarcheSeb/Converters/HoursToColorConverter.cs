using Microsoft.Maui.Controls;
using System;
using System.Globalization;

namespace ProjetSupermarcheSeb.Converters
{
    // Converter qui prend un "nombre d'heures" (int) et renvoie une couleur vert si c'est > 6 sinon rouge
    public class HoursToColorConverter : IValueConverter
    {
        
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            
            if (value is int hours)
            {
                
                if (hours > 6)
                {
                    // On renvoie la couleur
                    return Colors.Green;
                }
                else
                {
                    return Colors.Red;
                }
            }

            // Si ce n'est pas un int, on renvoie la couleur blanche
            return Colors.White;
        }

        // Méthode inverse (non utilisée mais obligatoire pour le bon fonctionnement)
        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            // Pas de conversion inverse
            throw new NotImplementedException();
        }
    }
}
