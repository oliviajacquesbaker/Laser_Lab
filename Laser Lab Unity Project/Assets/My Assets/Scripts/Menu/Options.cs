using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Options : MonoBehaviour
{
    [SerializeField]
    GameObject mainMenu;
    [SerializeField]
    GameObject optionsMenu;

    [SerializeField]
    Text resetButtonText;

    [SerializeField]
    Text resolutionButtonText;

    [SerializeField]
    Text fullScreenButtonText;

    bool resetConfirm = false;
    bool resetDone = false;

    int resolutionIndex;

    private void Start()
    {
        for (int i = 0; i < Screen.resolutions.Length; i++)
        {
            if (Screen.resolutions[i].Equals(Screen.currentResolution))
            {
                resolutionIndex = i;
                break;
            }
        }

        resolutionButtonText.text = "RES: " + Screen.currentResolution.width + "x" + Screen.currentResolution.height + "@" + Screen.currentResolution.refreshRate;
        fullScreenButtonText.text = "FULL SCREEN: " + (Screen.fullScreen ? "ON" : "OFF");
        resetButtonText.text = "RESET PROGRESS";
    }

    public void showOptions()
    {
        optionsMenu.SetActive(true);
        mainMenu.SetActive(false);
    }

    public void hideOptions()
    {
        optionsMenu.SetActive(false);
        mainMenu.SetActive(true);

        resetConfirm = false;
        resetDone = false;
        resetButtonText.text = "RESET PROGRESS";
    }

    public void clickReset()
    {
        if (!resetConfirm)
        {
            resetConfirm = true;
            resetButtonText.text = "CONFIRM RESET?";
        } else
        {
            if (!resetDone)
            {
                PlayerPrefs.DeleteAll();
                resetDone = true;
                resetButtonText.text = "RESET COMPLETE";
            }
        }
    }

    public void clickResolution()
    {
        if (Screen.resolutions.Length < 1)
            return;

        resolutionIndex++;

        resolutionIndex %= Screen.resolutions.Length;

        Resolution newRes = Screen.resolutions[resolutionIndex];

        Screen.SetResolution(newRes.width, newRes.height, Screen.fullScreen, newRes.refreshRate);
        resolutionButtonText.text = "RES: " + newRes.width + "x" + newRes.height + "@" + newRes.refreshRate;
    }

    public void clickFullScreen()
    {
        bool newFullScreen = !Screen.fullScreen;
        Screen.fullScreen = newFullScreen;
        fullScreenButtonText.text = "FULL SCREEN: " + (newFullScreen ? "ON" : "OFF");
    }
}
