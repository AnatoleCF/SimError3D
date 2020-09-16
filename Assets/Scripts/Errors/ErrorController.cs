using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;
using DG.Tweening;
using UnityEngine.UI;
using TMPro;
using Sirenix.Utilities;
using UnityEngine.Events;

public enum ErrorState {
    NOTFOUND, //N'a pas encore trouvé
    REPORTING, //Se demande si c'est une erreur
    CONFIRMING, //Pense que ce n'est pas une erreur, doit confirmer
    QUIZZ, //Réponds au quizz (c'est une erreur et le joueur a indiqué que ceci en est une)
    REPORTED, //A terminé, l'erreur est "lock"
    RAS //A indiqué que non-erreur, donc locké !
}

public class ErrorController : MonoBehaviour {
    [SerializeField]
    private ErrorScriptable m_Error;

    [SerializeField]
    private GameObject m_Normal;

    [SerializeField]
    private GameObject m_Erronous;

    [ReadOnly, ShowInInspector]
    private bool _isErroneous = false;

    [ReadOnly, ShowInInspector]
    private ErrorState _state;

    [ReadOnly, ShowInInspector]
    private int[] _answers;

    [SerializeField]
    private CanvasGroup m_ReportUI;
    [SerializeField]
    private CanvasGroup m_ConfirmUI;
    [SerializeField]
    private CanvasGroup m_QuizzUI;
    [SerializeField]
    private CanvasGroup m_MessageUI;
    [SerializeField]
    private TextMeshProUGUI m_MessageText;
    [SerializeField]
    private CanvasGroup m_FinalReportUI;

    [SerializeField]
    private QuizzController m_Quizz;

    [SerializeField]
    private Transform m_UI;
    [SerializeField]
    private Transform m_UITetherPoint;
    [SerializeField]
    private Transform m_GoodUISpot;
    [SerializeField]
    private Transform m_BadUISPot;
    [SerializeField]
    private LineRenderer m_UILine;

    [SerializeField]
    private CanvasGroup m_UIBackground;

    public UnityEvent UponStoping = null;

    public ErrorScriptable GetData() {
        return m_Error;
    }

    public int[] GetAnswers() {
        return _answers;
    }

    private void Start() {
        m_UILine.material = Instantiate(m_UILine.material);

        ToggleCollidersOf(m_QuizzUI.gameObject, false);
        ToggleCollidersOf(m_ReportUI.gameObject, false);
        ToggleCollidersOf(m_ConfirmUI.gameObject, false);

        UponStoping.AddListener(() => ToggleCollidersOf(gameObject, false));
    }

    public void Setup(bool erronous) {
        if(!ErrorManager.Instance.IsInErrorMode())
            return;

        m_Normal.SetActive(!erronous);
        m_Erronous.SetActive(erronous);

        _isErroneous = erronous;

        m_UI.transform.position = !_isErroneous ? m_GoodUISpot.position : m_BadUISPot.position;
        m_UI.transform.rotation = !_isErroneous ? m_GoodUISpot.rotation : m_BadUISPot.rotation;

        if(_isErroneous) {
            if(m_Erronous.transform.childCount > 0) {
                m_UILine.SetPosition(0, m_Erronous.transform.GetChild(0).position);
            }
        } else {
            if(m_Normal.transform.childCount > 0) {
                m_UILine.SetPosition(0, m_Normal.transform.GetChild(0).position);
            }
        }

        m_UILine.SetPosition(1, m_UITetherPoint.transform.position);

        _state = ErrorState.NOTFOUND;
    }

    public void Interact() {
        if(_state != ErrorState.NOTFOUND || !ErrorManager.Instance.IsInErrorMode())
            return;

        _state = ErrorState.REPORTING;

        m_ReportUI.DOKill();
        m_ReportUI.DOFade(1f, 0.7f).SetEase(Ease.OutQuint);
        m_ReportUI.interactable = true;
        m_ReportUI.blocksRaycasts = true;
        DelayCollidersOf(m_ReportUI.gameObject, true);
    }

    public void Report(bool hasChosenErroneous) {
        if(_state != ErrorState.REPORTING || !ErrorManager.Instance.IsInErrorMode())
            return;

        if(hasChosenErroneous) {
            _state = _isErroneous ? ErrorState.QUIZZ : ErrorState.REPORTED;
        } else {
            _state = ErrorState.CONFIRMING;
        }

        //Close is erroneous question UI
        m_ReportUI.DOKill();
        m_ReportUI.DOFade(0f, 0.35f).SetEase(Ease.OutQuint);
        m_ReportUI.interactable = false;
        m_ReportUI.blocksRaycasts = false;
        ToggleCollidersOf(m_ReportUI.gameObject, false);

        if(_state == ErrorState.QUIZZ) {
            //launch quizz UI
            m_QuizzUI.DOKill();
            m_QuizzUI.DOFade(1f, 0.7f).SetDelay(0.35f).SetEase(Ease.OutQuint);
            m_QuizzUI.interactable = true;
            m_QuizzUI.blocksRaycasts = true;
            ToggleCollidersOf(m_QuizzUI.gameObject, true);

            m_Quizz.SetupQuizz(m_Error);
        } else if(_state == ErrorState.CONFIRMING) {
            m_ConfirmUI.DOKill();
            m_ConfirmUI.DOFade(1f, 0.7f).SetDelay(0.35f).SetEase(Ease.OutQuint);
            m_ConfirmUI.interactable = true;
            m_ConfirmUI.blocksRaycasts = true;
            ToggleCollidersOf(m_ConfirmUI.gameObject, true);

        } else {
            m_MessageText.text = "Choix enregistré";
            m_MessageUI.DOKill();
            m_MessageUI.DOFade(1f, 0.7f).SetDelay(0.35f).SetEase(Ease.OutQuint).OnComplete(delegate {
                m_MessageUI.DOFade(0f, 0.35f).SetDelay(3f).SetEase(Ease.OutQuint);
                UponStoping.Invoke();
            });
        }
    }

