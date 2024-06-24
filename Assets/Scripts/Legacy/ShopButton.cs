using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public enum IncomeOption
{
    PerClick,
    PerSecond
}

public class ShopButton : MonoBehaviour
{
    [SerializeField] private string _name;
    [SerializeField] private double _price;
    [SerializeField] private double _income;
    [SerializeField] private IncomeOption _incomeOption;
    [SerializeField] private Sprite _sprite;
    [Space(5)]
    [SerializeField] private TMP_Text _nameText;
    [SerializeField] private TMP_Text _descriptionText;
    [SerializeField] private TMP_Text _priceText;
    [SerializeField] private Button _button;
    [SerializeField] private Image _image;
    [SerializeField] private Progress _progress;

    private void Awake()
    {
        _progress.OnCoinsChanged += UpdateButton;

    }

    private void OnDisable()
    {
        _progress.OnCoinsChanged -= UpdateButton;  
        _button.onClick.RemoveListener(Buy);
    }

    private void UpdateButton(double coins)
    {
        if (_price <= coins)
        {
            _button.interactable = true;
        }
        else
        {
            _button.interactable = false;
        }
    }

    private void Start()
    {
        _button.onClick.AddListener(Buy);
    }

    private void OnValidate()
    {
        _nameText.text = _name;

        if (_incomeOption == IncomeOption.PerClick)
        {
            _descriptionText.text = $"+{Formater.Format(_income)} за клик";
        }
        else if (_incomeOption == IncomeOption.PerSecond)
        {
            _descriptionText.text = $"+ {Formater.Format(_income)} в секунду";
        }

        _priceText.text = Formater.Format(_price);
        _image.sprite = _sprite;
    }

    public void Buy()
    {
        if (_incomeOption == IncomeOption.PerClick)
        {
            _progress.AddCoinsPerClick(_income, _price);
            
        }
        else
        {
            _progress.AddCoinsPerSecond(_income, _price);
        }
    }
}
