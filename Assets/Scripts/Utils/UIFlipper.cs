using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIFlipper : MonoBehaviour
{
    [SerializeField]
    private bool m_CanFlip = true;

    // Update is called once per frame
    void Update()
    {
        if (m_CanFlip)
        {
            if(Vector3.Dot(transform.forward, (transform.position - Camera.main.transform.position).normalized) < 0)
            {
                transform.localRotation *= Quaternion.Euler(0, 180, 0);
            }
        }
    }
}
