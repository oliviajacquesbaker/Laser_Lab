using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WallObjectWallReciever : WallObject, ILaserReciever
{
    private float redReq;
    private float greenReq;
    private float blueReq;

    public bool redMet = false;
    public bool greenMet = false;
    public bool blueMet = false;

    public WallObjectWallReciever()
    {
        redReq = greenReq = blueReq = 1;
    }

    public WallObjectWallReciever(int red, int green, int blue)
    {
        redReq = red;
        greenReq = green;
        blueReq = blue;
    }

    public override Laser[] OnLaserHit(Laser laser)
    {
        if(laser.red == redReq)
        {
            redMet = true;
        }
        if(laser.green == greenReq)
        {
            greenMet = true;
        }
        if(laser.blue == blueReq)
        {
            blueMet = true;
        }
        return new Laser[0];
    }

    public void Reset()
    {
        redMet = greenMet = blueMet = false;
    }

    public bool IsLaserConditionSatisfied()
    {
        throw new System.NotImplementedException();
    }

    public void ResetLaserCondition()
    {
        throw new System.NotImplementedException();
    }
}
