using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Chess;

namespace Games
{
    public class ReachPoint
    {
        ChessBoard chessboard;
        public bool canMove;
        public bool isCheck = false;
        public bool isMat = false;
        public FigureColorEnum player;
        Figure[] whiteFigures;
        Figure[] blackFigures;
        public ChessBoard ChessBoard
        {
            get
            {
                return chessboard;
            }
            set
            {
                chessboard = value;
            }

        }
        public Figure[] WhiteFigures
        {
            get
            {
                return whiteFigures;
            }
            set
            {
                whiteFigures = value;
            }
        }
        public Figure[] BlackFigures
        {
            get
            {
                return blackFigures;
            }
            set
            {
                blackFigures = value;
            }
        }
        public ReachPoint()
        {
            ChessBoard = new ChessBoard();
        }
        public void Move(string startPoint, string target)
        {
            Position startPosition = new Position(startPoint);
            Position targetPosition = new Position(target);
            Move(startPosition, targetPosition);
        }
        public void Move(Position startPosition, Position targetPosition)
        {
            Figure figure = chessboard.GetFigure(startPosition);
            if (figure is not null)
            {
                if (figure.Color == player)
                {
                    Figure enemyFigure = chessboard.GetFigure(targetPosition);
                    figure.figurePassedSteps = ReturnPassedSteps(figure);
                    if (enemyFigure is not null)
                    {
                        enemyFigure.figurePassedSteps = ReturnPassedSteps(enemyFigure);
                    }
                    Position tempPosition = new Position(targetPosition.x, targetPosition.y);
                    if (figure.CanMove(targetPosition, chessboard))
                    {
                        canMove = true;
                        ChessBoard.Board[targetPosition.x, targetPosition.y] = ChessBoard.Board[startPosition.x, startPosition.y];
                        ChessBoard.Board[startPosition.x, startPosition.y] = '\u0020';
                        if (chessboard.TakeOnPass(figure, tempPosition))
                        {
                            ChessBoard.Board[figure.X, tempPosition.y] = '\u0020';
                        }
                        if (!CheckMoveAuthorisation(startPosition, targetPosition, chessboard, player, enemyFigure))
                        {
                            figure.figurePassedSteps = GetPassedSteps(player, tempPosition, figure);
                            if (enemyFigure is not null)
                            {
                                enemyFigure.figurePassedSteps = ReturnPassedSteps(enemyFigure);
                            }
                            if (chessboard.CheckEnemyKingIsUnderCheck(chessboard, player))
                            { isCheck = true; }
                            else { isCheck = false; }
                            if (chessboard.isMat(chessboard, player))
                            { isMat = true; }
                            else { isMat = false; }
                            if (player == FigureColorEnum.White)
                                player = FigureColorEnum.Black;
                            else
                                player = FigureColorEnum.White;
                        }
                    }
                    else if (chessboard.ShortCastling(chessboard, startPosition, targetPosition, figure, enemyFigure))
                    {
                        ChessBoard.Board[startPosition.x, startPosition.y + 2] = figure.GetSymbol();
                        ChessBoard.Board[startPosition.x, startPosition.y] = '\u0020';
                        ChessBoard.Board[startPosition.x, startPosition.y + 1] = enemyFigure.GetSymbol();
                        ChessBoard.Board[targetPosition.x, targetPosition.y] = '\u0020';

                        if (!CheckMoveAuthorisation(startPosition, targetPosition, chessboard, player, enemyFigure))
                        {
                            if (player == FigureColorEnum.White)
                                player = FigureColorEnum.Black;
                            else
                                player = FigureColorEnum.White;
                        }
                        else
                        {
                            ChessBoard.Board[startPosition.x, startPosition.y] = figure.GetSymbol();
                            ChessBoard.Board[startPosition.x, startPosition.y + 2] = '\u0020';
                            ChessBoard.Board[startPosition.x, startPosition.y + 1] = '\u0020';
                        }
                    }
                    else if (chessboard.LongCastling(chessboard, startPosition, targetPosition, figure, enemyFigure))
                    {
                        ChessBoard.Board[startPosition.x, startPosition.y - 2] = figure.GetSymbol();
                        ChessBoard.Board[startPosition.x, startPosition.y] = '\u0020';
                        ChessBoard.Board[startPosition.x, startPosition.y - 1] = enemyFigure.GetSymbol();
                        ChessBoard.Board[targetPosition.x, targetPosition.y] = '\u0020';
                        if (!CheckMoveAuthorisation(startPosition, targetPosition, chessboard, player, enemyFigure))
                        {
                            if (player == FigureColorEnum.White)
                                player = FigureColorEnum.Black;
                            else
                                player = FigureColorEnum.White;
                        }
                        else
                        {
                            ChessBoard.Board[startPosition.x, startPosition.y] = figure.GetSymbol();
                            ChessBoard.Board[startPosition.x, startPosition.y - 2] = '\u0020';
                            ChessBoard.Board[startPosition.x, startPosition.y - 1] = '\u0020';
                        }
                    }
                }
            }
        }
        public char[,] GetChessBoard()
        {
            return ChessBoard.Board;
        }
        public void PlayChess()
        {
            whiteFigures = GetWhiteFigures();
            ChessBoard.PutFigures(whiteFigures);
            blackFigures = GetBlackFigures();
            ChessBoard.PutFigures(blackFigures);
            player = FigureColorEnum.White;
        }
        Figure[] GetWhiteFigures()
        {
            var color = FigureColorEnum.White;
            var returnWhiteFigures = new Figure[16];
            returnWhiteFigures[0] = new King(color, 7, 4);
            returnWhiteFigures[1] = new Queen(color, 7, 3);
            returnWhiteFigures[2] = new Bishop(color, 7, 2);
            returnWhiteFigures[3] = new Bishop(color, 7, 5);
            returnWhiteFigures[4] = new Knight(color, 7, 1);
            returnWhiteFigures[5] = new Knight(color, 7, 6);
            returnWhiteFigures[6] = new Rook(color, 7, 0);
            returnWhiteFigures[7] = new Rook(color, 7, 7);
            for (int i = 0; i < 8; i++)
            {
                returnWhiteFigures[8 + i] = new Pawn(color, 6, i);
            }
            return returnWhiteFigures;
        }
        Figure[] GetBlackFigures()
        {
            var color = FigureColorEnum.Black;
            var returnBlackFigures = new Figure[16];
            returnBlackFigures[0] = new King(color, 0, 4);
            returnBlackFigures[1] = new Queen(color, 0, 3);
            returnBlackFigures[2] = new Bishop(color, 0, 2);
            returnBlackFigures[3] = new Bishop(color, 0, 5);
            returnBlackFigures[4] = new Knight(color, 0, 1);
            returnBlackFigures[5] = new Knight(color, 0, 6);
            returnBlackFigures[6] = new Rook(color, 0, 0);
            returnBlackFigures[7] = new Rook(color, 0, 7);
            for (int i = 0; i < 8; i++)
            {
                returnBlackFigures[8 + i] = new Pawn(color, 1, i);
            }
            return returnBlackFigures;
        }
        public bool CheckMoveAuthorisation(Position startPosition, Position targetPosition, ChessBoard chessBoard, FigureColorEnum player, Figure enemyFigure)
        {
            if (chessboard.GetUnauthorisedMove(chessboard, player))
            {
                ChessBoard.Board[startPosition.x, startPosition.y] = ChessBoard.Board[targetPosition.x, targetPosition.y];
                if (enemyFigure is not null)
                    ChessBoard.Board[targetPosition.x, targetPosition.y] = enemyFigure.GetSymbol();
                else
                    ChessBoard.Board[targetPosition.x, targetPosition.y] = '\u0020';
                return true;
            }
            return false;
        }
        public List<Position> GetPassedSteps(FigureColorEnum player, Position tempPosition, Figure figure)
        {
            if (player == FigureColorEnum.White)
            {
                for (int i = 0; i < WhiteFigures.Length; i++)
                {
                    if (figure.X.Equals(WhiteFigures[i].X) && figure.Y.Equals(WhiteFigures[i].Y))
                    {
                        WhiteFigures[i].figurePassedSteps.Add(tempPosition);
                        WhiteFigures[i].X = tempPosition.x;
                        WhiteFigures[i].Y = tempPosition.y;
                        return WhiteFigures[i].FigurePassedSteps;
                        break;
                    }
                }
            }
            else
            {
                for (int i = 0; i < BlackFigures.Length; i++)
                {
                    if (figure.X.Equals(BlackFigures[i].X) && figure.Y.Equals(BlackFigures[i].Y))
                    {
                        BlackFigures[i].figurePassedSteps.Add(tempPosition);
                        BlackFigures[i].X = tempPosition.x;
                        BlackFigures[i].Y = tempPosition.y;
                        return BlackFigures[i].FigurePassedSteps;
                        break;
                    }
                }

            }
            return null;
        }
        public List<Position> ReturnPassedSteps(Figure figure)
        {
            if (figure.Color == FigureColorEnum.White)
            {
                for (int i = 0; i < WhiteFigures.Length; i++)
                {
                    if (figure.X.Equals(WhiteFigures[i].X) && figure.Y.Equals(WhiteFigures[i].Y))
                    {
                        return WhiteFigures[i].FigurePassedSteps;
                        break;
                    }
                }
            }
            else
            {
                for (int i = 0; i < BlackFigures.Length; i++)
                {
                    if (figure.X.Equals(BlackFigures[i].X) && figure.Y.Equals(BlackFigures[i].Y))
                    {
                        return BlackFigures[i].FigurePassedSteps;
                        break;
                    }
                }

            }
            return null;
        }
        public void SavePositionInSQL()
        {
            SqlDb sqlDb = new SqlDb();
            for (int i = 0; i < 8; i++)
            {
                for (int j = 0; j < 8; j++)
                {
                    if (chessboard.Board[i, j] != '\u0020')
                    {
                        Position pos = new Position(i, j);
                        sqlDb.InsertFigure(chessboard.GetFigure(pos));
                    }
                }
            }
        }
        public void ReadDataFromSQL()
        {
            SqlDb sqlDb = new SqlDb();
            ChessBoard temp = new ChessBoard();
            temp.Board = sqlDb.Reader();
            for (int i = 0; i < 8; i++)
            {
                for (int j = 0; j < 8; j++)
                {
                    chessboard.Board[i, j]= temp.Board[i, j];
                }
            }
        }
    }
}
