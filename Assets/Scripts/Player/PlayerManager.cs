using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class PlayerManager : MonoBehaviour
{
    [SerializeField] private SaveLoadData _saveLoadStats;
    [SerializeField] private EnemyManager _enemyManager;
    [SerializeField] private PlayerUI _playerUI;
    [SerializeField] private PlayerHealZoneController _playerHealZoneController;
    [SerializeField] private EvasionZonesController _evasionZonesController;

    private float _currentHealth = 0f;
    private string _currentName;

    public UnityEvent<float, float> OnPlayerHealthChanged;
    public UnityEvent<string> OnNameChanged;

    private void OnEnable()
    {
        _enemyManager.OnCurrentEnemyUpdated.AddListener(RestoreHealth);
        _enemyManager.OnCurrentEnemyUpdated.AddListener(UpdateName);
    }

    private void OnDisable()
    {
        _enemyManager.OnCurrentEnemyUpdated.RemoveListener(RestoreHealth);
        _enemyManager.OnCurrentEnemyUpdated.RemoveListener(UpdateName);
    }

    public PunchData MakePunch(PunchZone punchZone)
    {
        return _enemyManager.TakePunch(punchZone, _saveLoadStats);
    }

    public void TakePunch(EvasionZoneType evasionType, List<PlayerEvasionZone> playerEvasionZones)
    {
        var enemyAttackInfo = _enemyManager.AttackPlayer(playerEvasionZones);

        if (enemyAttackInfo.Zone == evasionType)
        {
            _currentHealth -= enemyAttackInfo.Damage;

            if (_currentHealth < 0f)
            {     
                _currentHealth = 0f;
                _playerHealZoneController.StartHealing();
            }

            OnPlayerHealthChanged?.Invoke(_currentHealth, _saveLoadStats.Health);
        }
    }

    public void ActivateHeal()
    {
        if (_currentHealth < _saveLoadStats.Health)
        {
            _currentHealth += _saveLoadStats.Heal;

            if (_currentHealth > _saveLoadStats.Health)
                _currentHealth = _saveLoadStats.Health;

            OnPlayerHealthChanged?.Invoke(_currentHealth, _saveLoadStats.Health);
        }
    }



    private void RestoreHealth(EnemyData enemyData)
    {
        _currentHealth = _saveLoadStats.Health;
        OnPlayerHealthChanged?.Invoke(_currentHealth, _saveLoadStats.Health);
    }

    private void UpdateName(EnemyData enemyData)
    {
        _currentName = _saveLoadStats.Name;
        OnNameChanged?.Invoke(_currentName);
    }

    //private void IncreaseCritMultiplier(float critMultiplier, float money)
    //{
    //    _critMultiplier += critMultiplier;
    //    _money -= money;
    //}

    //private void IncreaseKnockdownHeal(float knockdownHeal, float money)
    //{
    //    _saveLoadStats.Heal += knockdownHeal;
    //    _money -= money;
    //}

    //private void IncreaseHealth(float health, float money)
    //{
    //    _saveLoadStats.Heal += health;
    //    _money -= money;
    //}


}
