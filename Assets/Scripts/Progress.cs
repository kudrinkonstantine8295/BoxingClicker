using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Progress : MonoBehaviour
{
    [SerializeField] private float _numberOfCoins;
    [SerializeField] private float _coinsPreClick;
    [SerializeField] private float _coinsPerSecond;
    [SerializeField] private TextMeshProUGUI _coinsText;
    [SerializeField] private TextMeshProUGUI _perSecondText;

    private float _timer;

    private void Update()
    {
        _timer += Time.deltaTime;

        if(_timer>1f)
        {
            _timer = 0f;
            _numberOfCoins += _coinsPerSecond;
            UpdateCoinsText();
        }
    }

    public void AddClick()
    {
        _numberOfCoins += _coinsPerSecond;
        UpdateCoinsText();
    }

    private void UpdateCoinsText()
    {
        _coinsText.text = _numberOfCoins.ToString();
    }
}
