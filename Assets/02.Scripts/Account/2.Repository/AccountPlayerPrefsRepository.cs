using UnityEngine;

public class AccountPlayerPrefsRepository : MonoBehaviour
{
    private const string SAVE_KEY = nameof(AccountPlayerPrefsRepository);

    public void Save(AccountDTO accountDTO)
    {
        AccountSaveData data = new AccountSaveData(accountDTO);
        string json = JsonUtility.ToJson(data);

        PlayerPrefs.SetString(SAVE_KEY + data.Email, json);
        PlayerPrefs.Save();
    }

    public AccountSaveData Find(string email)
    {
        if(!PlayerPrefs.HasKey(SAVE_KEY + email))
        {
            return null;
        }

        string json = PlayerPrefs.GetString(SAVE_KEY + email);
        AccountSaveData data = JsonUtility.FromJson<AccountSaveData>(json);

        return data;
    }
}

public class AccountSaveData
{
    public string Email;
    public string Nickname;
    public string Password;

    public AccountSaveData() { }

    public AccountSaveData(AccountDTO accountDTO)
    {
        Email = accountDTO.Email;
        Nickname = accountDTO.Nickname;
        Password = accountDTO.Password;
    }
}
