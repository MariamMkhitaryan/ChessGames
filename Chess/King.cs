using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chess
{
    public class King : Figure
    {
        public King(FigureColorEnum _color, int _x, int _y) : base(_color, _x, _y)
        {

        }
        public override List<Position> GetFigureSteps(ChessBoard chessBoard)
        {
            List<Position> movesList = new List<Position>();
            Position p;
            {
                p = new Position(x - 1, y - 1);
                if (p.IsInBoard())
                {
                    Figure targetFigure = chessBoard.GetFigure(p);
                    if (p.CanBeAddedToSteps(chessBoard, targetFigure, Color))
                    {
                        movesList.Add(p);
                    }
                }
            }
            {
                p = new Position(x - 1, y);
                if (p.IsInBoard())
                {
                    Figure targetFigure = chessBoard.GetFigure(p);
                    if (p.CanBeAddedToSteps(chessBoard, targetFigure, Color))
                    {
                        movesList.Add(p);
                    }
                }
            }
            {
                p = new Position(x - 1, y + 1);
                if (p.IsInBoard())
                {
                    Figure targetFigure = chessBoard.GetFigure(p);
                    if (p.CanBeAddedToSteps(chessBoard, targetFigure, Color))
                    {
                        movesList.Add(p);
                    }
                }
            }
            {
                p = new Position(x, y + 1);
                if (p.IsInBoard())
                {
                    Figure targetFigure = chessBoard.GetFigure(p);
                    if (p.CanBeAddedToSteps(chessBoard, targetFigure, Color))
                    {
                        movesList.Add(p);
                    }
                }
            }
            {
                p = new Position(x + 1, y + 1);
                if (p.IsInBoard())
                {
                    Figure targetFigure = chessBoard.GetFigure(p);
                    if (p.CanBeAddedToSteps(chessBoard, targetFigure, Color))
                    {
                        movesList.Add(p);
                    }
                }
            }
            {
                p = new Position(x + 1, y);
                if (p.IsInBoard())
                {
                    Figure targetFigure = chessBoard.GetFigure(p);
                    if (p.CanBeAddedToSteps(chessBoard, targetFigure, Color))
                    {
                        movesList.Add(p);
                    }
                }
            }
            {
                p = new Position(x + 1, y - 1);
                if (p.IsInBoard())
                {
                    Figure targetFigure = chessBoard.GetFigure(p);
                    if (p.CanBeAddedToSteps(chessBoard, targetFigure, Color))
                    {
                        movesList.Add(p);
                    }
                }
            }
            {
                p = new Position(x, y - 1);
                if (p.IsInBoard())
                {
                    Figure targetFigure = chessBoard.GetFigure(p);
                    if (p.CanBeAddedToSteps(chessBoard, targetFigure, Color))
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
                return '\u2654';
            else
                return '\u265A';
        }
    }
}
