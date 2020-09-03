using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using UnityEngine.Events;

public class RotationAnimator : SimpleAnimator
{
    [SerializeField]
    private Vector3[] m_Rotations;

    [SerializeField]
    private bool m_IsLocal = false;

    [SerializeField]
    [Tooltip("More often than not, using quaternions will yield better results.")]
    private bool m_UsesQuaternions = true;

    // Start is called before the first frame update
    protected override void Start()
    {
        base.Start();
    }

    public override void ToStateInstant(int state)
    {
        if (state >= m_Rotations.Length)
            return;

        transform.DOKill();
        if (m_UsesQuaternions)
        {
            if (m_IsLocal)
            {
                transform.localRotation = Quaternion.Euler(m_Rotations[state]);
            }
            else
            {
                transform.rotation = Quaternion.Euler(m_Rotations[state]);
            }
        } else
        {
            if (m_IsLocal)
            {
                transform.localEulerAngles = m_Rotations[state];
            }
            else
            {
                transform.eulerAngles = m_Rotations[state];
            }
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

    public override float ToStateFullCustom(int state, float duration, float delay, Ease ease, bool kills = true)
    {
        if (state >= m_Rotations.Length)
            return -1;

        if(kills)
            _tween?.Kill();

        if (m_UsesQuaternions)
        {
            if (m_IsLocal)
            {
                _tween = transform.DOLocalRotateQuaternion(Quaternion.Euler(m_Rotations[state]), duration).SetEase(ease).SetDelay(delay).OnComplete(delegate
                {
                    base.ResolveCallbacks();
                });
            }
            else
            {
                _tween = transform.DORotateQuaternion(Quaternion.Euler(m_Rotations[state]), duration).SetEase(ease).SetDelay(delay).OnComplete(delegate
                {
                    base.ResolveCallbacks();
                });
            }
        } else
        {
            if (m_IsLocal)
            {
                _tween = transform.DOLocalRotate(m_Rotations[state], duration).SetEase(ease).SetDelay(delay).OnComplete(delegate
                {
                    base.ResolveCallbacks();
                });
            }
            else
            {
                _tween = transform.DORotate(m_Rotations[state], duration).SetEase(ease).SetDelay(delay).OnComplete(delegate
                {
                    base.ResolveCallbacks();
                });
            }
        }

        return duration;
    }
}
