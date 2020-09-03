using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;

public class Datator : MonoBehaviour
{
    [SerializeField]
    private TMP_Text m_TextDay;
    [SerializeField]
    private TMP_Text m_TextMonth;
    [SerializeField]
    private TMP_Text m_TextYear;
    [SerializeField]
    private int m_DifDay;
    [SerializeField]
    private int m_DifMonth;
    [SerializeField]
    private int m_DifYear;

    // Start is called before the first frame update
    void Start()
    {
        DateTime now = DateTime.Now;
        now = now.AddDays(m_DifDay);
        now = now.AddMonths(m_DifMonth);
        now = now.AddYears(m_DifYear);

        m_TextDay.text = now.Day.ToString("00");
        m_TextMonth.text = now.Month.ToString("00");
        m_TextYear.text = now.Year.ToString("0000");
    }
}
