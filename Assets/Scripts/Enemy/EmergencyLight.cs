using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EmergencyLight : MonoBehaviour
{
    [SerializeField] private float _enableTime = 60f;
    [SerializeField] private float _enableTimeSpread = 10f;
    [SerializeField] private Button _emergencyLightButton;

    private float _timer = 0f;

    private void TurnOn()
    {

    }







    private void Update()
    {
        if (_timer != 0f)
        {
            _timer -= Time.deltaTime;

            if (_timer < 0f)
                _timer = 0f;


        }

    }


}
