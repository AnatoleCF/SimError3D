using BMLRealizer;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Animator))]
public class PostureController : MonoBehaviour {
    private Animator Animator = null;

    [Header("Parameters")]
    [SerializeField] protected bool _DebugMode = false;


    private void Awake() {
        Animator = GetComponent<Animator>();
    }

    // Start is called before the first frame update
    void Start() {

    }

    public void SetPosture(BMLPosture posture) {
        if(_DebugMode) {
            Debug.Log($"Posture received : {posture.stance}");
        }
        SetPosture(posture.stance);
    }

    public void SetPosture(string animationName) {
        Animator.SetTrigger(animationName);
        //Animator.Play(animationName);
    }
}
