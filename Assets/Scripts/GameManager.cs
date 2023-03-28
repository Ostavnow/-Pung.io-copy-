using UnityEngine.SceneManagement;
using UnityEngine;
using System.IO;
using System.Text;
using System.Collections.Generic;
public class GameManager : MonoBehaviour
{
    public User user;
    public static GameManager instance;
    public Skin skin;
    public Abilities.AbilityDelegate firstAbilityDelegate;
    public Abilities.AbilityDelegate secondAbilityDelegate;
    public Abilities.AbilityDelegate thirdAbilityDelegate;
    public Abilities.AbilityDelegate fourthAbilityDelegate;
    private Abilities abilities;
    private static string path
    {
        get{return Application.persistentDataPath + "/saveFile.json";}
    }
    private static string accountsDatabasePath
    {
        get{return Application.persistentDataPath + "/accountsDatabase.json";}
    }
    private void Awake()
    {
        // user = null;
        if(instance != null)
        {
            Destroy(gameObject);
        }
        else
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        if(SceneManager.GetActiveScene().name == "Menu")
        {
            AuthorizationVerification();
            abilities = GetComponent<Abilities>();
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
        GameManager g = FindObjectOfType<GameManager>();
        Debug.Log(((int)mainUIHandler.firstAbilitySelected.GetComponent<AbilitySelectUI>().abilityEnum));
        Debug.Log(((int)mainUIHandler.secondAbilitySelected.GetComponent<AbilitySelectUI>().abilityEnum));
        g.firstAbilityDelegate = Abilities.abilitiesDelegate[((int)mainUIHandler.firstAbilitySelected.GetComponent<AbilitySelectUI>().abilityEnum)];
        g.secondAbilityDelegate = Abilities.abilitiesDelegate[((int)mainUIHandler.secondAbilitySelected.GetComponent<AbilitySelectUI>().abilityEnum)];
        g.thirdAbilityDelegate = Abilities.abilitiesDelegate[((int)mainUIHandler.thirdAbilitySelected.GetComponent<AbilitySelectUI>().abilityEnum)];
        g.fourthAbilityDelegate = Abilities.abilitiesDelegate[((int)mainUIHandler.fourthAbilitySelected.GetComponent<AbilitySelectUI>().abilityEnum)];
        SceneManager.LoadScene("Game");
    }
    public void SaveUserDatabase(User user)
    {
        using(StreamReader reader = new StreamReader(accountsDatabasePath))
        {
            int i = 0;
            while(!reader.EndOfStream)
            {
                reader.ReadLine();
                i++;
            }
            user.id = i;
        }
        using(StreamWriter writer = new StreamWriter(accountsDatabasePath,true))
        {
            string data = JsonUtility.ToJson(user);
            writer.WriteLine(data);
        }
        this.user = user;
    }
    public void SaveUserProgress()
    {
        string data = JsonUtility.ToJson(user);
        Debug.Log(user.email);
        if(File.Exists(accountsDatabasePath))
        {
            RewrateLine(accountsDatabasePath,user.id,data);
        }
        else
        {
            File.WriteAllText(path,data);
        }
    }
    public static string AccountDataSearch(string email)
    {
        if(File.Exists(path))
        {
            using(StreamReader reader = new StreamReader(accountsDatabasePath))
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
        using(StreamReader sr = new StreamReader(accountsDatabasePath))
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
        skin.spriteSkinBody = Resources.Load<Sprite>("Sprites/Skins/body " + (id + 1) + " skin");
        skin.spriteSkinHand = Resources.Load<Sprite>("Sprites/Skins/hand " + (id + 1) + " skin");
        return skin;
    }
    public static Ability DeserializeAbility(int id)
    {
        string data = Resources.Load<TextAsset>("Text/abilities").text;
        string[] d = data.Split("\n",9);
        Ability ability = JsonUtility.FromJson<Ability>(d[id]);
        ability.spriteAbility = Resources.Load<Sprite>("Sprites/Abilities/ability icon " + (id + 1));
        return ability;
    }
    private void AuthorizationVerification()
    {
        if(File.Exists(path))
        {
        string localData = File.ReadAllText(path);
        string email = localData.Split('"',11)[9];
        user = JsonUtility.FromJson<User>(localData);
        if(email != "")
        {
           user = JsonUtility.FromJson<User>(AccountDataSearch(email));
        }  
        }
        else
        {
            user = new User();
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