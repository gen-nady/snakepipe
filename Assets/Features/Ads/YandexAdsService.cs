using System;
using System.Runtime.InteropServices;
using UnityEngine;

public class YandexAdsService : AdsService
{
    public Action _rewardedCalback;
    
    protected override void Awake()
    {
        base.Awake();
        DontDestroyOnLoad(this.gameObject);
    }

    public override void ShowInterstitial()
    {
        ShowInterstitialExtern();
    }

    public override void ShowBanner()
    {
    }

    public override void ShowRewarded(Action callBack = null)
    {
        _rewardedCalback = callBack;
        ShowRewardedExtern();
    }
    
    [DllImport("__Internal")]
    private static extern void ShowInterstitialExtern();
    
    [DllImport("__Internal")]
    private static extern void ShowRewardedExtern();

    public void GetReward()
    {
        _rewardedCalback?.Invoke();
    }
}
