using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(LineRenderer))]
public class VisualLaser : MonoBehaviour
{
    private Laser laser;
    private int length;
    private LineRenderer lineRenderer;

    void Start()
    {
        lineRenderer = GetComponent<LineRenderer>();
    }

    public void SetProperties(Laser laser, int length)
    {
        this.laser = laser;
        this.length = length;
        ReloadRenderer();
    }

    private void ReloadRenderer()
    {
        Vector3 origin = new Vector3(laser.origin.x, 0.5f, laser.origin.y);
        Vector3 end = new Vector3(origin.x, 0.5f, origin.z);

        end = end + laser.Get3DDirectionVector() * length;

        lineRenderer.SetPositions(new Vector3[] { origin, end });
        Vector3 color = new Vector3(laser.red, laser.green, laser.blue);
        lineRenderer.material.SetColor("_EmissiveColor", new Color(color.x, color.y, color.z));

    }
}
