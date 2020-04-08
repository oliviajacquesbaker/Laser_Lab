using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LockRotation : MonoBehaviour
{
    private static Quaternion startPos;

    private void Awake()
    {
        if (startPos == null)
            startPos = Quaternion.Euler(0, 0, 0);

        transform.rotation = Quaternion.Euler(0, 0, 0);
    }

    private void Update()
    {
        transform.rotation = Quaternion.Euler(0, 0, 0);
    }
}
