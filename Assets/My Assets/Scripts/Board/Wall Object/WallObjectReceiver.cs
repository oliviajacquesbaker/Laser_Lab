using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
    [PrefabData]
    public TextMesh reqTextR;
    [PrefabData]
    public TextMesh reqTextG;
    [PrefabData]
    public TextMesh reqTextB;


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

        reqTextR.gameObject.SetActive(false);
        reqTextG.gameObject.SetActive(false);
        reqTextB.gameObject.SetActive(false);
    }

    void SetText()
    {
        if(reqType == Requirement.EQUAL)
        {
            reqTextR.text = "Red: " + redVal.ToString() + "=" + redReq.ToString();
            reqTextG.text = "Green: " + greenVal.ToString() + "=" + greenReq.ToString();
            reqTextB.text = "Blue: " + blueVal.ToString() + "=" + blueReq.ToString();
        }
        else if(reqType == Requirement.AT_LEAST)
        {
            reqTextR.text = "Red: " + redVal.ToString() + ">" + redReq.ToString();
            reqTextG.text = "Green: " + greenVal.ToString() + ">" + greenReq.ToString();
            reqTextB.text = "Blue: " + blueVal.ToString() + ">" + blueReq.ToString();
        }
        else if(reqType == Requirement.AT_MOST)
        {
            reqTextR.text = "Red: " + redVal.ToString() + "<" + redReq.ToString();
            reqTextG.text = "Green: " + greenVal.ToString() + "<" + greenReq.ToString();
            reqTextB.text = "Blue: " + blueVal.ToString() + "<" + blueReq.ToString();
        }
    }

    public override Laser[] OnLaserHit(Laser laser)
    {
        redVal += laser.red;
        greenVal += laser.green;
        blueVal += laser.blue;

        SetText();

        if (reqType == Requirement.EQUAL)
        {
            CheckEqual();
        }
        else if (reqType == Requirement.AT_MOST)
        {
            CheckUnder();
        }
        else if (reqType == Requirement.AT_LEAST)
        {
            CheckOver();
        }
        return new Laser[0];
    }

    public override void OnHoverEnter()
    {
        base.OnHoverEnter();
        reqTextR.gameObject.SetActive(true);
        reqTextG.gameObject.SetActive(true);
        reqTextB.gameObject.SetActive(true);
    }

    public override void OnHoverExit()
    {
        base.OnHoverExit();
        reqTextR.gameObject.SetActive(false);
        reqTextG.gameObject.SetActive(false);
        reqTextB.gameObject.SetActive(false);
    }

    public bool IsLaserConditionSatisfied()
    {
        if (reqType == Requirement.EQUAL)
        {
            CheckEqual();
        }
        else if (reqType == Requirement.AT_MOST)
        {
            CheckUnder();
        }
        else if (reqType == Requirement.AT_LEAST)
        {
            CheckOver();
        }

        if (redMet == true && greenMet == true && blueMet == true)
        {
            renderer_.materials[1].SetColor("_EmissiveColor", Color.green);
            return true;
        }
        else
        {
            renderer_.materials[1].SetColor("_EmissiveColor", Color.red);
            return false;
        }
    }

    public void ResetLaserCondition()
    {
        redVal = greenVal = blueVal = 0;
        redMet = greenMet = blueMet = false;
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

    public void CheckEqual()
    {
        double check = 0.0001;
        redMet = (abs(redVal - redReq) < check);
        greenMet = (abs(greenVal - greenReq) < check);
        blueMet = (abs(blueVal - blueReq) < check);
    }

    public void CheckUnder()
    {
        redMet = (redVal <= redReq);
        greenMet = (greenVal <= greenReq);
        blueMet = (blueVal <= blueReq);
    }

    public void CheckOver()
    {
        redMet = (redVal >= redReq);
        greenMet = (greenVal >= greenReq);
        blueMet = (blueVal >= blueReq);
    }


    public enum Requirement
    {
        EQUAL,
        AT_MOST,
        AT_LEAST
    }
}
