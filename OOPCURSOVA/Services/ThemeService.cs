using Microsoft.Identity.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace OOPCURSOVA.Services
{
    internal class ThemeService
    {
        private bool _isDarkTheme;
        public bool IsDarkTheme => _isDarkTheme;

        public void ToggleTheme()
        {
            _isDarkTheme = !_isDarkTheme;
            ApplyTheme();
        }
        public void ApplyTheme()
        {
            var dict = new ResourceDictionary();

            if(_isDarkTheme)
                dict.Source=new Uri("pack://application:,,,/OOPCURSOVA;component/Themes/DarkTheme.xaml");
            else
                dict.Source = new Uri("pack://application:,,,/OOPCURSOVA;component/Themes/LightTheme.xaml");

            Application.Current.Resources.MergedDictionaries.Clear();
            Application.Current.Resources.MergedDictionaries.Add(dict);
        }

    }
}
