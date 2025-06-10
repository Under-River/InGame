using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UI_Achievement : MonoBehaviour
{
    [SerializeField] private Transform _slotParent;
    [SerializeField] private GameObject _slotPrefab;
    private List<UI_AchievementSlot> _slots;
    private void Awake()
    {
        AchievementManager.OnDataInitialized += Init;
    }
   
    private void Init()
    {
        List<AchievementDTO> achievements = AchievementManager.Instance.Achievements;
        _slots = new List<UI_AchievementSlot>();

        for(int i = 0; i < achievements.Count; i++)
        {
            GameObject slot = Instantiate(_slotPrefab, _slotParent);
            _slots.Add(slot.GetComponent<UI_AchievementSlot>());
        }
        Refresh();
    }
    public void Refresh()
    {
        List<AchievementDTO> achievements = AchievementManager.Instance.Achievements;

        for(int i = 0; i < achievements.Count; i++)
        {
            _slots[i].Refresh(achievements[i]);
        }
    }
}
