using System;
using System.Collections;
using System.Collections.Generic;
using PlayFab;
using PlayFab.ClientModels;
using UnityEngine;
 
public static class Login
{
    private static string myID;
    private static InitialUserData initialUserData=new InitialUserData();
    private static string userName,password;
    #region Playfab login
    public static void LogIn(string username,string pass){
        PlayFabClientAPI.LoginWithPlayFab(new LoginWithPlayFabRequest{
            Username=username,
            Password=pass
        },OnLoginSuccess,OnLoginFailure);
    }
    private static void OnLoginSuccess(LoginResult result){
        myID=result.PlayFabId;
        PlayFabClientAPI.GetUserData(new GetUserDataRequest{Keys=new List<string>{"IsInitialized"}},
            resultCallback=>{
                if(!resultCallback.Data.ContainsKey("IsInitialized")){
                    InitializeUser();
                }else{
                     
                    PlayFabClientAPI.GetUserData(new GetUserDataRequest{Keys=new List<string>{"totalPoints","highscore","ballsDestroyed","highestLevelReached"}},
                    resultCallback=>{
                        var stats=resultCallback.Data["stats"].Value;
                        Stats st=JsonUtility.FromJson<Stats>(stats);
                        PersistentData.SetPersistentData(new PlayFabUserPersistentData
                        {
                            highscore=st.highscore,
                            ballsDestroyed=st.ballsDestroyed,
                            totalPoints=st.totalPoints,
                            highestLevelReached=st.highestLevelReached,
                            username=userName,
                            password=password
                        });
                    },
                    errorCallback=>{

                    }
                    );
                }
            },
            errorCallback=>{

            }
        );
        LoginUIHandler.login("");
    }
    private static void OnLoginFailure(PlayFabError error){
        LoginUIHandler.login(error.ErrorMessage);
    }
    private static void InitializeUser(){
        GetTitleDataRequest request = new GetTitleDataRequest()
        {
            Keys = new List<string>() { "InitialUserData" }
        };
        PlayFabClientAPI.GetTitleData(request, result =>
        {
            var dataValues = result.Data["InitialUserData"];
            initialUserData = JsonUtility.FromJson<InitialUserData>(dataValues);
            PersistentData.SetPersistentData(new PlayFabUserPersistentData{
               username=userName,
               password=password 
            });
            PlayFabClientAPI.UpdateUserData(new UpdateUserDataRequest
            {
                Permission=UserDataPermission.Public,
                Data = new Dictionary<string, string>{
                {"IsInitialized",JsonUtility.ToJson(new PlayfabUserIsInitialized{IsInitialized=true})},
                {"stats",JsonUtility.ToJson(new Stats{totalPoints=0,highscore=0,ballsDestroyed=0,highestLevelReached=0})},
            }
            }, resultCallback =>
            {

            }, errorCallback =>
            {

            });
        },
        errorCallback =>
        {
            LoginUIHandler.login(errorCallback.ErrorMessage);
        });
         
    }
    #endregion
    #region  Playfab register
    public static void Registry(string username, string password, string email)
    {
        RegisterPlayFabUserRequest registryRequest;
        registryRequest = new RegisterPlayFabUserRequest();
        registryRequest.Email = email;
        registryRequest.Username = username;
        registryRequest.Password = password;
        PlayFabClientAPI.RegisterPlayFabUser(registryRequest, OnRegisterSuccess,OnRegisterFailure);
    }
    private static void OnRegisterFailure(PlayFabError result){
        LoginUIHandler.register(result.ErrorMessage);
    }
    private static void OnRegisterSuccess(RegisterPlayFabUserResult result){
        LoginUIHandler.register("");
    }
    #endregion
}