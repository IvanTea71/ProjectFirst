using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColorChanger : MonoBehaviour
{
    [SerializeField] private Color _endColor;

    private Renderer _colorRenderer;
    private Color _startColor;
    private Transform _cellTransform;

    private void Start()
    {
        _cellTransform = GetComponent<Transform>();
        _colorRenderer = GetComponent<Renderer>();
        _startColor = _colorRenderer.material.color;
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.TryGetComponent<Match>(out Match match))
        {
            _colorRenderer.material.color = _endColor;
            match.SetCollision(true);
            match.SetCellPosition(_cellTransform.transform.position);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.TryGetComponent<Match>(out Match match))
        {
            _colorRenderer.material.color = _startColor;
            match.SetCollision(false);
        }
    }
}
