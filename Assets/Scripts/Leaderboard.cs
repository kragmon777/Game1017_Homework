using UnityEngine;
using TMPro;
using System.Collections.Generic;

public class Leaderboard : MonoBehaviour
{
   [SerializeField] private TextMeshProUGUI leaderboardText; 
    
   public void Initialize(List<float> leaderboard)
    {
        string leaderboardString = "<align=\"center\"><b>Leaderboard</b></align>";

        for (int i = 0; i < leaderboard.Count; i++)
        {
            leaderboardString += "\nPosition # " + (i + 1) + " " + leaderboard[i].ToString("0.00");
        }
        leaderboardText.text = leaderboardString;
    }
   
}
