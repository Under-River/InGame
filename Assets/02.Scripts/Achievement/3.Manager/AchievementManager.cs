using System;
using System.Collections.Generic;
using UnityEngine;

public class AchievementManager : MonoBehaviour
{
    public static AchievementManager Instance;
    public static event Action OnDataInitialized;

    [SerializeField] private List<AchievementSO> _metaDatas;
    private List<Achievement> _achievements;
    public List<AchievementDTO> Achievements => _achievements.ConvertAll(achievement => new AchievementDTO(achievement));
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
    }
    private void Start()
    {
        Init();
    }

    private void Init()
    {
        _achievements = new List<Achievement>();
        _repository = new AchievementPlayerPrefsRepository();

        List<AchievementSaveData> saveDatas = _repository.Load();

        foreach(AchievementSO metaData in _metaDatas)
        {
            Achievement duplicateAchievement = FindByID(metaData.Id);

            if(duplicateAchievement != null)
            {
                throw new Exception($"업적 ID({metaData.Id})가 중복됩니다.");
            }

            AchievementSaveData saveData = saveDatas?.Find(a => a.Id == metaData.Id) ?? new AchievementSaveData();
            Achievement achievement = new Achievement(metaData, saveData);
            _achievements.Add(achievement);
        }
        OnDataInitialized?.Invoke();
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
                    UnityEventManager.Instance.OnClearAchievement.Invoke(new AchievementDTO(achievement));
                }
            }
        }
        _repository.Save(Achievements);
        UnityEventManager.Instance.OnChangedAchievement.Invoke();
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
            CurrencyManager.Instance.Add(achievement.RewardCurrencyType, achievement.RewardAmount);
            _repository.Save(Achievements);
            UnityEventManager.Instance.OnChangedAchievement.Invoke();
            return true;
        }

        return false;
    }
}
