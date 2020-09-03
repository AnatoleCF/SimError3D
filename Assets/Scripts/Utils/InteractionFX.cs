using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;

public class InteractionFX : MonoBehaviour
{
    [SerializeField]
    private CanvasGroup[] m_Canvas;
    
    [SerializeField]
    private GameObject m_GoodObject;
    [SerializeField]
    private GameObject m_BadObject;

    [SerializeField]
    private ErrorController m_Error;

    [SerializeField]
    private Material m_Material;

    // Start is called before the first frame update
    void Start()
    {
        m_BadObject.SetActive(false);
        m_GoodObject.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        float greaterAlpha = 0f;
        foreach(CanvasGroup cg in m_Canvas)
        {
            greaterAlpha = Mathf.Max(greaterAlpha, cg.alpha);
        }
        
        if(greaterAlpha > 0)
        {
            if (m_Error.GetErroneous())
            {
                m_BadObject.SetActive(true);
            } else
            {
                m_GoodObject.SetActive(true);
            }
        } else
        {
            m_BadObject.SetActive(false);
            m_GoodObject.SetActive(false);
        }
    }

    [Button]
    public void Setup()
    {
        List<Transform> toRekt = new List<Transform>();

        toRekt.AddRange(m_BadObject.transform.GetComponentsInChildren<Transform>());
        toRekt.AddRange(m_GoodObject.transform.GetComponentsInChildren<Transform>());

        foreach (Transform t in toRekt)
        {
            if (t == null)
                continue;

            if (t != m_GoodObject.transform && t != m_BadObject.transform)
            {
                DestroyImmediate(t.gameObject);
            }
        }

        GameObject good = Instantiate(m_Error.GetGood(), m_GoodObject.transform);

        GameObject bad = Instantiate(m_Error.GetBad(), m_BadObject.transform);

        foreach(MeshRenderer mr in m_BadObject.GetComponentsInChildren<MeshRenderer>())
        {
            mr.material = m_Material;
        }
        foreach (MeshRenderer mr in m_GoodObject.GetComponentsInChildren<MeshRenderer>())
        {
            mr.material = m_Material;
        }

        m_BadObject.SetActive(false);
        m_GoodObject.SetActive(false);
    }
}
