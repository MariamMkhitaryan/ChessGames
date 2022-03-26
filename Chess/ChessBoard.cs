using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chess
{
    public class ChessBoard
    {
        char[,] board;
        public ChessBoard()
        {
            board = new char[8, 8];
            for (int i = 0; i < 8; i++)
            {
                for (int j = 0; j < 8; j++)
                {
                    board[i, j] = '\u0020';
                }
            }
        }
        public char[,] Board
        {
            get
            {
                return board;
            }
            set
            {
                board = value;
            }
        }
        public void PutFigures(Figure[] figures)
        {
            for (int i = 0; i < figures.Length; i++)
            {
                PutFigure(figures[i]);
            }
        }
        public void PutFigure(Figure figure)
        {
            board[figure.X, figure.Y] = figure.GetSymbol();
        }
        public Figure GetFigure(Position p)
        {
            Figure figure = null;
            char c = Board[p.x, p.y];
            switch (c)
            {
                case '\u2654':
                    {
                        figure = new King(FigureColorEnum.White, p.x, p.y);
                        break;
                    }
                case '\u265A':
                    {
                        figure = new King(FigureColorEnum.Black, p.x, p.y);
                        break;
                    }
                case '\u2655':
                    {
                        figure = new Queen(FigureColorEnum.White, p.x, p.y);
                        break;
                    }
                case '\u265B':
                    {
                        figure = new Queen(FigureColorEnum.Black, p.x, p.y);
                        break;
                    }
                case '\u2656':
                    {
                        figure = new Rook(FigureColorEnum.White, p.x, p.y);
                        break;
                    }
                case '\u265C':
                    {
                        figure = new Rook(FigureColorEnum.Black, p.x, p.y);
                        break;
                    }
                case '\u2657':
                    {
                        figure = new Bishop(FigureColorEnum.White, p.x, p.y);
                        break;
                    }
                case '\u265D':
                    {
                        figure = new Bishop(FigureColorEnum.Black, p.x, p.y);
                        break;
                    }
                case '\u2658':
                    {
                        figure = new Knight(FigureColorEnum.White, p.x, p.y);
                        break;
                    }
                case '\u265E':
                    {
                        figure = new Knight(FigureColorEnum.Black, p.x, p.y);
                        break;
                    }
                case '\u2659':
                    {
                        figure = new Pawn(FigureColorEnum.White, p.x, p.y);
                        break;
                    }
                case '\u265F':
                    {
                        figure = new Pawn(FigureColorEnum.Black, p.x, p.y);
                        break;
                    }
            }
            return figure;
        }
        public List<Position> GetAllMoves(ChessBoard chessBoard, FigureColorEnum player)//gets all possible moves for figures with given colour
        {
            List<Position> allMoves = new List<Position>();
            Figure figure = null;
            Position position;
            for (int i = 0; i < 8; i++)
            {
                for (int j = 0; j < 8; j++)
                {
                    position = new Position(i, j);
                    if (Board[i, j] != '\u0020')
                    {
                        figure = GetFigure(position);
                        if (player == figure.Color)
                        {
                            foreach (var item in figure.GetFigureSteps(chessBoard))
                            {
                                if (item.x != 0 && item.y != 0 || item.x != 0 && item.y == 0 || item.x == 0 && item.y != 0)
                                    allMoves.Add(item);
                            }
                        }
                    }
                }
            }
            return allMoves;
        }
        public bool CheckEnemyKingIsUnderCheck(ChessBoard chessBoard, FigureColorEnum player)//gets player's moves and looks for the enemy's king there 
        {
            List<Position> playerMoves = GetAllMoves(chessBoard, player);
            bool isCheck = false;
            foreach (var item in playerMoves)
            {
                if (player == FigureColorEnum.Black && Board[item.x, item.y] == '\u2654')
                    isCheck = true;
                if (player == FigureColorEnum.White && Board[item.x, item.y] == '\u265A')
                    isCheck = true;
            }
            return isCheck;
        }
        public bool GetUnauthorisedMove(ChessBoard chessBoard, FigureColorEnum player)//if after player's move player's king is under check, the move is anauthorised
        {
            List<Position> enemyMoves;
            if (player == FigureColorEnum.Black)
                enemyMoves = GetAllMoves(chessBoard, FigureColorEnum.White);
            else
                enemyMoves = GetAllMoves(chessBoard, FigureColorEnum.Black);
            bool isUnauthorisedMove = false;
            foreach (var item in enemyMoves)
            {
                if (player == FigureColorEnum.White && Board[item.x, item.y] == '\u2654')
                    isUnauthorisedMove = true;
                if (player == FigureColorEnum.Black && Board[item.x, item.y] == '\u265A')
                    isUnauthorisedMove = true;
            }
            return isUnauthorisedMove;
        }
        //public void IsCheck(ChessBoard chessBoard, FigureColorEnum player)//checks if after player's move enemy's king is under check
        //{
        //    bool kingIsCheck = CheckEnemyKingIsUnderCheck(chessBoard, player);
        //    if (player == FigureColorEnum.Black && kingIsCheck == true)
        //    {
        //        Console.WriteLine("White king is under check");
        //    }
        //    if (player == FigureColorEnum.White && kingIsCheck == true)
        //    {
        //        Console.WriteLine("Black king is under check");
        //    }
        //}
        public Figure GetKing(ChessBoard chessBoard, FigureColorEnum player)//gets the position of enemy's king
        {
            Figure king = null;
            Position position;
            for (int i = 0; i < 8; i++)
            {
                for (int j = 0; j < 8; j++)
                {
                    position = new Position(i, j);
                    if (player == FigureColorEnum.Black && Board[i, j] == '\u2654')//white king
                        king = GetFigure(position);
                    if (player == FigureColorEnum.White && Board[i, j] == '\u265A')//black king
                        king = GetFigure(position);
                }
            }
            return king;
        }
        public List<Figure> GetFigures(ChessBoard chessBoard, FigureColorEnum figureColor)//gets figures of given color
        {
            List<Figure> list = new List<Figure>();
            Position position;
            Figure figure;
            for (int i = 0; i < 8; i++)
            {
                for (int j = 0; j < 8; j++)
                {
                    position = new Position(i, j);
                    if (Board[i, j] != '\u0020')
                    {
                        figure = GetFigure(position);
                        if (figure.Color == figureColor)
                            list.Add(figure);
                    }
                }
            }
            return list;
        }
        public List<Figure> GetFiguresCanAttackEnemyKing(ChessBoard chessBoard, FigureColorEnum player)
        {
            List<Figure> FiguresCanAttackEnemyKing = new List<Figure>();
            List<Figure> myFigures = GetFigures(chessBoard, player);
            Position position;
            for (int i = 0; i < 8; i++)
            {
                for (int j = 0; j < 8; j++)
                {
                    if (player == FigureColorEnum.Black && Board[i, j] == '\u2654' || player == FigureColorEnum.White && Board[i, j] == '\u265A')
                    {
                        position = new Position(i, j);
                        foreach (Figure f in myFigures)
                        {
                            if (f.CanMove(position, chessBoard))
                                FiguresCanAttackEnemyKing.Add(f);
                        }
                    }
                }
            }
            return FiguresCanAttackEnemyKing;
        }
        public bool CanEnemyKingMove(ChessBoard chessBoard, FigureColorEnum player)
        {
            Figure enemyKing = GetKing(chessBoard, player);
            bool enemyKingCheck;
            List<Position> enemyKingSteps = new List<Position>();
            Position position;
            bool canEnemyKingStep = false;
            for (int i = 0; i < 8; i++)
            {
                for (int j = 0; j < 8; j++)
                {
                    position = new Position(i, j);
                    if (enemyKing.CanMove(position, chessBoard))
                    {
                        enemyKingSteps.Add(position);
                        Board[enemyKing.X, enemyKing.Y] = Board[position.x, position.y];
                        Board[position.x, position.y] = enemyKing.GetSymbol();
                        enemyKingCheck = CheckEnemyKingIsUnderCheck(chessBoard, player);
                        Board[position.x, position.y] = Board[enemyKing.X, enemyKing.Y];
                        Board[enemyKing.X, enemyKing.Y] = enemyKing.GetSymbol();
                        if (!enemyKingCheck)
                        {
                            canEnemyKingStep = true;
                            return canEnemyKingStep;
                            break;
                        }
                    }
                }
            }
            return canEnemyKingStep;
        }
        public bool CanEnemyFiguresProtectKing(ChessBoard chessBoard, FigureColorEnum player)
        {
            List<Figure> enemyFigures;
            if (player == FigureColorEnum.Black)
                enemyFigures = GetFigures(chessBoard, FigureColorEnum.White);
            else
                enemyFigures = GetFigures(chessBoard, FigureColorEnum.Black);
            Position position;
            bool enemyKingCheck;
            bool canEnemyFiguresProtectKing = false;
            for (int i = 0; i < 8; i++)
            {
                for (int j = 0; j < 8; j++)
                {
                    position = new Position(i, j);
                    foreach (var item in enemyFigures)
                    {
                        if (Board[position.x, position.y] == '\u0020')
                        {
                            if (item.CanMove(position, chessBoard))
                            {
                                Board[position.x, position.y] = Board[item.X, item.Y];
                                Board[item.X, item.Y] = '\u0020';
                                enemyKingCheck = CheckEnemyKingIsUnderCheck(chessBoard, player);
                                Board[item.X, item.Y] = Board[position.x, position.y];
                                Board[position.x, position.y] = '\u0020';
                                if (!enemyKingCheck)
                                {
                                    canEnemyFiguresProtectKing = true;
                                    return canEnemyFiguresProtectKing;
                                    break;
                                }
                            }
                        }
                    }
                }
            }
            return canEnemyFiguresProtectKing;
        }
        public bool CanEnemyFiguresEatPlayerFigures(ChessBoard chessBoard, FigureColorEnum player)
        {
            List<Figure> enemyFigures;
            if (player == FigureColorEnum.Black)
                enemyFigures = GetFigures(chessBoard, FigureColorEnum.White);
            else
                enemyFigures = GetFigures(chessBoard, FigureColorEnum.Black);
            List<Figure> FiguresCanAttackEnemyKing = GetFiguresCanAttackEnemyKing(chessBoard, player);
            Position position1;
            Figure tempFigure;
            bool enemyKingCheck;
            bool canEnemyFiguresEatPlayerFigure = false;
            foreach (Figure f in enemyFigures)
            {
                foreach (var item in FiguresCanAttackEnemyKing)
                {
                    position1 = new Position(item.X, item.Y);
                    if (f.CanMove(position1, chessBoard))
                    {
                        tempFigure = GetFigure(position1);
                        Board[position1.x, position1.y] = Board[f.X, f.Y];
                        Board[f.X, f.Y] = '\u0020';
                        enemyKingCheck = CheckEnemyKingIsUnderCheck(chessBoard, player);
                        Board[f.X, f.Y] = Board[position1.x, position1.y];
                        Board[position1.x, position1.y] = tempFigure.GetSymbol();
                        if (!enemyKingCheck)
                        {
                            canEnemyFiguresEatPlayerFigure = true;
                            return canEnemyFiguresEatPlayerFigure;
                            break;
                        }
                    }
                }
            }
            return canEnemyFiguresEatPlayerFigure;
        }
        public bool isMat(ChessBoard chessBoard, FigureColorEnum player)
        {
            bool enemyKingCheck = CheckEnemyKingIsUnderCheck(chessBoard, player);
            bool canEnemyKingStep = false;
            bool canEnemyFiguresSaveKing = false;
            bool canEnemyFiguresEatPlayerFigure = false;
            bool isMat = false;
            if (enemyKingCheck == true)
            {
                canEnemyKingStep = CanEnemyKingMove(chessBoard, player);
                canEnemyFiguresSaveKing = CanEnemyFiguresProtectKing(chessBoard, player);
                canEnemyFiguresEatPlayerFigure = CanEnemyFiguresEatPlayerFigures(chessBoard, player);
                if (canEnemyKingStep == false && canEnemyFiguresSaveKing == false && canEnemyFiguresEatPlayerFigure == false)
                    isMat = true;
                //if(isMat==true && player==FigureColorEnum.Black)
                //    Console.WriteLine("White King is Mat");
                //if (isMat==true && player == FigureColorEnum.White)
                //    Console.WriteLine("Black King is Mat");
            }
            return isMat;
        }
        public bool ShortCastling(ChessBoard chessBoard, Position startPosition, Position targetPosition,Figure figure,Figure targetFigure)
        {
            bool kingIsUnderCheck = false;
            if (startPosition.x == 7 && startPosition.y == 4 && Board[startPosition.x, startPosition.y] == '\u2654' && figure.FigurePassedSteps.Count==0)
            {
                if (targetPosition.x == 7 && targetPosition.y == 7 && Board[targetPosition.x, targetPosition.y] == '\u2656'&& targetFigure.FigurePassedSteps.Count==0)
                    if (Board[7, 5] == '\u0020' && Board[7, 6] == '\u0020')
                        if (!CheckEnemyKingIsUnderCheck(chessBoard, FigureColorEnum.Black))
                        {
                            Board[7, 5] = Board[startPosition.x, startPosition.y];
                            Board[startPosition.x, startPosition.y] = '\u0020';
                            kingIsUnderCheck = CheckEnemyKingIsUnderCheck(chessBoard, FigureColorEnum.Black);
                            Board[startPosition.x, startPosition.y] = Board[7, 5];
                            Board[7, 5] = '\u0020';
                            if (!kingIsUnderCheck)
                                return true;
                        }
            }
            else if (startPosition.x == 0 && startPosition.y == 4 && Board[startPosition.x, startPosition.y] == '\u265A' && figure.FigurePassedSteps.Count == 0)
            {
                if (targetPosition.x == 0 && targetPosition.y == 7 && Board[targetPosition.x, targetPosition.y] == '\u265C' && targetFigure.FigurePassedSteps.Count == 0)
                    if (Board[0, 5] == '\u0020' && Board[0, 6] == '\u0020')
                        if (!CheckEnemyKingIsUnderCheck(chessBoard, FigureColorEnum.Black))
                        {
                            Board[0, 5] = Board[startPosition.x, startPosition.y];
                            Board[startPosition.x, startPosition.y] = '\u0020';
                            kingIsUnderCheck = CheckEnemyKingIsUnderCheck(chessBoard, FigureColorEnum.White);
                            Board[startPosition.x, startPosition.y] = Board[0, 5];
                            Board[0, 5] = '\u0020';
                            if (!kingIsUnderCheck)
                                return true;
                        }
            }
        return false;
        }
        public bool LongCastling(ChessBoard chessBoard,Position startPosition, Position targetPosition, Figure figure, Figure targetFigure)
        {
            bool kingIsUnderCheck=false;
            if (startPosition.x == 7 && startPosition.y == 4 && Board[startPosition.x,startPosition.y] == '\u2654' && figure.FigurePassedSteps.Count == 0)
            {
                if (targetPosition.x == 7 && targetPosition.y == 0 && Board[targetPosition.x,targetPosition.y] == '\u2656' && targetFigure.FigurePassedSteps.Count == 0)
                    if (Board[7, 1] == '\u0020' && Board[7, 2] == '\u0020' && Board[7, 3] == '\u0020')
                        if (!CheckEnemyKingIsUnderCheck(chessBoard, FigureColorEnum.Black))
                        {
                            Board[7, 3] = Board[startPosition.x, startPosition.y];
                            Board[startPosition.x, startPosition.y] = '\u0020';
                            kingIsUnderCheck = CheckEnemyKingIsUnderCheck(chessBoard, FigureColorEnum.Black);
                            Board[startPosition.x, startPosition.y] = Board[7, 3];
                            Board[7, 3] = '\u0020';
                            if (!kingIsUnderCheck)
                                return true;
                        }
            }
            if (startPosition.x == 0 && startPosition.y == 4 && Board[startPosition.x, startPosition.y] == '\u265A' && figure.FigurePassedSteps.Count == 0)
            {
                if (targetPosition.x == 0 && targetPosition.y == 0 && Board[targetPosition.x,targetPosition.y] == '\u265C' && targetFigure.FigurePassedSteps.Count == 0)
                    if (Board[0, 1] == '\u0020' && Board[0, 2] == '\u0020' && Board[0, 3] == '\u0020')
                        if (!CheckEnemyKingIsUnderCheck(chessBoard, FigureColorEnum.White))
                        {
                            Board[0, 3] = Board[startPosition.x, startPosition.y];
                            Board[startPosition.x, startPosition.y] = '\u0020';
                            kingIsUnderCheck = CheckEnemyKingIsUnderCheck(chessBoard, FigureColorEnum.White);
                            Board[startPosition.x, startPosition.y] = Board[0, 3];
                            Board[0, 3] = '\u0020';
                            if (!kingIsUnderCheck)
                                return true;
                        }
            }
            return false;
        }
        public bool TakeOnPass(Figure figure, Position tempPosition)
        {
            if (figure.GetSymbol() == '\u2659')
            { 
            if(tempPosition.x==figure.X-1 && (tempPosition.y==figure.Y-1 || tempPosition.y == figure.Y + 1))
                    return true;
            }
            if (figure.GetSymbol() == '\u265F')
            {
                if (tempPosition.x == figure.X + 1 && (tempPosition.y == figure.Y - 1 || tempPosition.y == figure.Y + 1))
                    return true;
            }
            return false;
        }
    }
}