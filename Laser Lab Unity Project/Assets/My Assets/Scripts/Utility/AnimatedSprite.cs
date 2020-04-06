using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Sprites/Animated Sprite")]
public class AnimatedSprite : ScriptableObject
{
    public float framerate;
    public Sprite[] frames;
}
