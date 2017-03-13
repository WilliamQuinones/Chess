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
using System.Windows.Navigation;
using System.Windows.Shapes;
using static Chess.MainWindow;

namespace Chess
{
    /// <summary>
    /// Interaction logic for pieceButton.xaml
    /// </summary>
    public partial class pieceButton : UserControl
    {

        //vars used in move calculations
        Board legalMove, currPos, temp;
        List<Board> legalMoveList = new List<Board>();
        List<Board> legalMovesKnight = new List<Board>();
        public string tempName, playerTurn, updateHistory, currPosLetter, currPosNumber, currPosString;
        public string p1turn = "Player 1 turn", p2turn = "Player 2 turn";
        int left, right, up, down;


        //brush used to highlight legal, possible moves
        Brush color = Brushes.Green;

        //custom button properties
        #region
        public pieceButton()
        {
            InitializeComponent();

        }

        public ImageSource Image
        {
            get { return (ImageSource)GetValue(ImageProperty); }
            set { SetValue(ImageProperty, value); }
        }

        public static DependencyProperty ImageProperty =
            DependencyProperty.Register("Image", typeof(ImageSource), typeof(pieceButton), new UIPropertyMetadata(null));

        public double ImageWidth
        {
            get { return (double)GetValue(ImageWidthProperty); }
            set { SetValue(ImageWidthProperty, value); }
        }


        public static DependencyProperty ImageWidthProperty =
            DependencyProperty.Register("ImageWidth", typeof(double), typeof(pieceButton), new UIPropertyMetadata(16d));

        public double ImageHeight
        {
            get { return (double)GetValue(ImageHeightProperty); }
            set { SetValue(ImageHeightProperty, value); }
        }


        public static DependencyProperty ImageHeightProperty =
            DependencyProperty.Register("ImageHeight", typeof(double), typeof(pieceButton), new UIPropertyMetadata(16d));

        public string Text
        {
            get { return (string)GetValue(TextProperty); }
            set { SetValue(TextProperty, value); }
        }


        public static DependencyProperty TextProperty =
            DependencyProperty.Register("Text", typeof(string), typeof(pieceButton), new UIPropertyMetadata(""));

        public string Type
        {
            get { return (string)GetValue(TypeProperty); }
            set { SetValue(TypeProperty, value); }
        }


        public static DependencyProperty TypeProperty =
            DependencyProperty.Register("Type", typeof(string), typeof(pieceButton), new UIPropertyMetadata(""));

        public Brush BackgroundColor
        {
            get { return (Brush)GetValue(BackgroundColorProperty); }
            set { SetValue(BackgroundColorProperty, value); }
        }


        public static DependencyProperty BackgroundColorProperty =
            DependencyProperty.Register("ButtonBackground", typeof(Brush), typeof(pieceButton), new UIPropertyMetadata(null));

        public Boolean isPotentialMove
        {
            get { return (Boolean)GetValue(PotentialMoveProperty); }
            set { SetValue(PotentialMoveProperty, value); }
        }
        public static DependencyProperty PotentialMoveProperty =
            DependencyProperty.Register("currentlyPotentialMove", typeof(Boolean), typeof(pieceButton), new UIPropertyMetadata(false));

        public Brush originalColor
        {
            get { return (Brush)GetValue(OriginalColorProperty); }
            set { SetValue(OriginalColorProperty, value); }
        }
        public static DependencyProperty OriginalColorProperty =
            DependencyProperty.Register("originalBackground", typeof(Brush), typeof(pieceButton), new UIPropertyMetadata(null));

        private void button_MouseEnter(object sender, MouseEventArgs e)
        {
            var mainWindow = Application.Current.Windows.Cast<Window>().FirstOrDefault(window => window is MainWindow) as MainWindow;
            mainWindow.currentPosition.Content = Name;
        }

        private void button_MouseLeave(object sender, MouseEventArgs e)
        {
            var mainWindow = Application.Current.Windows.Cast<Window>().FirstOrDefault(window => window is MainWindow) as MainWindow;
            mainWindow.currentPosition.Content = "";
        }

        #endregion 



