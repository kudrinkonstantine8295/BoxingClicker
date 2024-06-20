using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ClickEffect : MonoBehaviour
{
    [SerializeField] private TMP_Text _text;

    public void Setup(double value)
    {
        _text.text = value.ToString();
    }
}
