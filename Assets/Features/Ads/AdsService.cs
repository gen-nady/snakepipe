using System;
using UnityEngine;

public abstract class AdsService:Singleton<AdsService>
{
  public abstract void ShowInterstitial();

  public abstract void ShowBanner();

  public abstract void ShowRewarded(Action callBack=null);
  
  private const int TimeInSecondsToShowInter = 61;

  private float counter = 0;

  public bool CanShowInterstitial { get; private set; } = false;

  protected override void Awake()
  {
    base.Awake();
  }

  protected void Update()
  {
    counter += Time.deltaTime;
    if (counter >=TimeInSecondsToShowInter)
    {
      CanShowInterstitial = true;
    }
  }

  protected virtual void OnDisable()
  {
    }

  protected virtual void TryToShowInterstitialAd()
  {
    if (CanShowInterstitial)
    {
      ShowInterstitial();
      ResetTimer();
    }
  }

  public void ResetTimer()
  {
    CanShowInterstitial = false;
    counter = 0;
  }
}
