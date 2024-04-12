using System.Collections;
using TicTacToe;
using TicTacToe.Enums;
using TicTacToe.Extensions;
using TMPro;
using UnityEngine;

public class GameInfoDisplay : MonoBehaviour
{
    [SerializeField] private TMP_Text gameStateText;
    [SerializeField] private TMP_Text playerTurnText;
    [SerializeField] private TMP_Text determiningTurnText;
    [SerializeField] private float delayBetweenDots = 0.25f;

    private WaitForSeconds waitTime;

    private const string GAME_STATE_FORMAT = "Game State:\n{0}";
    private const string PLAYER_TURN_FORMAT = "Turn:\n{0}";
    private const string DETERMINING_TURN = "Determining Turn";
    
    private void Awake()
    {
        waitTime = new WaitForSeconds(delayBetweenDots);
        GameController.OnStateChanged += OnStateChanged;
    }
    
    private void OnDestroy()
    {
        GameController.OnStateChanged -= OnStateChanged;
    }

    private void OnStateChanged(StateChangedData stateChangedData)
    {
        gameStateText.text = string.Format(GAME_STATE_FORMAT, stateChangedData.state);
        
        determiningTurnText.gameObject.SetActive(!stateChangedData.playerTurn.HasValue);
        playerTurnText.gameObject.SetActive(stateChangedData.playerTurn.HasValue);

        if (stateChangedData.playerTurn.HasValue)
        {
            playerTurnText.text = string.Format(PLAYER_TURN_FORMAT, ((PlayerType)stateChangedData.playerTurn).ToUIString());
        }
        else
        {
            determiningTurnText.text = DETERMINING_TURN;
            StartCoroutine(AnimateDeterminingText());
        }
    }

    private IEnumerator AnimateDeterminingText()
    {
        while (true)
        {
            for (int i = 0; i <= 3; i++)
            {
                determiningTurnText.text = DETERMINING_TURN + new string('.', i);
                yield return waitTime;
            }
        }
    }
}
