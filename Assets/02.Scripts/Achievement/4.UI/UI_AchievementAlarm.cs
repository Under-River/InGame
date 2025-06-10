using System.Collections;
using TMPro;
using UnityEngine;

public class UI_AchievementAlarm : MonoBehaviour
{
    [SerializeField] private GameObject _canClaimRewardMessage;
    [SerializeField] private TextMeshProUGUI _canClaimRewardMessageText;

    private void Start()
    {
        _canClaimRewardMessage.SetActive(false);
        AchievementManager.Instance.OnNewAchievementRewarded += Alarm;
    }
    private void Alarm(AchievementDTO achievementDTO)
    {
        _canClaimRewardMessageText.text = $"{achievementDTO.Name} 달성!";
        StartCoroutine(PlayRewardAnimation());
    }
    private IEnumerator PlayRewardAnimation()
    {
        _canClaimRewardMessage.SetActive(true);
        yield return new WaitForSeconds(2f);
        _canClaimRewardMessage.SetActive(false);
    }
}
