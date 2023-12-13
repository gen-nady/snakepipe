using System;
using UnityEngine;

public class ProjectContext : Singleton<ProjectContext>
{
    public SaveService SaveService { get; private set; }
    
    public AdsService AdsService { get; private set; }

    protected override void Awake()
    {
        base.Awake();
        DontDestroyOnLoad(this.gameObject);
        SaveService = new SaveService();

        AdsService = FindObjectOfType<YandexAdsService>(); //TODO

        SaveService.Load();
        Debug.Log("Project context initialized");
    }
}
