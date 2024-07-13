using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class EnemyName : MonoBehaviour
{
    [SerializeField] private EnemyManager _enemyManager;
    [SerializeField] private TMP_Text _enemyNameText;

    private void OnEnable()
    {
        _enemyManager.OnCurrentEnemyUpdated.AddListener(ChangeName);
    }

    private void OnDisable()
    {
        _enemyManager.OnCurrentEnemyUpdated.RemoveListener(ChangeName);
    }

    private void ChangeName(EnemyData enemyData)
    {
        _enemyNameText.text = enemyData.Name;
    }
}
