using System;
using System.Collections.Generic;
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
    /// Interaction logic for Configuration.xaml
    /// </summary>
    public partial class Configuration : Window
    {

        List<Color> darkSquareColorList = new List<Color>();
        List<Color> lightSquareColorList = new List<Color>();
        Brush color1, color2;
        string letter, number;
        public Configuration()
        {
            InitializeComponent();

            //populate dark sq combo
            darkSquareColorList.Add(new Color { Photo = "Resources/colors/black.png" });
            darkSquareColorList.Add(new Color { Photo = "Resources/colors/gray.png" });
            darkSquareColorList.Add(new Color { Photo = "Resources/colors/blue.png" });
            darkSquareColorList.Add(new Color { Photo = "Resources/colors/green.png" });
            darkSquareColorList.Add(new Color { Photo = "Resources/colors/red.png" });
            DarkSquareColorcomboBox.ItemsSource = darkSquareColorList;
            DarkSquareColorcomboBox.SelectedIndex = 0;

            //populate light sq combo
            lightSquareColorList.Add(new Color { Photo = "Resources/colors/white.png" });
            lightSquareColorList.Add(new Color { Photo = "Resources/colors/orange.png" });
            lightSquareColorList.Add(new Color { Photo = "Resources/colors/pink.png" });
            lightSquareColorList.Add(new Color { Photo = "Resources/colors/violet.png" });
            lightSquareColorList.Add(new Color { Photo = "Resources/colors/yellow.png" });
            LightSquareColorcomboBox.ItemsSource = lightSquareColorList;
            LightSquareColorcomboBox.SelectedIndex = 0;


        }

        private void button1_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void button_Click(object sender, RoutedEventArgs e)
        {

            var mainWindow = Application.Current.Windows.Cast<Window>().FirstOrDefault(window => window is MainWindow) as MainWindow;

            

            switch (DarkSquareColorcomboBox.SelectedIndex)
            {
                case 0:
                    color1 = Brushes.Black;
                    break;
                case 1:
                    color1 = Brushes.Gray;
                    break;
                case 2:
                    color1 = Brushes.Blue;
                    break;
                case 3:
                    color1 = Brushes.Green;
                    break;
                case 4:
                    color1 = Brushes.Red;
                    break;
            }

            switch (LightSquareColorcomboBox.SelectedIndex)
            {
                case 0:
                    color2 = Brushes.White;
                    break;
                case 1:
                    color2 = Brushes.Orange;
                    break;
                case 2:
                    color2 = Brushes.Pink;
                    break;
                case 3:
                    color2 = Brushes.Violet;
                    break;
                case 4:
                    color2 = Brushes.Yellow;
                    break;
            }

            if (defaultBackgroundradioButton.IsChecked == true)
            {
                mainWindow.gameGrid.Background = ((ImageBrush)this.Resources["BackgroundTexture"]); 
            }else if (classyBackgroundradioButton.IsChecked == true)
            {
                mainWindow.gameGrid.Background = ((ImageBrush)this.Resources["BackgroundTexture2"]);
            }else if (spaceBackgroundradioButton.IsChecked == true)
            {
                mainWindow.gameGrid.Background = ((ImageBrush)this.Resources["BackgroundTexture1"]);
            }

            if (defaultPieceThemeradioButton.IsChecked == true)
            {
                foreach(pieceButton button in mainWindow.board.Children)
                {
                    if (button.Type.Equals("P1Pawn"))
                    {
                        button.Image = ((Image)this.Resources["P1Pawn"]).Source;                        
                    }
                    else if (button.Type.Equals("P1Rook"))
                    {
                        button.Image = ((Image)this.Resources["P1Rook"]).Source;
                    }else if (button.Type.Equals("P1Bishop"))
                    {
                        button.Image = ((Image)this.Resources["P1Bishop"]).Source;
                    }
                    else if (button.Type.Equals("P1Knight"))
                    {
                        button.Image = ((Image)this.Resources["P1Knight"]).Source;
                    }
                    else if (button.Type.Equals("P1Queen"))
                    {
                        button.Image = ((Image)this.Resources["P1Queen"]).Source;
                    }
                    else if (button.Type.Equals("P1King"))
                    {
                        button.Image = ((Image)this.Resources["P1King"]).Source;
                    }else if (button.Type.Equals("P2Pawn"))
                    {
                        button.Image = ((Image)this.Resources["P2Pawn"]).Source;
                    }
                    else if (button.Type.Equals("P2Rook"))
                    {
                        button.Image = ((Image)this.Resources["P2Rook"]).Source;
                    }
                    else if (button.Type.Equals("P2Bishop"))
                    {
                        button.Image = ((Image)this.Resources["P2Bishop"]).Source;
                    }
                    else if (button.Type.Equals("P2Knight"))
                    {
                        button.Image = ((Image)this.Resources["P2Knight"]).Source;
                    }
                    else if (button.Type.Equals("P2Queen"))
                    {
                        button.Image = ((Image)this.Resources["P2Queen"]).Source;
                    }
                    else if (button.Type.Equals("P2King"))
                    {
                        button.Image = ((Image)this.Resources["P2King"]).Source;
                    }
                }
            }
            else if (classyPieceThemeradioButton.IsChecked == true)
            {
                foreach (pieceButton button in mainWindow.board.Children)
                {
                    if (button.Type.Equals("P1PawnClassy"))
                    {
                        button.Image = ((Image)this.Resources["P1PawnClassy"]).Source;
                    }
                    else if (button.Type.Equals("P1RookClassy"))
                    {
                        button.Image = ((Image)this.Resources["P1RookClassy"]).Source;
                    }
                    else if (button.Type.Equals("P1BishopClassy"))
                    {
                        button.Image = ((Image)this.Resources["P1BishopClassy"]).Source;
                    }
                    else if (button.Type.Equals("P1KnightClassy"))
                    {
                        button.Image = ((Image)this.Resources["P1KnightClassy"]).Source;
                    }
                    else if (button.Type.Equals("P1QueenClassy"))
                    {
                        button.Image = ((Image)this.Resources["P1QueenClassy"]).Source;
                    }
                    else if (button.Type.Equals("P1KingClassy"))
                    {
                        button.Image = ((Image)this.Resources["P1KingClassy"]).Source;
                    }
                    else if (button.Type.Equals("P2PawnClassy"))
                    {
                        button.Image = ((Image)this.Resources["P2PawnClassy"]).Source;
                    }
                    else if (button.Type.Equals("P2RookClassy"))
                    {
                        button.Image = ((Image)this.Resources["P2RookClassy"]).Source;
                    }
                    else if (button.Type.Equals("P2BishopClassy"))
                    {
                        button.Image = ((Image)this.Resources["P2BishopClassy"]).Source;
                    }
                    else if (button.Type.Equals("P2KnightClassy"))
                    {
                        button.Image = ((Image)this.Resources["P2KnightClassy"]).Source;
                    }
                    else if (button.Type.Equals("P2QueenClassy"))
                    {
                        button.Image = ((Image)this.Resources["P2QueenClassy"]).Source;
                    }
                    else if (button.Type.Equals("P2KingClassy"))
                    {
                        button.Image = ((Image)this.Resources["P2KingClassy"]).Source;
                    }
                }
            }
            else if (spookyPieceThemeradioButton.IsChecked == true)
            {
                foreach (Chess.pieceButton button in mainWindow.board.Children)
                {
                    if (button.Type.Equals("P1PawnSpook"))
                    {
                        button.Image = ((Image)this.Resources["P1PawnSpook"]).Source;
                    }
                    else if (button.Type.Equals("P1RookSpook"))
                    {
                        button.Image = ((Image)this.Resources["P1RookSpook"]).Source;
                    }
                    else if (button.Type.Equals("P1BishopSpook"))
                    {
                        button.Image = ((Image)this.Resources["P1BishopSpook"]).Source;
                    }
                    else if (button.Type.Equals("P1KnightSpook"))
                    {
                        button.Image = ((Image)this.Resources["P1KnightSpook"]).Source;
                    }
                    else if (button.Type.Equals("P1QueenSpook"))
                    {
                        button.Image = ((Image)this.Resources["P1QueenSpook"]).Source;
                    }
                    else if (button.Type.Equals("P1KingSpook"))
                    {
                        button.Image = ((Image)this.Resources["P1KingSpook"]).Source;
                    }
                    else if (button.Type.Equals("P2PawnSpook"))
                    {
                        button.Image = ((Image)this.Resources["P2PawnSpook"]).Source;
                    }
                    else if (button.Type.Equals("P2RookSpook"))
                    {
                        button.Image = ((Image)this.Resources["P2RookSpook"]).Source;
                    }
                    else if (button.Type.Equals("P2BishopSpook"))
                    {
                        button.Image = ((Image)this.Resources["P2BishopSpook"]).Source;
                    }
                    else if (button.Type.Equals("P2KnightSpook"))
                    {
                        button.Image = ((Image)this.Resources["P2KnightSpook"]).Source;
                    }
                    else if (button.Type.Equals("P2QueenSpook"))
                    {
                        button.Image = ((Image)this.Resources["P2QueenSpook"]).Source;
                    }
                    else if (button.Type.Equals("P2KingSpook"))
                    {
                        button.Image = ((Image)this.Resources["P2KingSpook"]).Source;
                    }
                }
            }

            

            foreach (Chess.pieceButton button in mainWindow.board.Children)
            {
                letter = button.Name.Substring(0, 1);
                number = button.Name.Substring(1);
                if (((letter.Equals("a") || letter.Equals("c") || letter.Equals("e") || letter.Equals("g"))
                   && (number.Equals("8") || number.Equals("6") || number.Equals("4") || number.Equals("2")))
                   || ((letter.Equals("b") || letter.Equals("d") || letter.Equals("f") || letter.Equals("h"))
                   && (number.Equals("7") || number.Equals("5") || number.Equals("3") || number.Equals("1"))))
                {
                    button.BackgroundColor = color2;
                    button.originalColor = color2;
                }
                else
                {
                    button.BackgroundColor = color1;
                    button.originalColor = color1;
                }
            }

            Close();

        }

        //color class for comboboxes
        public class Color
        {
            public String Photo { get; set; }
        }
    }
}
