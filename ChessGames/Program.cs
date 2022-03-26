using Games;
using System;

Console.Write("Choose the game <<1-PrintBoard(), 2-PrintBoardWithFigures>>:");
int game = int.Parse(Console.ReadLine());
Start(game);


void Start(int game)
{
    if (game == 1)
    {
        PrintBoard();
    }
    else if (game == 2)
    {
        PrintBoardWithFigures();
    }
}
void PrintBoard()
{
    for (int i = 0; i < 9; i++)
    {
        Console.BackgroundColor = ConsoleColor.Black;
        if ((8 - i) != 0)
            Console.Write(8 - i + " ");
        for (int j = 0; j < 9; j++)
        {
            if (j != 0 && i != 8)
            {
                if ((i + j) % 2 == 0)
                {
                    Console.BackgroundColor = ConsoleColor.Black;
                    Console.Write("   ");
                }
                else
                {
                    Console.BackgroundColor = ConsoleColor.White;
                    Console.Write("   ");
                }
            }
        }
        if (i == 8)
            Console.Write("  A  B  C  D  E  F  G  H");
        Console.WriteLine();
    }
}
void PrintBoardWithFigures()
{
    var reach = new ReachPoint();
    reach.PlayChess();
    PrintChessboard(reach.GetChessBoard());
    while (true)
    {
        Console.Write("Input startpoint coordinates (A-H) (1-8):");
        string startPoint = Console.ReadLine();
        startPoint = Validation.Cordinates(startPoint);
        if (startPoint == null)
            Console.WriteLine("Incorrect cordinates!!!!!!!");

        Console.Write("Input point coordinates (A-H) (1-8):");
        string target = Console.ReadLine();
        target = Validation.Cordinates(target);
        if (target == null)
            Console.WriteLine("Incorrect cordinates!!!!!!!");
        Console.Clear();
        reach.Move(startPoint, target);
        if (reach.isCheck)
        {
                Console.WriteLine("King is under check");
        }
        if (reach.isMat)
        {
            Console.WriteLine("Mat");
        }
        PrintChessboard(reach.GetChessBoard());
    }
}
void PrintChessboard(char[,] board)
{
    Console.OutputEncoding = System.Text.Encoding.Unicode;
    string s = "    A B C D E F G H";
    Console.WriteLine(s);
    for (int i = 0; i < 8; i++)
    {
        Console.Write(8 - i + "  ");
        for (int j = 0; j < 8; j++)
        {
            if ((i + j) % 2 == 0)
            {
                Console.BackgroundColor = ConsoleColor.White;
            }
            if (board[i, j] != '\u265F')
                Console.Write(board[i, j].ToString().PadRight(2));
            else
                Console.Write(board[i, j].ToString());
            Console.ResetColor();
        }
        Console.WriteLine("  " + (8 - i));
    }
    Console.WriteLine(s);
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




