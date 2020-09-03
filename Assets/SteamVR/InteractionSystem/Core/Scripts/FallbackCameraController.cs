//======= Copyright (c) Valve Corporation, All rights reserved. ===============
//
// Purpose: Controls for the non-VR debug camera
//
//=============================================================================

using UnityEngine;
using System.Collections;

namespace Valve.VR.InteractionSystem
{
	//-------------------------------------------------------------------------
	[RequireComponent( typeof( Camera ) )]
    public class FallbackCameraController : MonoBehaviour
    {
        [SerializeField]
        private float speed = 4.0f;

        private Vector3 startEulerAngles;
        private Vector3 startMousePosition;

        private Camera _cam;

        [SerializeField] private Vector3 _MinBound = Vector3.zero;
        [SerializeField] private Vector3 _MaxBound = Vector3.zero;

        private bool IsWithinBounds(Vector3 vector3) {
            return _MinBound.x <= vector3.x && _MaxBound.x >= vector3.x
            && _MinBound.y <= vector3.y && _MaxBound.y >= vector3.y
            && _MinBound.z <= vector3.z && _MaxBound.z >= vector3.z;
        }

        private void Start()
        {
            _cam = GetComponent<Camera>();
        }

        //-------------------------------------------------
        void Update()
        {
            float forward = 0.0f;
            if (Input.GetKey(KeyCode.Z) || Input.GetKey(KeyCode.UpArrow))
            {
                forward += 1.0f;
            }
            if (Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.DownArrow))
            {
                forward -= 1.0f;
            }

            float right = 0.0f;
            if (Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.RightArrow))
            {
                right += 1.0f;
            }
            if (Input.GetKey(KeyCode.Q) || Input.GetKey(KeyCode.LeftArrow))
            {
                right -= 1.0f;
            }

            Vector3 delta = new Vector3(right, 0, forward) * speed * Time.deltaTime;

            Vector3 pos = transform.position + transform.TransformDirection(delta);

            if(IsWithinBounds(pos))
                transform.position = pos;

            //transform.position += transform.TransformDirection(delta);

            Vector3 mousePosition = Input.mousePosition;

            if (Input.GetMouseButtonDown(1) /* right mouse */)
            {
                startMousePosition = mousePosition;
                startEulerAngles = transform.localEulerAngles;
            }

            if (Input.GetMouseButton(1) /* right mouse */)
            {
                Vector3 offset = mousePosition - startMousePosition;
                transform.localEulerAngles = startEulerAngles + new Vector3(-offset.y * 360.0f / Screen.height, offset.x * 360.0f / Screen.width, 0.0f);
            }
        }

        public void ResetTo(Transform target)
        {
            transform.position = target.position;
            transform.rotation = target.rotation;
        }
    }
}
