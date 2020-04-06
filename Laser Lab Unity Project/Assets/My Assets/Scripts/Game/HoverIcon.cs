using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Renderer))]
public class HoverIcon : MonoBehaviour
{
    public Material Lock, MoveOnly, RotateOnly, MoveRotate;
    private new Renderer renderer;

    private bool m_canMove, m_canRotate;

    public bool CanMove { get { return m_canMove; } set { m_canMove = value; ReloadMaterial(); } }
    public bool CanRotate { get { return m_canRotate; } set { m_canRotate = value; ReloadMaterial(); } }

    public void Refresh()
    {
        transform.rotation = Quaternion.Euler(0, 180, 0);
        ReloadMaterial();
    }

    private void ReloadMaterial()
    {
        if (!renderer)
            renderer = GetComponent<Renderer>();
        renderer.sharedMaterial = CanMove ? (CanRotate ? MoveRotate : MoveOnly) : CanRotate ? RotateOnly : Lock;
    }
}
