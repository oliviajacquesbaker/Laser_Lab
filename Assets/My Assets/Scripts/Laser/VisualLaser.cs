using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(LineRenderer))]
public class VisualLaser : MonoBehaviour
{
    private Laser laser;
    private LineRenderer lineRenderer;

    public void SetProperties(Laser laser)
    {
        this.laser = laser;
        ReloadRenderer();
    }

    private void ReloadRenderer()
    {
        Vector3 origin = new Vector3(laser.origin.x, 0.5f, laser.origin.y);
        Vector3 end = new Vector3(origin.x, 0.5f, origin.z);

        end = end + laser.Get3DDirectionVector() * laser.length;

        if (!lineRenderer)
            lineRenderer = GetComponent<LineRenderer>();

        lineRenderer.SetPositions(new Vector3[] { origin, end });
        Vector3 color = new Vector3(laser.red * 2, laser.green * 2, laser.blue * 2);
        lineRenderer.material.SetColor("_EmissiveColor", new Color(color.x, color.y, color.z));

    }
}
