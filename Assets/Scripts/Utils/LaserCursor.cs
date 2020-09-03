using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR.Extras;

public class LaserCursor : MonoBehaviour
{
    [HideInInspector]
    public SteamVR_LaserPointer m_laserPointer;

    private CursorState m_currentState = CursorState.Default;

    [SerializeField]
    private Color m_defaultColor = Color.white;
    [SerializeField]
    private Color m_hoverColor = Color.yellow;
    [SerializeField]
    private Color m_clicColor = Color.blue;

    private MeshRenderer m_meshRenderer;

    // Start is called before the first frame update
    void Start()
    {
        m_meshRenderer = GetComponentInChildren<MeshRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        Ray raycast = new Ray(m_laserPointer.transform.position, m_laserPointer.transform.forward);

        RaycastHit hit;
        bool bHit = Physics.Raycast(raycast, out hit);

        if(bHit) {
            transform.position = hit.point;
            transform.LookAt(hit.point + hit.normal);
        }
    }

    public enum CursorState {
        Default,
        Hover,
        Clic,

        Inactive
    }

    public void SwitchState(CursorState cursorState) {
        if(m_currentState == cursorState)
            return;

        gameObject.SetActive(cursorState != CursorState.Inactive);

        Color tmp = m_defaultColor;

        switch(cursorState) {
            case CursorState.Default:
            tmp = m_defaultColor;
            break;
            case CursorState.Hover:
            tmp = m_hoverColor;
            break;
            case CursorState.Clic:
            tmp = m_clicColor;
            break;
        }

        m_meshRenderer.material.color = tmp;
        m_currentState = cursorState;

    }
}
