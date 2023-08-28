using UnityEngine;
namespace Manager
{
    public class SceneManager
    {
        public enum Scene
        {
            Menu,
            Game,
            Shop,
            VipShop
        }
        public void LoadScene(Scene scene)
        {
            UnityEngine.SceneManagement.SceneManager.LoadScene((int)scene);
        }
    }
}   

