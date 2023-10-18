using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using TMPro;

public class InventoryItem : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    public Ingredient ingredient;
    [SerializeField] public TextMeshProUGUI countText;                               //text to display number of items
    public TextMeshProUGUI CountText => countText;
    public int count = 1;
    //default number of Item

    private void Awake()
    {
        Init();
    }
    private void Init()
    {
        image.sprite = ingredient.Img;
    }
    
    public Image image;
    public Transform parentAfterDrag;
    // Drag and Drop
    public void OnBeginDrag(PointerEventData eventData)
    {
        image.raycastTarget = false;
        parentAfterDrag = transform.parent;
        transform.SetParent(transform.root);                    //on top of everything
    }
    public void OnDrag(PointerEventData eventData)
    {
        transform.position = Input.mousePosition;
    }
    public void OnEndDrag(PointerEventData eventData)
    {
        Debug.Log("EndDrag");
        image.raycastTarget = true;
        transform.SetParent(parentAfterDrag);                   //return to original parent
    }
}
