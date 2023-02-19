using UnityEngine.SceneManagement;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public User user = new User();
    private static GameManager instance;
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
    private void Start()
    {
        
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
    private void ListAvailableSkins()
    {

    }
    public void RegisterUser()
    {
        
    }
}