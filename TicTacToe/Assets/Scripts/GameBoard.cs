using System;
using System.Collections.Generic;
using System.Linq;
using TicTacToe.Enums;
using TicTacToe.Extensions;
using UnityEngine;

namespace TicTacToe
{
    public class GameBoard : MonoBehaviour
    {
        public static Action<GridCell> OnCellClicked;

        public List<GridCell> Cells => cells;
        public int BoardWidth => boardSize_X;
        public int BoardHeight=> boardSize_Y;
        
        [SerializeField] private int boardSize_X = 3;
        [SerializeField] private int boardSize_Y = 3;
        [SerializeField] private Transform grid;
        [SerializeField] private CanvasGroup canvasGroup;

        [SerializeField] private GridCell gridCellPrefab;

        private GridCellType currentCellType;
        private List<GridCell> cells;

        public void Initialize()
        {
            cells = new List<GridCell>();
            GenerateCells();
        }

        private void GenerateCells()
        {
            grid.transform.ClearChildren();
            
            for (int y = 0; y < boardSize_Y; y++)
            {
                for (int x = 0; x < boardSize_X; x++)
                {
                    GridCell cell = Instantiate(gridCellPrefab, grid.transform);
                    cell.Initialize(x, y, OnGridCellClicked);
                    cells.Add(cell);
                }
            }
        }

        public void EnableCellInteractabilty()
        {
            canvasGroup.interactable = true;
        }

        public void DisableCellInteractabilty()
        {
            canvasGroup.interactable = false;
        }

        private void OnGridCellClicked(GridCell cell)
        {
            OnCellClicked?.Invoke(cell);
        }

        public bool HasEmptyCells()
        {
            return cells.Any(cell => cell.IsEmpty);
        }

        public GridCell GetCell(int x, int y)
        {
            if (x < 0 || x >= boardSize_X || y < 0 || y >= boardSize_Y)
            {
                throw new ArgumentException($"Invalid indices. x and y must be between 0 and {boardSize_X-1}.");
            }
        
            // Calculate the index in a 1D list based on x and y indices
            int index = x + y * boardSize_Y;
            return cells[index];
        }

        public GridCellType CalculateWinner()
        {
            // Check Diagonal
            if (GetCell(0, 0).CellType == GetCell(1, 1).CellType &&
                GetCell(1, 1).CellType == GetCell(2, 2).CellType &&
                !GetCell(2, 2).IsEmpty)
            {
                return GetCell(2, 2).CellType;
            }
            
            // Check Reverse Diagonal
            if (GetCell(0, 2).CellType == GetCell(1, 1).CellType &&
                     GetCell(1, 1).CellType == GetCell(2, 0).CellType &&
                     !GetCell(2, 0).IsEmpty)
            {
                return GetCell(2, 0).CellType;
            }
            
            // Check vertical Wins 
            for (int x = 0; x < boardSize_X; x++)
            {
                if (GetCell(x, 0).CellType == GetCell(x, 1).CellType &&
                    GetCell(x, 1).CellType == GetCell(x, 2).CellType &&
                    !GetCell(x, 2).IsEmpty)
                {
                    return GetCell(x, 0).CellType;
                }
            }

            // Check Horizontal Wins
            for (int y = 0; y < boardSize_Y; y++)
            {
                if (GetCell(0, y).CellType == GetCell(1, y).CellType &&
                    GetCell(1, y).CellType == GetCell(2, y).CellType &&
                    !GetCell(2, y).IsEmpty)
                {
                    return GetCell(2, y).CellType;
                }
            }
            
            // No Winner 
            return GridCellType.NONE;
        }
    }
}
