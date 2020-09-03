using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;
using DG.Tweening;
using UnityEngine.UI;
using TMPro;
using System.Linq;

public class ReportController : MonoBehaviour
{
    [SerializeField]
    private ErrorController m_Error;

    [SerializeField]
    private CanvasGroup m_UI;
    [SerializeField]
    private TextMeshProUGUI m_InfoTitle;
    [SerializeField]
    private TextMeshProUGUI m_Info;

    [SerializeField]
    private Material m_RedMaterial;
    [SerializeField]
    private Material m_OrangeMaterial;
    [SerializeField]
    private Material m_GreenMaterial;

    [SerializeField]
    private GameObject m_GoodEffectsContainer;
    [SerializeField]
    private GameObject m_BadEffectsContainer;

    private MeshRenderer[] _toHighlight;

    private int _successLevel; //0 => red (missed or failed), 1 => orange (quizz failed), 2 => green (correct)

    private bool _shown = false;

    private void Start()
    {
        m_GoodEffectsContainer.SetActive(false);
        m_BadEffectsContainer.SetActive(false);
    }

    public void StartReporting(int id)
    {
        if (!ReportManager.Instance.IsReporting())
            return;

        //Activate report color / effect, fill text
        bool isError = m_Error.GetErroneous();
        ErrorState errorState = m_Error.GetState();

        if (isError)
        {
            m_Info.text = m_Error.GetData().ExplanationError;
        } else
        {
            m_Info.text = m_Error.GetData().ExplanationNonError;
        }

        string recapText = "";

        switch (errorState)
        {
            case ErrorState.REPORTED:
                if (isError)
                {
                    //Player has found the error
                    bool quizzSuccess = m_Error.GetQuizzSuccess();
                    if (quizzSuccess)
                    {
                        //Green, player has found the correct answers
                        _successLevel = 2;
                        m_InfoTitle.text = "Félicitations";
                        m_Info.text = "Erreur repérée avec succès";
                        recapText = "erreur repérée avec succès";
                    }
                    else
                    {
                        //Orange, player failed the quizz
                        _successLevel = 1;
                        m_InfoTitle.text = "Erreur repérée mais incorrecte";
                        recapText = "rapport incorrect";
                    }
                } else
                {
                    //Player reported an error, but there wasn't any. RED
                    _successLevel = 0;
                    m_InfoTitle.text = "Erreur rapportée sur objet correct";
                    recapText = "erreur rapportée sur objet correct";
                }
                break;
            case ErrorState.RAS:
                if (isError)
                {
                    //Said no error but there was an error, RED
                    _successLevel = 0;
                    m_InfoTitle.text = "Erreur non repérée";
                    recapText = "Erreur non repérée";
                } else
                {
                    //Said no error and there indeed wasn't any, GREEN
                    _successLevel = 2;
                    m_InfoTitle.text = "Félicitations.";
                    m_Info.text = "En effet, objet sans erreur.";
                    recapText = "Objet correct bien indiqué";
                }
                break;
            default:
                if (isError)
                {
                    //Incomplete answer or missed, RED
                    _successLevel = 0;
                    m_InfoTitle.text = isError ? "Erreur oubliée" : "Objet oublié";
                    recapText = isError ? "Erreur oubliée" : "Objet oublié";
                } else
                {
                    //Incomplete answer or missed, RED
                    _successLevel = 2;
                    m_InfoTitle.text = "Aucune erreur";
                    recapText = "Rien à signaler";
                }
                break;
        }

        if (_successLevel == 2 && !isError)
            return;

        m_GoodEffectsContainer.SetActive(!m_Error.GetErroneous());
        m_BadEffectsContainer.SetActive(m_Error.GetErroneous());

        _toHighlight = m_Error.GetErroneous() ? m_BadEffectsContainer.GetComponentsInChildren<MeshRenderer>() : m_GoodEffectsContainer.GetComponentsInChildren<MeshRenderer>();

        foreach (MeshRenderer mr in _toHighlight)
        {
            mr.material = _successLevel == 0 ? m_RedMaterial : (_successLevel == 1 ? m_OrangeMaterial : m_GreenMaterial);
        }

        //Fetch target (ensure the line points to the correct item)
        Transform target = null;
        GameObject container = isError ? m_BadEffectsContainer : m_GoodEffectsContainer;
        if(container.transform.childCount > 0)
        {
            if(container.transform.GetChild(0).childCount > 0)
            {
                target = container.transform.GetChild(0).GetChild(0);
            }
        }
        //End fetch target

        ReportManager.Instance.AddLineToRecap(m_Error.GetData().Name, recapText, target, _successLevel, id);
    }

    public void Interact()
    {
        if (!ReportManager.Instance.IsReporting())
            return;

        //SHOW HIDE
        _shown = !_shown;

        m_UI.DOKill();
        m_UI.DOFade(_shown ? 1f : 0f, _shown ? 0.7f : 0.3f).SetEase(Ease.OutQuint).OnComplete(delegate {
            m_UI.GetComponent<UIColliders>().Toggle(_shown);
        });
    }

    public int GetSuccessLevel()
    {
        return _successLevel;
    }

    public ErrorScriptable GetData()
    {
        return m_Error.GetData();
    }

    [Button]
    public void Setup()
    {
        List<Transform> toRekt = new List<Transform>();

        toRekt.AddRange(m_BadEffectsContainer.transform.GetComponentsInChildren<Transform>());
        toRekt.AddRange(m_GoodEffectsContainer.transform.GetComponentsInChildren<Transform>());

        foreach (Transform t in toRekt)
        {
            if (t == null)
                continue;

            if(t != m_GoodEffectsContainer.transform && t != m_BadEffectsContainer.transform)
            {
                DestroyImmediate(t.gameObject);
            }
        }

        GameObject good = Instantiate(m_Error.GetGood(), m_GoodEffectsContainer.transform);

        GameObject bad = Instantiate(m_Error.GetBad(), m_BadEffectsContainer.transform);
    }
}
