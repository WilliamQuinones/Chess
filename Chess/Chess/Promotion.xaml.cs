using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace Chess
{
    /// <summary>
    /// Interaction logic for Promotion.xaml
    /// </summary>
    public partial class Promotion : Window
    {

        bool choiceMade = false;
        protected override void OnClosing(CancelEventArgs e)
        {
            if (!choiceMade)
            {
                base.OnClosing(e);
                e.Cancel = true;
            }
            
        }

        public Promotion()
        {
            try{ InitializeComponent();} catch { }
            
        }

        private void knight_Click(object sender, RoutedEventArgs e)
        {
            var mainWindow = Application.Current.Windows.Cast<Window>().FirstOrDefault(window => window is MainWindow) as MainWindow;
            mainWindow.isKnight = true;
            choiceMade = true;
            Close();

        }

        private void bishop_Click(object sender, RoutedEventArgs e)
        {
            var mainWindow = Application.Current.Windows.Cast<Window>().FirstOrDefault(window => window is MainWindow) as MainWindow;
            mainWindow.isBishop = true;
            choiceMade = true;
            Close();
        }

        private void rook_Click(object sender, RoutedEventArgs e)
        {
            var mainWindow = Application.Current.Windows.Cast<Window>().FirstOrDefault(window => window is MainWindow) as MainWindow;
            mainWindow.isRook = true;
            choiceMade = true;
            Close();
        }

        private void queen_Click(object sender, RoutedEventArgs e)
        {
            var mainWindow = Application.Current.Windows.Cast<Window>().FirstOrDefault(window => window is MainWindow) as MainWindow;
            mainWindow.isQueen = true;
            choiceMade = true;
            Close();
        }
    }
}
