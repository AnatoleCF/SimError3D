using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AgentLookAtTarget : MonoBehaviour {
    [SerializeField] private bool _CastToGround = false;
    [SerializeField] private LayerMask _GroundMask;
    [SerializeField] private float _VerticalOffset = 1.8f;



    // Update is called once per frame
    void Update() {
        if(!_CastToGround)
            return;
        Ray ray = new Ray(transform.position, Vector3.down);
        RaycastHit hit;
        if(Physics.Raycast(ray, out hit, 10, _GroundMask)) {
            Vector3 hitPos = hit.point;
            transform.position = hit.point + _VerticalOffset * Vector3.up;
        }

        transform.localEulerAngles = Vector3.zero;
    }
}
