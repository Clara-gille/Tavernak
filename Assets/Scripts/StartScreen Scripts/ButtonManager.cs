using System.Collections;
using System.Collections.Generic;
using System.IO;
using TMPro;
using Unity.VisualScripting.Antlr3.Runtime.Tree;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class StartGame : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    [SerializeField] TextMeshProUGUI btnTxt;
    private Button btn;
    // Start is called before the first frame update
    void Start()
    {
        resetInventoryTxt();
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void OnPointerEnter (PointerEventData eventData)
    {
            // FAIRE UN PETIT SHAKE ON HOVER ET UN CLICK ONCLICK
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

    void resetInventoryTxt()
    {
        string path = "Assets/Scripts/Player/PlayerInventory.txt";
        File.WriteAllText(path, string.Empty);
    }
}
