using TicTacToe.Interfaces;

namespace TicTacToe.AI.Logic
{
    public abstract class AiLogicBase : IAiLogicBehavior
    {
        protected AiInput aiInput;
        protected GameBoard board;

        public virtual void Initialize(AiInput input, GameBoard gameboard)
        {
            aiInput = input;
            board = gameboard;
        }

        public abstract GridCell ChooseMove();
    }
}