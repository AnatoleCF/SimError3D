using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;
using DG.Tweening;
using UnityEngine.UI;
using TMPro;

public class QuizzController : MonoBehaviour
{
    [SerializeField]
    private ErrorController m_Controller;

    [SerializeField]
    private Transform m_AnwsersContainer;

    [SerializeField]
    private Button m_AnswerPrefab;

    public QuizzCorrection m_Correction = null;

    private List<Outline> _answersOutlines = new List<Outline>();

    private List<int> _answers = new List<int>();

    public void SetupQuizz(ErrorScriptable data)
    {
        for (int i = 0; i < data.Answers.Length; i++)
        {
            Button bt = Instantiate(m_AnswerPrefab, m_AnwsersContainer);
            int tmp = i;
            bt.onClick.AddListener(delegate { SelectAnswer(tmp); });
            _answersOutlines.Add(bt.GetComponent<Outline>());
            bt.GetComponentInChildren<TextMeshProUGUI>().text = data.Answers[i];
        }
        m_Correction.SetupQuizz(data);
    }

    public void SelectAnswer(int id)
    {
        if (_answers.Contains(id))
        {
            _answers.Remove(id);
            _answersOutlines[id].DOFade(0f, 0.3f);
        } else
        {
            _answers.Add(id);
            _answersOutlines[id].DOFade(1f, 0.3f);
        }
    }

    public void Confirm()
    {
        m_Controller.ConfirmAnwsers(_answers.ToArray());
        GetComponent<UIColliders>().Toggle(false);
    }

}
