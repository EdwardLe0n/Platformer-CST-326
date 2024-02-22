using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GameManagerScript : MonoBehaviour
{

    public TextMeshProUGUI scoreText;
    public TextMeshProUGUI coinsText;
    public TextMeshProUGUI timerText;
    public TextMeshProUGUI skillText;

    public int levelTime = 10;
    public int coinCount = 0;
    public int currentScore = 0;
    public bool levelCompleted = false;

    // Update is called once per frame
    void Update()
    {
        // Updates the coins text
        string coinStr = $"x{coinCount}";
        coinsText.text = coinStr;

        // Ok so this is a wierd one, but I learned how to do leading zeros from this:
        // https://learn.microsoft.com/en-us/dotnet/standard/base-types/how-to-pad-a-number-with-leading-zeros?redirectedfrom=MSDN

        // Formats the score text and displays it properly
        string lead = "000000.##";
        string scoreStr = $"MARIO \n{currentScore.ToString(lead)}";
        scoreText.text = scoreStr;

        // Coin sanity check
        // addCoin();

        // it works :)

        // will only update the time text if the level hasn't been completed

        if (!levelCompleted)
        {
            int intTime = levelTime - (int)Time.realtimeSinceStartup;
            string timeStr = $"TIME \n {intTime}";
            timerText.text = timeStr;

            // If the time is up, this text will then be displayed
            if (intTime <= 0)
            {
                skillText.gameObject.active = (true);
            }

        }

    }

    // :)
    public void addCoin()
    {
        coinCount++;
        currentScore += 200;
    }
}
