using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UIController : MonoBehaviour
{
    public Canvas menuCanvas;
    public Canvas hudCanvas;

    public RectTransform hpPlayerImage;

    // Start is called before the first frame update
    void Start()
    {
        menuCanvas.gameObject.SetActive(false);
        hudCanvas.gameObject.SetActive(true);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            PauseMethod();
        }


        // �������� ������� ���� ��������
        var activeCat = Player.instance.ActiveCat;
        if (activeCat != null)
        {
            var destructible = Player.instance.ActiveCat.GetComponent<Destructible>();
            if (destructible != null)
            {
                //float currentHP = destructible.currentHitPoints;
                float maxHP = destructible._hitPoints;

                // ������������ ������ ������� (��������� ��� ��� ������)
                float maxWidth = maxHP / 20 * 62;

                // ������ �������� ����������
                float height = hpPlayerImage.sizeDelta.y;

                // ��������� ������ RectTransform
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
        SceneManager.LoadScene(nameof(Scenes.MainMenu));
    }
    public void AppQuit()
    {
        Application.Quit();
    }
}
