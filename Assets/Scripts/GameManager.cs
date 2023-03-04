using UnityEngine.SceneManagement;
using UnityEngine;
using System.IO;
using System.Text;
public class GameManager : MonoBehaviour
{
    public User user;
    public static GameManager instance;
    private void Awake()
    {
        if(instance != null)
        {
            Destroy(gameObject);
            return;
        }
        instance = this;
        DontDestroyOnLoad(gameObject);
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
        string path = Application.persistentDataPath + "/saveFile.json";
        using(StreamReader reader = new StreamReader(path))
        {
            int i = 0;
            while(!reader.EndOfStream)
            {
                reader.ReadLine();
                i++;
            }
            user.id = i;
        }
        using(StreamWriter writer = new StreamWriter(path,true))
        {
            string data = JsonUtility.ToJson(user);
            writer.WriteLine(data);
        }
        this.user = user;
    }
    public void SaveUserProgress()
    {
        string path = Application.persistentDataPath + "/saveFile.json";
        string data = JsonUtility.ToJson(user);
        RewrateLine(path,user.id,data);
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
    public static string AccountDataSearch(string email)
    {
        string path = Application.persistentDataPath + "/saveFile.json";
        if(File.Exists(path))
        {
            using(StreamReader reader = new StreamReader(path))
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
    public static bool CheckingExistingEmail(string verifiedEmail)
    {
        string path = Application.persistentDataPath + "/saveFile.json";
        using(StreamReader sr = new StreamReader(path))
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
}