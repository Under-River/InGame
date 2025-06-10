using System;
using System.Collections.Generic;
using UnityEngine;

public class AchievementPlayerPrefsRepository : MonoBehaviour
{
    private const string SAVE_KEY = nameof(AchievementPlayerPrefsRepository);

    public void Save(List<AchievementDTO> achievementDTOs)
    {
        AchievementSaveDatas data = new AchievementSaveDatas();
        data.DataList = achievementDTOs.ConvertAll(data => new AchievementSaveData
        {
            Id = data.Id,
            CurrentValue = data.CurrentValue,
            IsRewardClaimed = data.IsRewardClaimed
        });

        string json = JsonUtility.ToJson(data);
        PlayerPrefs.SetString(SAVE_KEY, json);
    }

    public List<AchievementSaveData> Load()
    {
        if(!PlayerPrefs.HasKey(SAVE_KEY))
        {
            return null;
        }

        string json = PlayerPrefs.GetString(SAVE_KEY);
        AchievementSaveDatas data = JsonUtility.FromJson<AchievementSaveDatas>(json);

        return data.DataList;
    }

}

[Serializable]
public struct AchievementSaveDatas
{
    public List<AchievementSaveData> DataList;
}
