using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WallObjectLaser : WallObject, ILaserEmitter
{
    public float redOut;
    public float greenOut;
    public float blueOut;

    public Text outTextR;
    public Text outTextG;
    public Text outTextB;
    WallObjectLaser()
    {
        redOut = greenOut = blueOut = 1;
        outTextR.text = "Red: 1";
        outTextG.text = "Green: 1";
        outTextB.text = "Blue: 1";
    }

    WallObjectLaser(float red, float green, float blue)
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
