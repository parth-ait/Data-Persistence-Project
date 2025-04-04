using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.SocialPlatforms.Impl;

public class LeaderboardManager : MonoBehaviour
{
    public static LeaderboardManager Instance;
    public string bestPlayer;
    public int bestScore;
    public string currentPlayer;
    public int currentScore;

    private void Awake()
    {
        if (Instance != null)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
        DontDestroyOnLoad(gameObject);
    }

    [System.Serializable]
    class SaveLeaderboard
    {
        public string playerName;
        public int playerScore;
    }

    public void SaveScore()
    {
        SaveLeaderboard leaderboard = new() {
            playerName = bestPlayer,
            playerScore = bestScore
        };

        string json = JsonUtility.ToJson(leaderboard);

        File.WriteAllText(Application.persistentDataPath + "/leaderboard.json", json);
    }

    public void LoadScore()
    {
        string path = Application.persistentDataPath + "/leaderboard.json";

        if (File.Exists(path))
        {
            string json = File.ReadAllText(path);
            SaveLeaderboard highScore = JsonUtility.FromJson<SaveLeaderboard>(json);

            bestPlayer = highScore.playerName;
            bestScore = highScore.playerScore;
        }
    }

    public string CurrentPlayerStats(int score)
    {
        return $"Current Player\nName: {currentPlayer}\nScore: {score}";
    }

    public string BestPlayerStats(bool isInMenu)
    {
        if (isInMenu)
        {
            return $"Best Player\nName: {bestPlayer} | Score: {bestScore}";
        }
        else
        {
            return $"Best Player\nName: {bestPlayer}\nScore: {bestScore}";
        }
    }
}
