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

    public Requirement reqType;

    public WallObjectReceiver()
    {
        redReq = greenReq = blueReq = 1;
        reqType = Requirement.EQUAL;
    }

    public WallObjectReceiver(int red, int green, int blue, Requirement req)
    {
        redReq = red;
        greenReq = green;
        blueReq = blue;
        reqType = req;
    }

    public override Laser[] OnLaserHit(Laser laser)
    {
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
        redMet = greenMet = blueMet = false;
    }

    public void CheckEqual(Laser laser)
    {
        if (laser.red == redReq)
        {
            redMet = true;
        }
        if (laser.green == greenReq)
        {
            greenMet = true;
        }
        if (laser.blue == blueReq)
        {
            blueMet = true;
        }
    }

    public void CheckUnder(Laser laser)
    {
        if (laser.red <= redReq)
        {
            redMet = true;
        }
        if (laser.green <= greenReq)
        {
            greenMet = true;
        }
        if (laser.blue <= blueReq)
        {
            blueMet = true;
        }
    }

    public void CheckOver(Laser laser)
    {
        if (laser.red >= redReq)
        {
            redMet = true;
        }
        if (laser.green >= greenReq)
        {
            greenMet = true;
        }
        if (laser.blue >= blueReq)
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
