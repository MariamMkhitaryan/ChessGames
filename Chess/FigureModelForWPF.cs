using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chess
{
    enum FigureWPFNamesEnum : byte
    {
        Pawn = 0,
        Bishop = 1,
        Knight = 2,
        Rook = 3,
        Queen = 4,
        King = 5
    }
    enum FigureWPFColorsEnum : byte
    {
        White = 0,
        Black = 1
    }
    public class FigureWPFModel
    {
        public byte FigureColor { get; set; }
        public byte FigureID { get; set; }
        public string FigureIconPath { get; set; }
    }
    public static class FiguresList
    {
        public static List<FigureWPFModel> figuresList;
        static FiguresList()
        {
            figuresList = new List<FigureWPFModel>();
            FillFiguresList();
        }
        private static void FillFiguresList()
        {
            figuresList.Add(new FigureWPFModel
            {
                FigureColor = (byte)FigureWPFColorsEnum.White,
                FigureID = (byte)FigureWPFNamesEnum.Pawn,
                FigureIconPath = @"graphics\blackKing.png"
            });
        }
    }
}
