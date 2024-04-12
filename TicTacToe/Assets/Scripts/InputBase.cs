using TicTacToe.Attributes;
using TicTacToe.Enums;
using TicTacToe.Interfaces;
using UnityEngine;

namespace TicTacToe
{
    public abstract class InputBase : MonoBehaviour, IPlayerInput
    {
        public PlayerType PlayerType => playerType;
        public GridCellType CellType => _cellType;
        public GameBoard GameBoard => board;

        protected GameBoard board; 

        [SerializeField]
        protected PlayerType playerType; 
        
        [ReadOnly, SerializeField]
        protected GridCellType _cellType = GridCellType.NONE;

        private void Awake()
        {
            board = GetComponentInParent<GameController>().GameBoard;
        }

        public virtual void Initialize(GridCellType cellType)
        {
            _cellType = cellType;
            DisableInput();
        }

        public abstract void EnableInput();

        public abstract void DisableInput();

        public bool IsPlayerControlled()
        {
            return playerType != PlayerType.AI;
        }
    }
}