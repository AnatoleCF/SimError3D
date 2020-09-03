using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using UnityEngine.Events;

public class TransformAnimator : SimpleAnimator
{
    [SerializeField]
    private Transform[] m_Positions;

    [SerializeField]
    [Tooltip("More often than not, using quaternions will yield better results.")]
    private bool m_UsesQuaternionForRotations = true;

    private Tweener _tweenRot;

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
        transform.position = m_Positions[state].position;
        if (m_UsesQuaternionForRotations)
        {
            transform.rotation = m_Positions[state].rotation;
        } else
        {
            transform.eulerAngles = m_Positions[state].eulerAngles;
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
        {
            _tween?.Kill();
            _tweenRot?.Kill();
        }

        _tween = transform.DOMove(m_Positions[state].position, duration).SetEase(ease).SetDelay(delay).OnComplete(delegate
        {
            base.ResolveCallbacks();
        });

        if (m_UsesQuaternionForRotations)
        {
            _tweenRot = transform.DORotateQuaternion(m_Positions[state].rotation, duration).SetEase(ease).SetDelay(delay);
        } else
        {
            _tweenRot = transform.DORotate(m_Positions[state].eulerAngles, duration).SetEase(ease).SetDelay(delay);
        }

        return duration;
    }
}
