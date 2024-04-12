using System;
using TicTacToe.Attributes;
using TicTacToe.Enums;
using TicTacToe.Interfaces;
using UnityEngine;

namespace TicTacToe
{
    public abstract class InputBase : MonoBehaviour, IPlayerInput
    {
        public static Action<GridCell> PlayerInputReceived;
        public bool InputEnabled => inputEnabled;
        public PlayerType PlayerType => playerType;
        public GridCellType CellType => _cellType;
        public GameBoard GameBoard => GameController.Instance.GameBoard;

        [SerializeField]
        protected PlayerType playerType; 
        
        [ReadOnly]
        [SerializeField]
        protected GridCellType _cellType = GridCellType.NONE;

        protected bool inputEnabled;

        protected virtual void Start()
        {
            DisableInput();
        }

        public virtual void Initialize(GridCellType cellType)
        {
            _cellType = cellType;
        }

        public virtual void EnableInput()
        {
            inputEnabled = true;
        }

        public virtual void DisableInput()
        {
            inputEnabled = false;
        }

        public bool IsPlayerControlled()
        {
            return playerType != PlayerType.AI;
        }
    }
}