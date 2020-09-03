using BMLRealizer;
using CrazyMinnow.SALSA;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(BMLListener))]
public class SlangCharacter : MonoBehaviour {
    [Header("References")]
    [SerializeField] protected AudioSource _AudioSource = null;
    [SerializeField] protected Salsa _Salsa = null;
    [SerializeField] protected BlendShapeController _BlendShapeController = null;
    [SerializeField] protected PostureController _PostureController = null;

    [Header("Parameters")]
    [SerializeField] protected bool _DebugMode = false;


    protected BMLListener BMLListener;

    protected virtual void Awake() {

        // References auto gathering.
        if(_AudioSource == null)
            _AudioSource = GetComponentInChildren<AudioSource>();
        if(_Salsa == null) {
            _Salsa = GetComponentInChildren<Salsa>();
            _Salsa.audioSrc = _AudioSource;
        }
        if(_BlendShapeController == null)
            _BlendShapeController = GetComponentInChildren<BlendShapeController>();
        if(_PostureController == null)
            _PostureController = GetComponentInChildren<PostureController>();

        // Plug in BML callbacks.
        BMLListener = GetComponent<BMLListener>();

        BMLListener.m_SpeechEvent.AddListener(OnSpeechReceived);
        BMLListener.m_FaceEvent.AddListener(OnFaceReceived);
        BMLListener.m_PostureEvent.AddListener(OnPostureReceived);
    }

    /// <summary>
    /// Meant to be called as a BMLFaceEvent.
    /// Process a BMLFace block.
    /// </summary>
    /// <param name="face">The BMLFace block to process.</param>
    protected virtual void OnFaceReceived(BMLFace face) {
        _BlendShapeController.SetWeight(face.au, face.amount);
    }

    /// <summary>
    /// Meant to be called as a BMLPostureEvent.
    /// Process a BMLPosture block.
    /// </summary>
    /// <param name="posture">The BMLPosture block to process.</param>
    private void OnPostureReceived(BMLPosture posture) {
        _PostureController.SetPosture(posture);
    }

    /// <summary>
    /// Meant to be called as a BMLSpeechEvent.
    /// Process a BMLSpeech block.
    /// </summary>
    /// <param name="speech">The BMLSpeech block to process.</param>
    protected virtual void OnSpeechReceived(BMLSpeech speech) {
        StartCoroutine(ProcessSpeechBlock(speech));
    }

    /// <summary>
    /// Coroutine recovering the audio file of the given BMLSpeech block, playing it and ensuring the LipSync.
    /// </summary>
    /// <param name="speech"></param>
    protected virtual IEnumerator ProcessSpeechBlock(BMLSpeech speech) {
        if(_DebugMode)
            Debug.Log($"{this} : Received BML Speech ({speech.fileName}).");

        WWW audioLoader = new WWW("file:///" + speech.fileName);
        yield return new WaitUntil(() => audioLoader.isDone);

        if(_DebugMode)
            Debug.Log($"{this} : AudioSource ready.");

        _AudioSource.clip = audioLoader.GetAudioClip(false, false, AudioType.WAV);
        yield return new WaitUntil(() => _Salsa.configReady);

        if(_DebugMode)
            Debug.Log($"{this} : Salsa ready.");

        _Salsa.audioSrc.Play();
    }
}
