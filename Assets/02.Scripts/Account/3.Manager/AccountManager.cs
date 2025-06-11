using UnityEngine;
using static CryptoUtil;

public class AccountManager : MonoBehaviour
{
    public static AccountManager Instance;

    private Account _myAccount;
    public AccountDTO CurrentAccount => _myAccount.ToDTO();
    private AccountPlayerPrefsRepository _repository;
    
    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
            
            _repository = gameObject.AddComponent<AccountPlayerPrefsRepository>();
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private const string SALT = "123456";
    
    public Result TryRegister(string email, string nickname, string password)
    {
        AccountSaveData saveData = _repository.Find(email);
        if (saveData != null)
        {
            return new Result(false, "이메일 중복되었습니다.");
        }

        string encryptedPassword = Encryption(password, SALT);
        Account account = new Account(email, nickname, encryptedPassword);
        _repository.Save(account.ToDTO());
        
        return new Result(true, "회원가입 성공");
    }
    
    public bool TryLogin(string email, string password)
    {
        AccountSaveData saveData = _repository.Find(email);
        if (saveData == null)
        {
            return false;
        }

        if (Verify(password, saveData.Password, SALT))
        {
            _myAccount = new Account(saveData.Email, saveData.Nickname, saveData.Password);
            return true;
        }

        return false;
    }
}
                                                     