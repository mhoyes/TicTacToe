namespace TicTacToe
{
    public class PlayerInput : InputBase
    {
        public override void EnableInput()
        {
            board.EnableCellInteractabilty();
        }

        public override void DisableInput()
        {
            board.DisableCellInteractabilty();
        }
    }
}
