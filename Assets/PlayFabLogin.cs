using PlayFab;
using PlayFab.ClientModels;
using UnityEngine;
using System.Collections.Generic;

public class PlayFabLogin : MonoBehaviour
{
    public void Start()
    {
        //Note: Setting title Id here can be skipped if you have set the value in Editor Extensions already.
        if (string.IsNullOrEmpty(PlayFabSettings.TitleId)){
            PlayFabSettings.TitleId = "5E654"; // Please change this value to your own titleId from PlayFab Game Manager
        }
        var request = new LoginWithCustomIDRequest { CustomId = "GettingStartedGuide", CreateAccount = true};
        PlayFabClientAPI.LoginWithCustomID(request, OnLoginSuccess, OnLoginFailure);
    }

    private void OnLoginSuccess(LoginResult result)
    {
        Debug.Log("Congratulations, you made your first successful API call!");

        GetUserData();

        UpdateUserData(); // ここを追加
    }

    private void OnLoginFailure(PlayFabError error)
    {
        Debug.LogWarning("Something went wrong with your first API call.  :(");
        Debug.LogError("Here's some debug information:");
        Debug.LogError(error.GenerateErrorReport());
    }

     public static void GetUserData()
    {
        var request = new GetUserDataRequest();
        PlayFabClientAPI.GetUserData(request, OnSuccess, OnError);

        void OnSuccess(GetUserDataResult result)
        {
            Debug.Log("GetUserData: Success!");
            Debug.Log($"My name is {result.Data["Name"].Value}");
        }

        void OnError(PlayFabError error)
        {
            Debug.Log("GetUserData: Fail...");
            Debug.Log(error.GenerateErrorReport());
        }
    }


//データ変更
    public static void UpdateUserData()
    {
        var request = new UpdateUserDataRequest()
        {
            Data = new Dictionary<string, string>
            {
                { "Name", "Yoichi" },
                // { "Job", "Engineer" },  // Job の登録
                { "Job", null }  // Job の削除
            }
        };

        PlayFabClientAPI.UpdateUserData(request, OnSuccess, OnError);

        void OnSuccess(UpdateUserDataResult result)
        {
            Debug.Log("UpdateUserData: Success!");
            GetUserData();
        }

        void OnError(PlayFabError error)
        {
            Debug.Log("UpdateUserData: Fail...");
            Debug.Log(error.GenerateErrorReport());
        }
    }
}