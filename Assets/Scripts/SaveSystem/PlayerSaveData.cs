using System;
using System.Collections.Generic;

[Serializable]
public class PlayerSaveData
{
   public int Coins;
   public int CurrentSkinId;
   public int BestScore;
   public List<LevelsSaveData> AvailableLevels=new List<LevelsSaveData>();
}

[Serializable]
public class LevelsSaveData
{
    public string ID;
    public int Levels;
}
