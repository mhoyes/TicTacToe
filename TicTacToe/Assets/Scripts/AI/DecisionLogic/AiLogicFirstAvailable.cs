using System.Collections.Generic;
using System.Linq;

namespace TicTacToe.AI.Logic
{
    public class AiLogicFirstAvailable : AiLogicBase
    {
        public override GridCell ChooseMove()
        {
            List<GridCell> availableCells = board.Cells.Where(cell => cell.IsEmpty).ToList();
            GridCell chosenCell = availableCells.FirstOrDefault();
            
            return chosenCell;
        }
    }
}