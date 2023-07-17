using UnityEngine.SceneManagement;
using UnityEngine;
using System.IO;
using System.Text;
using System.Collections.Generic;
public sealed class GameManager : MonoBehaviour
{
    public User _user;
    public static GameManager _instance;
    public Skin _skin;
    public Abilities.AbilityDelegate _firstAbilityDelegate;
    public Abilities.AbilityDelegate _secondAbilityDelegate;
    public Abilities.AbilityDelegate _thirdAbilityDelegate;
    public Abilities.AbilityDelegate _fourthAbilityDelegate;
    private Abilities _abilities;
    private static string Path
    {
        get{return Application.persistentDataPath + "/saveFile.json";}
    }
    private static string AccountsDatabasePath
    {
        get{return Application.persistentDataPath + "/accountsDatabase.json";}
    }
    private void Awake()
    {
        Application.targetFrameRate = 300;
        if(_instance != null)
        {
            Destroy(gameObject);
        }
        else
        {
            _instance = this;
            DontDestroyOnLoad(gameObject);
        }
        if(SceneManager.GetActiveScene().name == "Menu")
        {
            AuthorizationVerification();
            _abilities = GetComponent<Abilities>();
        }
    }
    public static void LoadMenuScene()
    {
        SceneManager.LoadScene("Menu");
    }
    public static void LoadShopScene()
    {
        SceneManager.LoadScene("Shop");
    }
    public static void LoadVipShopScene()
    {
        SceneManager.LoadScene("Vip shop");
    }
    public static void LoadGame()
    {
        MainUIHandler mainUIHandler = FindObjectOfType<MainUIHandler>();
        GameManager gameManager = FindObjectOfType<GameManager>();
        // Debug.Log(((int)mainUIHandler._firstAbilitySelected.GetComponent<AbilitySelectUI>()._abilityEnum));
        // Debug.Log(((int)mainUIHandler._secondAbilitySelected.GetComponent<AbilitySelectUI>()._abilityEnum));
        gameManager._firstAbilityDelegate = Abilities._abilitiesDelegate[((int)mainUIHandler._firstAbilitySelected.GetComponent<AbilitySelectUI>()._abilityEnum)];
        gameManager._secondAbilityDelegate = Abilities._abilitiesDelegate[((int)mainUIHandler._secondAbilitySelected.GetComponent<AbilitySelectUI>()._abilityEnum)];
        gameManager._thirdAbilityDelegate = Abilities._abilitiesDelegate[((int)mainUIHandler._thirdAbilitySelected.GetComponent<AbilitySelectUI>()._abilityEnum)];
        gameManager._fourthAbilityDelegate = Abilities._abilitiesDelegate[((int)mainUIHandler._fourthAbilitySelected.GetComponent<AbilitySelectUI>()._abilityEnum)];
        SceneManager.LoadScene("Game");
    }
    public void UpdateSkin()
    {
        PlayerController playerController = FindObjectOfType<UserPlayerController>();
        UserPlayer userPlayer = FindObjectOfType<UserPlayer>();
        Skin skin = GameManager.DeserializeSkin(GameManager._instance._user._numberSelectedSkin);
        userPlayer.gameObject.GetComponent<SpriteRenderer>().sprite = skin._spriteSkinBody;
        playerController.hand.GetComponent<SpriteRenderer>().sprite = skin._spriteSkinHand;
        userPlayer._multiplierAttackDamageImprovement = skin._attackDamage;
        userPlayer._healthImprovementMultiplier = skin._health;
        userPlayer._staminaImprovementMultiplier = skin._stamina;
        userPlayer._criticalDamageImprovementMultiplier = skin._criticalDamage;
        userPlayer._attackSpeedImprovementMultiplier = skin._attackSpeed;
    }
    public void SaveUserDatabase(User user)
    {
        using(StreamReader reader = new StreamReader(AccountsDatabasePath))
        {
            int i = 0;
            while(!reader.EndOfStream)
            {
                reader.ReadLine();
                i++;
            }
            user._id = i;
        }
        using(StreamWriter writer = new StreamWriter(AccountsDatabasePath,true))
        {
            string data = JsonUtility.ToJson(user);
            writer.WriteLine(data);
        }
        this._user = user;
    }
    public void SaveUserProgress()
    {
        string data = JsonUtility.ToJson(_user);
        Debug.Log(_user._email);
        if(File.Exists(AccountsDatabasePath))
        {
            RewrateLine(AccountsDatabasePath,_user._id,data);
        }
        else
        {
            File.WriteAllText(Path,data);
        }
    }
    public static string AccountDataSearch(string email)
    {
        if(File.Exists(Path))
        {
            using(StreamReader reader = new StreamReader(AccountsDatabasePath))
            {
                while(!(reader.EndOfStream))
                {
                string line = reader.ReadLine();
                string emailDataBase = line.Split('"',11)[9];
                if(emailDataBase == email)
                {
                    return line;
                }
                }
            }
        }
        return null;
    }
    private static void RewrateLine(string path,int lineIndex,string newValue)
    {
        int i = 0;
        string tempPath = path + ".tmp";
        using(StreamReader sr = new StreamReader(path))
        using(StreamWriter sw = new StreamWriter(tempPath))
        {
            while(!sr.EndOfStream)
            {
                string line = sr.ReadLine();
                if(lineIndex == i)
                    sw.WriteLine(newValue);
                else
                    sw.WriteLine(line);
                i++;
            }
        }
        File.Delete(path);
        File.Move(tempPath,path);
    }
    public static bool CheckingExistingEmail(string verifiedEmail)
    {
        using(StreamReader sr = new StreamReader(AccountsDatabasePath))
        {
            while(!sr.EndOfStream)
            {
                string email = sr.ReadLine().Split('"',11)[9];
                if(verifiedEmail == email)
                {
                    return false;
                }
            }
        }
        return true;
    }
    public static Skin DeserializeSkin(int id)
    {
        string data = Resources.Load<TextAsset>("Text/skins").text;
        string[] d = data.Split("\n",9);
        Skin skin = JsonUtility.FromJson<Skin>(d[id]);
        skin._spriteSkinBody = Resources.Load<Sprite>("Sprites/Skins/body " + (id + 1) + " skin");
        skin._spriteSkinHand = Resources.Load<Sprite>("Sprites/Skins/hand " + (id + 1) + " skin");
        return skin;
    }
    public static Ability DeserializeAbility(int id)
    {
        string data = Resources.Load<TextAsset>("Text/abilities").text;
        string[] d = data.Split("\n",9);
        Ability ability = JsonUtility.FromJson<Ability>(d[id]);
        ability._spriteAbility = Resources.Load<Sprite>("Sprites/Abilities/ability icon " + (id + 1));
        return ability;
    }
    private void AuthorizationVerification()
    {
        if(File.Exists(Path))
        {
        string localData = File.ReadAllText(Path);
        string email = localData.Split('"',11)[9];
        _user = JsonUtility.FromJson<User>(localData);
        if(email != "")
        {
           _user = JsonUtility.FromJson<User>(AccountDataSearch(email));
        }  
        }
        else
        {
            _user = new User();
            SaveUserProgress();
            return;
        }
    }
    public static void SerializeClassAbility(List<Ability> abilities)
    {
        string path = "Assets/Resources/abilities.json";
        File.WriteAllText(path,"");
        using(StreamWriter sw = new StreamWriter(path))
        {
            for(int i = 0;i < abilities.Count;i++)
            {
                string ability = JsonUtility.ToJson(abilities[i]);
                sw.WriteLine(ability);
            }
        }
        Debug.Log("Serilization was successful");
    }
    public static void SerializeClassSkins(List<Skin> skins)
    {
        string path = "Assets/Resources/skins.json";
        File.WriteAllText(path,"");
        using(StreamWriter sw = new StreamWriter(path))
        {
            for(int i = 0;i < skins.Count;i++)
            {
                string skin = JsonUtility.ToJson(skins[i]);
                sw.WriteLine(skin);
            }
        }
        Debug.Log("Serilization was successful");
    }
}