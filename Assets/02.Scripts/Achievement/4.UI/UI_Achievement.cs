using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UI_Achievement : MonoBehaviour
{
    [SerializeField] private Transform _slotParent;
    [SerializeField] private GameObject _slotPrefab;
    [SerializeField] private GameObject _canClaimRewardMessage;
    private List<UI_AchievementSlot> _slots;
    private void Start()
    {
        Init();
        Refresh();
        AchievementManager.Instance.OnDataChanged += Refresh;
        AchievementManager.Instance.OnNewAchievementRewarded += Refresh;
    }
    private void Init()
    {
        List<AchievementDTO> achievements = AchievementManager.Instance.Achievements();
        _slots = new List<UI_AchievementSlot>();

        for(int i = 0; i < achievements.Count; i++)
        {
            GameObject slot = Instantiate(_slotPrefab, _slotParent);
            _slots.Add(slot.GetComponent<UI_AchievementSlot>());
        }
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
        StartCoroutine(PlayRewardAnimation());
    }
    private IEnumerator PlayRewardAnimation()
    {
        _canClaimRewardMessage.SetActive(true);
        yield return new WaitForSeconds(2f);
        _canClaimRewardMessage.SetActive(false);
    }
}
