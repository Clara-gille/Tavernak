using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting.Antlr3.Runtime.Tree;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class StartGame : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    [SerializeField] TextMeshProUGUI btnTxt;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public void OnPointerEnter (PointerEventData eventData)
    {
        btnTxt.color = 
    }
    public void OnPointerExit (PointerEventData eventData)
    {

    }

    public void OnClickStart()
    {
        SceneManager.LoadScene(sceneBuildIndex: 1);
    }
    public void OnClickQuit()
    {
        Application.Quit();
    }
}
