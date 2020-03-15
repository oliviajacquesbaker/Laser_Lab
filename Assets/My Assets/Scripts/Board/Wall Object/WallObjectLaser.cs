using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WallObjectLaser : WallObject, ILaserEmitter
{
    public float redOut;
    public float greenOut;
    public float blueOut;

    [PrefabData]
    public TextMesh outTextR;
    [PrefabData]
    public TextMesh outTextG;
    [PrefabData]
    public TextMesh outTextB;
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
        outTextR.text = "Red: " + redOut.ToString();
        outTextG.text = "Green: " + greenOut.ToString();
        outTextB.text = "Blue: " + blueOut.ToString();
    }
    public override Laser[] OnLaserHit(Laser laser)
    {
        return new Laser[0];
    }

    public Laser[] OnLaserEmit()
    {
        Laser newLaser = new Laser(redOut, greenOut, blueOut);
        return new Laser[] { newLaser };
    }
    public override void OnHoverEnter()
    {
        base.OnHoverEnter();
        outTextR.gameObject.SetActive(true);
        outTextG.gameObject.SetActive(true);
        outTextB.gameObject.SetActive(true);
    }

    public override void OnHoverExit()
    {
        base.OnHoverExit();
        outTextR.gameObject.SetActive(false);
        outTextG.gameObject.SetActive(false);
        outTextB.gameObject.SetActive(false);
    }
}
