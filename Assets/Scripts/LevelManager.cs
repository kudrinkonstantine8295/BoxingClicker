using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class LevelManager : MonoBehaviour
{
    [SerializeField] private int[] _clicksForLevel;
    [SerializeField] private GameObject[] _models;
    [SerializeField] private Image _scaleImage;
    [SerializeField] private TMP_Text _levelText;

    private int _currentLevelIndex;
    private int _clicks;

    private void Start()
    {
        _scaleImage.fillAmount = (float)_clicks / _clicksForLevel[_currentLevelIndex];
        _levelText.text = "Уровень" + _currentLevelIndex + 1;
    }

    private void IncreaseLevel()
    {
        _models[_currentLevelIndex].SetActive(false);
        _currentLevelIndex++;
        _models[_currentLevelIndex].SetActive(true);
        _levelText.text = "Уровень" + _currentLevelIndex + 1;
    }



    public void AddClick()
    {
        _clicks++;

        if (_clicks >= _clicksForLevel[_currentLevelIndex])
        {
            IncreaseLevel();
            _clicks = 0;
        }

        _scaleImage.fillAmount = (float)_clicks / _clicksForLevel[_currentLevelIndex];
    }



}
