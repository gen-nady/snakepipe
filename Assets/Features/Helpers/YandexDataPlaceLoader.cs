using System;
using System.Collections;
using Newtonsoft.Json;
using UnityEngine;

public class YandexDataPlaceLoader : Singleton<YandexDataPlaceLoader>
{
    protected override void Awake()
    {
        base.Awake();
        DontDestroyOnLoad(this.gameObject);
    }

    public void GetRewarded()
    {
#if UNITY_WEBGL && !UNITY_EDITOR
       var adseService= ProjectContext.Instance.AdsService as YandexAdsService;
       adseService.GetReward();
#endif
    }
}
