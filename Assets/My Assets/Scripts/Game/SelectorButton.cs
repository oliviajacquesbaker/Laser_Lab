using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using UnityEngine.EventSystems;

[RequireComponent(typeof(Button))]
public class SelectorButton : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public Image icon;
    public Image movability;
    private Button button;
    public int id = -1;
    public ObjectListDisplayer displayer;
    public Sprite Lock, Rotate, Move, MoveRotate;

    void Start()
    {
        button = GetComponent<Button>();
        movability.enabled = false;
    }

    private void Update()
    {
        if (id == displayer.manager.selectedObjectIndex)
        {
            button.interactable = false;
        } else
        {
            button.interactable = true;
        }
    }

    void IPointerEnterHandler.OnPointerEnter(PointerEventData data)
    {
        movability.enabled = true;
    }

    void IPointerExitHandler.OnPointerExit(PointerEventData eventData)
    {
        movability.enabled = false;
    }

    public void SetOrientation(Direction orientation)
    {
        icon.transform.rotation = Quaternion.Euler(0, 0, 90 * (int)orientation);
    }

    public void SetMovability(bool canMove, bool canRotate)
    {
        Sprite selectedTexture = canMove ? (canRotate ? MoveRotate : Move) : canRotate ? Rotate : Lock;
        movability.sprite = selectedTexture;
    }

    public void OnClick()
    {
        //if (button.interactable && displayer.manager.UnplacedObjects[id].CanRotate)
        //{
        //    displayer.manager.UnplacedObjects[id].Rotate();
        //}
        displayer.OnClickButton(id);
    }

}
