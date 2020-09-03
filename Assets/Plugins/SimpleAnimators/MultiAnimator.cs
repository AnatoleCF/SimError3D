using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class MultiAnimator : MonoBehaviour
{

    private SimpleAnimator[] _animators;

    // Start is called before the first frame update
    void Start()
    {
        _animators = GetComponents<SimpleAnimator>();
    }

    public void AddCallback(UnityAction evt)
    {
        //Use the first one
        _animators[0].AddCallback(evt);
    }

    public void ToStateInstant(int state)
    {
        foreach(SimpleAnimator an in _animators)
        {
            an.ToStateInstant(state);
        }
    }

    public void ToState(int state)
    {
        foreach (SimpleAnimator an in _animators)
        {
            an.ToState(state);
        }
    }
    public void ToStateNoKill(int state)
    {
        foreach (SimpleAnimator an in _animators)
        {
            an.ToStateInspectorNoKill(state);
        }
    }

    public void ToStateDelayed(int state, float delay)
    {
        foreach (SimpleAnimator an in _animators)
        {
            an.ToStateDelayed(state, delay);
        }
    }

    public void ToStateFullCustom(int state, float duration, float delay, Ease ease, bool kills = false)
    {
        foreach (SimpleAnimator an in _animators)
        {
            an.ToStateFullCustom(state, duration, delay, ease);
        }
    }
}
