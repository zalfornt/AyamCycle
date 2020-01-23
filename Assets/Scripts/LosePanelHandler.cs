using UnityEngine;
using UnityEngine.UI;
//using UnityEngine.Advertisements;

public class LosePanelHandler : MonoBehaviour
{

    public GameObject rewardedAds;
    public GameObject adsPanel;

    public void ActivateRewardedAds()
    {
        rewardedAds.GetComponent<Button>().interactable = true;
    }

    public void ShowRewardedAds()
    {
        AdsHandler.Instance.ShowRewardedAds();
        rewardedAds.GetComponent<Button>().interactable = false;
        adsPanel.SetActive(false);
    }

}
