using System.Collections;
using System.Collections.Generic;
using System.IO;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DayTimer : MonoBehaviour
{
    [SerializeField] private float timeLeft;
    [SerializeField] private TextMeshProUGUI timerTxt;
    private bool isTimerOn = false;
    [SerializeField] private GameObject inventoryCanvas;

    // Start is called before the first frame update
    void Start()
    {
        isTimerOn = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (isTimerOn)
        {
            if (timeLeft > 0)
            {
                timeLeft -= Time.deltaTime;
                UpdateTimer(timeLeft);
            }
            else
            {
                switch (SceneManager.GetActiveScene().buildIndex)
                {
                    case 1:
                        timeLeft = 10;
                        if (!SceneManager.GetSceneByBuildIndex(2).isLoaded)
                        {
                            SceneManager.LoadScene(2);
                        }
                        // Move the object to the scene
                        MoveInventoryCanvasToScene(2);
                        SceneManager.LoadScene(2);
                        break;
                    case 2:
                        timeLeft = 10;
                        if (!SceneManager.GetSceneByBuildIndex(1).isLoaded)
                        {
                            SceneManager.LoadScene(1);
                        }
                        // Move the object to the scene
                        MoveInventoryCanvasToScene(1);
                        SceneManager.LoadScene(1);
                        break;
                    default:
                        SceneManager.LoadScene(sceneBuildIndex: 0);
                        break;
                }
            }
        }
    }

    void UpdateTimer(float currentTime)
    {
        currentTime += 1;

        float minutes = Mathf.FloorToInt(currentTime / 60);
        float seconds = Mathf.FloorToInt(currentTime % 60);

        timerTxt.text = string.Format("{0:00} : {1:00}", minutes, seconds);
    }
    void MoveInventoryCanvasToScene(int targetSceneBuildIndex)
    {
        // Move the object to the target scene
        SceneManager.MoveGameObjectToScene(inventoryCanvas, SceneManager.GetSceneByBuildIndex(targetSceneBuildIndex));
        DontDestroyOnLoad(inventoryCanvas);
    }
}
