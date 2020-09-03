using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using TMPro;

public class TMPRoAnimator : SimpleAnimator
{
    [SerializeField]
    private string[] m_Texts;

    private TextMeshProUGUI _Text;
    private TextMeshPro _Text3D;

    // Start is called before the first frame update
    protected override void Start()
    {
        _Text = GetComponent<TextMeshProUGUI>();
        if(_Text == null)
        {
            _Text3D = GetComponent<TextMeshPro>();
        }

        base.Start();
    }

    public override void ToStateInstant(int state)
    {
        if (state >= m_Texts.Length || (_Text == null && _Text3D == null))
            return;

        if(_Text != null)
        {
            _Text.DOKill();
            _Text.text = m_Texts[state];
        } else
        {
            _Text3D.DOKill();
            _Text3D.text = m_Texts[state];
        }

        base.ResolveCallbacks();
    }

    public override float ToState(int state)
    {
        return ToStateFullCustom(state, m_Duration, 0, m_Ease);
    }

    public override float ToStateDelayed(int state, float delay)
    {
        return ToStateFullCustom(state, m_Duration, delay, m_Ease);
    }

    public override float ToStateFullCustom(int state, float duration, float delay, Ease ease, bool kills = false)
    {
        if (state >= m_Texts.Length || (_Text == null && _Text3D == null))
            return -1;

        if(_Text != null)
        {
            if(kills)
                _tween?.Kill();
            _tween = DOTween.To(() => _Text.text, x => _Text.text = x, m_Texts[state], duration).SetTarget(_Text).SetEase(ease).SetDelay(delay).OnComplete(delegate
            {
                base.ResolveCallbacks();
            });
        } else
        {
            if(kills)
                _tween?.Kill();
            _tween = DOTween.To(() => _Text3D.text, x => _Text3D.text = x, m_Texts[state], duration).SetTarget(_Text3D).SetEase(ease).SetDelay(delay).OnComplete(delegate
            {
                base.ResolveCallbacks();
            });
        }

        return duration;
    }
}
