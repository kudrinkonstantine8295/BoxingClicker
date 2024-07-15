using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public enum EvasionZoneType
{
    Left,
    Middle,
    Right,
}

public class PlayerEvasionZone : MonoBehaviour
{
    [SerializeField] private EvasionZoneType _evasionZoneType;
    [SerializeField] private Button _button;
    [SerializeField] private EvasionZonesController _evasionZonesController;

    public EvasionZoneType EvasionZoneType => _evasionZoneType;

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
        _evasionZonesController.SetEvasionZone(_evasionZoneType);
    }
}
