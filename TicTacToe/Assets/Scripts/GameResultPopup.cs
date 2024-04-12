using System;
using System.Collections;
using TicTacToe;
using TicTacToe.Enums;
using TicTacToe.Extensions;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameResultPopup : MonoBehaviour
{
    [SerializeField] private TMP_Text winningPlayerText;
    [SerializeField] private Animator animator;
    [SerializeField] private Button quitButton;
    [SerializeField] private Button playAgainButton;

    private const string PLAYER_WINS_FORMAT = "{0} Wins!";
    private const string TIE_TEXT = "It was a Tie!";
    
    private static readonly int SHOW_HASH = Animator.StringToHash("Show");
    private static readonly int HIDE_HASH = Animator.StringToHash("Hide");

    private void OnEnable()
    {
        GameController.OnGameEnded += OnGameEnd;
    }

    private void OnDisable()
    {
        GameController.OnGameEnded -= OnGameEnd;
    }

    private void OnGameEnd(PlayerType? playerType)
    {
        Show(playerType);
    }

    public void Show(PlayerType? winningPlayer)
    {
        gameObject.SetActive(true);
        quitButton.interactable = true;
        playAgainButton.interactable = true;
        RegisterEvents();
        SetWinningPlayer(winningPlayer);
        animator.Play(SHOW_HASH);
    }

    private void SetWinningPlayer(PlayerType? winningPlayer)
    {
        if (winningPlayer.HasValue)
        {
            PlayerType player = (PlayerType)winningPlayer;
            
            winningPlayerText.text = string.Format(PLAYER_WINS_FORMAT, player.ToUIString());   
        }
        else
        {
            winningPlayerText.text = TIE_TEXT;;
        }
    }

    public void Hide(Action onHideComplete = null)
    {
        UnregisterEvents();
        animator.Play(HIDE_HASH);
        StartCoroutine(WaitForAnimation(() =>
        {
            gameObject.SetActive(false);
            onHideComplete?.Invoke();
        }));
    }

    private void RegisterEvents()
    {
        quitButton.onClick.AddListener(Quit);
        playAgainButton.onClick.AddListener(PlayAgain);
    }

    private void UnregisterEvents()
    {
        quitButton.onClick.RemoveAllListeners();
        playAgainButton.onClick.RemoveAllListeners();
    }

    private void Quit()
    {
        quitButton.interactable = true;
        Application.Quit();
    }

    private void PlayAgain()
    {
        playAgainButton.interactable = false;
        GoToMainMenu();
    }

    private void GoToMainMenu()
    {
        SceneManager.LoadSceneAsync("Menu", LoadSceneMode.Single).completed += (operation) =>
        {
            Destroy(GameController.Instance.gameObject);
        };
    }

    private IEnumerator WaitForAnimation(Action onAnimComplete = null)
    {
        yield return null;

        while (true)
        {
            AnimatorStateInfo animatorStateInfo = animator.GetCurrentAnimatorStateInfo(0);

            if (float.IsPositiveInfinity(animatorStateInfo.length) ||
                (!animator.IsInTransition(0) &&
                 animatorStateInfo.length > 0 &&
                 animatorStateInfo.normalizedTime >= 1f))
            {
                // Animation has finished
                onAnimComplete?.Invoke();
                yield break;
            }
                
            yield return null;
        }
    }
}
