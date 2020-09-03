using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(SkinnedMeshRenderer))]
public class BlendShapeController : MonoBehaviour {
    [SerializeField, Header("References")]
    private SkinnedMeshRenderer SkinnedMeshRenderer;

    [SerializeField, Header("Parameters")]
    protected bool _DebugMode = false;

    private void Start() {
        if(SkinnedMeshRenderer == null)
            SkinnedMeshRenderer = GetComponent<SkinnedMeshRenderer>();
    }

    public void SetWeight(string id, float value) {
        if(_DebugMode)
            Debug.Log($"Face received : id = {id}, value = {value}");

        if(id == "ALL") {
            SetAllWeight(value);
        } else {
            SkinnedMeshRenderer.SetBlendShapeWeight(int.Parse(id), value);
        }
    }

    public void SetAllWeight(float value) {
        for(int i = 0; i < SkinnedMeshRenderer.sharedMesh.blendShapeCount; i++) {
            SkinnedMeshRenderer.SetBlendShapeWeight(i, value);
        }
    }

}
