using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WallObjectLaser : WallObject, ILaserEmitter
{
    public float redOut;
    public float greenOut;
    public float blueOut;

    public Text outText;
    WallObjectLaser()
    {
        redOut = greenOut = blueOut = 1;
        outText.text = "Red: 1\nGreen:1\nBlue:1";
    }

    WallObjectLaser(float red, float green, float blue)
    {
        redOut = red;
        greenOut = green;
        blueOut = blue;
    }

    void Start()
    {
        outText.text = "Red: " + redOut.ToString() + "\nGreen: " + greenOut.ToString() + "\nBlue: " + blueOut.ToString();
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
        outText.gameObject.SetActive(true);
    }

    public override void OnHoverExit()
    {
        base.OnHoverExit();
        outText.gameObject.SetActive(false);
    }
}
