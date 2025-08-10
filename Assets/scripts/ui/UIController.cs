using UnityEngine;
using UnityEngine.SceneManagement;

public class UIController : MonoBehaviour
{
    public Canvas menuCanvas;
    public Canvas hudCanvas;

    public RectTransform hpPlayerImage;

    void Start()
    {
        menuCanvas.gameObject.SetActive(false);
        hudCanvas.gameObject.SetActive(true);
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            PauseMethod();
        }


        var activeCat = Player.instance.ActiveCat;
        if (activeCat != null)
        {
            var destructible = Player.instance.ActiveCat.GetComponent<Destructible>();
            if (destructible != null)
            {
                float maxHP = destructible.currentHitPoints;
                float maxWidth;
                //if (destructible.currentHitPoints == destructible._hitPoints)
                //{
                    maxWidth = maxHP / 20 * 62;
                //}
                //else
                //{
                //    maxWidth = maxHP / destructible.takenDmg * 62;
                //}
                    

                float height = hpPlayerImage.sizeDelta.y;

                hpPlayerImage.sizeDelta = new Vector2(maxWidth, height);
            }
        }


    }
    public void PauseMethod()
    {
        if (GameController.isPaused == false)
        {
            Time.timeScale = 0.0f;
            menuCanvas.gameObject.SetActive(true);
            hudCanvas.gameObject.SetActive(false);
        }
        else
        {
            Time.timeScale = 1.0f;
            menuCanvas.gameObject.SetActive(false);
            hudCanvas.gameObject.SetActive(true);
        }
        GameController.isPaused = !GameController.isPaused;
    }
    public void ToMainMenu()
    {
        Time.timeScale = 1.0f;
        GameController.isPaused = false;
        SceneManager.LoadScene(nameof(Scenes.MainMenu));
    }
    public void AppQuit()
    {
        Application.Quit();
    }
}
