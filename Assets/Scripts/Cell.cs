using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Cell : MonoBehaviour
{
    private Transform _cellTransform;

    public bool isOccupied { get; private set; }

    public static event UnityAction<Vector3> Collisioned;

    private void Start()
    {
        isOccupied = false;
        _cellTransform = GetComponent<Transform>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent<Match>(out Match match))
        {
            Collisioned?.Invoke(_cellTransform.position);
        }
    }

    public void Occupy(bool status)
    {
        isOccupied = status;
    }
}
