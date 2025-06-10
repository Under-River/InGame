using UnityEngine;

public enum EAchievementCondition
{
    GoldCollect,
    DronKillCount,
    BossKillCount,
    PlayTime,
    Trigger,


    Count
}

public class Achievement
{
    // 데이터
    public readonly string Id;
    public readonly string Name;
    public readonly string Description;
    public readonly EAchievementCondition Condition;
    public int GoalValue;
    public ECurrencyType RewardCurrencyType;
    public int RewardAmount;

    // 상태
    private int _currentValue;
    public int CurrentValue => _currentValue;

    private bool _isRewardClaimed;
    public bool IsRewardClaimed => _isRewardClaimed;

    // 생성자
    public Achievement(){ }
    public Achievement(AchievementSO metaData)
    {
        if(string.IsNullOrEmpty(metaData.Id) || string.IsNullOrWhiteSpace(metaData.Id)) { throw new System.Exception("업적 ID는 비어있을 수 없습니다."); }
        if(string.IsNullOrEmpty(metaData.Name)) { throw new System.Exception("업적 이름은 비어있을 수 없습니다."); }
        if(string.IsNullOrEmpty(metaData.Description)) { throw new System.Exception("업적 설명은 비어있을 수 없습니다."); }
        if(metaData.GoalValue <= 0) { throw new System.Exception("목표 값은 0보다 커야 합니다."); }
        if(metaData.RewardAmount <= 0) { throw new System.Exception("보상 금액은 0보다 커야 합니다."); }

        Id = metaData.Id;
        Name = metaData.Name;
        Description = metaData.Description;
        Condition = metaData.Condition;
        GoalValue = metaData.GoalValue;
        RewardCurrencyType = metaData.RewardCurrencyType;
        RewardAmount = metaData.RewardAmount;
    }

    public void Increase(int value)
    {
        if(value <= 0) { throw new System.Exception("증가할 값은 0보다 커야 합니다."); }

        _currentValue += value;
    }

    public bool CanClaimReward()
    {
        return !_isRewardClaimed && _currentValue >= GoalValue;
    }

    public bool TryClaimReward()
    {
        if(!CanClaimReward())
        {
            return false;
        }

        _isRewardClaimed = true;
        return true;
    }
}
