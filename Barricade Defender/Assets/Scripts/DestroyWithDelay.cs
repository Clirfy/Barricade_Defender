using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyWithDelay : MonoBehaviour
{
    public float DelayTimer;

    private void Start()
    {
        Destroy(gameObject, DelayTimer);
    }
}
