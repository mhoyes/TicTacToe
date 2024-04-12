using TicTacToe.Enums;
using UnityEngine;

namespace TicTacToe.AI.Logic
{
    public class AiLogicSmart : AiLogicBase
    {
        private GridCellType minimizingCellType;
        private GridCellType maximizingCellType;

        public override void Initialize(AiInput input, GameBoard gameboard)
        {
            base.Initialize(input, gameboard);
            maximizingCellType = input.CellType;

            if (maximizingCellType == GridCellType.X)
            {
                minimizingCellType = GridCellType.O;
            }
            else
            {
                minimizingCellType = GridCellType.O;
            }
        }

        public override GridCell ChooseMove()
        {
            int bestScore = int.MinValue;
            GridCell bestMove = null;

            for (int y = 0; y < board.BoardHeight; y++)
            {
                for (int x = 0; x < board.BoardWidth; x++)
                {
                    GridCell cell = board.GetCell(x, y);
                    if (cell.IsEmpty)
                    {
                        // Set the cell as though its being chosen
                        cell.SetCellType(aiInput.CellType);
                        
                        // Calculate a score based on that choice
                        int moveScore = Minimax(aiInput.LogicConfig.MovesAheadToPredict, false);
                        
                        // Reset the cell so that all others are scored the same way
                        cell.SetCellType(GridCellType.NONE);
                        
                        if (moveScore > bestScore)
                        {
                            bestScore = moveScore;
                            bestMove = cell;
                        }
                    }
                }
            }
            
            return bestMove;
        }

        private int Minimax(int depth, bool isMaximizingPlayer)
        {
            int score = EvaluateScore();

            // If the score is not 0 (game over), or the depth has been reached, return the calculated score
            if (score != 0 || depth == 0)
            {
                return score;
            }

            // If depth is reached, return the s
            if (isMaximizingPlayer)
            {
                int bestScore = int.MinValue;

                for (int y = 0; y < board.BoardHeight; y++)
                {
                    for (int x = 0; x < board.BoardWidth; x++)
                    {
                        GridCell cell = board.GetCell(x, y);
                        if (cell.IsEmpty)
                        {
                            cell.SetCellType(GetCellType(true));
                            int currentScore = Minimax(depth - 1, false);
                            bestScore = Mathf.Max(bestScore, currentScore);
                            cell.SetCellType(GridCellType.NONE);
                        }
                    }
                }

                return bestScore;
            }
            else
            {
                int bestScore = int.MaxValue;

                for (int y = 0; y < board.BoardHeight; y++)
                {
                    for (int x = 0; x < board.BoardWidth; x++)
                    {
                        GridCell cell = board.GetCell(x, y);
                        if (cell.IsEmpty)
                        {
                            cell.SetCellType(GetCellType(false));
                            int currentScore = Minimax(depth - 1, true);
                            bestScore = Mathf.Min(bestScore, currentScore);
                            cell.SetCellType(GridCellType.NONE);
                        }
                    }
                }

                return bestScore;
            }
        }

        private int EvaluateScore()
        {
            int maximizingScore = 10;
            int minimizingScore = -10;
            
            for (int index = 0; index < board.BoardWidth; index++)
            {
                // Check horizontal
                if (board.GetCell(index, 0).CellType == board.GetCell(index, 1).CellType &&
                    board.GetCell(index, 1).CellType == board.GetCell(index, 2).CellType)
                {
                    GridCell cell = board.GetCell(index, 0);
                    if (cell.CellType == aiInput.CellType)
                    {
                        // Maximizing player wins
                        return maximizingScore;
                    }
                    else if (cell.CellType != GridCellType.NONE && cell.CellType != aiInput.CellType)
                    {
                        // Minimizing player wins
                        return minimizingScore;
                    }
                }
                
                // Check vertical
                if (board.GetCell(0, index).CellType == board.GetCell(1, index).CellType &&
                    board.GetCell(1, index).CellType == board.GetCell(2, index).CellType)
                {
                    GridCell cell = board.GetCell(0, index);
                    if (cell.CellType == aiInput.CellType)
                    {
                        // Maximizing player wins
                        return maximizingScore;
                    }
                    else if (cell.CellType != GridCellType.NONE && cell.CellType != aiInput.CellType)
                    {
                        // Minimizing player wins
                        return minimizingScore;
                    }
                }
                
                // Check diagonal
                if (board.GetCell(0, 0).CellType == board.GetCell(1, 1).CellType &&
                    board.GetCell(1, 1).CellType == board.GetCell(2, 2).CellType)
                {
                    GridCell cell = board.GetCell(0, 0);
                    if (cell.CellType == aiInput.CellType)
                    {
                        // Maximizing player wins
                        return maximizingScore;
                    }
                    else if (cell.CellType != GridCellType.NONE && cell.CellType != aiInput.CellType)
                    {
                        // Minimizing player wins
                        return minimizingScore;
                    }
                }
                
                // Check reverse diagonal
                if (board.GetCell(0, 2).CellType == board.GetCell(1, 1).CellType &&
                    board.GetCell(1, 1).CellType == board.GetCell(2, 0).CellType)
                {
                    GridCell cell = board.GetCell(0, 2);
                    if (cell.CellType == aiInput.CellType)
                    {
                        // Maximizing player wins
                        return maximizingScore;
                    }
                    else if (cell.CellType != GridCellType.NONE && cell.CellType != aiInput.CellType)
                    {
                        // Minimizing player wins
                        return minimizingScore;
                    }
                }
            }

            // If no winner yet, return 0 (draw or game still ongoing)
            return 0;
        }

        private GridCellType GetCellType(bool isMaximizingPlayer)
        {
            return isMaximizingPlayer ? maximizingCellType : minimizingCellType;
        }
    }
}