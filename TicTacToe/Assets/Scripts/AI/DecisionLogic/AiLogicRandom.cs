using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace TicTacToe.AI.Logic
{
    public class AiLogicRandom : AiLogicBase
    {
        public override GridCell ChooseMove()
        {
            List<GridCell> availableCells = board.Cells.Where(cell => cell.IsEmpty).ToList();
            int randomIndex = Random.Range(0, availableCells.Count);

            GridCell chosenCell = availableCells[randomIndex];
            return chosenCell;
        }
    }
}