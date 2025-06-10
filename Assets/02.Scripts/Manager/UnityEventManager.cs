using UnityEngine;
using UnityEngine.Events;

public class UnityEventManager : MonoBehaviour
{
    public UnityEvent OnChangedCurrency;
    public UnityEvent OnChangedAchievement;
    public UnityEvent<AchievementDTO> OnClearAchievement;

    public static UnityEventManager Instance;

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
}
