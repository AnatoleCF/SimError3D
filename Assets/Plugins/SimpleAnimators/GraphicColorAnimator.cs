using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

public class GraphicColorAnimator : SimpleAnimator
{
    [SerializeField]
    private Color[] m_Colors;

    private Graphic _Graphic;

    // Start is called before the first frame update
    protected override void Start()
    {
        _Graphic = GetComponent<Graphic>();

        base.Start();
    }

    public override void ToStateInstant(int state)
    {
        if (state >= m_Colors.Length || _Graphic == null)
            return;

        _Graphic.DOKill();
        _Graphic.color = m_Colors[state];

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
        if (state >= m_Colors.Length || _Graphic == null)
            return -1;

        if(kills)
            _tween?.Kill();
        _tween = _Graphic.DOColor(m_Colors[state], duration).SetEase(ease).SetDelay(delay).OnComplete(delegate
        {
            base.ResolveCallbacks();
        });

        return duration;
    }
}
