using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChestOpener : MonoBehaviour
{
    [SerializeField] private Animator chest;
    [SerializeField] private bool isOpen;
    private void Start()
    {
        chest.SetTrigger("close");
        isOpen = false;
    }
    private void Update()
    {
        if (Input.GetKeyUp(KeyCode.F))
        {
            if(!isOpen) Open();
        }
    }
    private void Open()
    {
        chest.SetTrigger("open");
        isOpen = true;

    }
}
