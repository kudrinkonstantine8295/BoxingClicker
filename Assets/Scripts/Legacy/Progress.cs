using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Progress : MonoBehaviour
{
    [SerializeField] private double _coinsPerClick;
    [SerializeField] private TextMeshProUGUI _coinsText;
    [SerializeField] private TextMeshProUGUI _perSecondText;

    private double _numberOfCoins = 0f;
    private double _coinsPerSecond = 0f;
    private float _timer = 0f;

    public event Action<double> OnCoinsChanged;
    public double CoinsPerCLick => _coinsPerClick;

    private void Start()
    {
        UpdateCoinsText();
        UpdatePerSecondText();
    }

    private void Update()
    {
        _timer += Time.deltaTime;

        if (_timer > 1f)
        {
            _timer = 0f;
            _numberOfCoins += _coinsPerSecond;
            UpdateCoinsText();
        }
    }

    public void AddClick()
    {
        _numberOfCoins += _coinsPerClick;
        UpdateCoinsText();
    }

    private void UpdateCoinsText()
    {
        _coinsText.text = Formater.Format(_numberOfCoins);
        OnCoinsChanged?.Invoke(_numberOfCoins);
    }

    private void UpdatePerSecondText()
    {
        _perSecondText.text = Formater.Format(_coinsPerSecond);
    }

    public void AddCoinsPerClick(double value, double price)
    {
        if (_numberOfCoins < price)
            return;

        _coinsPerClick += value;
        _numberOfCoins -= price;
        UpdateCoinsText();
    }

    public void AddCoinsPerSecond(double value, double price)
    {
        if (_numberOfCoins < price)
            return;

        _coinsPerSecond += value;
        UpdatePerSecondText();
        _numberOfCoins -= price;
        UpdateCoinsText();
    }
}
