using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class PlayerManager : MonoBehaviour
{
    [SerializeField] private SaveLoadData _playerStats;
    [SerializeField] private EnemyManager _enemyManager;


    private float _minDamage;
    private float _maxDamage;
    private float _money;
    private float _critMultiplier;
    private float _heal;
    private float _health;
    //private int _index = 0;
    //private int _gameCompletedTimes = 0;

    public float Damage => _minDamage;

    public UnityEvent<float> OnPlayerHealthChanged;

    private void Start()
    {
        _minDamage = _playerStats.MinDamage;
        _maxDamage = _playerStats.MaxDamage;
        _money = _playerStats.Money;
        _critMultiplier = _playerStats.CritMultiplier;
        _heal = _playerStats.Heal;
        _health = _playerStats.Health;
    }

    public PunchData MakePunch(PunchZone punchZone)
    {

        return _enemyManager.TakePunch(punchZone, _playerStats);
    }

    private void IncreaseCritMultiplier(float critMultiplier, float money)
    {
        _critMultiplier += critMultiplier;
        _money -= money;
    }

    private void IncreaseKnockdownHeal(float knockdownHeal, float money)
    {
        _playerStats.Heal += knockdownHeal;
        _money -= money;
    }

    private void IncreaseHealth(float health, float money)
    {
        _playerStats.Heal += health;
        _money -= money;
    }


}
