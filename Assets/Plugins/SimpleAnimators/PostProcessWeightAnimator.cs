using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using UnityEngine.Rendering.PostProcessing;

public class PostProcessWeightAnimator : SimpleAnimator
{
    [SerializeField]
    private float[] m_Values;

    private PostProcessVolume _Volume;

    // Start is called before the first frame update
    protected override void Start()
    {
        _Volume = GetComponent<PostProcessVolume>();

        base.Start();
    }

    public override void ToStateInstant(int state)
    {
        if (state >= m_Values.Length || _Volume == null)
            return;

        _Volume.DOKill();
        _Volume.weight = m_Values[state];

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
        if (state >= m_Values.Length || _Volume == null)
            return -1;

        if (kills)
            _tween?.Kill();

        _tween = DOTween.To(()=> _Volume.weight, x=> _Volume.weight = x, m_Values[state], duration).SetTarget(_Volume).SetEase(ease).SetDelay(delay).OnComplete(delegate
        {
            base.ResolveCallbacks();
        });

        return duration;
    }
}
