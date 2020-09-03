﻿using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using UnityEngine.Events;

public class AnchoredPosAnimator : SimpleAnimator
{
    [SerializeField]
    private Vector3[] m_Positions;

    private RectTransform _Rect;

    // Start is called before the first frame update
    protected override void Start()
    {
        _Rect = GetComponent<RectTransform>();

        base.Start();
    }

    public override void ToStateInstant(int state)
    {
        if (state >= m_Positions.Length || _Rect == null)
            return;

        _Rect.DOKill();
        _Rect.anchoredPosition = m_Positions[state];

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
        if (state >= m_Positions.Length || _Rect == null)
            return -1;

        if(kills)
            _tween?.Kill();

        _tween = _Rect.DOAnchorPos(m_Positions[state], duration).SetEase(ease).SetDelay(delay).OnComplete(delegate
        {
            base.ResolveCallbacks();
        });

        return duration;
    }
}
