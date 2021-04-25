using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using PlayFab.ClientModels;
using PlayFab;
public sealed class PersistentData:MonoBehaviour
{
    private static string userName;
    public static int highscore,totalPoints,ballsDestroyed,highestLevelReached;
    private static string password;
     
    private void OnEnable() {
        Game.Player.Health.onDeath+=UpdateAllPlayFabUserData;
    }
    private void OnDisable() {
        Game.Player.Health.onDeath-=UpdateAllPlayFabUserData;
    }
    private void UpdateAllPlayFabUserData(){
        UpdatePlayfabUserData.UpdateAll();
    }
    public static void SetPersistentData(PlayFabUserPersistentData playfabUserPersistentData){
        userName=playfabUserPersistentData.username;
        password=playfabUserPersistentData.password;
        highscore=playfabUserPersistentData.highscore;
        totalPoints=playfabUserPersistentData.totalPoints;
        ballsDestroyed=playfabUserPersistentData.ballsDestroyed;
        highestLevelReached=playfabUserPersistentData.highestLevelReached;
        UserDataUIHandle.dataUpdate(playfabUserPersistentData);
    } 
}
