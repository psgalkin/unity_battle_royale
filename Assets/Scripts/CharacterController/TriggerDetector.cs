using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerDetector : MonoBehaviour
{
    public bool IsInTrigger { private set; get; }

    private void OnTriggerEnter(Collider other)
    {
        IsInTrigger = true;
    }

    private void OnTriggerExit(Collider other)
    {
        IsInTrigger = false;
    }
}
