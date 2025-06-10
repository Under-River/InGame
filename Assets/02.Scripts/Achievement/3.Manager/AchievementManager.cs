using System;
using System.Collections.Generic;
using UnityEngine;

public class AchievementManager : MonoBehaviour
{
    public static AchievementManager Instance;

    [SerializeField] private List<AchievementSO> _metaDatas;
    private List<Achievement> _achievements;
    public List<AchievementDTO> Achievements() => _achievements.ConvertAll(achievement => new AchievementDTO(achievement));

    public event Action OnDataChanged;
    public event Action<AchievementDTO> OnNewAchievementRewarded;

    private AchievementPlayerPrefsRepository _repository;

    private void Awake()
    {
        if(Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }

        Init();
    }

    private void Init()
    {
        _achievements = new List<Achievement>();
        _repository = new AchievementPlayerPrefsRepository();

        List<AchievementDTO> loadedAchievements = _repository.Load();
        if(loadedAchievements == null)
        {
            for(int i = 0; i < (int)EAchievementCondition.Count; i++)
            {
                Achievement achievement = new Achievement();
                _achievements.Add(achievement);
            }
        }
        else
        {
            foreach(AchievementDTO data in loadedAchievements)
            {
                Achievement achievement = new Achievement(_metaDatas.Find(metaData => metaData.Id == data.Id));
                achievement.Increase(data.CurrentValue);
                achievement.TryClaimReward();

                _achievements.Add(achievement);
            }
        }
        foreach(var metaData in _metaDatas)
        {
            Achievement duplicateAchievement = FindByID(metaData.Id);

            if(duplicateAchievement != null)
            {
                throw new Exception($"업적 ID({metaData.Id})가 중복됩니다.");
            }

            Achievement achievement = new Achievement(metaData);
            _achievements.Add(achievement);
        }
    }

    private Achievement FindByID(string id)
    {
        return _achievements.Find(achievement => achievement.Id == id);
    }

    public void Increase(EAchievementCondition condition, int value)
    {
        foreach(var achievement in _achievements)
        {
            if(achievement.Condition == condition)
            {
                bool prevCanClaimReward = achievement.CanClaimReward();
                achievement.Increase(value);
                bool canClaimReward = achievement.CanClaimReward();

                if(prevCanClaimReward != canClaimReward)
                {
                    OnNewAchievementRewarded?.Invoke(new AchievementDTO(achievement));
                }
            }
        }
        _repository.Save(Achievements());
        OnDataChanged?.Invoke();
    }

    public bool TryClaimReward(AchievementDTO achievementDTO)
    {
        Achievement achievement = FindByID(achievementDTO.Id);

        if(achievement == null)
        {
            return false;
        }

        if(achievement.TryClaimReward())
        {
            _repository.Save(Achievements());
            CurrencyManager.Instance.Add(achievement.RewardCurrencyType, achievement.RewardAmount);
            OnDataChanged?.Invoke();
            return true;
        }

        return false;
    }
}
