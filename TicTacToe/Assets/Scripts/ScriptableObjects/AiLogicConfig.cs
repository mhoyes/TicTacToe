using System;
using TicTacToe.AI.Logic;
using TicTacToe.Enums;
using TicTacToe.Interfaces;
using UnityEngine;

namespace TicTacToe.ScriptableObjects
{
    [CreateAssetMenu(fileName = "AiLogicConfig", menuName = "Tic-Tac-Toe/AiLogicConfig", order = 1)]
    public class AiLogicConfig : ScriptableObject
    {
        public AiLogicType LogicBehaviorType;
        
        [Range(1, 5)]
        public int MovesAheadToPredict = 1;
        
        [Range(0.5f, 2f)]
        public float DecisionTimeMinimum = 0.5f;
        
        [Range(2.5f, 5f)]
        public float DecisionTimeMaximum = 2.5f;

        public IAiLogicBehavior GetAssignedLogic()
        {
            return GetLogicFromType(LogicBehaviorType);
        }

        public IAiLogicBehavior GetLogicFromType(AiLogicType type)
        {
            switch (type)
            {
                case AiLogicType.Random:
                    return new AiLogicRandom();
                case AiLogicType.FirstAvailable:
                    return new AiLogicFirstAvailable();
                case AiLogicType.Smart:
                    return new AiLogicSmart();
                default:
                    throw new NotImplementedException();
            }
        }
    }
}