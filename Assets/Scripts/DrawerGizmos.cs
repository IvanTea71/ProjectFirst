using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DrawerGizmos : MonoBehaviour
{
    [SerializeField] private Vector2Int _size = new Vector2Int();
    [SerializeField] private Color _gizmosColor;

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
}
