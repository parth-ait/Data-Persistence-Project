using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;



#if UNITY_EDITOR
using UnityEditor;
#endif

public class MenuUIHandler : MonoBehaviour
{
    public TMP_InputField nameInput;
    public string playerName;
    public TextMeshProUGUI currentPlayerName;

    public TextMeshProUGUI bestPlayerScore;

    public void Start()
    {
        LeaderboardManager.Instance.LoadScore();

        bestPlayerScore.text = LeaderboardManager.Instance.BestPlayerStats(true);
    }

    private void Update()
    {
        playerName = nameInput.text;

        if (string.IsNullOrEmpty(playerName))
        {
            playerName = "Guest";
        }

        LeaderboardManager.Instance.currentPlayer = playerName;

        currentPlayerName.text = $"Current Player: {playerName}";
    }
    public void StartNew()
    {
        SceneManager.LoadScene(1);
    }

    public void Exit()
    {
        #if UNITY_EDITOR
            EditorApplication.ExitPlaymode();
        #else
            Application.Quit();
        #endif
    }
}
