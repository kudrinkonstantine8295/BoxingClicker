using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
    [SerializeField] private int[] _clicksForLevel;
    [SerializeField] private GameObject[] _models;

    private int _currentLevelIndex;
    private int _clicks;

    private void IncreaseLevel()
    {
        _models[_currentLevelIndex].SetActive(false);
        _currentLevelIndex++;
        _models[_currentLevelIndex].SetActive(true);
    }



    public void AddClick()
    {
        _clicks++;

        if (_clicks>=_clicksForLevel[_currentLevelIndex])
        {
            IncreaseLevel();
            _clicks = 0;
        }
    }



}
