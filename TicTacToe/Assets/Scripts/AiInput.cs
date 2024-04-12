namespace TicTacToe
{
    public class AiInput : InputBase
    {
        public override void EnableInput()
        {
            board.DisableCellInteractabilty();
        }

        public override void DisableInput()
        {
            board.DisableCellInteractabilty();
        }
    }
}
