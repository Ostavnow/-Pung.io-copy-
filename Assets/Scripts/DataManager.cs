using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
public class DataManager : MonoBehaviour , ISaveProgress
{
    public User _user;
    // public Abilities.AbilityDelegate _firstAbilityDelegate;
    // public Abilities.AbilityDelegate _secondAbilityDelegate;
    // public Abilities.AbilityDelegate _thirdAbilityDelegate;
    // public Abilities.AbilityDelegate _fourthAbilityDelegate;
    private static string Path
    {
        get{return Application.persistentDataPath + "/saveFile.json";}
    }
    private static string AccountsDatabasePath
    {
        get{return Application.persistentDataPath + "/accountsDatabase.json";}
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
    public void SaveProgress()
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
    public string AccountDataSearch(string email)
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
    public bool CheckingExistingEmail(string verifiedEmail)
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
    public Skin DeserializeSkin(int id)
    {
        string data = Resources.Load<TextAsset>("Text/skins").text;
        string[] d = data.Split("\n",9);
        Skin skin = JsonUtility.FromJson<Skin>(d[id]);
        skin._spriteSkinBody = Resources.Load<Sprite>("Sprites/Skins/body " + (id + 1) + " skin");
        skin._spriteSkinHand = Resources.Load<Sprite>("Sprites/Skins/hand " + (id + 1) + " skin");
        return skin;
    }
    public Ability DeserializeAbility(int id)
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
            SaveProgress();
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
