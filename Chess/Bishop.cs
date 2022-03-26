using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chess
{
    public class Bishop : Figure
    {
        public Bishop(FigureColorEnum _color, int _x, int _y) : base(_color, _x, _y)
        {

        }
            public override List<Position> GetFigureSteps(ChessBoard chessBoard)
            {
            List<Position> movesList = new List<Position>();
            Position p;
            for (int i = 1; i <= 7; i++)
            {
                p = new Position(X + i, Y + i);
                if (p.IsInBoard())
                {
                    Figure targetFigure = chessBoard.GetFigure(p);
                    if (p.CanBeAddedToSteps(chessBoard, targetFigure, Color))
                    {
                       movesList.Add(p);
                    }
                    if (chessBoard.Board[p.x, p.y] != '\u0020')
                        break;
                }
                else
                    break;
            }
            for (int i = 1; i <= 7; i++)
            {
                p = new Position(X + i, Y - i);
                if (p.IsInBoard())
                {
                    Figure targetFigure = chessBoard.GetFigure(p);
                    if (p.CanBeAddedToSteps(chessBoard, targetFigure, Color))
                    {
                        movesList.Add(p);
                    }
                    if (chessBoard.Board[p.x, p.y] != '\u0020')
                        break;
                }
                else
                    break;
            }
            for (int i = 1; i <=7; i++)
            {
                p = new Position(X - i, Y + i);
                if (p.IsInBoard())
                {
                    Figure targetFigure = chessBoard.GetFigure(p);
                    if (p.CanBeAddedToSteps(chessBoard, targetFigure, Color))
                    {
                        movesList.Add(p);
                    }
                    if (chessBoard.Board[p.x, p.y] != '\u0020')
                        break;
                }
                else
                    break;
            }
            for (int i = 1; i <= 7; i++)
            {
                p = new Position(X - i, Y - i);
                if (p.IsInBoard())
                {
                    Figure targetFigure = chessBoard.GetFigure(p);
                    if (p.CanBeAddedToSteps(chessBoard, targetFigure, Color))
                    {
                        movesList.Add(p);
                    }
                    if (chessBoard.Board[p.x, p.y] != '\u0020')
                        break;
                }
                else
                    break;
            }
            return movesList;
        }
        public override char GetSymbol()
        {
            if (Color == FigureColorEnum.White)
                return '\u2657';
            else
                return '\u265D';
        }
    }
}
