using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

public class CanvasFaderAnimator : SimpleAnimator
{
    [SerializeField]
    private float[] m_Values;

    private CanvasGroup _Group;

    // Start is called before the first frame update
    protected override void Start()
    {
        _Group = GetComponent<CanvasGroup>();

        base.Start();
    }

    public override void ToStateInstant(int state)
    {
        if (state >= m_Values.Length || _Group == null)
            return;

        _Group.DOKill();
        _Group.alpha = m_Values[state];

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

    public override float ToStateFullCustom(int state, float duration, float delay, Ease ease, bool kills = true)
    {
        if (state >= m_Values.Length || _Group == null)
            return -1;

        if(kills)
            _tween?.Kill();
        _tween = _Group.DOFade(m_Values[state], duration).SetEase(ease).SetDelay(delay).OnComplete(delegate
        {
            base.ResolveCallbacks();
        });

        return duration;
    }
}
