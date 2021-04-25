using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UserDataUIHandle : MonoBehaviour
{
    [SerializeField]Image playerIcon;
    [SerializeField]TMPro.TextMeshProUGUI[] userName;
    [SerializeField]Text accountCreated,highScore,ballsDestroyed,timePlayed,totalPoints,highestLevel;
    public static Action<PlayFabUserPersistentData> dataUpdate;
    private void OnEnable() {
        dataUpdate+=SetDataUI;
    }
    private void OnDisable() {
        dataUpdate-=SetDataUI;
    }
    
    private void SetDataUI(PlayFabUserPersistentData playfabUserPersistentData){
        foreach(var e in userName){
            e.text=playfabUserPersistentData.username;
        }
        //accountCreated.text="Created: "+creationDate;
        highScore.text="High score: "+playfabUserPersistentData.highscore.ToString();
        ballsDestroyed.text="Balls destroyed: "+playfabUserPersistentData.ballsDestroyed.ToString();
       /* timePlayed.text="Time played: "+initialUserData.timePlayed.ToString();*/
        totalPoints.text="Total points: "+playfabUserPersistentData.totalPoints.ToString();
        highestLevel.text="Highest level reached: "+playfabUserPersistentData.highestLevelReached.ToString();
    }
}
