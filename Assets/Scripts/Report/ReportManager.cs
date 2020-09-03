using UnityEngine;
using System.Collections;
using Sirenix.OdinInspector;

public class ReportManager : MonoBehaviour
{
    public static ReportManager Instance;

    [SerializeField]
    private ReportController[] m_Reports;

    [SerializeField]
    private Transform m_LinesContainer;

    [SerializeField]
    private ReportLine m_LinesPrefab;

    private bool _isReporting = false;

    private void Awake()
    {
        Instance = this;
    }

    [Button]
    public void RetrieveReports()
    {
        m_Reports = FindObjectsOfType<ReportController>();
    }

    [Button, HideInEditorMode]
    public void StartReporting()
    {
        _isReporting = true;
        for(int i = 0; i < m_Reports.Length; i++)
        {
            m_Reports[i].StartReporting(i);
        }
    }

    public bool IsReporting()
    {
        return _isReporting;
    }

    public void AddLineToRecap(string itemName, string itemInfo, Transform target, int successLevel, int reportID)
    {
        Transform container = m_LinesContainer;
        Instantiate(m_LinesPrefab, container).Setup(itemName, itemInfo, target, successLevel);
    }
}
