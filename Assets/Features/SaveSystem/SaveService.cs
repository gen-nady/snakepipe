using System.Collections;
using System.Collections.Generic;
using Newtonsoft.Json;
using UnityEngine;

public class SaveService 
{
   public PlayerSaveData PlayerSaveData { get; set; }

    public const string PlayerDataKey = "PlayerData";

   public void Save()
   {
        string newData = JsonUtility.ToJson(PlayerSaveData);
        SaveServiceHelper.SetString(PlayerDataKey, newData);
        SaveServiceHelper.Save();
    }

    public void Load()
    {
        if(!SaveServiceHelper.HasKey(PlayerDataKey))
        {
            PlayerSaveData = new PlayerSaveData();
            return;
        }

        var dataInString=SaveServiceHelper.GetString(PlayerDataKey);
        PlayerSaveData = JsonUtility.FromJson<PlayerSaveData>(dataInString);

    }
}
