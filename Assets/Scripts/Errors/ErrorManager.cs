using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;
using TMPro;
using UnityEngine.UI;

public class ErrorManager : MonoBehaviour
{
    public static ErrorManager Instance;

    [SerializeField]
    private int m_NumberOfErrors;

    [SerializeField]
    private ErrorController[] m_Errors;

    [SerializeField]
    private TMP_Text m_ErrorText;

    [SerializeField]
    private bool _isInErrorMode = false;

    [SerializeField]
    private bool _ForceStop = false;

    [SerializeField, ShowIf(nameof(_ForceStop))]
    private int _MaxErrorCount = 7;

    [SerializeField]
    private Button _EndButton = null;



    private void Awake()
    {
        Instance = this;
    }

    [Button]
    public void RetrieveErrors()
    {
        m_Errors = FindObjectsOfType<ErrorController>();
    }

    void Start()
    {
        LetsGo(); //TMP
    }

    [Button, HideInEditorMode]
    public void LetsGo()
    {
        if (_isInErrorMode)
            return;

        _isInErrorMode = true;

        int errorsToFind = Mathf.Clamp(m_NumberOfErrors, 0, m_Errors.Length);

        List<ErrorController> errors = Shuffle(m_Errors);

        for(int i = 0; i < errors.Count; i++)
        {
            errors[i].Setup(i < m_NumberOfErrors);
        }
    }

    [Button, HideInEditorMode]
    public void StopErrorMode()
    {
        _isInErrorMode = false;

        foreach(ErrorController ec in m_Errors)
        {
            ec.Stop();
        }
    }

    public bool IsInErrorMode()
    {
        return _isInErrorMode;
    }

    private List<ErrorController> Shuffle(ErrorController[] toShuffle)
    {
        List<ErrorController> shuffled = new List<ErrorController>();
        shuffled.AddRange(toShuffle);

        for (int i = 0; i < shuffled.Count; i++)
        {
            ErrorController temp = shuffled[i];
            int randomIndex = Random.Range(i, shuffled.Count);
            shuffled[i] = shuffled[randomIndex];
            shuffled[randomIndex] = temp;
        }

        return shuffled;
    }

    private void Update()
    {
        if (_isInErrorMode)
        {
            int found = 0;
            foreach(ErrorController ec in m_Errors)
            {
                if (ec.IsFound())
                    found++;
            }

            m_ErrorText.text = found + " / " + m_NumberOfErrors + " erreurs trouvées";

            if(_ForceStop) {
                if(found >= _MaxErrorCount)
                    _EndButton.onClick.Invoke();
            }
        }
    }

}
