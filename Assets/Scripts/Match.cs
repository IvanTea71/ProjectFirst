using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using UnityEngine;

public class Match : MonoBehaviour
{
    [SerializeField] private Vector2Int _size = new Vector2Int();
    [SerializeField] private Color _gizmosColor;

    private bool _isCollision;
    private Vector3 _startPosition;
    private Vector3 _cellPosition;
    private Coroutine _moveControl;

    private void Start()
    {
        _isCollision = false;
        _startPosition = transform.position;
    }

    private void OnDrawGizmosSelected()
    {
        for (int x = 0; x < _size.x; x++)
        {
            for (int y = 0; y < _size.y; y++)
            {
                Gizmos.color = _gizmosColor;
                Gizmos.DrawCube(transform.position + new Vector3(x, 0, y), new Vector3(1.1f, 0.05f, 1.1f));
            }
        }
    }

    private void OnMouseDown()
    {
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
            transform.position = new Vector3(worldPosition.x, 0.1f, worldPosition.z);
        }
    }

    private void OnMouseUp()
    {
        if(_isCollision == false)
        {
            CoroutineControl(_startPosition, transform.position);
        }
        else
        {
            CoroutineControl(_cellPosition, transform.position);
        }
    }

    public void SetCollision(bool isCollision)
    {
        _isCollision = isCollision;
    }

    public void SetCellPosition(Vector3 cellPosition)
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

    private IEnumerator PlaceChanger(Vector3 target,Vector3 currentPosition)
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
}