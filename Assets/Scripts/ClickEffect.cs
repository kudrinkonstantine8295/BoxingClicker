using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ClickEffect : MonoBehaviour
{
    [SerializeField] private TMP_Text _text;
    [SerializeField] private float _deactivateSeconds = 3f;

    private void OnEnable()
    {
        StartCoroutine(Deactivate());
    }

    public void Setup(double value)
    {
        _text.text = Formater.Format(value);
    }

    private IEnumerator Deactivate()
    {
        yield return new WaitForSeconds(_deactivateSeconds);
        ObjectPoolManager.ReturnObjectToPool(gameObject);
    }
}
