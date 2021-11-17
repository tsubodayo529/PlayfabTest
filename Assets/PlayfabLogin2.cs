using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PlayFab;
using PlayFab.ClientModels;

public class PlayfabLogin2 : MonoBehaviour
{

    public int score;
    // Start is called before the first frame update
    void Start()
    {
        Login();
    }

    // Update is called once per frame
    void Login(){
        var request = new LoginWithCustomIDRequest {
            CustomId = SystemInfo.deviceUniqueIdentifier,
            CreateAccount = true
        };
        PlayFabClientAPI.LoginWithCustomID(request, OnSuccess, OnError);
    }

//ログイン成功時の処理
    void OnSuccess(LoginResult result){
        Debug.Log("Successful login/account create!");
    }

//ログイン失敗時の処理
    void OnError(PlayFabError error){
        Debug.Log("Error while logging in/creating account!");
        Debug.Log(error.GenerateErrorReport());
    }


//スコアをPlayFabへ送信する処理
    public void SendLeaderboard(int score){
        var request = new UpdatePlayerStatisticsRequest{
            Statistics = new List<StatisticUpdate> {
                new StatisticUpdate{
                    StatisticName = "PlatformScore",
                    Value = score
                } 

            }
        };
        PlayFabClientAPI.UpdatePlayerStatistics(request, OnLeaderboardUpdate, OnError);
    }

    void OnLeaderboardUpdate(UpdatePlayerStatisticsResult result){
        Debug.Log("Successful leaderboard sent!");
    }



//ランキングのスコアと順位を取得する
    public void GetLeaderboard(){
        var request = new GetLeaderboardRequest {
            StatisticName = "PlatformScore",
            StartPosition = 0,
            MaxResultsCount = 10
        };
        PlayFabClientAPI.GetLeaderboard(request, OnLeaderboardGet, OnError);
    }

    void OnLeaderboardGet(GetLeaderboardResult result){
        foreach (var item in result.Leaderboard){
            Debug.Log("Rank : " +  item.Position + "ID : " + item.PlayFabId + "Score : " + item.StatValue);
        }
    }


    

    public void ScoreButton(){
        SendLeaderboard(score);
    }
}
