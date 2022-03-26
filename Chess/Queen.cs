using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chess
{
    public class Queen : Figure
    {
        public Queen(FigureColorEnum _color, int _x, int _y) : base(_color, _x, _y)
        {

        }
        public override List<Position> GetFigureSteps(ChessBoard chessBoard)
        {
            Rook rook = new(Color, X, Y);
            List<Position> ortogonals = rook.GetFigureSteps(chessBoard);
            Bishop bishop = new(Color, X, Y);
            List<Position> diagonals = bishop.GetFigureSteps(chessBoard);
             List<Position> pos = ortogonals.Concat(diagonals).ToList();
            return pos;
        }
        public override char GetSymbol()
        {
            if (Color == FigureColorEnum.White)
                return '\u2655';
            else
                return '\u265B';
        }
    }
}
