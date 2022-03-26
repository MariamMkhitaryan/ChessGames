using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chess
{
    public abstract class Figure
    {
        protected int x;
        protected int y;
        protected FigureColorEnum color;
        public List<Position> figurePassedSteps;
        public Figure()
        {
            //x = 0;
            //y = 0;
            //color = FigureColorEnum.White;
        }
        public Figure(FigureColorEnum _color, int _x, int _y)
        {
            color = _color;
            x = _x;
            y = _y;
            figurePassedSteps = new List<Position>();
        }

        public int X
        {
            get => x;
            set => x = value;
        }
        public int Y
        {
            get => y;
            set => y = value;
        }
        public FigureColorEnum Color
        {
            get => color;
        }
        public List<Position> FigurePassedSteps
        {
            get => figurePassedSteps;
            set => figurePassedSteps = value;
        }
     
    public bool CanMove(Position finishPos, ChessBoard chessboard)
    {
        bool b = false;
        foreach (var item in GetFigureSteps(chessboard))
        {
            //if (item.x != 0 && item.y != 0 || item.x != 0 && item.y == 0 || item.x == 0 && item.y != 0)
            //{
            if (item.x == finishPos.x && item.y == finishPos.y)
            {
                b = true;
                break;
            }
            //}
        }
        return b;
    }
    public abstract List<Position> GetFigureSteps(ChessBoard chessBoard);
    public abstract char GetSymbol();
}
}
