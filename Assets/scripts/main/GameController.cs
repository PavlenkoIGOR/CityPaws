using UnityEngine;
using UnityEngine.SceneManagement;

public enum Scenes
{
    MainMenu,
    GameScene_Room,
    GameScene_Street,
    GameScene_Roof,
    GameScene_Boss
}
public class GameController : SingletonBase<GameController>
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void StartApp()
    {
        SceneManager.LoadScene(nameof(Scenes.GameScene_Room));
    }
    public void ExitApp()
    {
        Application.Quit();
    }
}
