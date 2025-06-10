using UnityEngine;

// 런타임시 변하지 않는값을 SO로 관리하면
// - 기획자가 에디터에서 직접 수정이 가능하다.
// - 유지보수와 확장성이 증가한다.
// - 도메인 객체(Achievement)는 상태(CurrentValue, IsClaimed)만 관리하면 된다.

[CreateAssetMenu(fileName = "AchievementSO", menuName = "Scriptable Objects/AchievementSO")]
public class AchievementSO : ScriptableObject
{
    [Header("기본 정보")]
    [SerializeField] private string _id;
    public string Id => _id;
    [SerializeField] private string _name;
    public string Name => _name;
    [TextArea(2, 5)]
    [SerializeField] private string _description;
    public string Description => _description;

    [Header("달성 조건")]
    [SerializeField] private EAchievementCondition _condition;
    public EAchievementCondition Condition => _condition;
    [SerializeField] private int _goalValue;
    public int GoalValue => _goalValue;

    [Header("보상")]
    [SerializeField] private ECurrencyType _rewardCurrencyType;
    public ECurrencyType RewardCurrencyType => _rewardCurrencyType;
    [SerializeField] private int _rewardAmount;
    public int RewardAmount => _rewardAmount;
}