        //main driver of program
        //the main logic is that on a given players turn, their game pieces will be the highest opacity, when a move is made,
        //their pieces' opacity will lower and the other player's pieces opacity will be set to highest
        //when button is clicked, it will check if it was a highlighted, legal move, and then check if that move is an empty space
        //or capturing a piece, otherwise it will check if it is a game piece and then calculate movement and focus onto it
        private void button_Click(object sender, RoutedEventArgs e)
        {

            var mainWindow = Application.Current.Windows.Cast<Window>().FirstOrDefault(window => window is MainWindow) as MainWindow;

            //logic for if button was a highlighted, legal move when clicked
            #region
            //if green button is clicked
            if (isPotentialMove == true)
            {

                var turn = Application.Current.Properties["Turn"];
                int Turn = (int)turn;
                Turn++;
                try { Application.Current.Properties["Turn"] = Turn; } catch { }

                //reset turn timer
                mainWindow.sw.Reset();
                mainWindow.timer.Content = "00:00:00";
                mainWindow.sw.Start();


                foreach (Chess.pieceButton button in mainWindow.board.Children)
                {
                    if (button.Opacity == 1.0) //this is how current player turn is calculated
                    {
                        playerTurn = button.Type;
                        tempName = button.Name;
                        button.Opacity = 0.75;
                    }

                    

                }

                //this block will flip player turns by making the other teams pieces less opaque
                if (playerTurn.Contains("P1"))
                {
                    foreach (Chess.pieceButton but in mainWindow.board.Children)
                    {
                        if (but.Type.Contains("P2"))
                        {
                            but.Opacity = 1.0;
                        }
                    }
                }
                else if (playerTurn.Contains("P2"))
                {
                    foreach (Chess.pieceButton but in mainWindow.board.Children)
                    {
                        if (but.Type.Contains("P1"))
                        {
                            but.Opacity = 1.0;
                        }
                    }
                }


                foreach (Chess.pieceButton button in mainWindow.board.Children) 
                {
                   

                        //translate piece to new space, as well as updated captured pieces field and move history field
                    if (button.Name.Equals(tempName))
                    {
                       
                        
                        string x = turn.ToString();
                        

                        if ((int)turn > 99)
                        {
                            x += "                ";
                        }
                        else if ((int)turn > 9)
                        {
                            x += "                  ";
                        }
                        else
                        {
                            x += "                    ";
                        }

                        

                        if (playerTurn.Contains("P1"))
                        {
                            x += "P1";
                            mainWindow.labelPlayerTurn.Content = p2turn;

                            if(Text != null)
                            {
                                if (Text.Equals("pawn"))
                                {
                                    var pieces = Application.Current.Properties["P1PawnsCaptured"];
                                    int Pieces = (int)pieces;
                                    Pieces++;
                                    Application.Current.Properties["P1PawnsCaptured"] = Pieces;
                                    if (Pieces == 1)
                                    {
                                        mainWindow.p1CaptureLabelPawn.Content = Pieces + " pawn";
                                    }
                                    else
                                    {
                                        mainWindow.p1CaptureLabelPawn.Content = Pieces + " pawns";
                                    }

                                }
                                else if (Text.Equals("knight"))
                                {
                                    var pieces = Application.Current.Properties["P1KnightsCaptured"];
                                    int Pieces = (int)pieces;
                                    Pieces++;
                                    Application.Current.Properties["P1KnightsCaptured"] = Pieces;
                                    if (Pieces == 1)
                                    {
                                        mainWindow.p1CaptureLabelKnight.Content = Pieces + " knight";
                                    }
                                    else
                                    {
                                        mainWindow.p1CaptureLabelKnight.Content = Pieces + " knights";
                                    }
                                }
                                else if (Text.Equals("bishop"))
                                {
                                    var pieces = Application.Current.Properties["P1BishopsCaptured"];
                                    int Pieces = (int)pieces;
                                    Pieces++;
                                    Application.Current.Properties["P1BishopsCaptured"] = Pieces;
                                    if (Pieces == 1)
                                    {
                                        mainWindow.p1CaptureLabelBishop.Content = Pieces + " bishop";
                                    }
                                    else
                                    {
                                        mainWindow.p1CaptureLabelBishop.Content = Pieces + " bishops";
                                    }
                                }
                                else if (Text.Equals("rook"))
                                {
                                    var pieces = Application.Current.Properties["P1RooksCaptured"];
                                    int Pieces = (int)pieces;
                                    Pieces++;
                                    Application.Current.Properties["P1RooksCaptured"] = Pieces;
                                    if (Pieces == 1)
                                    {
                                        mainWindow.p1CaptureLabelRook.Content = Pieces + " rook";
                                    }
                                    else
                                    {
                                        mainWindow.p1CaptureLabelRook.Content = Pieces + " rooks";
                                    }
                                }
                                else if (Text.Equals("queen"))
                                {
                                    var pieces = Application.Current.Properties["P1QueensCaptured"];
                                    int Pieces = (int)pieces;
                                    Pieces++;
                                    Application.Current.Properties["P1QueensCaptured"] = Pieces;
                                    if (Pieces == 1)
                                    {
                                        mainWindow.p1CaptureLabelQueen.Content = Pieces + " queen";
                                    }
                                    else
                                    {
                                        mainWindow.p1CaptureLabelQueen.Content = Pieces + " queens";
                                    }
                                }//checkmate
                                else if (Text.Equals("king"))
                                {
                                    mainWindow.labelPlayerTurn.Content = "Checkmate!";
                                    mainWindow.sw.Stop();
                                    Checkmate popup = new Checkmate();
                                    popup.ShowDialog();
                                }
                            }

                            

                        }
                        else if (playerTurn.Contains("P2"))
                        {
                            x += "P2";
                            mainWindow.labelPlayerTurn.Content = p1turn;
                            if (Text != null)
                            {
                                if (Text.Equals("pawn"))
                                {
                                    var pieces = Application.Current.Properties["P2PawnsCaptured"];
                                    int Pieces = (int)pieces;
                                    Pieces++;
                                    Application.Current.Properties["P2PawnsCaptured"] = Pieces;
                                    if (Pieces == 1)
                                    {
                                        mainWindow.p2CaptureLabelPawn.Content = Pieces + " pawn";
                                    }
                                    else
                                    {
                                        mainWindow.p2CaptureLabelPawn.Content = Pieces + " pawns";
                                    }
                                }
                                else if (Text.Equals("knight"))
                                {
                                    var pieces = Application.Current.Properties["P2KnightsCaptured"];
                                    int Pieces = (int)pieces;
                                    Pieces++;
                                    Application.Current.Properties["P2KnightsCaptured"] = Pieces;
                                    if (Pieces == 1)
                                    {
                                        mainWindow.p2CaptureLabelKnight.Content = Pieces + " knight";
                                    }
                                    else
                                    {
                                        mainWindow.p2CaptureLabelKnight.Content = Pieces + " knights";
                                    }
                                }
                                else if (Text.Equals("bishop"))
                                {
                                    var pieces = Application.Current.Properties["P2BishopsCaptured"];
                                    int Pieces = (int)pieces;
                                    Pieces++;
                                    Application.Current.Properties["P2BishopsCaptured"] = Pieces;
                                    if (Pieces == 1)
                                    {
                                        mainWindow.p2CaptureLabelBishop.Content = Pieces + " bishop";
                                    }
                                    else
                                    {
                                        mainWindow.p2CaptureLabelBishop.Content = Pieces + " bishops";
                                    }
                                }
                                else if (Text.Equals("rook"))
                                {
                                    var pieces = Application.Current.Properties["P2RooksCaptured"];
                                    int Pieces = (int)pieces;
                                    Pieces++;
                                    Application.Current.Properties["P2RooksCaptured"] = Pieces;
                                    if (Pieces == 1)
                                    {
                                        mainWindow.p2CaptureLabelRook.Content = Pieces + " rook";
                                    }
                                    else
                                    {
                                        mainWindow.p2CaptureLabelRook.Content = Pieces + " rooks";
                                    }
                                }
                                else if (Text.Equals("queen"))
                                {
                                    var pieces = Application.Current.Properties["P2QueensCaptured"];
                                    int Pieces = (int)pieces;
                                    Pieces++;
                                    Application.Current.Properties["P2QueensCaptured"] = Pieces;
                                    if (Pieces == 1)
                                    {
                                        mainWindow.p2CaptureLabelQueen.Content = Pieces + " queen";
                                    }
                                    else
                                    {
                                        mainWindow.p2CaptureLabelQueen.Content = Pieces + " queens";
                                    }
                                }
                                else if (Text.Equals("king"))
                                {
                                    mainWindow.labelPlayerTurn.Content = "Checkmate!";
                                    mainWindow.sw.Stop();
                                    Checkmate popup = new Checkmate();
                                    popup.ShowDialog();
                                }
                            }
                            
                        }

                        

                        Image = button.Image;
                        Text = button.Text;
                        Type = button.Type;
                        button.Type = "";
                        button.Image = null;
                        button.Text = null;

                        x += "                " + Text + " to " + Name;

                        mainWindow.moveHistory.Text += x + "\n";
                        
                        //logic to promote pawn if landed on top or bottom edge of board
                        if (Name.Substring(1).Equals("8"))
                        {
                            if (Type.Equals("P1Pawn"))
                            {
                                Promotion popup = new Promotion();
                                popup.ShowDialog();

                                if (mainWindow.isBishop)
                                {
                                    Image = ((Image)this.Resources["P1Bishop"]).Source;
                                    Text = "bishop";
                                    Type = "P1Bishop";
                                    mainWindow.isBishop = false;
                                }
                                else if (mainWindow.isKnight)
                                {
                                    Image = ((Image)this.Resources["P1Knight"]).Source;
                                    Text = "knight";
                                    Type = "P1Knight";
                                    mainWindow.isKnight = false;
                                }
                                else if (mainWindow.isRook)
                                {
                                    Image = ((Image)this.Resources["P1Rook"]).Source;
                                    Text = "rook";
                                    Type = "P1Rook";
                                    mainWindow.isRook = false;
                                }
                                else if (mainWindow.isQueen)
                                {
                                    Image = ((Image)this.Resources["P1Queen"]).Source;
                                    Text = "queen";
                                    Type = "P1Queen";
                                    mainWindow.isQueen = false;
                                }
                            }                         
                        }
                        if (Name.Substring(1).Equals("1"))
                        {
                            if (Type.Equals("P2Pawn"))
                            {
                                Promotion popup = new Promotion();
                                popup.ShowDialog();

                                if (mainWindow.isBishop)
                                {
                                    Image = ((Image)this.Resources["P2Bishop"]).Source;
                                    Text = "bishop";
                                    Type = "P2Bishop";
                                    mainWindow.isBishop = false;
                                }
                                else if (mainWindow.isKnight)
                                {
                                    Image = ((Image)this.Resources["P2Knight"]).Source;
                                    Text = "knight";
                                    Type = "P2Knight";
                                    mainWindow.isKnight = false;
                                }
                                else if (mainWindow.isRook)
                                {
                                    Image = ((Image)this.Resources["P2Rook"]).Source;
                                    Text = "rook";
                                    Type = "P2Rook";
                                    mainWindow.isRook = false;
                                }
                                else if (mainWindow.isQueen)
                                {
                                    Image = ((Image)this.Resources["P2Queen"]).Source;
                                    Text = "queen";
                                    Type = "P2Queen";
                                    mainWindow.isQueen = false;
                                }

                            }

                        }



                    }

                    //reset all potential move buttons to their original state
                    if (button.isPotentialMove)
                    {
                        button.BackgroundColor = button.originalColor;
                        button.isPotentialMove = false;
                    }

                    Opacity = 0.75;

                }
                #endregion
            }
            
            else
            {   //logic if button was a game piece, calculations for legal moves done here
                if (Opacity == 1.0) //turns are calculated based on which piece has highest opacity, only pieces of correct turn will be
                                    //affected by button press to make move calculations
                {
                    //logic for pawn pieces
                    #region
                    //calculate legal moves if piece button is clicked
                    if (Type.Equals("P1Pawn"))
                    {

                        currPos = (Board)Enum.Parse(typeof(Board), Name);


                        //standard move case
                        legalMove = currPos + 8;


                        //check if theres a piece in front of pawn
                        foreach (pieceButton button in mainWindow.board.Children)
                        {
                            if (button.Name.Equals(legalMove.ToString()))
                            {
                                if (button.Type.Equals(""))
                                {
                                    legalMoveList.Add(legalMove);
                                }
                            }
                        }

                        //first time moving pawn bonus
                        if (currPos == Board.a2 || currPos == Board.b2 || currPos == Board.c2 || currPos == Board.d2 ||
                            currPos == Board.e2 || currPos == Board.f2 || currPos == Board.g2 || currPos == Board.h2)
                        {
                            legalMove = currPos + 16;
                            foreach (pieceButton button in mainWindow.board.Children)
                            {
                                if (button.Name.Equals(legalMove.ToString()))
                                {
                                    if (button.Type.Equals(""))
                                    {
                                        legalMoveList.Add(legalMove);
                                    }
                                }
                            }
                        }

                        //check if theres a piece to capture
                        //edge of board cases for capturing
                        if ((int)legalMove % 8 == 0) //right side of board
                        {
                            legalMove = currPos + 7;
                            foreach (pieceButton button in mainWindow.board.Children)
                            {
                                if (button.Name.Equals(legalMove.ToString()))
                                {
                                    if (!button.Type.Equals("") && button.Type.Contains("P2"))
                                    {
                                        legalMoveList.Add(legalMove);
                                    }
                                }
                            }
                        }
                        else if ((int)legalMove == 17 || (int)legalMove == 25 || (int)legalMove == 33 || (int)legalMove == 41 ||
                           (int)legalMove == 49 || (int)legalMove == 57) //left side of board
                        {
                            legalMove = currPos + 9;
                            foreach (pieceButton button in mainWindow.board.Children)
                            {
                                if (button.Name.Equals(legalMove.ToString()))
                                {
                                    if (!button.Type.Equals("") && button.Type.Contains("P2"))
                                    {
                                        legalMoveList.Add(legalMove);
                                    }
                                }
                            }
                        }
                        else //pieces inside of board
                        {
                            legalMove = currPos + 9;
                            foreach (pieceButton button in mainWindow.board.Children)
                            {
                                if (button.Name.Equals(legalMove.ToString()))
                                {
                                    if (!button.Type.Equals("") && button.Type.Contains("P2"))
                                    {
                                        legalMoveList.Add(legalMove);
                                    }
                                }
                            }
                            legalMove = currPos + 7;
                            foreach (pieceButton button in mainWindow.board.Children)
                            {
                                if (button.Name.Equals(legalMove.ToString()))
                                {
                                    if (!button.Type.Equals("") && button.Type.Contains("P2"))
                                    {
                                        legalMoveList.Add(legalMove);
                                    }
                                }
                            }
                        }

                        foreach (Board space in legalMoveList)
                        {

                            tempName = space.ToString();

                            foreach (pieceButton button in mainWindow.board.Children)
                            {
                                if (tempName.Equals(button.Name))
                                {
                                    button.BackgroundColor = color;
                                    button.isPotentialMove = true;
                                    button.Opacity = 0.9;
                                }
                            }
                        }

                        legalMoveList.Clear();

                    }
                    else if (Type.Equals("P2Pawn"))
                    {

                        currPos = (Board)Enum.Parse(typeof(Board), Name);

                        legalMove = currPos - 8;

                        foreach (pieceButton button in mainWindow.board.Children)
                        {
                            if (button.Name.Equals(legalMove.ToString()))
                            {
                                if (button.Type.Equals(""))
                                {
                                    legalMoveList.Add(legalMove);
                                }
                            }
                        }


                        //first time moving pawn bonus
                        if (currPos == Board.a7 || currPos == Board.b7 || currPos == Board.c7 || currPos == Board.d7 ||
                            currPos == Board.e7 || currPos == Board.f7 || currPos == Board.g7 || currPos == Board.h7)
                        {
                            legalMove = currPos - 16;
                            foreach (pieceButton button in mainWindow.board.Children)
                            {
                                if (button.Name.Equals(legalMove.ToString()))
                                {
                                    if (button.Type.Equals(""))
                                    {
                                        legalMoveList.Add(legalMove);
                                    }
                                }
                            }
                        }


                        //check if theres a piece to capture
                        //edge of board cases for capturing
                        if ((int)legalMove % 8 == 0) //right side of board
                        {
                            legalMove = currPos - 9;
                            foreach (pieceButton button in mainWindow.board.Children)
                            {
                                if (button.Name.Equals(legalMove.ToString()))
                                {
                                    if (!button.Type.Equals("") && button.Type.Contains("P1"))
                                    {
                                        legalMoveList.Add(legalMove);
                                    }
                                }
                            }
                        }
                        else if ((int)legalMove == 9 || (int)legalMove == 17 || (int)legalMove == 25 || (int)legalMove == 33 || (int)legalMove == 41 ||
                           (int)legalMove == 49 || (int)legalMove == 57) //left side of board
                        {
                            legalMove = currPos - 7;
                            foreach (pieceButton button in mainWindow.board.Children)
                            {
                                if (button.Name.Equals(legalMove.ToString()))
                                {
                                    if (!button.Type.Equals("") && button.Type.Contains("P1"))
                                    {
                                        legalMoveList.Add(legalMove);
                                    }
                                }
                            }
                        }
                        else //pieces inside of board
                        {
                            legalMove = currPos - 9;
                            foreach (pieceButton button in mainWindow.board.Children)
                            {
                                if (button.Name.Equals(legalMove.ToString()))
                                {
                                    if (!button.Type.Equals("") && button.Type.Contains("P1"))
                                    {
                                        legalMoveList.Add(legalMove);
                                    }
                                }
                            }
                            legalMove = currPos - 7;
                            foreach (pieceButton button in mainWindow.board.Children)
                            {
                                if (button.Name.Equals(legalMove.ToString()))
                                {
                                    if (!button.Type.Equals("") && button.Type.Contains("P1"))
                                    {
                                        legalMoveList.Add(legalMove);
                                    }
                                }
                            }
                        }

                        foreach (Board space in legalMoveList)
                        {

                            tempName = space.ToString();


                            foreach (pieceButton button in mainWindow.board.Children)
                            {
                                if (tempName.Equals(button.Name))
                                {
                                    button.BackgroundColor = color;
                                    button.isPotentialMove = true;
                                    button.Opacity = 0.9;
                                }
                            }
                        }

                        legalMoveList.Clear();

                    }
                    #endregion
                    //logic for rook pieces
                    #region
                    else if (Type.Equals("P1Rook"))
                    {

                        currPos = (Board)Enum.Parse(typeof(Board), Name);

                        currPosLetter = Name.Substring(0, 1);
                        currPosNumber = Name.Substring(1);

                        switch (currPosLetter)
                        {
                            case "a":
                                left = 0;
                                right = 7;
                                break;
                            case "b":
                                left = 1;
                                right = 6;
                                break;
                            case "c":
                                left = 2;
                                right = 5;
                                break;
                            case "d":
                                left = 3;
                                right = 4;
                                break;
                            case "e":
                                left = 4;
                                right = 3;
                                break;
                            case "f":
                                left = 5;
                                right = 2;
                                break;
                            case "g":
                                left = 6;
                                right = 1;
                                break;
                            case "h":
                                left = 7;
                                right = 0;
                                break;
                        }

                        switch (currPosNumber)
                        {
                            case "1":
                                down = 0;
                                up = 7;
                                break;
                            case "2":
                                down = 1;
                                up = 6;
                                break;
                            case "3":
                                down = 2;
                                up = 5;
                                break;
                            case "4":
                                down = 3;
                                up = 4;
                                break;
                            case "5":
                                down = 4;
                                up = 3;
                                break;
                            case "6":
                                down = 5;
                                up = 2;
                                break;
                            case "7":
                                down = 6;
                                up = 1;
                                break;
                            case "8":
                                down = 7;
                                up = 0;
                                break;
                        }


                        
                        bool flag = false;


                        for (int i = 1; i <= right; i++)
                        {
                            temp = currPos + i;
                            foreach (pieceButton button in mainWindow.board.Children)
                            {
                                if (button.Type.Equals("") && button.Name.Equals(temp.ToString()))
                                {
                                    legalMoveList.Add((Board)Enum.Parse(typeof(Board), button.Name));

                                }
                                else if (!button.Type.Equals("") && button.Name.Equals(temp.ToString()))
                                {
                                    if (button.Type.Contains("P2"))
                                    {
                                        legalMoveList.Add((Board)Enum.Parse(typeof(Board), button.Name));
                                    }
                                    flag = true;
                                }
                                if (flag) { break; }
                            }
                            if (flag) { break; }
                        }

                        flag = false;


                        for (int i = 1; i <= left; i++)
                        {
                            temp = currPos - i;
                            foreach (pieceButton button in mainWindow.board.Children)
                            {
                                if (button.Type.Equals("") && button.Name.Equals(temp.ToString()))
                                {
                                    legalMoveList.Add((Board)Enum.Parse(typeof(Board), button.Name));

                                }
                                else if (!button.Type.Equals("") && button.Name.Equals(temp.ToString()))
                                {
                                    if (button.Type.Contains("P2"))
                                    {
                                        legalMoveList.Add((Board)Enum.Parse(typeof(Board), button.Name));
                                    }
                                    flag = true;
                                }
                                if (flag) { break; }
                            }
                            if (flag) { break; }
                        }

                        flag = false;

                        for (int i = 1; i <= up; i++)
                        {
                            temp = currPos + i * 8;
                            foreach (pieceButton button in mainWindow.board.Children)
                            {
                                if (button.Type.Equals("") && button.Name.Equals(temp.ToString()))
                                {
                                    legalMoveList.Add((Board)Enum.Parse(typeof(Board), button.Name));

                                }
                                else if (!button.Type.Equals("") && button.Name.Equals(temp.ToString()))
                                {
                                    if (button.Type.Contains("P2"))
                                    {
                                        legalMoveList.Add((Board)Enum.Parse(typeof(Board), button.Name));
                                    }
                                    flag = true;
                                }
                                if (flag) { break; }
                            }
                            if (flag) { break; }
                        }

                        flag = false;

                        for (int i = 1; i <= down; i++)
                        {
                            temp = currPos - i * 8;
                            foreach (pieceButton button in mainWindow.board.Children)
                            {
                                if (button.Type.Equals("") && button.Name.Equals(temp.ToString()))
                                {
                                    legalMoveList.Add((Board)Enum.Parse(typeof(Board), button.Name));

                                }
                                else if (!button.Type.Equals("") && button.Name.Equals(temp.ToString()))
                                {
                                    if (button.Type.Contains("P2"))
                                    {
                                        legalMoveList.Add((Board)Enum.Parse(typeof(Board), button.Name));
                                    }
                                    flag = true;
                                }
                                if (flag) { break; }
                            }
                            if (flag) { break; }
                        }







                        foreach (Board space in legalMoveList)
                        {

                            tempName = space.ToString();


                            foreach (pieceButton button in mainWindow.board.Children)
                            {
                                if (tempName.Equals(button.Name))
                                {
                                    button.BackgroundColor = color;
                                    button.isPotentialMove = true;
                                    button.Opacity = 0.9;
                                }
                            }
                        }

                        legalMoveList.Clear();
                    }
                    else if (Type.Equals("P2Rook"))
                    {

                        currPos = (Board)Enum.Parse(typeof(Board), Name);
                        currPosLetter = Name.Substring(0, 1);
                        currPosNumber = Name.Substring(1);

                        switch (currPosLetter)
                        {
                            case "a":
                                left = 0;
                                right = 7;
                                break;
                            case "b":
                                left = 1;
                                right = 6;
                                break;
                            case "c":
                                left = 2;
                                right = 5;
                                break;
                            case "d":
                                left = 3;
                                right = 4;
                                break;
                            case "e":
                                left = 4;
                                right = 3;
                                break;
                            case "f":
                                left = 5;
                                right = 2;
                                break;
                            case "g":
                                left = 6;
                                right = 1;
                                break;
                            case "h":
                                left = 7;
                                right = 0;
                                break;
                        }

                        switch (currPosNumber)
                        {
                            case "1":
                                down = 0;
                                up = 7;
                                break;
                            case "2":
                                down = 1;
                                up = 6;
                                break;
                            case "3":
                                down = 2;
                                up = 5;
                                break;
                            case "4":
                                down = 3;
                                up = 4;
                                break;
                            case "5":
                                down = 4;
                                up = 3;
                                break;
                            case "6":
                                down = 5;
                                up = 2;
                                break;
                            case "7":
                                down = 6;
                                up = 1;
                                break;
                            case "8":
                                down = 7;
                                up = 0;
                                break;
                        }



                        bool flag = false;


                        for (int i = 1; i <= right; i++)
                        {
                            temp = currPos + i;
                            foreach (pieceButton button in mainWindow.board.Children)
                            {
                                if (button.Type.Equals("") && button.Name.Equals(temp.ToString()))
                                {
                                    legalMoveList.Add((Board)Enum.Parse(typeof(Board), button.Name));

                                }
                                else if (!button.Type.Equals("") && button.Name.Equals(temp.ToString()))
                                {
                                    if (button.Type.Contains("P1"))
                                    {
                                        legalMoveList.Add((Board)Enum.Parse(typeof(Board), button.Name));
                                    }
                                    flag = true;
                                }
                                if (flag) { break; }
                            }
                            if (flag) { break; }
                        }

                        flag = false;


                        for (int i = 1; i <= left; i++)
                        {
                            temp = currPos - i;
                            foreach (pieceButton button in mainWindow.board.Children)
                            {
                                if (button.Type.Equals("") && button.Name.Equals(temp.ToString()))
                                {
                                    legalMoveList.Add((Board)Enum.Parse(typeof(Board), button.Name));

                                }
                                else if (!button.Type.Equals("") && button.Name.Equals(temp.ToString()))
                                {
                                    if (button.Type.Contains("P1"))
                                    {
                                        legalMoveList.Add((Board)Enum.Parse(typeof(Board), button.Name));
                                    }
                                    flag = true;
                                }
                                if (flag) { break; }
                            }
                            if (flag) { break; }
                        }

                        flag = false;

                        for (int i = 1; i <= up; i++)
                        {
                            temp = currPos + i * 8;
                            foreach (pieceButton button in mainWindow.board.Children)
                            {
                                if (button.Type.Equals("") && button.Name.Equals(temp.ToString()))
                                {
                                    legalMoveList.Add((Board)Enum.Parse(typeof(Board), button.Name));

                                }
                                else if (!button.Type.Equals("") && button.Name.Equals(temp.ToString()))
                                {
                                    if (button.Type.Contains("P1"))
                                    {
                                        legalMoveList.Add((Board)Enum.Parse(typeof(Board), button.Name));
                                    }
                                    flag = true;
                                }
                                if (flag) { break; }
                            }
                            if (flag) { break; }
                        }

                        flag = false;

                        for (int i = 1; i <= down; i++)
                        {
                            temp = currPos - i * 8;
                            foreach (pieceButton button in mainWindow.board.Children)
                            {
                                if (button.Type.Equals("") && button.Name.Equals(temp.ToString()))
                                {
                                    legalMoveList.Add((Board)Enum.Parse(typeof(Board), button.Name));

                                }
                                else if (!button.Type.Equals("") && button.Name.Equals(temp.ToString()))
                                {
                                    if (button.Type.Contains("P1"))
                                    {
                                        legalMoveList.Add((Board)Enum.Parse(typeof(Board), button.Name));
                                    }
                                    flag = true;
                                }
                                if (flag) { break; }
                            }
                            if (flag) { break; }
                        }

                        foreach (Board space in legalMoveList)
                        {

                            tempName = space.ToString();

                            foreach (pieceButton button in mainWindow.board.Children)
                            {
                                if (tempName.Equals(button.Name))
                                {
                                    button.BackgroundColor = color;
                                    button.isPotentialMove = true;
                                    button.Opacity = 0.9;
                                }
                            }
                        }

                        legalMoveList.Clear();

                    }
                    #endregion
                    //logic for bishop pieces
                    #region
                    else if (Type.Equals("P1Bishop"))
                    {
                        currPos = (Board)Enum.Parse(typeof(Board), Name);
                        currPosLetter = Name.Substring(0, 1);
                        currPosNumber = Name.Substring(1);

                        int boundaryRight = 0, boundaryLeft = 0, boundaryUp = 0, boundaryDown = 0;

                        switch (currPosLetter)
                        {
                            case "a":
                                boundaryRight = 7;
                                boundaryLeft = 0;
                                break;
                            case "b":
                                boundaryRight = 6;
                                boundaryLeft = 1;
                                break;
                            case "c":
                                boundaryRight = 5;
                                boundaryLeft = 2;
                                break;
                            case "d":
                                boundaryRight = 4;
                                boundaryLeft = 3;
                                break;
                            case "e":
                                boundaryRight = 3;
                                boundaryLeft = 4;
                                break;
                            case "f":
                                boundaryRight = 2;
                                boundaryLeft = 5;
                                break;
                            case "g":
                                boundaryRight = 1;
                                boundaryLeft = 6;
                                break;
                            case "h":
                                boundaryRight = 0;
                                boundaryLeft = 7;
                                break;
                        }
                        switch (currPosNumber)
                        {
                            case "1":
                                boundaryUp = 7;
                                boundaryDown = 0;
                                break;
                            case "2":
                                boundaryUp = 6;
                                boundaryDown = 1;
                                break;
                            case "3":
                                boundaryUp = 5;
                                boundaryDown = 2;
                                break;
                            case "4":
                                boundaryUp = 4;
                                boundaryDown = 3;
                                break;
                            case "5":
                                boundaryUp = 3;
                                boundaryDown = 4;
                                break;
                            case "6":
                                boundaryUp = 2;
                                boundaryDown = 5;
                                break;
                            case "7":
                                boundaryUp = 1;
                                boundaryDown = 6;
                                break;
                            case "8":
                                boundaryUp = 0;
                                boundaryDown = 7;
                                break;
                        }

                        bool flag = false;
                        int i = 1, 
                            boundaryUpCounter = 0,
                            boundaryDownCounter = 0,
                            boundaryLeftCounter = 0,
                            boundaryRightCounter = 0;
                        temp = Board.a2;

                        //up and right direction 
                        while ((int)temp <= 64)
                        {
                            temp = currPos + i * 9;
                            i++;

                            if (boundaryUp >= boundaryRight)
                            {
                                if (boundaryRightCounter == boundaryRight)
                                {
                                    flag = true;
                                }

                            }
                            if (boundaryRight >= boundaryUp)
                            {
                                if (boundaryUpCounter == boundaryUp)
                                {
                                    flag = true;
                                }
                            }

                            boundaryRightCounter++;
                            boundaryUpCounter++;

                            if (flag) { break; }
                            foreach (pieceButton button in mainWindow.board.Children)
                            {
                                if (button.Type.Equals("") && button.Name.Equals(temp.ToString()))
                                {
                                    legalMoveList.Add((Board)Enum.Parse(typeof(Board), button.Name));
                                }
                                else if (!button.Type.Equals("") && button.Name.Equals(temp.ToString()))
                                {
                                    if (button.Type.Contains("P2"))
                                    {
                                        legalMoveList.Add((Board)Enum.Parse(typeof(Board), button.Name));
                                    }
                                    flag = true;
                                }
                                if (flag) { break; }
                            }
                            if (flag) { break; }
                        }

                        flag = false;
                        i = 1;
                        temp = Board.a2;

                        boundaryUpCounter = 0;
                        boundaryDownCounter = 0;
                        boundaryLeftCounter = 0;
                        boundaryRightCounter = 0;

                        //up and left direction
                        while ((int)temp <= 64)
                        {
                            temp = currPos + i * 7;
                            i++;
                            if (boundaryUp >= boundaryLeft)
                            {
                                if (boundaryLeftCounter == boundaryLeft)
                                {
                                    flag = true;
                                }

                            }
                            if (boundaryLeft >= boundaryUp)
                            {
                                if (boundaryUpCounter == boundaryUp)
                                {
                                    flag = true;
                                }
                            }

                            boundaryLeftCounter++;
                            boundaryUpCounter++;

                            if (flag) { break; }
                            foreach (pieceButton button in mainWindow.board.Children)
                            {
                                if (button.Type.Equals("") && button.Name.Equals(temp.ToString()))
                                {
                                    legalMoveList.Add((Board)Enum.Parse(typeof(Board), button.Name));

                                }
                                else if (!button.Type.Equals("") && button.Name.Equals(temp.ToString()))
                                {
                                    if (button.Type.Contains("P2"))
                                    {
                                        legalMoveList.Add((Board)Enum.Parse(typeof(Board), button.Name));
                                    }
                                    flag = true;
                                }
                                if (flag) { break; }
                            }
                            if (flag) { break; }
                        }

                        flag = false;
                        i = 1;

                        temp = Board.a2;

                        boundaryUpCounter = 0;
                        boundaryDownCounter = 0;
                        boundaryLeftCounter = 0;
                        boundaryRightCounter = 0;

                        //down and left direction
                        while ((int)temp >= 1)
                        {
                            temp = currPos - i * 9;
                            i++;
                            if (boundaryDown >= boundaryLeft)
                            {
                                if (boundaryLeftCounter == boundaryLeft)
                                {
                                    flag = true;
                                }

                            }
                            if (boundaryLeft >= boundaryDown)
                            {
                                if (boundaryDownCounter == boundaryDown)
                                {
                                    flag = true;
                                }
                            }

                            boundaryLeftCounter++;
                            boundaryDownCounter++;

                            if (flag) { break; }
                            foreach (pieceButton button in mainWindow.board.Children)
                            {
                                if (button.Type.Equals("") && button.Name.Equals(temp.ToString()))
                                {
                                    legalMoveList.Add((Board)Enum.Parse(typeof(Board), button.Name));

                                }
                                else if (!button.Type.Equals("") && button.Name.Equals(temp.ToString()))
                                {
                                    if (button.Type.Contains("P2"))
                                    {
                                        legalMoveList.Add((Board)Enum.Parse(typeof(Board), button.Name));
                                    }
                                    flag = true;
                                }
                                if (flag) { break; }
                            }
                            if (flag) { break; }
                        }

                        flag = false;
                        i = 1;
                        temp = Board.a2;
                        boundaryUpCounter = 0;
                        boundaryDownCounter = 0;
                        boundaryLeftCounter = 0;
                        boundaryRightCounter = 0;


                        //down and right direction
                        while ((int)temp >= 1)
                        {

                            temp = currPos - i * 7;
                            i++;
                            if (boundaryDown >= boundaryRight)
                            {
                                if (boundaryRightCounter == boundaryRight)
                                {
                                    flag = true;
                                }

                            }
                            if (boundaryRight >= boundaryDown)
                            {
                                if (boundaryDownCounter == boundaryDown)
                                {
                                    flag = true;
                                }
                            }

                            boundaryRightCounter++;
                            boundaryDownCounter++;

                            if (flag) { break; }
                            foreach (pieceButton button in mainWindow.board.Children)
                            {
                                if (button.Type.Equals("") && button.Name.Equals(temp.ToString()))
                                {
                                    legalMoveList.Add((Board)Enum.Parse(typeof(Board), button.Name));

                                }
                                else if (!button.Type.Equals("") && button.Name.Equals(temp.ToString()))
                                {
                                    if (button.Type.Contains("P2"))
                                    {
                                        legalMoveList.Add((Board)Enum.Parse(typeof(Board), button.Name));
                                    }
                                    flag = true;
                                }
                                if (flag) { break; }
                            }
                            if (flag) { break; }
                        }


                        foreach (Board space in legalMoveList)
                        {

                            tempName = space.ToString();


                            foreach (pieceButton button in mainWindow.board.Children)
                            {
                                if (tempName.Equals(button.Name))
                                {
                                    button.BackgroundColor = color;
                                    button.isPotentialMove = true;
                                    button.Opacity = 0.9;
                                }
                            }
                        }

                        legalMoveList.Clear();
                    }
                    else if (Type.Equals("P2Bishop"))
                    {
                        currPos = (Board)Enum.Parse(typeof(Board), Name);
                        currPosLetter = Name.Substring(0, 1);
                        currPosNumber = Name.Substring(1);

                        int boundaryRight = 0, boundaryLeft = 0, boundaryUp = 0, boundaryDown = 0;

                        switch (currPosLetter)
                        {
                            case "a":
                                boundaryRight = 7;
                                boundaryLeft = 0;
                                break;
                            case "b":
                                boundaryRight = 6;
                                boundaryLeft = 1;
                                break;
                            case "c":
                                boundaryRight = 5;
                                boundaryLeft = 2;
                                break;
                            case "d":
                                boundaryRight = 4;
                                boundaryLeft = 3;
                                break;
                            case "e":
                                boundaryRight = 3;
                                boundaryLeft = 4;
                                break;
                            case "f":
                                boundaryRight = 2;
                                boundaryLeft = 5;
                                break;
                            case "g":
                                boundaryRight = 1;
                                boundaryLeft = 6;
                                break;
                            case "h":
                                boundaryRight = 0;
                                boundaryLeft = 7;
                                break;
                        }
                        switch (currPosNumber)
                        {
                            case "1":
                                boundaryUp = 7;
                                boundaryDown = 0;
                                break;
                            case "2":
                                boundaryUp = 6;
                                boundaryDown = 1;
                                break;
                            case "3":
                                boundaryUp = 5;
                                boundaryDown = 2;
                                break;
                            case "4":
                                boundaryUp = 4;
                                boundaryDown = 3;
                                break;
                            case "5":
                                boundaryUp = 3;
                                boundaryDown = 4;
                                break;
                            case "6":
                                boundaryUp = 2;
                                boundaryDown = 5;
                                break;
                            case "7":
                                boundaryUp = 1;
                                boundaryDown = 6;
                                break;
                            case "8":
                                boundaryUp = 0;
                                boundaryDown = 7;
                                break;
                        }

                        bool flag = false;
                        int i = 1, boundaryUpCounter = 0, boundaryDownCounter = 0, boundaryLeftCounter = 0, boundaryRightCounter = 0;
                        temp = Board.a2;

                        //up and right direction 
                        while ((int)temp <= 64)
                        {
                            temp = currPos + i * 9;
                            i++;

                            if (boundaryUp >= boundaryRight)
                            {
                                if (boundaryRightCounter == boundaryRight)
                                {
                                    flag = true;
                                }

                            }
                            if (boundaryRight >= boundaryUp)
                            {
                                if (boundaryUpCounter == boundaryUp)
                                {
                                    flag = true;
                                }
                            }

                            boundaryRightCounter++;
                            boundaryUpCounter++;

                            if (flag) { break; }
                            foreach (pieceButton button in mainWindow.board.Children)
                            {
                                if (button.Type.Equals("") && button.Name.Equals(temp.ToString()))
                                {
                                    legalMoveList.Add((Board)Enum.Parse(typeof(Board), button.Name));
                                }
                                else if (!button.Type.Equals("") && button.Name.Equals(temp.ToString()))
                                {
                                    if (button.Type.Contains("P1"))
                                    {
                                        legalMoveList.Add((Board)Enum.Parse(typeof(Board), button.Name));
                                    }
                                    flag = true;
                                }
                                if (flag) { break; }
                            }
                            if (flag) { break; }
                        }

                        flag = false;
                        i = 1;
                        temp = Board.a2;

                        boundaryUpCounter = 0;
                        boundaryDownCounter = 0;
                        boundaryLeftCounter = 0;
                        boundaryRightCounter = 0;

                        //up and left direction
                        while ((int)temp <= 64)
                        {
                            temp = currPos + i * 7;
                            i++;
                            if (boundaryUp >= boundaryLeft)
                            {
                                if (boundaryLeftCounter == boundaryLeft)
                                {
                                    flag = true;
                                }

                            }
                            if (boundaryLeft >= boundaryUp)
                            {
                                if (boundaryUpCounter == boundaryUp)
                                {
                                    flag = true;
                                }
                            }

                            boundaryLeftCounter++;
                            boundaryUpCounter++;

                            if (flag) { break; }
                            foreach (pieceButton button in mainWindow.board.Children)
                            {
                                if (button.Type.Equals("") && button.Name.Equals(temp.ToString()))
                                {
                                    legalMoveList.Add((Board)Enum.Parse(typeof(Board), button.Name));

                                }
                                else if (!button.Type.Equals("") && button.Name.Equals(temp.ToString()))
                                {
                                    if (button.Type.Contains("P1"))
                                    {
                                        legalMoveList.Add((Board)Enum.Parse(typeof(Board), button.Name));
                                    }
                                    flag = true;
                                }
                                if (flag) { break; }
                            }
                            if (flag) { break; }
                        }

                        flag = false;
                        i = 1;

                        temp = Board.a2;

                        boundaryUpCounter = 0;
                        boundaryDownCounter = 0;
                        boundaryLeftCounter = 0;
                        boundaryRightCounter = 0;

                        //down and left direction
                        while ((int)temp >= 1)
                        {
                            temp = currPos - i * 9;
                            i++;
                            if (boundaryDown >= boundaryLeft)
                            {
                                if (boundaryLeftCounter == boundaryLeft)
                                {
                                    flag = true;
                                }

                            }
                            if (boundaryLeft >= boundaryDown)
                            {
                                if (boundaryDownCounter == boundaryDown)
                                {
                                    flag = true;
                                }
                            }

                            boundaryLeftCounter++;
                            boundaryDownCounter++;

                            if (flag) { break; }
                            foreach (pieceButton button in mainWindow.board.Children)
                            {
                                if (button.Type.Equals("") && button.Name.Equals(temp.ToString()))
                                {
                                    legalMoveList.Add((Board)Enum.Parse(typeof(Board), button.Name));

                                }
                                else if (!button.Type.Equals("") && button.Name.Equals(temp.ToString()))
                                {
                                    if (button.Type.Contains("P1"))
                                    {
                                        legalMoveList.Add((Board)Enum.Parse(typeof(Board), button.Name));
                                    }
                                    flag = true;
                                }
                                if (flag) { break; }
                            }
                            if (flag) { break; }
                        }

                        flag = false;
                        i = 1;
                        temp = Board.a2;
                        boundaryUpCounter = 0;
                        boundaryDownCounter = 0;
                        boundaryLeftCounter = 0;
                        boundaryRightCounter = 0;


                        //down and right direction
                        while ((int)temp >= 1)
                        {

                            temp = currPos - i * 7;
                            i++;
                            if (boundaryDown >= boundaryRight)
                            {
                                if (boundaryRightCounter == boundaryRight)
                                {
                                    flag = true;
                                }

                            }
                            if (boundaryRight >= boundaryDown)
                            {
                                if (boundaryDownCounter == boundaryDown)
                                {
                                    flag = true;
                                }
                            }

                            boundaryRightCounter++;
                            boundaryDownCounter++;

                            if (flag) { break; }
                            foreach (pieceButton button in mainWindow.board.Children)
                            {
                                if (button.Type.Equals("") && button.Name.Equals(temp.ToString()))
                                {
                                    legalMoveList.Add((Board)Enum.Parse(typeof(Board), button.Name));

                                }
                                else if (!button.Type.Equals("") && button.Name.Equals(temp.ToString()))
                                {
                                    if (button.Type.Contains("P1"))
                                    {
                                        legalMoveList.Add((Board)Enum.Parse(typeof(Board), button.Name));
                                    }
                                    flag = true;
                                }
                                if (flag) { break; }
                            }
                            if (flag) { break; }
                        }


                        foreach (Board space in legalMoveList)
                        {

                            tempName = space.ToString();


                            foreach (pieceButton button in mainWindow.board.Children)
                            {
                                if (tempName.Equals(button.Name))
                                {
                                    button.BackgroundColor = color;
                                    button.isPotentialMove = true;
                                    button.Opacity = 0.9;
                                }
                            }
                        }

                        legalMoveList.Clear();
                    }
                    #endregion
                    //logic for knight pieces
                    #region
                    else if (Type.Equals("P1Knight"))
                    {

                        List<Board> doNotCalculate = new List<Board>();

                        currPos = (Board)Enum.Parse(typeof(Board), Name);
                        currPosLetter = Name.Substring(0, 1);
                        currPosNumber = Name.Substring(1);

                        //calculate all possible moves from current position
                        legalMove = currPos + 17;
                        legalMovesKnight.Add(legalMove);
                        legalMove = currPos + 15;
                        legalMovesKnight.Add(legalMove);
                        legalMove = currPos + 6;
                        legalMovesKnight.Add(legalMove);
                        legalMove = currPos - 10;
                        legalMovesKnight.Add(legalMove);
                        legalMove = currPos - 17;
                        legalMovesKnight.Add(legalMove);
                        legalMove = currPos - 15;
                        legalMovesKnight.Add(legalMove);
                        legalMove = currPos - 6;
                        legalMovesKnight.Add(legalMove);
                        legalMove = currPos + 10;
                        legalMovesKnight.Add(legalMove);

                        //these switches will remove moves that would loop around the board if piece is near edge
                        switch (currPosLetter)
                        {
                            case "a":
                                legalMove = currPos + 15;
                                legalMovesKnight.Remove(legalMove);
                                legalMove = currPos - 17;
                                legalMovesKnight.Remove(legalMove);
                                legalMove = currPos + 6;
                                legalMovesKnight.Remove(legalMove);
                                legalMove = currPos - 10;
                                legalMovesKnight.Remove(legalMove);
                                break;
                            case "b":
                                legalMove = currPos + 6;
                                legalMovesKnight.Remove(legalMove);
                                legalMove = currPos - 10;
                                legalMovesKnight.Remove(legalMove);
                                break;
                            case "g":
                                legalMove = currPos + 10;
                                legalMovesKnight.Remove(legalMove);
                                legalMove = currPos - 6;
                                legalMovesKnight.Remove(legalMove);
                                break;
                            case "h":
                                legalMove = currPos + 17;
                                legalMovesKnight.Remove(legalMove);
                                legalMove = currPos - 15;
                                legalMovesKnight.Remove(legalMove);
                                legalMove = currPos + 10;
                                legalMovesKnight.Remove(legalMove);
                                legalMove = currPos - 6;
                                legalMovesKnight.Remove(legalMove);
                                break;
                        }
                        switch (currPosNumber)
                        {
                            case "1":
                                try
                                {
                                    legalMove = currPos - 10;
                                    legalMovesKnight.Remove(legalMove);
                                }
                                catch { }
                                try
                                {
                                    legalMove = currPos - 6;
                                    legalMovesKnight.Remove(legalMove);
                                }
                                catch { }
                                try
                                {
                                    legalMove = currPos - 17;
                                    legalMovesKnight.Remove(legalMove);
                                }
                                catch { }
                                try
                                {
                                    legalMove = currPos - 15;
                                    legalMovesKnight.Remove(legalMove);
                                }
                                catch { }

                                break;
                            case "2":
                                try
                                {
                                    legalMove = currPos - 17;
                                    legalMovesKnight.Remove(legalMove);
                                }
                                catch { }
                                try
                                {
                                    legalMove = currPos - 15;
                                    legalMovesKnight.Remove(legalMove);
                                }
                                catch { }
                                break;
                            case "7":
                                try
                                {
                                    legalMove = currPos + 15;
                                    legalMovesKnight.Remove(legalMove);
                                }
                                catch { }
                                try
                                {
                                    legalMove = currPos + 17;
                                    legalMovesKnight.Remove(legalMove);
                                }
                                catch { }
                                break;
                            case "8":
                                try
                                {
                                    legalMove = currPos + 6;
                                    legalMovesKnight.Remove(legalMove);
                                }
                                catch { }
                                try
                                {
                                    legalMove = currPos + 10;
                                    legalMovesKnight.Remove(legalMove);
                                }
                                catch { }
                                try
                                {
                                    legalMove = currPos + 15;
                                    legalMovesKnight.Remove(legalMove);
                                }
                                catch { }
                                try
                                {
                                    legalMove = currPos + 17;
                                    legalMovesKnight.Remove(legalMove);
                                }
                                catch { }

                                break;
                        }


                        foreach (Board move in legalMovesKnight)
                        {
                            foreach (pieceButton button in mainWindow.board.Children)
                            {
                                if (button.Name.Equals(move.ToString()) && !button.Type.Contains("P1"))
                                {
                                    legalMoveList.Add((Board)Enum.Parse(typeof(Board), button.Name));

                                }
                            }

                        }

                        foreach (Board space in legalMoveList)
                        {

                            tempName = space.ToString();


                            foreach (pieceButton button in mainWindow.board.Children)
                            {
                                if (tempName.Equals(button.Name))
                                {
                                    button.BackgroundColor = color;
                                    button.isPotentialMove = true;
                                    button.Opacity = 0.9;
                                }
                            }
                        }

                        legalMoveList.Clear();
                        legalMovesKnight.Clear();

                    }
                    else if (Type.Equals("P2Knight"))
                    {

                        List<Board> doNotCalculate = new List<Board>();

                        currPos = (Board)Enum.Parse(typeof(Board), Name);
                        currPosLetter = Name.Substring(0, 1);
                        currPosNumber = Name.Substring(1);

                        //calculate all possible moves from current position
                        legalMove = currPos + 17;
                        legalMovesKnight.Add(legalMove);
                        legalMove = currPos + 15;
                        legalMovesKnight.Add(legalMove);
                        legalMove = currPos + 6;
                        legalMovesKnight.Add(legalMove);
                        legalMove = currPos - 10;
                        legalMovesKnight.Add(legalMove);
                        legalMove = currPos - 17;
                        legalMovesKnight.Add(legalMove);
                        legalMove = currPos - 15;
                        legalMovesKnight.Add(legalMove);
                        legalMove = currPos - 6;
                        legalMovesKnight.Add(legalMove);
                        legalMove = currPos + 10;
                        legalMovesKnight.Add(legalMove);

                        //these switches will remove moves that would loop around the board if piece is near edge
                        switch (currPosLetter)
                        {
                            case "a":
                                legalMove = currPos + 15;
                                legalMovesKnight.Remove(legalMove);
                                legalMove = currPos - 17;
                                legalMovesKnight.Remove(legalMove);
                                legalMove = currPos + 6;
                                legalMovesKnight.Remove(legalMove);
                                legalMove = currPos - 10;
                                legalMovesKnight.Remove(legalMove);
                                break;
                            case "b":
                                legalMove = currPos + 6;
                                legalMovesKnight.Remove(legalMove);
                                legalMove = currPos - 10;
                                legalMovesKnight.Remove(legalMove);
                                break;
                            case "g":
                                legalMove = currPos + 10;
                                legalMovesKnight.Remove(legalMove);
                                legalMove = currPos - 6;
                                legalMovesKnight.Remove(legalMove);
                                break;
                            case "h":
                                legalMove = currPos + 17;
                                legalMovesKnight.Remove(legalMove);
                                legalMove = currPos - 15;
                                legalMovesKnight.Remove(legalMove);
                                legalMove = currPos + 10;
                                legalMovesKnight.Remove(legalMove);
                                legalMove = currPos - 6;
                                legalMovesKnight.Remove(legalMove);
                                break;
                        }
                        switch (currPosNumber)
                        {
                            case "1":
                                try
                                {
                                    legalMove = currPos - 10;
                                    legalMovesKnight.Remove(legalMove);
                                }
                                catch { }
                                try
                                {
                                    legalMove = currPos - 6;
                                    legalMovesKnight.Remove(legalMove);
                                }
                                catch { }
                                try
                                {
                                    legalMove = currPos - 17;
                                    legalMovesKnight.Remove(legalMove);
                                }
                                catch { }
                                try
                                {
                                    legalMove = currPos - 15;
                                    legalMovesKnight.Remove(legalMove);
                                }
                                catch { }

                                break;
                            case "2":
                                try
                                {
                                    legalMove = currPos - 17;
                                    legalMovesKnight.Remove(legalMove);
                                }
                                catch { }
                                try
                                {
                                    legalMove = currPos - 15;
                                    legalMovesKnight.Remove(legalMove);
                                }
                                catch { }
                                break;
                            case "7":
                                try
                                {
                                    legalMove = currPos + 15;
                                    legalMovesKnight.Remove(legalMove);
                                }
                                catch { }
                                try
                                {
                                    legalMove = currPos + 17;
                                    legalMovesKnight.Remove(legalMove);
                                }
                                catch { }
                                break;
                            case "8":
                                try
                                {
                                    legalMove = currPos + 6;
                                    legalMovesKnight.Remove(legalMove);
                                }
                                catch { }
                                try
                                {
                                    legalMove = currPos + 10;
                                    legalMovesKnight.Remove(legalMove);
                                }
                                catch { }
                                try
                                {
                                    legalMove = currPos + 15;
                                    legalMovesKnight.Remove(legalMove);
                                }
                                catch { }
                                try
                                {
                                    legalMove = currPos + 17;
                                    legalMovesKnight.Remove(legalMove);
                                }
                                catch { }

                                break;
                        }


                        foreach (Board move in legalMovesKnight)
                        {
                            foreach (pieceButton button in mainWindow.board.Children)
                            {
                                if (button.Name.Equals(move.ToString()) && !button.Type.Contains("P2"))
                                {
                                    legalMoveList.Add((Board)Enum.Parse(typeof(Board), button.Name));

                                }
                            }

                        }

                        foreach (Board space in legalMoveList)
                        {

                            tempName = space.ToString();


                            foreach (pieceButton button in mainWindow.board.Children)
                            {
                                if (tempName.Equals(button.Name))
                                {
                                    button.BackgroundColor = color;
                                    button.isPotentialMove = true;
                                    button.Opacity = 0.9;
                                }
                            }
                        }

                        legalMoveList.Clear();
                        legalMovesKnight.Clear();

                    }
                    #endregion
                    //logic for queen pieces
                    #region
                    else if (Type.Equals("P1Queen"))
                    {

                        currPos = (Board)Enum.Parse(typeof(Board), Name);
                        currPosLetter = Name.Substring(0, 1);
                        currPosNumber = Name.Substring(1);

                        int spaceMultiplier = 1,
                            boundaryUpCounter = 0,
                            boundaryDownCounter = 0,
                            boundaryLeftCounter = 0,
                            boundaryRightCounter = 0;

                        Board temp;
                        bool flag = false;

                        switch (currPosLetter)
                        {
                            case "a":
                                left = 0;
                                right = 7;
                                break;
                            case "b":
                                left = 1;
                                right = 6;
                                break;
                            case "c":
                                left = 2;
                                right = 5;
                                break;
                            case "d":
                                left = 3;
                                right = 4;
                                break;
                            case "e":
                                left = 4;
                                right = 3;
                                break;
                            case "f":
                                left = 5;
                                right = 2;
                                break;
                            case "g":
                                left = 6;
                                right = 1;
                                break;
                            case "h":
                                left = 7;
                                right = 0;
                                break;
                        }

                        switch (currPosNumber)
                        {
                            case "1":
                                down = 0;
                                up = 7;
                                break;
                            case "2":
                                down = 1;
                                up = 6;
                                break;
                            case "3":
                                down = 2;
                                up = 5;
                                break;
                            case "4":
                                down = 3;
                                up = 4;
                                break;
                            case "5":
                                down = 4;
                                up = 3;
                                break;
                            case "6":
                                down = 5;
                                up = 2;
                                break;
                            case "7":
                                down = 6;
                                up = 1;
                                break;
                            case "8":
                                down = 7;
                                up = 0;
                                break;
                        }


                        

                        //sets legal moves along line, stops when friendly or opposing piece is encountered
                        for (int i = 1; i <= right; i++)
                        {
                            temp = currPos + i;
                            foreach (pieceButton button in mainWindow.board.Children)
                            {
                                if (button.Type.Equals("") && button.Name.Equals(temp.ToString()))
                                {
                                    legalMoveList.Add((Board)Enum.Parse(typeof(Board), button.Name));

                                }
                                else if (!button.Type.Equals("") && button.Name.Equals(temp.ToString()))
                                {
                                    if (button.Type.Contains("P2"))
                                    {
                                        legalMoveList.Add((Board)Enum.Parse(typeof(Board), button.Name));
                                    }
                                    flag = true;
                                }
                                if (flag) { break; }
                            }
                            if (flag) { break; }
                        }

                        flag = false;


                        for (int i = 1; i <= left; i++)
                        {
                            temp = currPos - i;
                            foreach (pieceButton button in mainWindow.board.Children)
                            {
                                if (button.Type.Equals("") && button.Name.Equals(temp.ToString()))
                                {
                                    legalMoveList.Add((Board)Enum.Parse(typeof(Board), button.Name));

                                }
                                else if (!button.Type.Equals("") && button.Name.Equals(temp.ToString()))
                                {
                                    if (button.Type.Contains("P2"))
                                    {
                                        legalMoveList.Add((Board)Enum.Parse(typeof(Board), button.Name));
                                    }
                                    flag = true;
                                }
                                if (flag) { break; }
                            }
                            if (flag) { break; }
                        }

                        flag = false;

                        for (int i = 1; i <= up; i++)
                        {
                            temp = currPos + i * 8;
                            foreach (pieceButton button in mainWindow.board.Children)
                            {
                                if (button.Type.Equals("") && button.Name.Equals(temp.ToString()))
                                {
                                    legalMoveList.Add((Board)Enum.Parse(typeof(Board), button.Name));

                                }
                                else if (!button.Type.Equals("") && button.Name.Equals(temp.ToString()))
                                {
                                    if (button.Type.Contains("P2"))
                                    {
                                        legalMoveList.Add((Board)Enum.Parse(typeof(Board), button.Name));
                                    }
                                    flag = true;
                                }
                                if (flag) { break; }
                            }
                            if (flag) { break; }
                        }

                        flag = false;

                        for (int i = 1; i <= down; i++)
                        {
                            temp = currPos - i * 8;
                            foreach (pieceButton button in mainWindow.board.Children)
                            {
                                if (button.Type.Equals("") && button.Name.Equals(temp.ToString()))
                                {
                                    legalMoveList.Add((Board)Enum.Parse(typeof(Board), button.Name));

                                }
                                else if (!button.Type.Equals("") && button.Name.Equals(temp.ToString()))
                                {
                                    if (button.Type.Contains("P2"))
                                    {
                                        legalMoveList.Add((Board)Enum.Parse(typeof(Board), button.Name));
                                    }
                                    flag = true;
                                }
                                if (flag) { break; }
                            }
                            if (flag) { break; }
                        }

                        flag = false;
                        
                        temp = Board.a2;

                        //up and right direction, prevents moves looping around board and stops at friendly/opposing piece
                        while ((int)temp <= 64)
                        {
                            temp = currPos + spaceMultiplier * 9;
                            spaceMultiplier++;

                            if (up >= right)
                            {
                                if (boundaryRightCounter == right)
                                {
                                    flag = true;
                                }

                            }
                            if (right >= up)
                            {
                                if (boundaryUpCounter == up)
                                {
                                    flag = true;
                                }
                            }

                            boundaryRightCounter++;
                            boundaryUpCounter++;

                            if (flag) { break; }
                            foreach (pieceButton button in mainWindow.board.Children)
                            {
                                if (button.Type.Equals("") && button.Name.Equals(temp.ToString()))
                                {
                                    legalMoveList.Add((Board)Enum.Parse(typeof(Board), button.Name));
                                }
                                else if (!button.Type.Equals("") && button.Name.Equals(temp.ToString()))
                                {
                                    if (button.Type.Contains("P2"))
                                    {
                                        legalMoveList.Add((Board)Enum.Parse(typeof(Board), button.Name));
                                    }
                                    flag = true;
                                }
                                if (flag) { break; }
                            }
                            if (flag) { break; }
                        }

                        flag = false;
                        spaceMultiplier = 1;
                        temp = Board.a2;

                        boundaryUpCounter = 0;
                        boundaryDownCounter = 0;
                        boundaryLeftCounter = 0;
                        boundaryRightCounter = 0;

                        //up and left direction
                        while ((int)temp <= 64)
                        {
                            temp = currPos + spaceMultiplier * 7;
                            spaceMultiplier++;
                            if (up >= left)
                            {
                                if (boundaryLeftCounter == left)
                                {
                                    flag = true;
                                }

                            }
                            if (left >= up)
                            {
                                if (boundaryUpCounter == up)
                                {
                                    flag = true;
                                }
                            }

                            boundaryLeftCounter++;
                            boundaryUpCounter++;

                            if (flag) { break; }
                            foreach (pieceButton button in mainWindow.board.Children)
                            {
                                if (button.Type.Equals("") && button.Name.Equals(temp.ToString()))
                                {
                                    legalMoveList.Add((Board)Enum.Parse(typeof(Board), button.Name));

                                }
                                else if (!button.Type.Equals("") && button.Name.Equals(temp.ToString()))
                                {
                                    if (button.Type.Contains("P2"))
                                    {
                                        legalMoveList.Add((Board)Enum.Parse(typeof(Board), button.Name));
                                    }
                                    flag = true;
                                }
                                if (flag) { break; }
                            }
                            if (flag) { break; }
                        }

                        flag = false;
                        spaceMultiplier = 1;

                        temp = Board.a2;

                        boundaryUpCounter = 0;
                        boundaryDownCounter = 0;
                        boundaryLeftCounter = 0;
                        boundaryRightCounter = 0;

                        //down and left direction
                        while ((int)temp >= 1)
                        {
                            temp = currPos - spaceMultiplier * 9;
                            spaceMultiplier++;
                            if (down >= left)
                            {
                                if (boundaryLeftCounter == left)
                                {
                                    flag = true;
                                }

                            }
                            if (left >= down)
                            {
                                if (boundaryDownCounter == down)
                                {
                                    flag = true;
                                }
                            }

                            boundaryLeftCounter++;
                            boundaryDownCounter++;

                            if (flag) { break; }
                            foreach (pieceButton button in mainWindow.board.Children)
                            {
                                if (button.Type.Equals("") && button.Name.Equals(temp.ToString()))
                                {
                                    legalMoveList.Add((Board)Enum.Parse(typeof(Board), button.Name));

                                }
                                else if (!button.Type.Equals("") && button.Name.Equals(temp.ToString()))
                                {
                                    if (button.Type.Contains("P2"))
                                    {
                                        legalMoveList.Add((Board)Enum.Parse(typeof(Board), button.Name));
                                    }
                                    flag = true;
                                }
                                if (flag) { break; }
                            }
                            if (flag) { break; }
                        }

                        flag = false;
                        spaceMultiplier = 1;
                        temp = Board.a2;
                        boundaryUpCounter = 0;
                        boundaryDownCounter = 0;
                        boundaryLeftCounter = 0;
                        boundaryRightCounter = 0;


                        //down and right direction
                        while ((int)temp >= 1)
                        {

                            temp = currPos - spaceMultiplier * 7;
                            spaceMultiplier++;
                            if (down >= right)
                            {
                                if (boundaryRightCounter == right)
                                {
                                    flag = true;
                                }

                            }
                            if (right >= down)
                            {
                                if (boundaryDownCounter == down)
                                {
                                    flag = true;
                                }
                            }

                            boundaryRightCounter++;
                            boundaryDownCounter++;

                            if (flag) { break; }
                            foreach (pieceButton button in mainWindow.board.Children)
                            {
                                if (button.Type.Equals("") && button.Name.Equals(temp.ToString()))
                                {
                                    legalMoveList.Add((Board)Enum.Parse(typeof(Board), button.Name));

                                }
                                else if (!button.Type.Equals("") && button.Name.Equals(temp.ToString()))
                                {
                                    if (button.Type.Contains("P2"))
                                    {
                                        legalMoveList.Add((Board)Enum.Parse(typeof(Board), button.Name));
                                    }
                                    flag = true;
                                }
                                if (flag) { break; }
                            }
                            if (flag) { break; }
                        }

                        foreach (Board space in legalMoveList)
                        {

                            tempName = space.ToString();


                            foreach (pieceButton button in mainWindow.board.Children)
                            {
                                if (tempName.Equals(button.Name))
                                {
                                    button.BackgroundColor = color;
                                    button.isPotentialMove = true;
                                    button.Opacity = 0.9;
                                }
                            }
                        }

                        legalMoveList.Clear();

                    }
                    else if (Type.Equals("P2Queen"))
                    {

                        currPos = (Board)Enum.Parse(typeof(Board), Name);
                        currPosLetter = Name.Substring(0, 1);
                        currPosNumber = Name.Substring(1);

                        int spaceMultiplier = 1,
                            boundaryUpCounter = 0,
                            boundaryDownCounter = 0,
                            boundaryLeftCounter = 0,
                            boundaryRightCounter = 0;

                        Board temp;
                        bool flag = false;

                        switch (currPosLetter)
                        {
                            case "a":
                                left = 0;
                                right = 7;
                                break;
                            case "b":
                                left = 1;
                                right = 6;
                                break;
                            case "c":
                                left = 2;
                                right = 5;
                                break;
                            case "d":
                                left = 3;
                                right = 4;
                                break;
                            case "e":
                                left = 4;
                                right = 3;
                                break;
                            case "f":
                                left = 5;
                                right = 2;
                                break;
                            case "g":
                                left = 6;
                                right = 1;
                                break;
                            case "h":
                                left = 7;
                                right = 0;
                                break;
                        }

                        switch (currPosNumber)
                        {
                            case "1":
                                down = 0;
                                up = 7;
                                break;
                            case "2":
                                down = 1;
                                up = 6;
                                break;
                            case "3":
                                down = 2;
                                up = 5;
                                break;
                            case "4":
                                down = 3;
                                up = 4;
                                break;
                            case "5":
                                down = 4;
                                up = 3;
                                break;
                            case "6":
                                down = 5;
                                up = 2;
                                break;
                            case "7":
                                down = 6;
                                up = 1;
                                break;
                            case "8":
                                down = 7;
                                up = 0;
                                break;
                        }





                        for (int i = 1; i <= right; i++)
                        {
                            temp = currPos + i;
                            foreach (pieceButton button in mainWindow.board.Children)
                            {
                                if (button.Type.Equals("") && button.Name.Equals(temp.ToString()))
                                {
                                    legalMoveList.Add((Board)Enum.Parse(typeof(Board), button.Name));

                                }
                                else if (!button.Type.Equals("") && button.Name.Equals(temp.ToString()))
                                {
                                    if (button.Type.Contains("P1"))
                                    {
                                        legalMoveList.Add((Board)Enum.Parse(typeof(Board), button.Name));
                                    }
                                    flag = true;
                                }
                                if (flag) { break; }
                            }
                            if (flag) { break; }
                        }

                        flag = false;


                        for (int i = 1; i <= left; i++)
                        {
                            temp = currPos - i;
                            foreach (pieceButton button in mainWindow.board.Children)
                            {
                                if (button.Type.Equals("") && button.Name.Equals(temp.ToString()))
                                {
                                    legalMoveList.Add((Board)Enum.Parse(typeof(Board), button.Name));

                                }
                                else if (!button.Type.Equals("") && button.Name.Equals(temp.ToString()))
                                {
                                    if (button.Type.Contains("P1"))
                                    {
                                        legalMoveList.Add((Board)Enum.Parse(typeof(Board), button.Name));
                                    }
                                    flag = true;
                                }
                                if (flag) { break; }
                            }
                            if (flag) { break; }
                        }

                        flag = false;

                        for (int i = 1; i <= up; i++)
                        {
                            temp = currPos + i * 8;
                            foreach (pieceButton button in mainWindow.board.Children)
                            {
                                if (button.Type.Equals("") && button.Name.Equals(temp.ToString()))
                                {
                                    legalMoveList.Add((Board)Enum.Parse(typeof(Board), button.Name));

                                }
                                else if (!button.Type.Equals("") && button.Name.Equals(temp.ToString()))
                                {
                                    if (button.Type.Contains("P1"))
                                    {
                                        legalMoveList.Add((Board)Enum.Parse(typeof(Board), button.Name));
                                    }
                                    flag = true;
                                }
                                if (flag) { break; }
                            }
                            if (flag) { break; }
                        }

                        flag = false;

                        for (int i = 1; i <= down; i++)
                        {
                            temp = currPos - i * 8;
                            foreach (pieceButton button in mainWindow.board.Children)
                            {
                                if (button.Type.Equals("") && button.Name.Equals(temp.ToString()))
                                {
                                    legalMoveList.Add((Board)Enum.Parse(typeof(Board), button.Name));

                                }
                                else if (!button.Type.Equals("") && button.Name.Equals(temp.ToString()))
                                {
                                    if (button.Type.Contains("P1"))
                                    {
                                        legalMoveList.Add((Board)Enum.Parse(typeof(Board), button.Name));
                                    }
                                    flag = true;
                                }
                                if (flag) { break; }
                            }
                            if (flag) { break; }
                        }

                        flag = false;

                        temp = Board.a2;

                        //up and right direction 
                        while ((int)temp <= 64)
                        {
                            temp = currPos + spaceMultiplier * 9;
                            spaceMultiplier++;

                            if (up >= right)
                            {
                                if (boundaryRightCounter == right)
                                {
                                    flag = true;
                                }

                            }
                            if (right >= up)
                            {
                                if (boundaryUpCounter == up)
                                {
                                    flag = true;
                                }
                            }

                            boundaryRightCounter++;
                            boundaryUpCounter++;

                            if (flag) { break; }
                            foreach (pieceButton button in mainWindow.board.Children)
                            {
                                if (button.Type.Equals("") && button.Name.Equals(temp.ToString()))
                                {
                                    legalMoveList.Add((Board)Enum.Parse(typeof(Board), button.Name));
                                }
                                else if (!button.Type.Equals("") && button.Name.Equals(temp.ToString()))
                                {
                                    if (button.Type.Contains("P1"))
                                    {
                                        legalMoveList.Add((Board)Enum.Parse(typeof(Board), button.Name));
                                    }
                                    flag = true;
                                }
                                if (flag) { break; }
                            }
                            if (flag) { break; }
                        }

                        flag = false;
                        spaceMultiplier = 1;
                        temp = Board.a2;

                        boundaryUpCounter = 0;
                        boundaryDownCounter = 0;
                        boundaryLeftCounter = 0;
                        boundaryRightCounter = 0;

                        //up and left direction
                        while ((int)temp <= 64)
                        {
                            temp = currPos + spaceMultiplier * 7;
                            spaceMultiplier++;
                            if (up >= left)
                            {
                                if (boundaryLeftCounter == left)
                                {
                                    flag = true;
                                }

                            }
                            if (left >= up)
                            {
                                if (boundaryUpCounter == up)
                                {
                                    flag = true;
                                }
                            }

                            boundaryLeftCounter++;
                            boundaryUpCounter++;

                            if (flag) { break; }
                            foreach (pieceButton button in mainWindow.board.Children)
                            {
                                if (button.Type.Equals("") && button.Name.Equals(temp.ToString()))
                                {
                                    legalMoveList.Add((Board)Enum.Parse(typeof(Board), button.Name));

                                }
                                else if (!button.Type.Equals("") && button.Name.Equals(temp.ToString()))
                                {
                                    if (button.Type.Contains("P1"))
                                    {
                                        legalMoveList.Add((Board)Enum.Parse(typeof(Board), button.Name));
                                    }
                                    flag = true;
                                }
                                if (flag) { break; }
                            }
                            if (flag) { break; }
                        }

                        flag = false;
                        spaceMultiplier = 1;

                        temp = Board.a2;

                        boundaryUpCounter = 0;
                        boundaryDownCounter = 0;
                        boundaryLeftCounter = 0;
                        boundaryRightCounter = 0;

                        //down and left direction
                        while ((int)temp >= 1)
                        {
                            temp = currPos - spaceMultiplier * 9;
                            spaceMultiplier++;
                            if (down >= left)
                            {
                                if (boundaryLeftCounter == left)
                                {
                                    flag = true;
                                }

                            }
                            if (left >= down)
                            {
                                if (boundaryDownCounter == down)
                                {
                                    flag = true;
                                }
                            }

                            boundaryLeftCounter++;
                            boundaryDownCounter++;

                            if (flag) { break; }
                            foreach (pieceButton button in mainWindow.board.Children)
                            {
                                if (button.Type.Equals("") && button.Name.Equals(temp.ToString()))
                                {
                                    legalMoveList.Add((Board)Enum.Parse(typeof(Board), button.Name));

                                }
                                else if (!button.Type.Equals("") && button.Name.Equals(temp.ToString()))
                                {
                                    if (button.Type.Contains("P1"))
                                    {
                                        legalMoveList.Add((Board)Enum.Parse(typeof(Board), button.Name));
                                    }
                                    flag = true;
                                }
                                if (flag) { break; }
                            }
                            if (flag) { break; }
                        }

                        flag = false;
                        spaceMultiplier = 1;
                        temp = Board.a2;
                        boundaryUpCounter = 0;
                        boundaryDownCounter = 0;
                        boundaryLeftCounter = 0;
                        boundaryRightCounter = 0;


                        //down and right direction
                        while ((int)temp >= 1)
                        {

                            temp = currPos - spaceMultiplier * 7;
                            spaceMultiplier++;
                            if (down >= right)
                            {
                                if (boundaryRightCounter == right)
                                {
                                    flag = true;
                                }

                            }
                            if (right >= down)
                            {
                                if (boundaryDownCounter == down)
                                {
                                    flag = true;
                                }
                            }

                            boundaryRightCounter++;
                            boundaryDownCounter++;

                            if (flag) { break; }
                            foreach (pieceButton button in mainWindow.board.Children)
                            {
                                if (button.Type.Equals("") && button.Name.Equals(temp.ToString()))
                                {
                                    legalMoveList.Add((Board)Enum.Parse(typeof(Board), button.Name));

                                }
                                else if (!button.Type.Equals("") && button.Name.Equals(temp.ToString()))
                                {
                                    if (button.Type.Contains("P1"))
                                    {
                                        legalMoveList.Add((Board)Enum.Parse(typeof(Board), button.Name));
                                    }
                                    flag = true;
                                }
                                if (flag) { break; }
                            }
                            if (flag) { break; }
                        }

                        foreach (Board space in legalMoveList)
                        {

                            tempName = space.ToString();


                            foreach (pieceButton button in mainWindow.board.Children)
                            {
                                if (tempName.Equals(button.Name))
                                {
                                    button.BackgroundColor = color;
                                    button.isPotentialMove = true;
                                    button.Opacity = 0.9;
                                }
                            }
                        }

                        legalMoveList.Clear();

                    }
                    #endregion
                    //logic for king pieces
                    #region
                    else if (Type.Equals("P1King"))
                    {
                        currPos = (Board)Enum.Parse(typeof(Board), Name);
                        currPosLetter = Name.Substring(0, 1);
                        currPosNumber = Name.Substring(1);

                        if (!currPosLetter.Equals("h"))
                        {
                            legalMove = currPos + 1;
                            foreach (pieceButton button in mainWindow.board.Children)
                            {
                                if (button.Type.Equals("") && button.Name.Equals(legalMove.ToString()))
                                {
                                    legalMoveList.Add((Board)Enum.Parse(typeof(Board), button.Name));

                                }
                                else if (!button.Type.Equals("") && button.Name.Equals(legalMove.ToString()))
                                {
                                    if (button.Type.Contains("P2"))
                                    {
                                        legalMoveList.Add((Board)Enum.Parse(typeof(Board), button.Name));
                                    }
                                }
                            }
                            legalMove = currPos + 9;
                            foreach (pieceButton button in mainWindow.board.Children)
                            {
                                if (button.Type.Equals("") && button.Name.Equals(legalMove.ToString()))
                                {
                                    legalMoveList.Add((Board)Enum.Parse(typeof(Board), button.Name));

                                }
                                else if (!button.Type.Equals("") && button.Name.Equals(legalMove.ToString()))
                                {
                                    if (button.Type.Contains("P2"))
                                    {
                                        legalMoveList.Add((Board)Enum.Parse(typeof(Board), button.Name));
                                    }
                                }
                            }
                            legalMove = currPos - 7;
                            foreach (pieceButton button in mainWindow.board.Children)
                            {
                                if (button.Type.Equals("") && button.Name.Equals(legalMove.ToString()))
                                {
                                    legalMoveList.Add((Board)Enum.Parse(typeof(Board), button.Name));

                                }
                                else if (!button.Type.Equals("") && button.Name.Equals(legalMove.ToString()))
                                {
                                    if (button.Type.Contains("P2"))
                                    {
                                        legalMoveList.Add((Board)Enum.Parse(typeof(Board), button.Name));
                                    }
                                }
                            }
                        }
                        if (!currPosLetter.Equals("a"))
                        {
                            legalMove = currPos - 1;
                            foreach (pieceButton button in mainWindow.board.Children)
                            {
                                if (button.Type.Equals("") && button.Name.Equals(legalMove.ToString()))
                                {
                                    legalMoveList.Add((Board)Enum.Parse(typeof(Board), button.Name));

                                }
                                else if (!button.Type.Equals("") && button.Name.Equals(legalMove.ToString()))
                                {
                                    if (button.Type.Contains("P2"))
                                    {
                                        legalMoveList.Add((Board)Enum.Parse(typeof(Board), button.Name));
                                    }
                                }
                            }
                            legalMove = currPos - 9;
                            foreach (pieceButton button in mainWindow.board.Children)
                            {
                                if (button.Type.Equals("") && button.Name.Equals(legalMove.ToString()))
                                {
                                    legalMoveList.Add((Board)Enum.Parse(typeof(Board), button.Name));

                                }
                                else if (!button.Type.Equals("") && button.Name.Equals(legalMove.ToString()))
                                {
                                    if (button.Type.Contains("P2"))
                                    {
                                        legalMoveList.Add((Board)Enum.Parse(typeof(Board), button.Name));
                                    }
                                }
                            }
                            legalMove = currPos + 7;
                            foreach (pieceButton button in mainWindow.board.Children)
                            {
                                if (button.Type.Equals("") && button.Name.Equals(legalMove.ToString()))
                                {
                                    legalMoveList.Add((Board)Enum.Parse(typeof(Board), button.Name));

                                }
                                else if (!button.Type.Equals("") && button.Name.Equals(legalMove.ToString()))
                                {
                                    if (button.Type.Contains("P2"))
                                    {
                                        legalMoveList.Add((Board)Enum.Parse(typeof(Board), button.Name));
                                    }
                                }
                            }
                        }
                        legalMove = currPos + 8;
                        foreach (pieceButton button in mainWindow.board.Children)
                        {
                            if (button.Type.Equals("") && button.Name.Equals(legalMove.ToString()))
                            {
                                legalMoveList.Add((Board)Enum.Parse(typeof(Board), button.Name));

                            }
                            else if (!button.Type.Equals("") && button.Name.Equals(legalMove.ToString()))
                            {
                                if (button.Type.Contains("P2"))
                                {
                                    legalMoveList.Add((Board)Enum.Parse(typeof(Board), button.Name));
                                }
                            }
                        }
                        legalMove = currPos - 8;
                        foreach (pieceButton button in mainWindow.board.Children)
                        {
                            if (button.Type.Equals("") && button.Name.Equals(legalMove.ToString()))
                            {
                                legalMoveList.Add((Board)Enum.Parse(typeof(Board), button.Name));

                            }
                            else if (!button.Type.Equals("") && button.Name.Equals(legalMove.ToString()))
                            {
                                if (button.Type.Contains("P2"))
                                {
                                    legalMoveList.Add((Board)Enum.Parse(typeof(Board), button.Name));
                                }
                            }
                        }

                        foreach (Board space in legalMoveList)
                        {

                            tempName = space.ToString();


                            foreach (pieceButton button in mainWindow.board.Children)
                            {
                                if (tempName.Equals(button.Name))
                                {
                                    button.BackgroundColor = color;
                                    button.isPotentialMove = true;
                                    button.Opacity = 0.9;
                                }
                            }
                        }

                        legalMoveList.Clear();



                    }
                    else if (Type.Equals("P2King"))
                    {
                        currPos = (Board)Enum.Parse(typeof(Board), Name);
                        currPosLetter = Name.Substring(0, 1);
                        currPosNumber = Name.Substring(1);

                        if (!currPosLetter.Equals("h"))
                        {
                            legalMove = currPos + 1;
                            foreach (pieceButton button in mainWindow.board.Children)
                            {
                                if (button.Type.Equals("") && button.Name.Equals(legalMove.ToString()))
                                {
                                    legalMoveList.Add((Board)Enum.Parse(typeof(Board), button.Name));

                                }
                                else if (!button.Type.Equals("") && button.Name.Equals(legalMove.ToString()))
                                {
                                    if (button.Type.Contains("P1"))
                                    {
                                        legalMoveList.Add((Board)Enum.Parse(typeof(Board), button.Name));
                                    }
                                }
                            }
                            legalMove = currPos + 9;
                            foreach (pieceButton button in mainWindow.board.Children)
                            {
                                if (button.Type.Equals("") && button.Name.Equals(legalMove.ToString()))
                                {
                                    legalMoveList.Add((Board)Enum.Parse(typeof(Board), button.Name));

                                }
                                else if (!button.Type.Equals("") && button.Name.Equals(legalMove.ToString()))
                                {
                                    if (button.Type.Contains("P1"))
                                    {
                                        legalMoveList.Add((Board)Enum.Parse(typeof(Board), button.Name));
                                    }
                                }
                            }
                            legalMove = currPos - 7;
                            foreach (pieceButton button in mainWindow.board.Children)
                            {
                                if (button.Type.Equals("") && button.Name.Equals(legalMove.ToString()))
                                {
                                    legalMoveList.Add((Board)Enum.Parse(typeof(Board), button.Name));

                                }
                                else if (!button.Type.Equals("") && button.Name.Equals(legalMove.ToString()))
                                {
                                    if (button.Type.Contains("P1"))
                                    {
                                        legalMoveList.Add((Board)Enum.Parse(typeof(Board), button.Name));
                                    }
                                }
                            }
                        }
                        if (!currPosLetter.Equals("a"))
                        {
                            legalMove = currPos - 1;
                            foreach (pieceButton button in mainWindow.board.Children)
                            {
                                if (button.Type.Equals("") && button.Name.Equals(legalMove.ToString()))
                                {
                                    legalMoveList.Add((Board)Enum.Parse(typeof(Board), button.Name));

                                }
                                else if (!button.Type.Equals("") && button.Name.Equals(legalMove.ToString()))
                                {
                                    if (button.Type.Contains("P1"))
                                    {
                                        legalMoveList.Add((Board)Enum.Parse(typeof(Board), button.Name));
                                    }
                                }
                            }
                            legalMove = currPos - 9;
                            foreach (pieceButton button in mainWindow.board.Children)
                            {
                                if (button.Type.Equals("") && button.Name.Equals(legalMove.ToString()))
                                {
                                    legalMoveList.Add((Board)Enum.Parse(typeof(Board), button.Name));

                                }
                                else if (!button.Type.Equals("") && button.Name.Equals(legalMove.ToString()))
                                {
                                    if (button.Type.Contains("P1"))
                                    {
                                        legalMoveList.Add((Board)Enum.Parse(typeof(Board), button.Name));
                                    }
                                }
                            }
                            legalMove = currPos + 7;
                            foreach (pieceButton button in mainWindow.board.Children)
                            {
                                if (button.Type.Equals("") && button.Name.Equals(legalMove.ToString()))
                                {
                                    legalMoveList.Add((Board)Enum.Parse(typeof(Board), button.Name));

                                }
                                else if (!button.Type.Equals("") && button.Name.Equals(legalMove.ToString()))
                                {
                                    if (button.Type.Contains("P1"))
                                    {
                                        legalMoveList.Add((Board)Enum.Parse(typeof(Board), button.Name));
                                    }
                                }
                            }
                        }
                        legalMove = currPos + 8;
                        foreach (pieceButton button in mainWindow.board.Children)
                        {
                            if (button.Type.Equals("") && button.Name.Equals(legalMove.ToString()))
                            {
                                legalMoveList.Add((Board)Enum.Parse(typeof(Board), button.Name));

                            }
                            else if (!button.Type.Equals("") && button.Name.Equals(legalMove.ToString()))
                            {
                                if (button.Type.Contains("P1"))
                                {
                                    legalMoveList.Add((Board)Enum.Parse(typeof(Board), button.Name));
                                }
                            }
                        }
                        legalMove = currPos - 8;
                        foreach (pieceButton button in mainWindow.board.Children)
                        {
                            if (button.Type.Equals("") && button.Name.Equals(legalMove.ToString()))
                            {
                                legalMoveList.Add((Board)Enum.Parse(typeof(Board), button.Name));

                            }
                            else if (!button.Type.Equals("") && button.Name.Equals(legalMove.ToString()))
                            {
                                if (button.Type.Contains("P1"))
                                {
                                    legalMoveList.Add((Board)Enum.Parse(typeof(Board), button.Name));
                                }
                            }
                        }

                        foreach (Board space in legalMoveList)
                        {

                            tempName = space.ToString();


                            foreach (pieceButton button in mainWindow.board.Children)
                            {
                                if (tempName.Equals(button.Name))
                                {
                                    button.BackgroundColor = color;
                                    button.isPotentialMove = true;
                                    button.Opacity = 0.9;
                                }
                            }
                        }

                        legalMoveList.Clear();
                    }

                    #endregion
                    
                    //focus on clicked piece, by lowering opacity of all other buttons
                    foreach (Chess.pieceButton button in mainWindow.board.Children)
                    {
                        if (button.Name != Name)
                        {
                            button.Opacity = 0.6;
                        }
                    }
                    

                    
                }
                //if click is made outside of legal move, once a game piece was clicked and focused on to, this will return all buttons to normal
                #region
                else if (Opacity == 0.6)
                {

                    foreach (Chess.pieceButton button in mainWindow.board.Children)
                    {

                        if (button.Opacity == 1.0) //this is how current player turn is calculated
                        {
                            playerTurn = button.Type;
                        }
                    }


                    foreach (Chess.pieceButton button in mainWindow.board.Children)
                    {


                        //returns all pieces to original state before button piece was clicked to make a move
                        if(button.BackgroundColor == color)
                        {
                            button.BackgroundColor = button.originalColor;
                            button.isPotentialMove = false;
                        }

                        if (playerTurn.Contains("P1"))
                        {
                            if (button.Type.Contains("P1"))
                            {
                                button.Opacity = 1.0;
                            }else if (!button.Type.Contains("P1"))
                            {
                                button.Opacity = 0.75;
                            }

                        }
                        else if (playerTurn.Contains("P2"))
                        {
                            if (button.Type.Contains("P2"))
                            {
                                button.Opacity = 1.0;
                            }
                            else if (!button.Type.Contains("P2"))
                            {
                                button.Opacity = 0.75;
                            }
                        }



                    }
                }
                #endregion
            }
        }
    }
}
