using TicTacToe.Enums;

namespace TicTacToe.Extensions
{
    public static class PlayerTypeExtensions
    {
        public static string ToUIString(this PlayerType playerType)
        {
            switch (playerType)
            {
                case PlayerType.Player1:
                    return "Player 1";
                    break;
                case PlayerType.Player2:
                    return "Player 2";
                default:
                    return playerType.ToString();
            }
        }
    }
}