    public void ConfirmNoError(bool isSure) {
        if(_state != ErrorState.CONFIRMING || !ErrorManager.Instance.IsInErrorMode())
            return;

        m_ConfirmUI.DOKill();
        m_ConfirmUI.DOFade(0f, 0.35f).SetEase(Ease.OutQuint);
        m_ConfirmUI.interactable = false;
        m_ConfirmUI.blocksRaycasts = false;
        ToggleCollidersOf(m_ConfirmUI.gameObject, false);

        if(isSure) {
            _state = ErrorState.RAS;

            m_MessageText.text = "Choix enregistré";
            m_MessageUI.DOKill();
            m_MessageUI.DOFade(1f, 0.7f).SetDelay(0.35f).SetEase(Ease.OutQuint).OnComplete(delegate {
                m_MessageUI.DOFade(0f, 0.35f).SetDelay(3f).SetEase(Ease.OutQuint);
                UponStoping.Invoke();
            });
        } else {
            _state = ErrorState.NOTFOUND;
        }
    }

    public void ConfirmAnwsers(int[] answers) {
        if(_state != ErrorState.QUIZZ || !ErrorManager.Instance.IsInErrorMode())
            return;

        _state = ErrorState.REPORTED;

        _answers = answers;

        m_QuizzUI.DOKill();
        m_QuizzUI.DOFade(0f, 0.35f).SetEase(Ease.OutQuint);
        m_QuizzUI.interactable = false;
        m_QuizzUI.blocksRaycasts = false;
        ToggleCollidersOf(m_QuizzUI.gameObject, false);

        m_MessageText.text = "Choix enregistré";
        m_MessageUI.DOKill();
        m_MessageUI.DOFade(1f, 0.7f).SetDelay(0.35f).SetEase(Ease.OutQuint).OnComplete(delegate {
            m_MessageUI.DOFade(0f, 0.35f).SetDelay(3f).SetEase(Ease.OutQuint);
            UponStoping.Invoke();
        });
    }

    [Button]
    public void Stop() //Close everything
    {
        m_QuizzUI.DOKill();
        m_QuizzUI.DOFade(0f, 0.35f).SetEase(Ease.OutQuint);
        m_QuizzUI.interactable = false;
        m_QuizzUI.blocksRaycasts = false;
        ToggleCollidersOf(m_QuizzUI.gameObject, false);

        m_ConfirmUI.DOKill();
        m_ConfirmUI.DOFade(0f, 0.35f).SetEase(Ease.OutQuint);
        m_ConfirmUI.interactable = false;
        m_ConfirmUI.blocksRaycasts = false;
        ToggleCollidersOf(m_ConfirmUI.gameObject, false);

        m_MessageUI.DOKill();
        m_MessageUI.DOFade(0f, 0.35f).SetEase(Ease.OutQuint);
        m_MessageUI.interactable = false;
        m_MessageUI.blocksRaycasts = false;

        m_ReportUI.DOKill();
        m_ReportUI.DOFade(0f, 0.35f).SetEase(Ease.OutQuint);
        m_ReportUI.interactable = false;
        m_ReportUI.blocksRaycasts = false;
        ToggleCollidersOf(m_ReportUI.gameObject, false);

    }

    public ErrorState GetState() {
        return _state;
    }

    public bool GetErroneous() {
        return _isErroneous;
    }

    public bool GetQuizzSuccess() {
        if(_state != ErrorState.REPORTED)
            return false;

        foreach(int answer in m_Error.CorrectAnswers) {
            bool hasFound = false;
            foreach(int playerAnswer in _answers) {
                if(answer == playerAnswer) {
                    hasFound = true;
                    break;
                }
            }
            if(!hasFound) {
                return false;
            }
        }
        return true;
    }

    public bool IsFound() {
        //if (!_isErroneous)
        //    return false;

        return _state == ErrorState.REPORTED;
    }

    public GameObject GetGood() {
        return m_Normal;
    }

    public GameObject GetBad() {
        return m_Erronous;
    }

    private void Update() {
        float greaterAlpha = Mathf.Max(m_ConfirmUI.alpha, m_ReportUI.alpha);
        greaterAlpha = Mathf.Max(greaterAlpha, m_QuizzUI.alpha);
        greaterAlpha = Mathf.Max(greaterAlpha, m_MessageUI.alpha);
        greaterAlpha = Mathf.Max(greaterAlpha, m_FinalReportUI.alpha);
        greaterAlpha = Mathf.Max(greaterAlpha, m_QuizzUI.GetComponent<QuizzController>().m_Correction.GetComponent<CanvasGroup>().alpha);

        m_UILine.material.SetFloat("_GeneralAlpha", greaterAlpha);

        m_UIBackground.alpha = greaterAlpha;
    }

    private void ToggleCollidersOf(GameObject go, bool truth) {
        go.GetComponentsInChildren<Collider>().ForEach(x => x.enabled = truth);
    }


    private void DelayCollidersOf(GameObject go, bool truth, float delay = .5f) {
        StartCoroutine(DelayCollidersRoutine(go, truth, delay));
    }
    private IEnumerator DelayCollidersRoutine(GameObject go, bool truth, float delay = .5f) {
        yield return new WaitForSeconds(delay);
        go.GetComponentsInChildren<Collider>().ForEach(x => x.enabled = truth);
    }
}