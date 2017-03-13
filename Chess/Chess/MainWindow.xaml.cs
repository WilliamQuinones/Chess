using System;
using System.Collections.Generic;
using System.Diagnostics;
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
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Threading;

namespace Chess
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    /// 


    public partial class MainWindow : Window
    {


        //used to calculate how pieces interact with board
        public enum Board
        {
            a1 = 1, b1, c1, d1, e1, f1, g1, h1,
            a2, b2, c2, d2, e2, f2, g2, h2,
            a3, b3, c3, d3, e3, f3, g3, h3,
            a4, b4, c4, d4, e4, f4, g4, h4,
            a5, b5, c5, d5, e5, f5, g5, h5,
            a6, b6, c6, d6, e6, f6, g6, h6,
            a7, b7, c7, d7, e7, f7, g7, h7,
            a8, b8, c8, d8, e8, f8, g8, h8
        }

        string letter, number;

        ImageSource img;


        public DispatcherTimer dt = new DispatcherTimer();
        public Stopwatch sw = new Stopwatch();
        string currentTime = string.Empty;

        //timer for each turn
        void dt_Tick(object sender, EventArgs e)
        {
            if (sw.IsRunning)
            {
                TimeSpan ts = sw.Elapsed;
                currentTime = String.Format("{0:00}:{1:00}:{2:00}",
                ts.Minutes, ts.Seconds, ts.Milliseconds / 10);
                timer.Content = currentTime;
            }
        }

        private void MenuItem_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        public bool isKnight = false, isBishop = false, isRook = false, isQueen = false;

        private void MenuItem_Click_1(object sender, RoutedEventArgs e)
        {
            Configuration popup = new Configuration();
            popup.ShowDialog();
        }

        public MainWindow()
        {
            
            InitializeComponent();

        

            dt.Tick += new EventHandler(dt_Tick);
            dt.Interval = new TimeSpan(0, 0, 0, 0, 1);

            sw.Start();
            dt.Start();


            //global vars
            Application.Current.Properties["Turn"] = 1;

            Application.Current.Properties["P1PawnsCaptured"] = 0;
            Application.Current.Properties["P1KnightsCaptured"] = 0;
            Application.Current.Properties["P1BishopsCaptured"] = 0;
            Application.Current.Properties["P1RooksCaptured"] = 0;
            Application.Current.Properties["P1QueensCaptured"] = 0;
            Application.Current.Properties["P2PawnsCaptured"] = 0;
            Application.Current.Properties["P2KnightsCaptured"] = 0;
            Application.Current.Properties["P2BishopsCaptured"] = 0;
            Application.Current.Properties["P2RooksCaptured"] = 0;
            Application.Current.Properties["P2QueensCaptured"] = 0;

            //Background color brushes
            Brush color1 = Brushes.White;
            Brush color2 = Brushes.Black;
            Brush color3 = Brushes.Red;

            //Player 1 pieces
            h1.Type = "P1Rook";
            g1.Type = "P1Knight";
            f1.Type = "P1Bishop";
            e1.Type = "P1King";
            d1.Type = "P1Queen";
            c1.Type = "P1Bishop";
            b1.Type = "P1Knight";
            a1.Type = "P1Rook";
            a2.Type = "P1Pawn";            b2.Type = "P1Pawn";            c2.Type = "P1Pawn";            d2.Type = "P1Pawn";
            e2.Type = "P1Pawn";            f2.Type = "P1Pawn";            g2.Type = "P1Pawn";            h2.Type = "P1Pawn";

            //Player 2 pieces
            a8.Type = "P2Rook";
            g8.Type = "P2Knight";
            f8.Type = "P2Bishop";
            e8.Type = "P2King";
            d8.Type = "P2Queen";
            c8.Type = "P2Bishop";
            b8.Type = "P2Knight";
            a8.Type = "P2Rook";
            h8.Type = "P2Rook";
            a7.Type = "P2Pawn";            b7.Type = "P2Pawn";            c7.Type = "P2Pawn";            d7.Type = "P2Pawn";
            e7.Type = "P2Pawn";            f7.Type = "P2Pawn";            g7.Type = "P2Pawn";            h7.Type = "P2Pawn";

            foreach (Chess.pieceButton button in board.Children)
                {

                //Set initial game pieces, resource names need to be bound to vars
                    if (button.Type.Equals("P1Pawn"))
                    {
                        img = ((Image)this.Resources["P1Pawn"]).Source;
                        button.Image = img;
                        button.Text = "pawn";
                    }else if (button.Type.Equals("P1Rook"))
                    {
                        img = ((Image)this.Resources["P1Rook"]).Source;
                        button.Image = img;
                        button.Text = "rook";
                    }else if (button.Type.Equals("P2Rook"))
                    {
                        img = ((Image)this.Resources["P2Rook"]).Source;
                        button.Image = img;
                        button.Text = "rook";
                    }else if (button.Type.Equals("P2Pawn"))
                    {
                        img = ((Image)this.Resources["P2Pawn"]).Source;
                        button.Image = img;
                        button.Text = "pawn";
                    }else if (button.Type.Equals("P1Bishop"))
                    {
                        img = ((Image)this.Resources["P1Bishop"]).Source;
                        button.Image = img;
                        button.Text = "bishop";
                    }else if (button.Type.Equals("P2Bishop"))
                    {
                        img = ((Image)this.Resources["P2Bishop"]).Source;
                        button.Image = img;
                        button.Text = "bishop";
                    }else if (button.Type.Equals("P2Knight"))
                    {
                        img = ((Image)this.Resources["P2Knight"]).Source;
                        button.Image = img;
                        button.Text = "knight";
                    }else if (button.Type.Equals("P1Knight"))
                    {
                        img = ((Image)this.Resources["P1Knight"]).Source;
                        button.Image = img;
                        button.Text = "knight";
                    }else if (button.Type.Equals("P2Queen"))
                    {
                        img = ((Image)this.Resources["P2Queen"]).Source;
                        button.Image = img;
                        button.Text = "queen";
                    }else if (button.Type.Equals("P2King"))
                    {
                        img = ((Image)this.Resources["P2King"]).Source;
                        button.Image = img;
                        button.Text = "king";
                    }else if (button.Type.Equals("P1Queen"))
                    {
                        img = ((Image)this.Resources["P1Queen"]).Source;
                        button.Image = img;
                        button.Text = "queen";
                    }else if (button.Type.Equals("P1King"))
                    {
                        img = ((Image)this.Resources["P1King"]).Source;
                        button.Image = img;
                        button.Text = "king";
                    }

                    //setup board
                    letter = button.Name.Substring(0, 1);
                    number = button.Name.Substring(1);
                    if (((letter.Equals("a") || letter.Equals("c") || letter.Equals("e") || letter.Equals("g"))
                       && (number.Equals("8") || number.Equals("6") || number.Equals("4") || number.Equals("2")))
                       || ((letter.Equals("b") || letter.Equals("d") || letter.Equals("f") || letter.Equals("h"))
                       && (number.Equals("7") || number.Equals("5") || number.Equals("3") || number.Equals("1"))))
                    {
                        button.BackgroundColor = color1;
                        button.originalColor = color1;
                    }
                    else
                    {
                        button.BackgroundColor = color2;
                        button.originalColor = color2;
                    }


                    //start of game look, can be changed into start/load option before aneabling this

                    button.Opacity = 0.75;
             
                    if (button.Type.Contains("P1"))
                    {
                        button.Opacity = 1.0;
                    }
                }
        }
    }
}
