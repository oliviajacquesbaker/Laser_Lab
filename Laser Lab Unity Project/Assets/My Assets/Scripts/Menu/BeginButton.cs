using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Text))]
public class BeginButton : MonoBehaviour
{
    Text text;
    public MenuFunctions menu;
    void Start()
    {
        text = GetComponent<Text>();
        if (menu.HasBeetFirstLevel())
            text.text = "CONTINUE";
    }
}
