using TicTacToe.Enums;

namespace TicTacToe.Interfaces
{
    public interface IPlayerInput
    {
        public PlayerType PlayerType { get; }
        public GridCellType CellType { get; }
        public void Initialize(GridCellType cellType);
        public void EnableInput();

        public void DisableInput();
        public bool IsPlayerControlled();
    }
}
