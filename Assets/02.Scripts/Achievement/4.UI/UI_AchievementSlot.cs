using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UI_AchievementSlot : MonoBehaviour
{
    public TextMeshProUGUI NameText;
    public TextMeshProUGUI DescriptionText;
    public TextMeshProUGUI RewardCountText;
    public Slider ProgressSlider;
    public TextMeshProUGUI ProgressText;
    public TextMeshProUGUI RewardClaimDateText;
    public Button RewardClaimButton;

    private AchievementDTO _achievementDTO;

    private void Start()
    {
        RewardClaimButton.onClick.AddListener(ClaimReward);
    }

    public void Refresh(AchievementDTO achievementDTO)
    {
        _achievementDTO = achievementDTO;

        NameText.text = _achievementDTO.Name;
        DescriptionText.text = _achievementDTO.Description;
        RewardCountText.text = _achievementDTO.RewardAmount.ToString();
        ProgressSlider.value = (float)_achievementDTO.CurrentValue / _achievementDTO.GoalValue;
        ProgressText.text = $"{_achievementDTO.CurrentValue}/{_achievementDTO.GoalValue}";

        RewardClaimButton.interactable = _achievementDTO.CanClaimReward();
    }
    public void ClaimReward()
    {
        if(AchievementManager.Instance.TryClaimReward(_achievementDTO))
        {
            // 축하
        }
        else
        {
            // 보상 받을 수 없음
        }
    }
}
