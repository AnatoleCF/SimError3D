using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.Events;

public abstract class SimpleAnimator : MonoBehaviour
{
    [SerializeField]
    protected Ease m_Ease;

    [SerializeField]
    protected float m_Duration;

    protected UnityEvent _Callbacks = new UnityEvent();

    [SerializeField]
    private bool m_FirstStateOnStart = true;

    protected Tweener _tween;

    protected virtual void Start()
    {
        if(m_FirstStateOnStart)
            ToStateInstant(0);
    }

    public void AddCallback(UnityAction evt)
    {
        _Callbacks.AddListener(evt);
    }

    protected void ResolveCallbacks()
    {
        _Callbacks.Invoke();
        _Callbacks.RemoveAllListeners();
    }

    public abstract void ToStateInstant(int state);

    public void ToStateInspector(int state) //Void version of To State to allow display in inspector
    {
        ToState(state);
    }

    public void ToStateInspectorNoKill(int state) //Void version of To State to allow display in inspector
    {
        ToStateFullCustom(state, m_Duration, 0, m_Ease, false);
    }

    public abstract float ToState(int state);

    public abstract float ToStateDelayed(int state, float delay);

    public abstract float ToStateFullCustom(int state, float duration, float delay, Ease ease, bool kills = true);
}
