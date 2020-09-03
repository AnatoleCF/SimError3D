using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class ReportLine : MonoBehaviour
{
    [SerializeField]
    private Button m_FindButton;
    [SerializeField]
    private TextMeshProUGUI m_FindButtonText;

    [SerializeField]
    private TextMeshProUGUI m_ItemName;
    [SerializeField]
    private TextMeshProUGUI m_ItemInfo;
    [SerializeField]
    private Transform m_GuidanceSource;
    [SerializeField]
    private Graphic m_GuidanceSourceImage;
    [SerializeField]
    private LineRenderer m_Guidance;

    [SerializeField]
    private Color[] m_Colors;

    private Transform _targetItem;


    public void Setup(string itemName, string itemInfo, Transform target, int successLevel)
    {
        m_ItemName.text = itemName;
        m_ItemInfo.text = itemInfo;
        m_GuidanceSourceImage.color = m_Colors[successLevel];
        m_Guidance.gameObject.SetActive(false);

        if (target != null)
        {
            _targetItem = target;

            m_Guidance.material = Instantiate(m_Guidance.material);
            m_Guidance.material.SetColor("_Color", m_Colors[successLevel]);
            m_Guidance.material.SetColor("_DefaultColor", m_Colors[successLevel]);
            m_Guidance.SetPositions(new Vector3[] { m_GuidanceSource.position, _targetItem.position });
            m_Guidance.useWorldSpace = true;
        } else
        {
            m_FindButton.interactable = false;
            m_FindButtonText.text = "AUCUNE ERREUR";
        }
    }

    private void Update()
    {
        m_Guidance.SetPosition(0, m_GuidanceSource.position);
    }

    public void ToggleGuidance()
    {
        m_Guidance.gameObject.SetActive(!m_Guidance.gameObject.activeSelf);
    }
}
