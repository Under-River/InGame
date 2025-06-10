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

    public List<AchievementDTO> Load()
    {
        if(!PlayerPrefs.HasKey(SAVE_KEY))
        {
            return null;
        }

        string json = PlayerPrefs.GetString(SAVE_KEY);
        AchievementSaveDatas data = JsonUtility.FromJson<AchievementSaveDatas>(json);

        return data.DataList.ConvertAll(data => new AchievementDTO(data.Id, data.CurrentValue, data.IsRewardClaimed));
    }

    [Serializable]
    public struct AchievementSaveData
    {
        public string Id;
        public int CurrentValue;
        public bool IsRewardClaimed;
    }
    
    [Serializable]
    public struct AchievementSaveDatas
    {
        public List<AchievementSaveData> DataList;
    }
}
