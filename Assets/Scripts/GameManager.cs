using UnityEngine.SceneManagement;
using UnityEngine;
using System.IO;
using System.Text;
public class GameManager : MonoBehaviour
{
    public User user;
    public static GameManager instance;
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
            Debug.Log("gthtvtyyfz bybwbkbpbhjdfyyf");
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        if(SceneManager.GetActiveScene().name == "Menu")
        {
            AuthorizationVerification();
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
        if(user.userName != "")
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
        string path = Application.persistentDataPath + "/skins.json";
        using(StreamReader sr = new StreamReader(path))
        {
            for(int i = 0; i <= id;i++)
            {
                if(i == id)
                {
                    return (Skin) JsonUtility.FromJson<Skin>(sr.ReadLine());
                }
                sr.ReadLine();
            }
        }
        return null;
    }
    private void AuthorizationVerification()
    {
        string Localdata = File.ReadAllText(path);
        string email = Localdata.Split('"',11)[9];
        if(email != "")
        {
           user = JsonUtility.FromJson<User>(AccountDataSearch(email));
        }
        else
        {
           user = JsonUtility.FromJson<User>(Localdata);
           Debug.Log(user.amountMoney);
        }
    }
}