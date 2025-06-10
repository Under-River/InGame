using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

// 아키텍쳐: 설계 그 잡채(설계마다 철학이 있다.)
// 디자인 패턴: 설계를 구현하는 과정에서 쓰이는 패턴

// '관리' + '창구'
public class CurrencyManager : MonoBehaviour
{
    public static CurrencyManager Instance;

    private Dictionary<ECurrencyType, Currency> _currencies;
    private CurrencyPlayerPrefsRepository _repository;


    // 로버트 C 마틴 : 미리하는 성능 최적화 90%는 필요없다.
    // public event Action OnGoldChanged;
    // public event Action OnDiamondChanged;

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

    private void Start()
    {
        Init();
    }

    private void Init()
    {
        // 초기화
        _currencies = new Dictionary<ECurrencyType, Currency>((int)ECurrencyType.Count);
        // 레포지토리(깃허브)
        _repository = new CurrencyPlayerPrefsRepository();

        List<CurrencyDTO> loadedCurrencies = _repository.Load();
        if(loadedCurrencies == null)
        {
            for(int i = 0; i < (int)ECurrencyType.Count; i++)
            {
                ECurrencyType type = (ECurrencyType)i;

                // 골드, 다이아몬드 등을 0 값으로 생산 후 딕셔너리에 삽입
                Currency currency = new Currency(type, 0);
                _currencies.Add(type, currency);
            }
            return;
        }
        else
        {
            foreach(CurrencyDTO data in loadedCurrencies)
            {
                Currency currency = new Currency(data.Type, data.Value);
                _currencies.Add(data.Type, currency);
            }
        }
    }

    private List<CurrencyDTO> ToDtoList()
    {
        return _currencies.ToList().ConvertAll(currency => new CurrencyDTO(currency.Value));
    }

    public CurrencyDTO Get(ECurrencyType type)
    {
        return new CurrencyDTO(_currencies[type]);
    }

    public void Add(ECurrencyType type, int value)
    {
        // 매니져에 유효성 검사 들어가면 안됨
        // 관리는 매니져, 규칙은 도메인
        // 도메인 클래스에서 유효성 검사 해야 함
        _currencies[type].Add(value);
        _repository.Save(ToDtoList());

        if(type == ECurrencyType.Gold)
        {
            AchievementManager.Instance.Increase(EAchievementCondition.GoldCollect, value);
        }
        else if(type == ECurrencyType.Diamond)
        {
            // AchievementManager.Instance.Increase(EAchievementCondition.DiamondCollect, value);
        }

        UnityEventManager.Instance.OnChangedCurrency.Invoke();
    }

    public bool TryBuy(ECurrencyType type, int value)
    {
        if(!_currencies[type].TryBuy(value))
        {
            return false;
        }

        _repository.Save(ToDtoList());
        UnityEventManager.Instance.OnChangedCurrency.Invoke();

        return true;
    }
}
