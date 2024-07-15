using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBarSlider : MonoBehaviour
{
    [SerializeField] private Slider _healthSlider;
    [SerializeField] private Slider _smoothSlider;
    [SerializeField] private float _smoothSpeed;

    public void ChangeHealthSliderPosition(float health, float maxHealth)
    {
        _healthSlider.value = health / maxHealth;
    }

    private void TryChangeSmoothSliderPosition()
    {
        if (_smoothSlider.value != _healthSlider.value)
        {
            if (_healthSlider.value < _healthSlider.value)
                _healthSlider.value = _healthSlider.value;
            else
            {
                _smoothSlider.value = Mathf.Lerp(_smoothSlider.value, _healthSlider.value, Time.deltaTime * _smoothSpeed);
            }
        }
    }

    private void Update()
    {
        TryChangeSmoothSliderPosition();
    }
}
