using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    [SerializeField] private EnemyManager _enemyManager;
    [SerializeField] private TMP_Text _enemyHealthText;

    private void OnEnable()
    {
        _enemyManager.OnCurrentEnemyHealthChanged.AddListener(ChangeHealth);
    }

    private void OnDisable()
    {
        _enemyManager.OnCurrentEnemyHealthChanged.RemoveListener(ChangeHealth);
    }

    private void ChangeHealth(float health)
    {
        _enemyHealthText.text = health.ToString();
    }
}
