using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformSpecific : MonoBehaviour
{
    public bool standalone;
    public bool mobile;
    public bool web;

    void Start()
    {

#if UNITY_STANDALONE
        gameObject.SetActive(standalone);
#endif

#if UNITY_IOS
        gameObject.SetActive(mobile);
#endif

#if UNITY_ANDROID
        gameObject.SetActive(mobile);
#endif

#if UNITY_WEBGL
        gameObject.SetActive(web);
#endif

    }
}
