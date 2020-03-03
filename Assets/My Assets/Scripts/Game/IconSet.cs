using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[CreateAssetMenu(menuName = "Game/IconSet")]
public class IconSet : ScriptableObject
{
    [SerializeField]
    public List<Icon> icons;

    public Sprite FindIcon(Type type)
    {
        string typename = type.ToString();
        for (int i = 0; i < icons.Count; i++)
        {
            if (typename.Equals(icons[i].ClassName))
            {
                return icons[i].Sprite;
            }
        }

        return null;
    }

    [System.Serializable]
    public class Icon
    {
        public string ClassName;
        public Sprite Sprite;
    }
}
