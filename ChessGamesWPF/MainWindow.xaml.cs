using System;
using System.Collections.Generic;
using System.IO;
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
using Games;
namespace ChessGamesWPF
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        ReachPoint reach;
        char[,] board;
        public MainWindow()
        {
            InitializeComponent();
            reach = new ReachPoint();
            reach.PlayChess();
            MessageBoxResult result = MessageBox.Show("Do you want to start a new game?", "Confirmation",
                                                        MessageBoxButton.YesNo,
                                                        MessageBoxImage.Question);
            if (result == MessageBoxResult.No)
            {
                reach.ReadDataFromSQL();
            }
            board = reach.GetChessBoard();
            PrintChessboard(reach.GetChessBoard());
        }
        void PrintChessboard(char[,] board)
        {
            for (int i = 0; i < 8; i++)
            {
                for (int j = 0; j < 8; j++)
                {
                    char c = board[i, j];
                    var finalImage = new Image();
                    finalImage.Width = 50;
                    finalImage.Height = 50;
                    BitmapImage logo = new BitmapImage();

                    switch (c)
                    {
                        case '\u2657':
                            finalImage.Source = new BitmapImage(new Uri(@"graphics\whiteBishop.png", UriKind.RelativeOrAbsolute));
                            break;
                        case '\u265D':
                            finalImage.Source = new BitmapImage(new Uri(@"graphics\blackBishop.png", UriKind.RelativeOrAbsolute));
                            break;
                        case '\u2654':
                            finalImage.Source = new BitmapImage(new Uri(@"graphics\whiteKing.png", UriKind.RelativeOrAbsolute));
                            break;
                        case '\u265A':
                            finalImage.Source = new BitmapImage(new Uri(@"graphics\blackKing.png", UriKind.RelativeOrAbsolute));
                            break;
                        case '\u2658':
                            finalImage.Source = new BitmapImage(new Uri(@"graphics\whiteKnight.png", UriKind.RelativeOrAbsolute));
                            break;
                        case '\u265E':
                            finalImage.Source = new BitmapImage(new Uri(@"graphics\blackKnight.png", UriKind.RelativeOrAbsolute));
                            break;
                        case '\u2659':
                            finalImage.Source = new BitmapImage(new Uri(@"graphics\whitePawn.png", UriKind.RelativeOrAbsolute));
                            break;
                        case '\u265F':
                            finalImage.Source = new BitmapImage(new Uri(@"graphics\blackPawn.png", UriKind.RelativeOrAbsolute));
                            break;
                        case '\u2655':
                            finalImage.Source = new BitmapImage(new Uri(@"graphics\whiteQueen.png", UriKind.RelativeOrAbsolute));
                            break;
                        case '\u265B':
                            finalImage.Source = new BitmapImage(new Uri(@"graphics\blackQueen.png", UriKind.RelativeOrAbsolute));
                            break;
                        case '\u2656':
                            finalImage.Source = new BitmapImage(new Uri(@"graphics\whiteRook.png", UriKind.RelativeOrAbsolute));
                            break;
                        case '\u265C':
                            finalImage.Source = new BitmapImage(new Uri(@"graphics\blackRook.png", UriKind.RelativeOrAbsolute));
                            break;
                    }
                    RowsAndColumns.Children.Add(finalImage);    
                    Grid.SetRow(finalImage, i);
                    Grid.SetColumn(finalImage, j);
                }
            }
        }

        private void button_Click(object sender, RoutedEventArgs e)
        {
            string start = startPoint.Text.ToString();
            start = Validation.Cordinates(start);
            if (start == null)
            {
                MessageBox.Show("Incorrect cordinates!!!!");
            }
            string finish = target.Text.ToString();
            finish = Validation.Cordinates(finish);
            if (finish == null)
            {
                MessageBox.Show("Incorrect cordinates!!!!");
            }
            reach.Move(start, finish);

            if (reach.canMove)
            {
                if (board[int.Parse(start[1].ToString()), int.Parse(start[0].ToString())] == '\u0020')
                {
                    RowsAndColumns.Children.Clear();
                    PrintChessboard(reach.GetChessBoard());
                }
                    if (reach.isCheck)
                {
                    MessageBox.Show("King is under check");
                }
                if (reach.isMat)
                {
                    MessageBox.Show("Mat");
                }
            }

        }
        class Validation
        {
            public static string Cordinates(string position)
            {
                if (Enum.TryParse(position[0].ToString(), out LetterCordinate x))
                {
                    int wpx = (int)x;
                    if (int.TryParse(position[1].ToString(), out int y))
                    {
                        int wpy = 8 - y;

                        if (wpx >= 0 && wpx <= 7 && wpy >= 0 && wpy <= 7)
                            return $"{wpx}{wpy}";
                        else
                            return null;
                    }
                    else
                        return null;
                }
                else
                    return null;
            }
        }
        enum LetterCordinate
        {
            A,
            B,
            C,
            D,
            E,
            F,
            G,
            H
        }

        private void buttonQuitAndSave_Click(object sender, RoutedEventArgs e)
        {
            reach.SavePositionInSQL();
        }

        private void buttonQuit_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
