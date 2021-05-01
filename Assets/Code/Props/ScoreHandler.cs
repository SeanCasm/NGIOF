using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public sealed class ScoreHandler : MonoBehaviour
{
    private static int score;
    public static int tierLvl=1;
    public static int Score
    {
        get => score; 
        set
        {
            score = value;
            if (score <= 100)
            {
                Game.Props.Spawn.Ball.tierLvl = 1;
            }
            else if (score > 100 && score <= 200)
            {
                Game.Props.Spawn.Ball.tierLvl = 2;
            }
            else if (score > 200 && score <= 400)
            {
                Game.Props.Spawn.Ball.tierLvl = 3;
            }
            else if (score > 400 && score <= 750)
            {
                Game.Props.Spawn.Ball.tierLvl = 4;
            }
            else if (score > 750 && score < 1000)
            {
                Game.Props.Spawn.Ball.tierLvl = 5;
            }
            ScoreUIHandler.score.Invoke(score);
        }
    } 
    private void OnEnable() {
        DeathScreen.deathPause+=ResetScore;
    }
    private void OnDisable() {
        DeathScreen.deathPause -= ResetScore;
    }
    private void ResetScore(){
        Score=0;
        tierLvl=1;
    }
}
