using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GateOpener : MonoBehaviour
{
    public GameObject OpenGateObj;
    public GameObject ClosedGateObj;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            OpenGateObj.SetActive(true);
            ClosedGateObj.SetActive(false);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            OpenGateObj.SetActive(false);
            ClosedGateObj.SetActive(true);
        }
    }
}
