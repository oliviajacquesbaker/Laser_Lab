using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;

[RequireComponent(typeof(Button))]
public class SelectorButton : MonoBehaviour
{
    public Image icon;
    private Button button;
    public int id = -1;
    public ObjectListDisplayer displayer;

    void Start()
    {
        button = GetComponent<Button>();
        button.onClick.AddListener(new UnityAction(onClick));
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

    public void SetOrientation(Direction orientation)
    {
        icon.transform.rotation = Quaternion.Euler(0, 0, 90 * (int)orientation);
    }

    private void onClick()
    {
        displayer.OnClickButton(id);
    }
}
