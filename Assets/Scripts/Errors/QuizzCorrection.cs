using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class QuizzCorrection : MonoBehaviour {

    [SerializeField]
    private ErrorController m_Controller;

    [SerializeField]
    private Transform m_AnwsersContainer;

    [SerializeField]
    private RectTransform m_AnswerPrefab;

    private List<Outline> _answersOutlines = new List<Outline>();

    private List<int> _answers = new List<int>();

    public void SetupQuizz(ErrorScriptable data) {
        for(int i = 0; i < data.Answers.Length; i++) {
            RectTransform bt = Instantiate(m_AnswerPrefab, m_AnwsersContainer);
            int tmp = i;
            _answersOutlines.Add(bt.GetComponent<Outline>());
            bt.GetComponentInChildren<TextMeshProUGUI>().text = data.Answers[i];
            bt.GetComponent<Outline>().effectColor = data.CorrectAnswers.ToList().Contains(i) ? Color.green : Color.red;
            bt.GetComponent<Outline>().DOFade(1,.5f);
        }
    }
}
