using System.Collections.Generic;
using UnityEngine;

public class SaveSystem : MonoBehaviour
{
    private const string LeaderboardKey = "LeaderboardScore";
    private const string LeaderboardCountKey = "LeaderboardCount";
    private const int MaxHighScores = 5;
    private List<float> leaderboard = new List<float>();

    // Added
    private void Awake()
    {
        LoadScores();
    }

    public void SaveTimer(float timer)
    {
        leaderboard.Add(timer);

        // Sort the list then reverse it so the highest values are at the start.
        leaderboard.Sort();
        leaderboard.Reverse();

        // If list is larger than MaxHighScores removes the last element (Lowest value)
        if (leaderboard.Count > MaxHighScores)
        {
            leaderboard.RemoveAt(leaderboard.Count - 1);
        }

        // Save all scores - Player prefs will override values if it finds the same key.
        for (int i = 0; i < leaderboard.Count; i++)
        {
            PlayerPrefs.SetFloat(LeaderboardKey + i, leaderboard[i]);
        }

        // Save the size of the leader board
        PlayerPrefs.SetInt(LeaderboardCountKey, leaderboard.Count);
        PlayerPrefs.Save();
    }

    public void LoadScores()
    {
        leaderboard.Clear();

        // Get the number of elements on the leaderboard.
        int count = PlayerPrefs.GetInt(LeaderboardCountKey, 0);

        // Get all float values for each element and add them to the array
        for (int i = 0; i < count; i++)
        {
            float score = PlayerPrefs.GetFloat(LeaderboardKey + i, 0f);
            leaderboard.Add(score);
        }

        // Sort the list then reverse it so the highest values are at the start.
        leaderboard.Sort();
        leaderboard.Reverse();
    }

    public float LoadBestTime()
    {
        if (leaderboard.Count == 0)
            return 0f;

        return leaderboard[0];
    }

    public List<float> GetScores()
    {
        return leaderboard;
    }

}