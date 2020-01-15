using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Advertisements;

public class LosePanelHandler : MonoBehaviour, IUnityAdsListener
{
#if UNITY_ANDROID
    string gameId = "3432596";
#elif UNITY_IOS
    string gameId = "3432597";
#endif

    public GameObject rewardedAds;

    public void ActivateRewardedAds()
    {
        rewardedAds.GetComponent<Button>().interactable = true;
    }

    public void ShowRewardedAds()
    {
        if (Advertisement.IsReady())
        {
            Advertisement.Show("rewardedVideo");
            rewardedAds.GetComponent<Button>().interactable = false;
            Blackboard.Instance.GameManager.RestartTimer();
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
            Blackboard.Instance.GameManager.RestartTimer();
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
