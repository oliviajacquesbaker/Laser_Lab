using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public abstract class LaserLabObject : MonoBehaviour
{
    public virtual void OnHoverEnter() { }
    public virtual void OnHoverExit() { }
    public Vector2Int getPos()
    {
        return new Vector2Int(Mathf.RoundToInt(transform.position.x), Mathf.RoundToInt(transform.position.z));
    }
}
