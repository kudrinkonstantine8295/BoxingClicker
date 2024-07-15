using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHealZoneController : MonoBehaviour
{
    [SerializeField] PlayerManager _playerManager;
    [SerializeField] private Transform _switcher;
    [SerializeField] private float _healTime;
    [SerializeField] private Button _healButton;

    private float _timer = 0f;
    private float _heal = 0f;

    private void Start()
    {
        TurnOff();
    }

    private void OnEnable()
    {
        _healButton.onClick.AddListener(Heal);
    }

    private void OnDisable()
    {
        _healButton.onClick.RemoveListener(Heal);
    }

    public void StartHealing()
    {
        _timer = _healTime;
        TurnOn();
    }

    private void TurnOn()
    {
        _switcher.gameObject.SetActive(true);
    }

    private void TurnOff()
    {
        _switcher.gameObject.SetActive(false);
    }

    public void Heal()
    {
        _playerManager.ActivateHeal();
    }

    private void Update()
    {
        if (_timer > 0f)
        {
            _timer -= Time.deltaTime;

            if (_timer <= 0f)
            {
                _timer = 0f;
                TurnOff();
            }
        }
    }
}
