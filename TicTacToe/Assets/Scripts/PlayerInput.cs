using UnityEngine;

namespace TicTacToe
{
    public class PlayerInput : InputBase
    {
        public override void EnableInput()
        {
            base.EnableInput();
            GameBoard.OnCellClicked += OnCellClicked;
            GameBoard.EnableCellInteractabilty();
        }

        public override void DisableInput()
        {
            base.DisableInput();
            GameBoard.OnCellClicked -= OnCellClicked;
            GameBoard.DisableCellInteractabilty();
        }

        private void OnCellClicked(GridCell cell)
        {
            if (!inputEnabled)
            {
                Debug.LogError("Attempted to play turn that wasn't yours");
                return;
            }

            PlayerInputReceived?.Invoke(cell);
        }
    }
}
