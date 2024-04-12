using TicTacToe.Enums;

namespace TicTacToe
{
    public struct StateChangedData
    {
        public GameState state;
        public PlayerType? playerTurn;
    }
}