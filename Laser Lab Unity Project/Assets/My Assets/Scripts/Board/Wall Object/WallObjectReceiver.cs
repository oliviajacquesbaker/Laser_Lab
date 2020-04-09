using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WallObjectReceiver : WallObject, ILaserReceiver
{
    public float redReq;
    public float greenReq;
    public float blueReq;

    private float redVal;
    private float greenVal;
    private float blueVal;

    public Requirement reqType;
    [PrefabData]
    public TextMesh reqTextR;
    [PrefabData]
    public TextMesh reqTextG;
    [PrefabData]
    public TextMesh reqTextB;

    [PrefabData]
    public GameObject GUI_Objects;


    private Renderer renderer_;


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

    void Start()
    {
        SetText();
        renderer_ = GetComponent<Renderer>();

        GUI_Objects.SetActive(false);
    }

    void SetText()
    {
        float tempr = (float)(0.5 * (int)System.Math.Round(redVal / 0.5));
        float tempg = (float)(0.5 * (int)System.Math.Round(greenVal / 0.5));
        float tempb = (float)(0.5 * (int)System.Math.Round(blueVal / 0.5));
        if (reqType == Requirement.EQUAL)
        {
            reqTextR.text = "Red: " + tempr.ToString() + "=" + redReq.ToString();
            reqTextG.text = "Green: " + tempg.ToString() + "=" + greenReq.ToString();
            reqTextB.text = "Blue: " + tempb.ToString() + "=" + blueReq.ToString();
        }
        else if(reqType == Requirement.AT_LEAST)
        {
            reqTextR.text = "Red: " + tempr.ToString() + ">" + redReq.ToString();
            reqTextG.text = "Green: " + tempg.ToString() + ">" + greenReq.ToString();
            reqTextB.text = "Blue: " + tempb.ToString() + ">" + blueReq.ToString();
        }
        else if(reqType == Requirement.AT_MOST)
        {
            reqTextR.text = "Red: " + tempr.ToString() + "<" + redReq.ToString();
            reqTextG.text = "Green: " + tempg.ToString() + "<" + greenReq.ToString();
            reqTextB.text = "Blue: " + tempb.ToString() + "<" + blueReq.ToString();
        }
    }

    public override Laser[] OnLaserHit(Laser laser)
    {
        redVal += laser.red;
        greenVal += laser.green;
        blueVal += laser.blue;

        SetText();
        
        return new Laser[0];
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

    public bool IsLaserConditionSatisfied()
    {
        bool check = false;
        if (reqType == Requirement.EQUAL)
        {
            check = CheckEqual();
        }
        else if (reqType == Requirement.AT_MOST)
        {
            check = CheckUnder();
        }
        else if (reqType == Requirement.AT_LEAST)
        {
            check = CheckOver();
        }

        if (check)
        {
            renderer_.materials[1].SetColor("_EmissionColor", Color.green);
            return true;
        }
        else
        {
            renderer_.materials[1].SetColor("_EmissionColor", Color.red);
            return false;
        }
    }

    public void ResetLaserCondition()
    {
        redVal = greenVal = blueVal = 0;
        renderer_.materials[1].SetColor("_EmissionColor", Color.red);
        SetText();

    }

    public double abs(double val)
    {
        if (val < 0)
        {
            return val * -1;
        }
        else
        {
            return val;
        }
    }

    public bool CheckEqual()
    {
        float tempr = (float)(0.5 * (int)System.Math.Round(redVal / 0.5));
        float tempg = (float)(0.5 * (int)System.Math.Round(greenVal / 0.5));
        float tempb = (float)(0.5 * (int)System.Math.Round(blueVal/ 0.5));
        double check = 0.0001;
        bool redMet = (abs(tempr - redReq) < check);
        bool greenMet = (abs(tempg - greenReq) < check);
        bool blueMet = (abs(tempb - blueReq) < check);
        return (redMet == true && greenMet == true && blueMet == true);
    }

    public bool CheckUnder()
    {
        float tempr = (float)(0.5 * (int)System.Math.Round(redVal / 0.5));
        float tempg = (float)(0.5 * (int)System.Math.Round(greenVal / 0.5));
        float tempb = (float)(0.5 * (int)System.Math.Round(blueVal / 0.5));
        bool redMet = (tempr < redReq);
        bool greenMet = (tempg < greenReq);
        bool blueMet = (tempb < blueReq);
        return (redMet == true && greenMet == true && blueMet == true);
    }

    public bool CheckOver()
    {
        float tempr = (float)(0.5 * (int)System.Math.Round(redVal / 0.5));
        float tempg = (float)(0.5 * (int)System.Math.Round(greenVal / 0.5));
        float tempb = (float)(0.5 * (int)System.Math.Round(blueVal / 0.5));
        bool redMet = (tempr > redReq);
        bool greenMet = (tempg > greenReq);
        bool blueMet = (tempb > blueReq);
        if (tempr == 0 && redReq == 0)
        {
            redMet = true;
        }
        if(tempg == 0 && greenReq == 0)
        {
            greenMet = true;
        }
        if(tempb == 0 && blueReq == 0)
        {
            blueMet = true;
        }
        return (redMet == true && greenMet == true && blueMet == true);
    }


    public enum Requirement
    {
        EQUAL,
        AT_MOST,
        AT_LEAST
    }
}
