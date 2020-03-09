using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WallObjectReceiver : WallObject, ILaserReceiver
{
    public float redReq;
    public float greenReq;
    public float blueReq;

    private bool redMet = false;
    private bool greenMet = false;
    private bool blueMet = false;

    private float redVal;
    private float greenVal;
    private float blueVal;

    public Requirement reqType;
    public Text reqText;


    public WallObjectReceiver()
    {
        redReq = greenReq = blueReq = 1;
        redVal = greenVal = blueVal = 0;
        reqType = Requirement.EQUAL;
        reqText.text = "Red: 1\nGreen:1\nBlue:1";
    }

    public WallObjectReceiver(int red, int green, int blue, Requirement req)
    {
        redReq = red;
        greenReq = green;
        blueReq = blue;
        reqType = req;
        redVal = greenVal = blueVal = 0;
        reqText.text = "Red: " + red.ToString() + "\nGreen: " + green.ToString() + "\nBlue: " + blue.ToString();
    }

    public override Laser[] OnLaserHit(Laser laser)
    {
        redVal += laser.red;
        greenVal += laser.green;
        blueVal += laser.blue;

        if(reqType == Requirement.EQUAL)
        {
            CheckEqual(laser);
        }
        else if (reqType == Requirement.AT_MOST)
        {
            CheckUnder(laser);
        }
        else if (reqType == Requirement.AT_LEAST)
        {
            CheckOver(laser);
        }
        return new Laser[0];
    }

    public override void OnHoverEnter()
    {
        base.OnHoverEnter();
        reqText.gameObject.SetActive(true);
    }

    public override void OnHoverExit()
    {
        base.OnHoverExit();
        reqText.gameObject.SetActive(true);
    }

    public bool IsLaserConditionSatisfied()
    {
        if (redMet == greenMet == blueMet == true)
        {
            gameObject.GetComponent<Renderer>().materials[1].color = Color.green;
            return true;
        }
        else
        {
            return false;
        }
    }

    public void ResetLaserCondition()
    {
        redVal = greenVal = blueVal = 0;
        redMet = greenMet = blueMet = false;
        gameObject.GetComponent<Renderer>().materials[1].color = Color.red;

    }


    public void CheckEqual(Laser laser)
    {
        if (redVal == redReq)
        {
            redMet = true;
        }
        if (greenVal == greenReq)
        {
            greenMet = true;
        }
        if (blueVal == blueReq)
        {
            blueMet = true;
        }
    }

    public void CheckUnder(Laser laser)
    {
        if (redVal <= redReq)
        {
            redMet = true;
        }
        if (greenVal <= greenReq)
        {
            greenMet = true;
        }
        if (blueVal <= blueReq)
        {
            blueMet = true;
        }
    }

    public void CheckOver(Laser laser)
    {
        if (redVal >= redReq)
        {
            redMet = true;
        }
        if (greenVal >= greenReq)
        {
            greenMet = true;
        }
        if (blueVal >= blueReq)
        {
            blueMet = true;
        }
    }


    public enum Requirement
    {
        EQUAL,
        AT_MOST,
        AT_LEAST
    }
}
