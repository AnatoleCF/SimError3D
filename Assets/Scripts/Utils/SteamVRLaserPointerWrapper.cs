using UnityEngine;
using UnityEngine.EventSystems;
using Valve.VR.Extras;

public class SteamVRLaserPointerWrapper : MonoBehaviour {
    private SteamVR_LaserPointer steamVrLaserPointer;


    [SerializeField]
    private LaserCursor m_cursor = null;

    [SerializeField] private bool _DisableOutOfUI = false;

    [SerializeField]
    private bool _AllowMultipleHandlers = true;

    private void Awake() {
        steamVrLaserPointer = gameObject.GetComponent<SteamVR_LaserPointer>();
        steamVrLaserPointer.PointerIn += OnPointerIn;
        steamVrLaserPointer.PointerOut += OnPointerOut;
        steamVrLaserPointer.PointerClick += OnPointerClick;
    }

    private void Start() {
        if(m_cursor) {
            m_cursor.m_laserPointer = steamVrLaserPointer;
            if(_DisableOutOfUI)
                Invoke("DisableLaser", 1f);
        }
    }

    private void OnPointerClick(object sender, PointerEventArgs e) {
        if(_AllowMultipleHandlers) {
            foreach(IPointerClickHandler clickHandler in e.target.GetComponents<IPointerClickHandler>()) {
                m_cursor?.SwitchState(LaserCursor.CursorState.Clic);
                clickHandler.OnPointerClick(new PointerEventData(EventSystem.current));
            }
        } else {
            IPointerClickHandler clickHandler = e.target.GetComponent<IPointerClickHandler>();

            if(clickHandler == null) {
                return;
            }

            m_cursor?.SwitchState(LaserCursor.CursorState.Clic);
            clickHandler.OnPointerClick(new PointerEventData(EventSystem.current));
        }
    }

    private void OnPointerOut(object sender, PointerEventArgs e) {
        if(_AllowMultipleHandlers) {
            foreach(IPointerExitHandler pointerExitHandler in e.target.GetComponents<IPointerExitHandler>()) {
                pointerExitHandler.OnPointerExit(new PointerEventData(EventSystem.current));
            }
            if(_DisableOutOfUI)
                DisableLaser();
        } else {
            IPointerExitHandler pointerExitHandler = e.target.GetComponent<IPointerExitHandler>();
            if(pointerExitHandler == null) {
                return;
            }

            pointerExitHandler.OnPointerExit(new PointerEventData(EventSystem.current));
            DisableLaser();
            //m_cursor?.SwitchState(LaserCursor.CursorState.Default);
            //steamVrLaserPointer.pointer.SetActive(false);
        }
    }

    private void OnPointerIn(object sender, PointerEventArgs e) {
        if(_AllowMultipleHandlers) {
            steamVrLaserPointer.pointer.SetActive(true);
            m_cursor?.SwitchState(LaserCursor.CursorState.Hover);
            foreach(IPointerEnterHandler pointerEnterHandler in e.target.GetComponents<IPointerEnterHandler>()) {
                pointerEnterHandler.OnPointerEnter(new PointerEventData(EventSystem.current));
            }
        } else {
            IPointerEnterHandler pointerEnterHandler = e.target.GetComponent<IPointerEnterHandler>();
            if(pointerEnterHandler == null) {
                return;
            }

            steamVrLaserPointer.pointer.SetActive(true);
            m_cursor?.SwitchState(LaserCursor.CursorState.Hover);
            pointerEnterHandler.OnPointerEnter(new PointerEventData(EventSystem.current));
        }
    }

    void DisableLaser() {
        m_cursor?.SwitchState(LaserCursor.CursorState.Inactive);
        steamVrLaserPointer.pointer.SetActive(false);
    }

}