using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Zona : MonoBehaviour
{
    [SerializeField] private Color _baseColor, _greenColor, _yellowColor, _redColor;
    [SerializeField] private SpriteRenderer _renderer;

    public void Init(int color) {
        if (color == 0) {
            _renderer.color = _baseColor;
        }
        if (color == 1)
        {
            _renderer.color = _greenColor;
        }
        if (color == 2)
        {
            _renderer.color = _yellowColor;
        }
        if (color == 3)
        {
            _renderer.color = _redColor;
        }
    }
}
