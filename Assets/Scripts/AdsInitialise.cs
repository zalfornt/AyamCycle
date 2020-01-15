using UnityEngine;
using UnityEngine.Advertisements;

public class AdsInitialise : MonoBehaviour
{
    string gameId;
    bool testMode = true;

    void Start()
    {
#if UNITY_ANDROID
        gameId = "3432596";
#elif UNITY_IOS
        gameId = "3432597";
#endif
        Advertisement.Initialize(gameId, testMode);
    }
}