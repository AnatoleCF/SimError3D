using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using Valve.VR.InteractionSystem;

public class ViewSwitcher : MonoBehaviour {
    [SerializeField]
    private KeyCode m_SwitchKey;

    [SerializeField]
    private GameObject m_FPSView;
    [SerializeField]
    private GameObject m_GodView;

    [SerializeField]
    private Hand m_Hand;
    [SerializeField]
    private Camera m_GodCamera;
    [SerializeField]
    private Camera m_FPSCamera;

    [SerializeField]
    private Transform _FollowHeadTransform = null;
    [SerializeField]
    private TMP_Text m_Text;

    // Start is called before the first frame update
    void Start() {
        if(SceneManager.GetActiveScene().buildIndex == 0)
            return;
        m_FPSView.SetActive(true);
        m_GodView.SetActive(false);
        m_Hand.noSteamVRFallbackCamera = m_FPSCamera;
    }

    // Update is called once per frame
    void Update() {
        if(SceneManager.GetActiveScene().buildIndex == 0)
            return;
        if(Input.GetKeyDown(m_SwitchKey)) {
            _Switch();
        }
    }

    private void OnDestroy() {
        _BackToStandard();
    }
    private void OnLevelWasLoaded(int level) {
        _BackToStandard();
    }

    public void _Switch() {
        m_FPSView.SetActive(!m_FPSView.activeSelf);
        m_GodView.SetActive(!m_GodView.activeSelf);

        m_Hand.noSteamVRFallbackCamera = m_FPSView.activeSelf ? m_FPSCamera : m_GodCamera;

        _FollowHeadTransform.SetParent(m_FPSView.activeSelf ? m_FPSCamera.transform : m_GodCamera.transform);
        _FollowHeadTransform.localPosition = Vector3.zero;
        _FollowHeadTransform.localEulerAngles = Vector3.zero;

        if(m_GodView.activeSelf) {
            m_GodCamera.transform.position = m_FPSCamera.transform.position;
            m_GodCamera.transform.rotation = m_FPSCamera.transform.rotation;
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = false;
            m_Text.text = "VUE LIBRE";
        } else {
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
            m_Text.text = "VUE PREMIERE PERSONNE";

        }
    }
    

    public void _BackToStandard() {
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }
}
