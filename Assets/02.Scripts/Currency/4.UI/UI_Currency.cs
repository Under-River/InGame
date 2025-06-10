using TMPro;
using Unity.FPS.Gameplay;
using Unity.FPS.Game;
using UnityEngine;

public class UI_Currency : MonoBehaviour
{
    public TextMeshProUGUI GoldCountText;
    public TextMeshProUGUI DiamondCountText;
    public TextMeshProUGUI BuyHealthText;

    private void Start()
    {
        Refresh();
    }

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.Alpha1))
        {
            BuyHealth();
        }
    }

    public void Refresh()
    {
        var gold = CurrencyManager.Instance.Get(ECurrencyType.Gold);
        var diamond = CurrencyManager.Instance.Get(ECurrencyType.Diamond);

        GoldCountText.text = $"Gold : {gold.Value}";
        DiamondCountText.text = $"Diamond : {diamond.Value}";
        BuyHealthText.color = gold.HasEnough(300) ? Color.green : Color.red;
    }

    public void BuyHealth()
    {
        if(CurrencyManager.Instance.TryBuy(ECurrencyType.Gold, 300))
        {
            var player = FindFirstObjectByType<PlayerCharacterController>();
            Health playerHealth = player.GetComponent<Health>();
            playerHealth.Heal(100);
        }
        else
        {
            Debug.Log("돈이 부족합니다.");
        }
    }
}
