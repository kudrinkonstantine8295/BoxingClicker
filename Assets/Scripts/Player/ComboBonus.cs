using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;
using System.Runtime.CompilerServices;

public class ComboBonus : MonoBehaviour
{
    [SerializeField] private float _minDamageMultipliier = 1f;
    [SerializeField] private float _maxDamageMultiplier = 2.8f;
    [SerializeField, Range(0f, 1f)] private float _bonusIncreasePower = 0.01f;
    [SerializeField, Range(0f, 1f)] private float _powerSliderDecreaseSpeed = 0.01f;
    [SerializeField, Range(0f, 1f)] private float _multiplierSliderDecreaseSpeed = 0.005f;

    [SerializeField] private TMP_Text _bonusText;
    [SerializeField] private Slider _powerSlider;
    [SerializeField] private Image _bonusPowerKnob;
    [SerializeField] private Slider _multiplierSlider;
    [SerializeField] private AnimationCurve _relativeIncreasePoints;

    private float _totalPoints = 0f;
    private const float _maxPoints = 100f;
    private float _totalDamageMultiplier = 0f;
    private List<PunchZone> _zones = new();

    public void Start()
    {
        ResetSliders();
    }

    public float GetBonusMultiplierDamage()
    {
        return _totalDamageMultiplier;
    }

    public void RefreshBonus(PunchZone punchZone)
    {
        if (_zones.Contains(punchZone) == false)
            _zones.Add(punchZone);

        float relativePointsPerZone = 0f;

        if (punchZone.Points > 0f)
            relativePointsPerZone = punchZone.Points / _maxPoints;

        punchZone.AddPoints(_maxPoints * _bonusIncreasePower * _relativeIncreasePoints.Evaluate(relativePointsPerZone));
    }

    private void ResetSliders()
    {
        _multiplierSlider.value = 0f;
        _powerSlider.value = 0f;
    }

    private void DecreaseZonePoints(PunchZone punchZone)
    {
        punchZone.RemovePoints(Time.deltaTime * (_maxPoints * _powerSliderDecreaseSpeed));
    }

    private void DecreaseAllZonesPoints()
    {
        foreach (var zone in _zones)
        {
            DecreaseZonePoints(zone);
        }
    }

    private void RefreshMultiplier()
    {
        _totalDamageMultiplier = Mathf.Round(Mathf.Lerp(_minDamageMultipliier, _maxDamageMultiplier, _multiplierSlider.value) * 10.0f) * 0.1f;
    }

    private void RenderMultiplier()
    {
        _bonusText.text = $"x{_totalDamageMultiplier}";
    }

    private void RefreshSliders()
    {
        _totalPoints = 0f;

        foreach (var zone in _zones)
        {
            _totalPoints += zone.Points;
        }

        _powerSlider.value = _totalPoints / _maxPoints;
        _bonusPowerKnob.fillAmount = _totalPoints / _maxPoints;

        if (_powerSlider.value > _multiplierSlider.value)
            _multiplierSlider.value = _powerSlider.value;
    }

    private void DecreaseMultiplierSlider()
    {
        _multiplierSlider.value -= Time.deltaTime * _multiplierSliderDecreaseSpeed;
    }

    private void Update()
    {
        RefreshMultiplier();
        DecreaseAllZonesPoints();
        DecreaseMultiplierSlider();
        RefreshSliders();
        RenderMultiplier();

    }
}
