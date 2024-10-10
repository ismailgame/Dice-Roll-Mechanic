using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FrameRate : MonoBehaviour
{
    [SerializeField] private int rate; //Max valu for fps
    private void Awake()
    {
        //Set the target frame rate
        Application.targetFrameRate = rate;
    }
}
