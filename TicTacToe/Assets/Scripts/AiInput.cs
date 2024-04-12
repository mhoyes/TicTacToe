using System.Collections;
using TicTacToe.Attributes;
using TicTacToe.Enums;
using TicTacToe.Interfaces;
using TicTacToe.ScriptableObjects;
using UnityEngine;

namespace TicTacToe
{
    public class AiInput : InputBase
    {
        public AiLogicConfig LogicConfig
        {
            get => logicConfig;
            set => logicConfig = value;
        }
        
        public bool LoadAiLogicFromConfig
        {
            get => loadAiLogicFromConfig;
            set => loadAiLogicFromConfig = value;
        }

        public AiLogicType LogicType { get; set; }

        [SerializeField] private AiLogicConfig logicConfig;

        private bool loadAiLogicFromConfig = true;
        private WaitForSeconds waitTime;
        private IAiLogicBehavior logicBehavior;

        [ReadOnly]
        [SerializeField]
        private AiLogicType loadedLogicType = AiLogicType.Random;
        
        protected override void Start()
        {
            base.Start();
            if (LoadAiLogicFromConfig)
            {
                logicBehavior = logicConfig.GetAssignedLogic();
                loadedLogicType = logicConfig.LogicBehaviorType;
            }
            else
            {
                logicBehavior = logicConfig.GetLogicFromType(LogicType);
                loadedLogicType = LogicType;
            }
            logicBehavior.Initialize(this, GameBoard);
        }

        public override void EnableInput()
        {
            base.EnableInput();
            GameBoard.DisableCellInteractabilty();
            StartCoroutine(ChooseMove());
        }

        private IEnumerator ChooseMove()
        {
            float decisionTime = Random.Range(logicConfig.DecisionTimeMinimum, logicConfig.DecisionTimeMaximum);
            waitTime = new WaitForSeconds(decisionTime);
            yield return waitTime;

            GridCell chosenCell = logicBehavior.ChooseMove();
            
            PlayerInputReceived?.Invoke(chosenCell);
        }

        public override void DisableInput()
        {
            base.DisableInput();
            GameBoard.DisableCellInteractabilty();
        }
    }
}
