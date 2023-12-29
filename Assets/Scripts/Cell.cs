using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Cell : MonoBehaviour
{
    public bool IsOccupied { get; private set; }

    private void Start()
    {
        IsOccupied = false;
    }

    public void Occupy(bool status)
    {
        IsOccupied = status;
    }
}
