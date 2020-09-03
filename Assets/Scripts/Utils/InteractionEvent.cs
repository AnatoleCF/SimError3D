using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR.InteractionSystem;
using UnityEngine.Events;

public class InteractionEvent : MonoBehaviour
{
    [SerializeField]
    private UnityEvent m_OnPinch;

    public void HandHoverUpdate(Hand hand) {
        if (!enabled)
            return;

        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            m_OnPinch.Invoke();
        }


        if (hand.uiInteractAction.GetStateDown(hand.handType))
        {
            m_OnPinch.Invoke();
        }
    }

}
