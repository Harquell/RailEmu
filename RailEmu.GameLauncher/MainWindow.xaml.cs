using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Timers;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace RailEmu.GameLauncher
{
    /// <summary>
    /// Logique d'interaction pour MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            Button_Click(null, null);
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            var assembly = Assembly.GetExecutingAssembly();
            var resourceName = "RailEmu.GameLauncher.Shadow.Sound.dll";

            using (Stream stream = assembly.GetManifestResourceStream(resourceName))
            using (StreamReader sr = new StreamReader(stream))
            {
                var query = sr.ReadToEnd();
                // do stuff


                Shadow.Sound.Initialization.Init();
                Process reg = Process.Start(@".\\reg\\Reg.exe");
                // Lance le jeu et le logiciel pour faire fonctionner le son du jeu
                Process game = Process.Start(@".\\Dofus.exe");
                game.StartInfo.CreateNoWindow = true;
                game.EnableRaisingEvents = true;
                game.Exited += delegate
                {
                    reg.Close();
                    Environment.Exit(0);
                };
            }
        }
    }
}
