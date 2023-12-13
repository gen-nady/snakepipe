using UnityEngine;

public class AdsTimer : Singleton<AdsTimer>
{
    private const int TimeInSecondsToShowInter = 61;

    private float counter = 0;

    public bool CanShowInterstitial { get; private set; } = false;

    protected override void Awake()
    {
        base.Awake();
    }

    private void Update()
    {
        counter += Time.deltaTime;
        if (counter >=TimeInSecondsToShowInter)
        {
            CanShowInterstitial = true;
        }
    }

    public void ResetTimer()
    {
        CanShowInterstitial = false;
        counter = 0;
    }
}
