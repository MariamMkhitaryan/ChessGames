using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chess
{
    public struct Position
    {
        public int x;
        public int y;
        public Position(int xPos, int yPos)
        {
            x = xPos;
            y = yPos;
        }
        public Position(string point)
        {
            x = int.Parse(point[1].ToString());
            y = int.Parse(point[0].ToString());
        }
        //Cheks if the position in board
        public bool IsInBoard()
        { 
        if(x>=0 && x<8 && y>=0 && y<8)
                return true;    
        else 
                return false;   
        }
        //includes position to possible moves ,if it is empty or occupied by an enemy figure
        public bool CanBeAddedToSteps(ChessBoard chessBoard,Figure targetFigure,FigureColorEnum Color)
        { 
        if (chessBoard.Board[x, y] == '\u0020')
            return true;
            else if (Color != targetFigure.Color)
                return true ;
        else return false;  
        }
    }   
}
