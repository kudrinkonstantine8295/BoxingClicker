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
    [SerializeField] private ComboBonus _comboBonus;

    private float _currentHealth = 0f;
    private string _currentName;

    public UnityEvent<float, float> OnPlayerHealthChanged;
    public UnityEvent<string> OnNameChanged;

    private void OnEnable()
    {
        _enemyManager.OnCurrentEnemyUpdated.AddListener(RestoreHealth);
        _enemyManager.OnCurrentEnemyUpdated.AddListener(UpdateName);
        _enemyManager.OnEnemyPunched.AddListener(TakePunch);
        _playerHealZoneController.TurnOff();
    }

    private void OnDisable()
    {
        _enemyManager.OnCurrentEnemyUpdated.RemoveListener(RestoreHealth);
        _enemyManager.OnCurrentEnemyUpdated.RemoveListener(UpdateName);
        _enemyManager.OnEnemyPunched.RemoveListener(TakePunch);
    }

    public PunchData AttackEnemy(PunchZone punchZone)
    {
        float bonusMultiplierDamage = _comboBonus.GetBonusMultiplierDamage();
        _comboBonus.RefreshBonus(punchZone);

        return _enemyManager.TakePunch(punchZone, _saveLoadStats, bonusMultiplierDamage);
    }

    public void PrepareToTakePunch(List<PlayerEvasionZone> playerEvasionZones)
    {
        _enemyManager.Attack(playerEvasionZones);
    }

    public void PrepareToTakePunch()
    {
        _enemyManager.Attack();
    }

    public void TakePunch()
    {
        TakeDamage(_evasionZonesController.GetDirection());
    }

    public bool TakeDamage(Direction evationZone)
    {
        bool isMissed = true;

        var enemyAttackInfo = _enemyManager.AttackPlayer();

        if (enemyAttackInfo.Zone == evationZone)
        {
            isMissed = false;
            _currentHealth -= enemyAttackInfo.Damage;

            if (_currentHealth <= 0f)
            {
                _currentHealth = 0f;
                _playerHealZoneController.StartHealing();
            }

            OnPlayerHealthChanged?.Invoke(_currentHealth, _saveLoadStats.Health);
        }

        return isMissed;
    }

    public void ActivateEvasion()
    {
        _evasionZonesController.TurnOnSelection();
        _enemyManager.PlayPrepareAttackAnimation();
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
