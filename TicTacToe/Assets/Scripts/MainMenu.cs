using TicTacToe.Enums;
using TicTacToe.SceneLoading;
using UnityEngine;
using UnityEngine.UI;

namespace TicTacToe
{
    public class MainMenu : MonoBehaviour
    {
        [SerializeField] private SceneLoader sceneLoader;
    
        [Header("Single Player Modes")]
        [SerializeField] private Button randomAiButton;
        [SerializeField] private Button firstAvailableAiButton;
        [SerializeField] private Button smartAiButton;
    
        [Header("2 Player Mode")]
        [SerializeField] private Button twoPlayerButton;

        private bool isSinglePlayer;
        private AiLogicType aiLogicType;
    
        private void OnEnable()
        {
            randomAiButton.onClick.AddListener(()=> ModeClicked(true, AiLogicType.Random));
            firstAvailableAiButton.onClick.AddListener(()=> ModeClicked(true, AiLogicType.FirstAvailable));
            smartAiButton.onClick.AddListener(()=> ModeClicked(true, AiLogicType.Smart));
            twoPlayerButton.onClick.AddListener(()=> ModeClicked(false));
        }

        private void OnDisable()
        {
            randomAiButton.onClick.RemoveAllListeners();
            firstAvailableAiButton.onClick.RemoveAllListeners();
            smartAiButton.onClick.RemoveAllListeners();
            twoPlayerButton.onClick.RemoveAllListeners();
        }

        private void ModeClicked(bool singlePlayer, AiLogicType logicType = AiLogicType.Random)
        {
            isSinglePlayer = singlePlayer;
            aiLogicType = logicType;
            sceneLoader.LoadScene(sceneLoader.SceneToLoad, OnSceneLoaded);
        }

        private void OnSceneLoaded(AsyncOperation operation)
        {
            GameController.Instance.SetModeData(isSinglePlayer, aiLogicType);
        }
    }
}