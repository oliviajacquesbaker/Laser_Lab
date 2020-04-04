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

    private void Start()
    {
        resolutionButtonText.text = "RES: " + Screen.currentResolution.width + " x " + Screen.currentResolution.height;
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
        int currentIndex = -1;
        for (int i = 0; i < Screen.resolutions.Length; i++)
        {
            if (Screen.resolutions[i].Equals(Screen.currentResolution))
            {
                currentIndex = i;
                break;
            }
        }
        currentIndex++;

        Resolution newRes = Screen.resolutions[currentIndex];

        Screen.SetResolution(newRes.width, newRes.height, Screen.fullScreen);
        resolutionButtonText.text = "Res: " + Screen.currentResolution.width + " x " + Screen.currentResolution.height;
    }

    public void clickFullScreen()
    {
        Screen.fullScreen = !Screen.fullScreen;
        fullScreenButtonText.text = "FULL SCREEN: " + (Screen.fullScreen ? "ON" : "OFF");
    }
}
