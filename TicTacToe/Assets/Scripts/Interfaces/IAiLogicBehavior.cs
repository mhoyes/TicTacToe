namespace TicTacToe.Interfaces
{
    public interface IAiLogicBehavior
    {
        public void Initialize(AiInput aiInput, GameBoard gameBoard);
        public GridCell ChooseMove();
    }
}