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

    private void onClick()
    {
        displayer.OnClickButton(id);
    }
}
