using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class PlayerManager : MonoBehaviour
{
    [SerializeField] private PlayerStats _playerStats;

    public UnityEvent<PlayerStats> OnPlayerStatsChanged;

    private void Start()
    {

    }

    private void SetDamage(float damage)
    {
        _playerStats.Damage = damage; 
    }

    private void IncreaseCritMultiplier(float critMultiplier)
    {
        _playerStats.CritMultiplier += critMultiplier;
    }

    private void IncreaseKnockdownHeal(float knockdownHeal)
    {
        _playerStats.Heal += knockdownHeal;
    }

    private void IncreaseHealth(float health)
    {
        _playerStats.Heal += health;
    }


}
