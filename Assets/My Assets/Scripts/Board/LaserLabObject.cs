using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public abstract class LaserLabObject : MonoBehaviour
{
    public virtual void OnHoverEnter() { }
    public virtual void OnHoverExit() { }
}
