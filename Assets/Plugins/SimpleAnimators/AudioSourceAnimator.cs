using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using UnityEngine.Events;

public class AudioSourceAnimator : SimpleAnimator
{
    [SerializeField]
    private float[] m_Volumes;
    [SerializeField]
    private float[] m_Pitchs;

    private AudioSource _Source;

    // Start is called before the first frame update
    protected override void Start()
    {
        _Source = GetComponent<AudioSource>();

        base.Start();
    }

    public override void ToStateInstant(int state)
    {
        if (state >= m_Volumes.Length || state >= m_Pitchs.Length || _Source == null)
            return;

        _Source.DOKill();
        _Source.volume = m_Volumes[state];
        _Source.pitch = m_Pitchs[state];

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
        if (state >= m_Volumes.Length || state >= m_Pitchs.Length || _Source == null)
            return -1;

        if(kills)
            _tween?.Kill();
        _tween = _Source.DOFade(m_Volumes[state], duration).SetDelay(delay).SetEase(ease).OnComplete(delegate
        {
            base.ResolveCallbacks();
        });
        _Source.DOPitch(m_Pitchs[state], duration).SetDelay(delay).SetEase(ease);

        return duration;
    }
}
