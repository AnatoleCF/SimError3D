using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using UnityEngine.Events;

public class MaterialColorAnimator : SimpleAnimator
{
    [SerializeField]
    private Color[] m_Colors;

    [SerializeField]
    private string m_PropertyName;

    private Material _Mat;

    [SerializeField]
    private bool m_InstantiatesMaterial = true;

    // Start is called before the first frame update
    protected override void Start()
    {
        MeshRenderer mr = GetComponent<MeshRenderer>();
        if(mr != null)
        {
            if (m_InstantiatesMaterial)
            {
                mr.material = Instantiate(mr.material);
            }
            _Mat = mr.material;
        }

        base.Start();
    }

    public override void ToStateInstant(int state)
    {
        if (state >= m_Colors.Length || _Mat == null)
            return;

        _Mat.DOKill();
        _Mat.SetColor(m_PropertyName, m_Colors[state]);

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
        if (state >= m_Colors.Length || _Mat == null)
            return -1;

        if(kills)
            _tween?.Kill();
        _tween = _Mat.DOColor(m_Colors[state], m_PropertyName, duration).SetEase(ease).SetDelay(delay).OnComplete(delegate
        {
            base.ResolveCallbacks();
        });

        return duration;
    }
}
