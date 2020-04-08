using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WallObjectLaser : WallObject, ILaserEmitter
{
    public float redOut;
    public float greenOut;
    public float blueOut;
    private Renderer renderer_;


    [PrefabData]
    public TextMesh outTextR;
    [PrefabData]
    public TextMesh outTextG;
    [PrefabData]
    public TextMesh outTextB;

    [PrefabData]
    public GameObject GUI_Objects;

    public WallObjectLaser()
    {
        redOut = greenOut = blueOut = 1;
    }

    public WallObjectLaser(float red, float green, float blue)
    {
        redOut = red;
        greenOut = green;
        blueOut = blue;
    }

    void Start()
    {
        renderer_ = GetComponent<Renderer>();

        outTextR.text = "Red: " + redOut.ToString();
        outTextG.text = "Green: " + greenOut.ToString();
        outTextB.text = "Blue: " + blueOut.ToString();

        GUI_Objects.SetActive(false);

    }
    public override Laser[] OnLaserHit(Laser laser)
    {
        return new Laser[0];
    }

    public Laser[] OnLaserEmit()
    {
        Laser newLaser = new Laser(redOut, greenOut, blueOut);
        Color lightColor = new Color(redOut, greenOut, blueOut, 1);
        renderer_.materials[2].SetColor("_EmissiveColor", lightColor);
        return new Laser[] { newLaser };
    }
    public override void OnHoverEnter()
    {
        base.OnHoverEnter();
        GUI_Objects.SetActive(true);

    }

    public override void OnHoverExit()
    {
        base.OnHoverExit();
        GUI_Objects.SetActive(false);

    }
}
