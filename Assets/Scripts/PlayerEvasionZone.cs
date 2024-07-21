using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public enum Direction
{
    Middle,
    Left,
    Right,
}

public class PlayerEvasionZone : MonoBehaviour
{
    [SerializeField] private Direction _direction;
    [SerializeField] private Button _button;
    [SerializeField] private EvasionZonesController _evasionZonesController;

    public Direction Direction => _direction;

    private void OnEnable()
    {
        _button.onClick.AddListener(Select);
    }

    private void OnDisable()
    {
        _button.onClick.RemoveListener(Select);
    }

    public void Select()
    {
        _evasionZonesController.SetDirection(_direction);
    }
}
