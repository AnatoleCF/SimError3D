using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using UnityEngine.Events;

public class PositionAnimator : SimpleAnimator
{
    [SerializeField]
    private Vector3[] m_Positions;

    [SerializeField]
    private bool m_IsLocal = false;

    // Start is called before the first frame update
    protected override void Start()
    {
        base.Start();
    }

    public override void ToStateInstant(int state)
    {
        if (state >= m_Positions.Length)
            return;

        transform.DOKill();
        if (m_IsLocal)
        {
            transform.localPosition = m_Positions[state];
        }
        else
        {
            transform.position = m_Positions[state];
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
        if (state >= m_Positions.Length)
            return -1;

        if (kills)
            _tween?.Kill();

        if (m_IsLocal)
        {
            _tween = transform.DOLocalMove(m_Positions[state], duration).SetEase(ease).SetDelay(delay).OnComplete(delegate
            {
                base.ResolveCallbacks();
            });
        }
        else
        {
            _tween = transform.DOMove(m_Positions[state], duration).SetEase(ease).SetDelay(delay).OnComplete(delegate
            {
                base.ResolveCallbacks();
            });
        }

        return duration;
    }
}

