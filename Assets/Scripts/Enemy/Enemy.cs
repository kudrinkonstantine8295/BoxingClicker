using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] private List<SpriteRenderer> _parts;
    [SerializeField] private float _highlightSeconds;
    [SerializeField] private Color _highlightColor;
    [SerializeField] private Animator _animator;

    private Color _savedColor;
    private Tween _tween;

    private void SetColor(SpriteRenderer part, Color color)
    {
        part.color = color;
    }

    private void TriggerShowing()
    {
        if(_animator != null)
        {
            _animator.SetTrigger("Showing");
        }
    }

    private void ChangePartsColorToBlack()
    {
        foreach (var part in _parts)
        {
            SetColor(part, Color.black);
        }
    }

    private void HighlightSprite(SpriteRenderer part)
    {
        _tween.Kill();

        if (_savedColor == null)
            _savedColor = part.color;

        SetColor(part, _highlightColor);
        part.DOColor(_savedColor, _highlightSeconds);
    }
}
