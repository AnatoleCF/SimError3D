using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class UIColliders : MonoBehaviour {
    public void Toggle(bool truth) {
        GetComponentsInChildren<Collider>().ToList().ForEach(x => x.enabled = truth);
        if(GetComponent<CanvasGroup>()) {
            GetComponent<CanvasGroup>().interactable = truth;
            GetComponent<CanvasGroup>().blocksRaycasts = truth;
        }
    }

}
