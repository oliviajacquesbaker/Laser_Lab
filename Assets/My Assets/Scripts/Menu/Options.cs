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

    bool resetConfirm = false;
    bool resetDone = false;

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
}
