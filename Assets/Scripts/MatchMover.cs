using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;

public class MatchMover : MonoBehaviour
{
    private Vector3 _startPosition;
    private Vector3 _cellPosition;
    private Coroutine _moveControl;

    private void OnEnable()
    {
        Cell.Collisioned += OnGetCollision;
    }

    private void OnDisable()
    {
        Cell.Collisioned -= OnGetCollision;
    }

    private void Start()
    {
        _startPosition = transform.position;
    }

    private void OnMouseDown()
    {
        /*
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit))
        {
            if (hit.collider.TryGetComponent<Cell>(out Cell cell))
            {
                cell.SetCell(false);               
            }
        }
        */
        if (_moveControl != null)
        {
            StopCoroutine(_moveControl);
        }
    }

    private void OnMouseDrag()
    {
        var groundPlane = new Plane(Vector3.up, Vector3.zero);
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

        if (groundPlane.Raycast(ray, out float position))
        {
            Vector3 worldPosition = ray.GetPoint(position);
            transform.position = new Vector3(worldPosition.x, 0.15f, worldPosition.z);
        }
    }

    private void OnMouseUp()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit))
        {
            if (hit.collider.TryGetComponent<Cell>(out Cell cell))
            {
                if (cell.isOccupied == false)
                {
                    CoroutineControl(_cellPosition, transform.position);
                    cell.Occupy(true);
                }
                else
                {
                    CoroutineControl(_startPosition, transform.position);
                }
            }
            else
            {
                CoroutineControl(_startPosition, transform.position);
            }
        }        
    }
    
    private IEnumerator PlaceChanger(Vector3 target, Vector3 currentPosition)
    {
        float recoveryRate = 5;

        while (currentPosition != target)
        {
            currentPosition.x =
                Mathf.MoveTowards(currentPosition.x, target.x, recoveryRate * Time.deltaTime);
            currentPosition.z =
                Mathf.MoveTowards(currentPosition.z, target.z, recoveryRate * Time.deltaTime);
            transform.position = currentPosition;

            yield return null;
        }
    }

    private void OnGetCollision(Vector3 cellPosition)
    {
        _cellPosition = cellPosition;
    }

    public void CoroutineControl(Vector3 target, Vector3 currentPosition)
    {
        if (_moveControl != null)
        {
            StopCoroutine(_moveControl);
        }

        _moveControl = StartCoroutine(PlaceChanger(target, currentPosition));
    }
}
