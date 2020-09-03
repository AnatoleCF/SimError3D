using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Valve.VR.InteractionSystem;

public class PlayerKiller : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        if(SceneManager.GetActiveScene().buildIndex == 0) {
            Destroy(FindObjectOfType<Player>().trackingOriginTransform.parent.gameObject);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
