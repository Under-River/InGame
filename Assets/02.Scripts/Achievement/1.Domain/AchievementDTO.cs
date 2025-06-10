using System;

[Serializable]
public class AchievementDTO
{
    public readonly string Id;
    public readonly string Name;
    public readonly string Description;
    public readonly EAchievementCondition Condition;
    public readonly int GoalValue;
    public readonly ECurrencyType RewardCurrencyType;
    public readonly int RewardAmount;
    public readonly int CurrentValue;
    public readonly bool IsRewardClaimed;

    public AchievementDTO
    (
        string id,
        int currentValue,
        bool isRewardClaimed
    )
    {
        Id = id;
        CurrentValue = currentValue;
        IsRewardClaimed = isRewardClaimed;
    }

    public AchievementDTO(Achievement achievement)
    {
        Id = achievement.Id;
        Name = achievement.Name;
        Description = achievement.Description;
        Condition = achievement.Condition;
        GoalValue = achievement.GoalValue;
        RewardCurrencyType = achievement.RewardCurrencyType;
        RewardAmount = achievement.RewardAmount;
        CurrentValue = achievement.CurrentValue;
        IsRewardClaimed = achievement.IsRewardClaimed;
    }

    public bool CanClaimReward()
    {
        return !IsRewardClaimed && CurrentValue >= GoalValue;
    }
}
