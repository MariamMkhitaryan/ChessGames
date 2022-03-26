using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chess
{
    public class Pawn : Figure
    {
        public Pawn(FigureColorEnum _color, int _x, int _y) : base(_color, _x, _y)
        {

        }
        public override List<Position> GetFigureSteps(ChessBoard chessBoard)
        {
            List<Position> movesList = new List<Position>();
            Position p;
            if (Color == FigureColorEnum.White)
            {
                if (X == 6 && chessBoard.Board[5, Y] == '\u0020' && chessBoard.Board[4, Y] == '\u0020')
                {
                    p = new Position(4, Y);
                    movesList.Add(p);
                }
                p = new Position(X - 1, Y);
                {
                    if (p.IsInBoard())
                    {
                        Figure targetFigure = chessBoard.GetFigure(p);
                        if (chessBoard.Board[p.x, p.y] == '\u0020')
                        {
                            movesList.Add(p);
                        }
                    }
                }
                p = new Position(X - 1, Y - 1);
                {
                    if (p.IsInBoard())
                    {
                        Figure targetFigure = chessBoard.GetFigure(p);
                        if (chessBoard.Board[p.x, p.y] == '\u0020')
                        { }
                        else if (Color != targetFigure.Color)
                        {
                            movesList.Add(p);
                        }
                    }
                }
                p = new Position(X - 1, Y + 1);
                {
                    if (p.IsInBoard())
                    {
                        Figure targetFigure = chessBoard.GetFigure(p);
                        if (chessBoard.Board[p.x, p.y] == '\u0020')
                        { }
                        else if (Color != targetFigure.Color)
                        {
                            movesList.Add(p);
                        }
                    }
                }
                if (X == 3)//take on the pass 
                {
                    p = new Position(X - 1, Y + 1);
                    if (p.IsInBoard())
                        if (chessBoard.Board[X, Y + 1] == '\u265F' && chessBoard.Board[X-1,Y+1]== '\u0020')
                    {
                        movesList.Add(p);
                    }
                    p = new Position(X - 1, Y - 1);
                    if (p.IsInBoard())
                        if (chessBoard.Board[X, Y - 1] == '\u265F' && chessBoard.Board[X - 1, Y - 1] == '\u0020')
                    {
                            movesList.Add(p);
                    }
                }
            }
            if (Color == FigureColorEnum.Black)
            {
                if (X == 1 && chessBoard.Board[2, Y] == '\u0020' && chessBoard.Board[3, Y] == '\u0020')
                {
                    p = new Position(3, Y);
                    movesList.Add(p);
                }
                p = new Position(X + 1, Y);
                {
                    if (p.IsInBoard())
                    {
                        Figure targetFigure = chessBoard.GetFigure(p);
                        if (chessBoard.Board[p.x, p.y] == '\u0020')
                        {
                            movesList.Add(p);
                        }
                    }
                }
                p = new Position(X + 1, Y - 1);
                {
                    if (p.IsInBoard())
                    {
                        Figure targetFigure = chessBoard.GetFigure(p);
                        if (chessBoard.Board[p.x, p.y] == '\u0020')
                        { }
                        else if (Color != targetFigure.Color)
                        {
                            movesList.Add(p);
                        }
                    }
                }
                p = new Position(X + 1, Y + 1);
                {
                    if (p.IsInBoard())
                    {
                        Figure targetFigure = chessBoard.GetFigure(p);
                        if (chessBoard.Board[p.x, p.y] == '\u0020')
                        { }
                        else if (Color != targetFigure.Color)
                        {
                            movesList.Add(p);
                        }
                    }
                }
                if (X == 4)//take on the pass 
                {
                    p = new Position(X + 1, Y + 1);
                    if (p.IsInBoard())
                        if (chessBoard.Board[X, Y + 1] == '\u2659' && chessBoard.Board[X + 1, Y + 1] == '\u0020')
                    {
                            movesList.Add(p);
                    }
                    p = new Position(X + 1, Y - 1);
                    if (p.IsInBoard())
                        if (chessBoard.Board[X, Y - 1] == '\u2659' && chessBoard.Board[X + 1, Y - 1] == '\u0020')
                    {
                            movesList.Add(p);
                    }
                }
            }
            return movesList;
        }
        public override char GetSymbol()
        {
            if (Color == FigureColorEnum.White)
                return '\u2659';
            else
                return '\u265F';
        }
    }
}
