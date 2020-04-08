using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TutorialBubble : MonoBehaviour
{
    public Text title;
    public Text information;
    public AnimatedImage image;

    public void loadMessage(in TutorialMessage message)
    {
        title.text = message.Title;
        information.text = message.Message;
        image.animatedSprite = message.image;
    }
}
