using UnityEngine.SceneManagement;
using UnityEngine;
public sealed class GameManager : MonoBehaviour
{
    public User _user;
    public static GameManager _instance;
    public Skin _skin;
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
    }
    private SceneManager sceneManager;
    private DataManager dataManager;
    public void UpdateSkin()
    {
        // PlayerController playerController = FindObjectOfType<UserPlayerController>();
        // UserPlayer userPlayer = FindObjectOfType<UserPlayer>();
        // Skin skin = GameManager.DeserializeSkin(GameManager._instance._user._numberSelectedSkin);
        // userPlayer.gameObject.GetComponent<SpriteRenderer>().sprite = skin._spriteSkinBody;
        // playerController.hand.GetComponent<SpriteRenderer>().sprite = skin._spriteSkinHand;
        // userPlayer._multiplierAttackDamageImprovement = skin._attackDamage;
        // userPlayer._healthImprovementMultiplier = skin._health;
        // userPlayer._staminaImprovementMultiplier = skin._stamina;
        // userPlayer._criticalDamageImprovementMultiplier = skin._criticalDamage;
        // userPlayer._attackSpeedImprovementMultiplier = skin._attackSpeed;
    }
}