using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Image))]
public class AnimatedImage : MonoBehaviour
{
    public AnimatedSprite animatedSprite;
    private Image m_image;

    private float FrameTimer;
    private float FrameLength { get { return 1f / animatedSprite.framerate; } }
    private int FrameIndex = 0;

    void Start()
    {
        FrameIndex = 0;
        m_image = GetComponent<Image>();
        if (animatedSprite != null)
            m_image.sprite = animatedSprite.frames[FrameIndex];
        else
        {
            m_image.sprite = null;
        }
    }

    void Update()
    {
        if (animatedSprite != null)
        {
            if (FrameTimer < 0)
            {
                int increment = 0;
                while (FrameTimer < 0)
                {
                    FrameTimer += FrameLength;
                    increment++;
                }

                FrameIndex += increment;
                FrameIndex = FrameIndex % animatedSprite.frames.Length;

                m_image.sprite = animatedSprite.frames[FrameIndex];
            }

            FrameTimer -= Time.deltaTime;
        }
    }
}
