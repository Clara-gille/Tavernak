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
    [SerializeField] Button btn;

    // Start is called before the first frame update
    void Start()
    {
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
        GameObject inventoryCanvas = GameObject.Find("InventoryCanvas");
        if (inventoryCanvas != null)
        {
            Destroy(inventoryCanvas);
        }
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void OnPointerEnter (PointerEventData eventData)
    {
        // FAIRE UN PETIT SHAKE ON HOVER ET UN CLICK ONCLICK
        btn.transform.position += new Vector3(2.713409f, -2.470963f, 0);
    }
    public void OnPointerExit (PointerEventData eventData)
    {
        btn.transform.position += new Vector3(-2.713409f, 2.470963f, 0);
    }

    public void OnClickStart()
    {
        resetInventoryTxt();
        SceneManager.LoadScene(sceneBuildIndex: 1);
    }
    public void OnClickLoad() {
        SceneManager.LoadScene(sceneBuildIndex: 1);
    }

    public void OnClickQuit()
    {
        Application.Quit();
    }

    void resetInventoryTxt()
    {
        string path = Path.Combine(Application.persistentDataPath, "PlayerInventory.txt");
        File.WriteAllText(path, string.Empty);
    }
}
