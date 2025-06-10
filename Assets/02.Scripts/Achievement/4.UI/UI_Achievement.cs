using System.Collections.Generic;
using UnityEngine;

public class UI_Achievement : MonoBehaviour
{
    [SerializeField] private List<UI_AchievementSlot> _slots;
    private void Start()
    {
        Refresh();
        AchievementManager.Instance.OnDataChanged += Refresh;
        AchievementManager.Instance.OnNewAchievementRewarded += Refresh;
    }
    private void Refresh()
    {
        List<AchievementDTO> achievements = AchievementManager.Instance.Achievements();

        for(int i = 0; i < achievements.Count; i++)
        {
            _slots[i].Refresh(achievements[i]);
        }
    }

    private void Refresh(AchievementDTO achievementDTO)
    {
        Refresh();
        print($"New Achievement Rewarded: {achievementDTO.Name}");
    }
}
