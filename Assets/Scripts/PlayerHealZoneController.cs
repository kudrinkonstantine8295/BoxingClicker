using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHealZoneController : MonoBehaviour
{
    [SerializeField] PlayerManager _playerManager;
    [SerializeField] EnemyManager _enemyManager;
    [SerializeField] EmergencyLight _emergencyLight;
    [SerializeField] private Transform _switcher;
    [SerializeField] private float _healTime;
    [SerializeField] private Button _healButton;

    private float _timer = 0f;

    private void OnEnable()
    {
        _healButton.onClick.AddListener(Heal);
    }

    private void OnDisable()
    {
        _healButton.onClick.RemoveListener(Heal);
    }

    private void Start()
    {
        TurnOff();
    }

    public void StartHealing()
    {
        _timer = _healTime;
        TurnOn();
    }

    private void TurnOn()
    {
        _switcher.gameObject.SetActive(true);
        _enemyManager.ChangeInteractionStatus(false);
        _emergencyLight.Stop();
    }

    public void TurnOff()
    {
        _switcher.gameObject.SetActive(false);
        _enemyManager.ChangeInteractionStatus(true);
        _emergencyLight.Resume();
    }

    public void Heal()
    {
        _playerManager.ActivateHeal();
    }

    public void UpdateTimer()
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

    private void Update()
    {
        UpdateTimer();
    }
}
