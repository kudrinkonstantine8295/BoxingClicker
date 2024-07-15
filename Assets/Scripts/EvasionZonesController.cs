using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EvasionZonesController : MonoBehaviour
{
    [SerializeField] private Transform _switcher;
    [SerializeField] private PlayerManager _playerManager;
    [SerializeField] private List<PlayerEvasionZone> _playerEvasionZones;

    private void Start()
    {
        TurnOffSelection();
    }

    public void TurnOnSelection()
    {
        _switcher.gameObject.SetActive(true);
    }

    private void TurnOffSelection()
    {
        _switcher.gameObject.SetActive(false);
    }

    public void SetEvasionZone(EvasionZoneType evasionZone)
    {
        _playerManager.TakePunch(evasionZone, _playerEvasionZones);
        TurnOffSelection();
    }
}
