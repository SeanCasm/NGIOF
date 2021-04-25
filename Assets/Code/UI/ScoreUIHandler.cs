using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;
using TMPro;

public class ScoreUIHandler : MonoBehaviour
{
    [SerializeField]Text scoreText;
    [SerializeField]TextMeshProUGUI text;
    public static Action<int> score;
    private void OnEnable() {
        score+=UpdateScore;
    }
    private void OnDisable()
    {
        score -= UpdateScore;
        scoreText.text = "Score: ";
        text.text = "Level: ";
    }
    private void UpdateScore(int amount){
        scoreText.text="Score: "+amount.ToString();
        text.text="Level: "+Game.Props.Spawn.Ball.tierLvl.ToString();
    }
}
