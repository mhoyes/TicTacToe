using System;
using System.Threading.Tasks;
using TicTacToe.Enums;
using TicTacToe.Interfaces;
using UnityEngine;
using Random = UnityEngine.Random;

namespace TicTacToe
{
    public class GameController : MonoBehaviour
    {
        public static GameController Instance => instance;

        // Creating a simple singleton
        private static GameController instance;
        
        public GameBoard GameBoard => board;

        public static Action<StateChangedData> OnStateChanged;
        public static Action<PlayerType?> OnGameEnded;

        [SerializeField] private GameBoard board;
        [SerializeField] private bool isSinglePlayer = true;
        [SerializeField] private PlayerInput player1Input;
        [SerializeField] private PlayerInput player2Input; // Support 2-player - same machine
        [SerializeField] private AiInput aiInput;
        
        private GameState currentGamestate;
        private IPlayerInput currentPlayer = null;

        private void Awake()
        {
            if (instance != null && instance != this)
            {
                Destroy(gameObject);
            }
            else
            {
                instance = this;
                DontDestroyOnLoad(gameObject);
            }
            
            board = FindObjectOfType<GameBoard>();
        }

        private async void Start()
        {
            Initialize();
        }

        private void OnDestroy()
        {
            UnregisterEvents();
        }

        public async void Initialize()
        {
            SetGameState(GameState.GameStart);
            board = FindObjectOfType<GameBoard>();
            GameBoard.Initialize();

            RegisterEvents();

            // Simulate a 2 second wait
            await Task.Delay(2000);

            InitializePlayers();
            DetermineFirstPlayer();
        }

        private void RegisterEvents()
        {
            InputBase.PlayerInputReceived += PlayerInputReceived;
        }

        private void UnregisterEvents()
        {
            InputBase.PlayerInputReceived -= PlayerInputReceived;
        }

        private void DetermineFirstPlayer()
        {
            bool player1First = Random.Range(0, 2) == 0;
            currentPlayer = player1First ? player1Input : GetSecondPlayer();
            currentPlayer.EnableInput();

            GameState state;
            if (player1First)
            {
                state = GameState.Player1Turn;
            }
            else
            {
                IPlayerInput secondPlayer = GetSecondPlayer();
                state = secondPlayer.IsPlayerControlled() ? GameState.Player2Turn : GameState.AiTurn;
            }

            SetGameState(state);
        }

        private void InitializePlayers()
        {
            bool player1IsX = Random.Range(0, 2) == 0;
            
            player1Input.Initialize(player1IsX ? GridCellType.X : GridCellType.O);

            IPlayerInput secondPlayer = GetSecondPlayer();
            secondPlayer.Initialize(player1IsX ? GridCellType.O : GridCellType.X);
        }

        private IPlayerInput GetSecondPlayer()
        {
            return isSinglePlayer ? (IPlayerInput)aiInput : player2Input;
        }

        private void PlayerInputReceived(GridCell cell)
        {
            if (!cell.IsEmpty)
            {
                return;
            }
            
            cell.SetCellType(GetCurrentPlayerType());
            ChangePlayer();
        }

        private void ChangePlayer()
        {
            currentPlayer.DisableInput();

            GridCellType winningCellType = board.CalculateWinner();

            if (!board.HasEmptyCells() || winningCellType != GridCellType.NONE)
            {
                SetGameState(GameState.GameEnd);
                
                PlayerType? playerType = GetPlayerTypeFromCellType(winningCellType);
                OnGameEnded?.Invoke(playerType);
                return;
            }

            if (currentGamestate == GameState.Player1Turn)
            {
                currentPlayer = GetSecondPlayer();
                if (currentPlayer.IsPlayerControlled())
                {
                    SetGameState(GameState.Player2Turn);
                }
                else
                {
                    SetGameState(GameState.AiTurn);
                }
            }
            else
            {
                currentPlayer = player1Input;
                SetGameState(GameState.Player1Turn);
            }
            currentPlayer.EnableInput();
        }

        private GridCellType GetCurrentPlayerType()
        {
            return currentPlayer.CellType;
        }

        private PlayerType? GetPlayerTypeFromCellType(GridCellType cellType)
        {
            if (player1Input.CellType == cellType)
            {
                return PlayerType.Player1;
            }

            IPlayerInput secondPlayer = GetSecondPlayer();
            if (secondPlayer.CellType == cellType)
            {
                return secondPlayer.IsPlayerControlled() ? PlayerType.Player2 : PlayerType.AI;
            }

            // No player for GridCellType.None
            return null;
        }

        private void SetGameState(GameState state)
        {
            currentGamestate = state;
            OnStateChanged?.Invoke(new StateChangedData()
            {
                state = state,
                playerTurn = currentPlayer?.PlayerType
            });
        }
        
        public void SetModeData(bool singlePlayer, AiLogicType aiLogicType)
        {
            isSinglePlayer = singlePlayer;
            aiInput.LoadAiLogicFromConfig = false;
            aiInput.LogicType = aiLogicType;
        }
    }
}
