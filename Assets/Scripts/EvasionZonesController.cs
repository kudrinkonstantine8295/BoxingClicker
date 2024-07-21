using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class EvasionZonesController : MonoBehaviour
{
    [SerializeField] private Transform _switcher;
    [SerializeField] private PlayerManager _playerManager;
    [SerializeField] private List<PlayerEvasionZone> _playerEvasionZones;

    private Direction _lastDirection = Direction.Middle;

    private void Start()
    {
        TurnOffSelection();
    }

    public void TurnOnSelection()
    {
        _switcher.gameObject.SetActive(true);
    }

    public Direction GetDirection()
    {
        Direction direction = _lastDirection;
        _lastDirection = Direction.Middle;

        return direction;
    }

    public List<PlayerEvasionZone> GetZones()
    {
        return _playerEvasionZones;
    }

    private void TurnOffSelection()
    {
        _switcher.gameObject.SetActive(false);
    }

    public void SetDirection(Direction direction)
    {
        _lastDirection = direction;
        _playerManager.PrepareToTakePunch(_playerEvasionZones);
        TurnOffSelection();
    }
}
