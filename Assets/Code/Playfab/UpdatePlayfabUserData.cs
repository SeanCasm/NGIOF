using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PlayFab;
using PlayFab.ClientModels;
 
[Serializable]
public class PlayfabUserIsInitialized
{
    public bool IsInitialized;
}
[Serializable]
public class InitialUserData
{
    public int initialCurrency;
}
[Serializable]
public class PlayFabUserPersistentData
{
    public int ballsDestroyed;
    public int highscore;
    public int totalPoints;
    public int highestLevelReached;
    public string username,password;
}
[Serializable]
public class Stats
{
    public int totalPoints;
    public int highscore;

    public int ballsDestroyed;
    public int highestLevelReached;
    public Stats (int totalPoints,int highscore,int ballsDestroyed,int highestLevelReached){
        this.totalPoints+=totalPoints;
        if (highscore > this.highscore) this.highscore = highscore;
        this.ballsDestroyed=ballsDestroyed;
        if(highestLevelReached>this.highestLevelReached)this.highestLevelReached=highestLevelReached;
    }
    public Stats(){}
}
public static class UpdatePlayfabUserData
{
    public static void UpdateAll(){
        PlayFabClientAPI.UpdateUserData(new UpdateUserDataRequest
        {
            Permission = UserDataPermission.Public,
            Data = new Dictionary<string, string>{
                {"stats",JsonUtility.ToJson(new Stats(
                    ScoreHandler.Score,
                    ScoreHandler.Score,
                    Game.Props.Spawn.Ball.ballsDestroyedInGame,
                    ScoreHandler.tierLvl
                ))}
            }
        }, resultCallback =>
        {

        }, errorCallback =>
        {

        });
    }
}