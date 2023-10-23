using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DayTimer : MonoBehaviour
{
    [SerializeField] private float timeLeft;
    [SerializeField] private TextMeshProUGUI timerTxt;
    private bool isTimerOn = false;

    // Start is called before the first frame update
    void Start()
    {
        isTimerOn = true;
        Debug.Log(SceneManager.GetActiveScene().buildIndex);
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
                        SceneManager.LoadScene(sceneBuildIndex: 2);
                        break;
                    case 2:
                        timeLeft = 10;
                        SceneManager.LoadScene(sceneBuildIndex: 1);
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
}
