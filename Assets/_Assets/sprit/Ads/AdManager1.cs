    //using System.Collections;
    //using UnityEngine;
    //using UnityEngine.Advertisements;

    //public class AdManager1 : MonoBehaviour, IUnityAdsListener
    //{
    //#if UNITY_ANDROID
    //    string gameId = "5720091";
    //#else
    //    string gameId = "5720090";
    //#endif

    //    void Start()
    //    {
    //        Advertisement.Initialize(gameId);
    //        Advertisement.AddListener(this);    
    //        ShowBanner();
    //    }

    //    public void PlayAd()
    //    {
    //        if (Advertisement.IsReady("video"))
    //        {
    //            Advertisement.Show("video");
    //        }
    //    }

    //    public void PlayRewardedAd()
    //    {
    //        if (Advertisement.IsReady("rewardedVideo"))
    //        {
    //            Advertisement.Show("rewardedVideo");

    //        }
    //        else
    //        {
    //            Debug.Log("Rewarded ad is not ready!");
    //        }
    //    }
    
    //    public void ShowBanner()
    //    {
    //        if (Advertisement.IsReady("banner"))
    //        {
    //            Advertisement.Banner.SetPosition (BannerPosition.BOTTOM_CENTER);
    //            Advertisement.Banner.Show("banner");
    //        }
    //        else
    //        {
    //            StartCoroutine(RepeatShowBanner());
    //        }
    //    }

    //    public void HideBanner()
    //    {
    //        Advertisement.Banner.Hide();
    //    }

    //    IEnumerator RepeatShowBanner()
    //    {
    //        yield return new WaitForSeconds(1);
    //        ShowBanner ();
    //    }
    //    public void OnUnityAdsReady(string placementId)
    //    {
    //        Debug.Log("ADS ARE READY !");
    //    }
    
    //    public void OnUnityAdsDieError(string message)
    //    {
    //        Debug.Log("ERROR: "+ message);

    //    }

    //    public void OnUnityAdsDidStart(string placementId)
    //    {
    //        Debug.Log("VIDEO STARTED");
    //    }
    
    //    public void OnUnityAdsDidFinish(string placementId, ShowResult showResult)
    //    {
    //        if (placementId == "rewardedVideo" && showResult == ShowResult.Finished) 
    //        {
    //            Debug.Log("PLAYER SHOULD BE REWARDED!");
    //        }
    //    }
    //}
