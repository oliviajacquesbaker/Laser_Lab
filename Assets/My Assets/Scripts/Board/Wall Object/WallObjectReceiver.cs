using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WallObjectReceiver : WallObject, ILaserReceiver
{
    private float redReq;
    private float greenReq;
    private float blueReq;

    private bool redMet = false;
    private bool greenMet = false;
    private bool blueMet = false;

    private float redVal;
    private float greenVal;
    private float blueVal;

    public Requirement reqType;

    public WallObjectReceiver()
    {
        redReq = greenReq = blueReq = 1;
        redVal = greenVal = blueVal = 0;
        reqType = Requirement.EQUAL;
    }

    public WallObjectReceiver(int red, int green, int blue, Requirement req)
    {
        redReq = red;
        greenReq = green;
        blueReq = blue;
        reqType = req;
        redVal = greenVal = blueVal = 0;
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


    public bool IsLaserConditionSatisfied()
    {
        if (redMet == greenMet == blueMet == true)
        {
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
