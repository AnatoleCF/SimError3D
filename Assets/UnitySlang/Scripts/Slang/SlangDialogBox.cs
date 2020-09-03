using BMLRealizer;
using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

[System.Serializable]
public class DialogEntry {
    [TextArea]
    public string Question;
    [TextArea]
    public string Answer;
    public ErrorController RelatedError = null;
}

public class SlangDialogBox : MonoBehaviour {
    [Header("References")]
    [SerializeField] private BMLListener _BmlListener = null;
    [SerializeField] private CanvasGroup _CanvasGroup = null;
    [SerializeField] private TextMeshProUGUI _AgentMessageTmp = null;
    [SerializeField] private TextMeshProUGUI _UserMessageTmp = null;
    [Header("Parameters")]
    [SerializeField] private float _ReadingDelay = 2;
    [SerializeField] private string _QuestionKeywordColor = "yellow";
    [SerializeField] private string _AnswerKeywordColor = "purple";

    [Header("Dialog")]
    [SerializeField]
    private List<DialogEntry> _DialogEntries = new List<DialogEntry>();

    private int _CurrentEntryId = -1;
    private DialogEntry _CurrentEntry = null;


    private bool _IsDisplayed = true;

    protected int _CurrentQuestionId = -1;
    private ErrorController _CurrentError = null;

    protected virtual void Awake() {
        _BmlListener.m_SpeechEvent.AddListener(OnSpeechReceived);
        ToggleDialogBox(false, 0);
    }

    // Start is called before the first frame update
    void Start() {
        NextEntry();
    }

    // Update is called once per frame
    void Update() {
        if(!_IsDisplayed && _CurrentEntryId == 0) {
            Ray ray = new Ray(Camera.main.transform.position, Camera.main.transform.forward);
            RaycastHit hit;
            if(Physics.Raycast(ray, out hit)) {
                if(hit.collider.name == "LookAtTarget") {
                    _IsDisplayed = true;
                    ToggleDialogBox(true, 1);
                }
            } 
        }
    }

    protected void OnSpeechReceived(BMLSpeech bmlSpeech) {
        if(!_IsDisplayed)
            ToggleDialogBox(true, 1);

        if(_AgentMessageTmp.text != "")
            return;

        //if(_AgentMessageTmp.text != )

        _AgentMessageTmp.text = "\"" + string.Format(_CurrentEntry.Answer, _AnswerKeywordColor) + "\"";

        if(_CurrentEntry.RelatedError == null) {
            Invoke(nameof(NextEntry), _ReadingDelay);
        } else {
            _CurrentError = _CurrentEntry.RelatedError;
            _CurrentError.UponStoping.AddListener(() => NextEntry());
            _CurrentError.Interact();
        }
    }

    public void NextEntry() {
        _CurrentEntryId++;

        if(_CurrentEntryId == _DialogEntries.Count) {
            ToggleDialogBox(false, 1);
            return;
        }
        _CurrentEntry = _DialogEntries[_CurrentEntryId];
        _UserMessageTmp.text = string.Format(_CurrentEntry.Question, _QuestionKeywordColor);
        _AgentMessageTmp.text = "";
    }

    public void ToggleDialogBox(bool truth, float duration) {
        _CanvasGroup.DOKill();
        _CanvasGroup.DOFade(truth ? 1 : 0, duration).SetEase(Ease.InOutSine).OnComplete(() => _IsDisplayed = truth);
    }

}
