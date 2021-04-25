using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PlayFab;
using PlayFab.ClientModels;
public class LocalDataInfoHandler : MonoBehaviour
{
    public void ResetDataFromGame(){
        ScoreHandler.Score = 0;
        ScoreHandler.tierLvl = 1;
        Game.Props.Spawn.Ball.ballsDestroyedInGame = 0;
    }
}
