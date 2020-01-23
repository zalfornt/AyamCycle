using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Advertisements;

public class AdsHandler : MonoBehaviour, IUnityAdsListener
{

    string gameId;
    bool testMode = false;

    string rewardedAds = "rewardedVideo";
    string videoAds = "video";

    private static AdsHandler instance;
    public static AdsHandler Instance
    {
        get
        {
            if (instance == null)
            {
                instance = new AdsHandler();
            }
            return instance;
        }
    }


    // Start is called before the first frame update
    void Start()
    {
        if (Application.isEditor) testMode = true;
#if UNITY_ANDROID
        gameId = "3432596";
#elif UNITY_IOS
        gameId = "3432597";
#endif
        Advertisement.Initialize(gameId, testMode);
        Debug.Log("test mode: " + testMode);
        instance = this;
    }

    public void ShowRewardedAds()
    {
        if (Advertisement.IsReady(rewardedAds))
        {
            Advertisement.Show(rewardedAds);
            Blackboard.Instance.GameManager.RestartTimer();
        }
    }

    public void ShowVideoAds()
    {
        if (Advertisement.IsReady(videoAds))
        {
            Advertisement.Show(videoAds);
        }
    }

    public void OnUnityAdsReady(string placementId)
    {
        throw new System.NotImplementedException();
    }

    public void OnUnityAdsDidError(string message)
    {
        throw new System.NotImplementedException();
    }

    public void OnUnityAdsDidStart(string placementId)
    {
        Debug.Log("Rewarded Ads Started");
    }

    public void OnUnityAdsDidFinish(string placementId, ShowResult showResult)
    {
        if (showResult == ShowResult.Finished)
        {
            Debug.Log("ads complete");
            //Blackboard.Instance.GameManager.RestartTimer();
        }
        else if (showResult == ShowResult.Skipped)
        {
            Debug.Log("ads cancelled");
        }
        else if (showResult == ShowResult.Failed)
        {
            Debug.LogWarning("The ad did not finish due to an error.");
        }
    }
}
