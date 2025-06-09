using System.Collections.Generic;
using UnityEngine;

// 아키텍쳐: 설계 그 잡채(설계마다 철학이 있다.)
// 디자인 패턴: 설계를 구현하는 과정에서 쓰이는 패턴

// '관리' + '창구'
public class CurrencyManager : MonoBehaviour
{
    public static CurrencyManager Instance;

    private Dictionary<ECurrencyType, Currency> _currencies = new Dictionary<ECurrencyType, Currency>();

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

        Init();
    }

    private void Init()
    {
        for(int i = 0; i < (int)ECurrencyType.Count; i++)
        {
            ECurrencyType type = (ECurrencyType)i;
            // 골드, 다이아몬드 등을 0 값으로 생산 후 딕셔너리에 삽입
            Currency currency = new Currency(type, 0);
            _currencies.Add(type, currency);
        }
    }

    public void Add(ECurrencyType type, int value)
    {
        // 매니져에 유효성 검사 들어가면 안됨
        // 관리는 매니져, 규칙은 도메인
        // 도메인 클래스에서 유효성 검사 해야 함
        _currencies[type].Add(value);

        Debug.Log($"{type} 현재 값: {_currencies[type].Value}");
    }

    public void Subtract(ECurrencyType type, int value)
    {
        _currencies[type].Subtract(value);
    }
}
