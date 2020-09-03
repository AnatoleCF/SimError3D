using AssetPackage;
using BMLRealizer;
using System.Collections;
using System.Collections.Generic;
using System.Xml;
using UnityEngine;

public class BMLListener : MonoBehaviour {
    [HideInInspector]
    public RageBMLRealizer BmlRealizer = null;


    [Header("References")]
    [SerializeField] private GameObject _Character = null;
    
    [Header("Parameters")]
    [SerializeField] private bool _DebugMode = true;

    [Header("Events")]
    public BMLFaceEvent m_FaceEvent = null;
    public BMLPostureEvent m_PostureEvent = null;
    public BMLSpeechEvent m_SpeechEvent = null;

    void Start() {
        if(_Character == null)
            _Character = gameObject;

        // Register this listener to the broadcast receiver.
        AgentSlangBroadcastReceiver.Instance.RegisterBmlListener(this);
    }
    
    /// <summary>
    /// Called upon when the broadcast receiver completes a Sync Point.
    /// Handles the received BMLBlocks.
    /// </summary>
    /// <param name="behaviorID"></param>
    /// <param name="eventName"></param>
    public void SyncPointCompleted(string behaviorID, string eventName) {
        if(_DebugMode)
            Debug.Log($"Sync Point Completed ({name}).");

        BMLBlock block = BmlRealizer.GetBehaviorFromId(behaviorID);
        if(block == null)
            return;

        GameObject character = GameObject.Find(block.getCharacterId());
        if(character == null)
            character = _Character;

        if(_DebugMode)
            Debug.Log(block.getCharacterId() + " " + behaviorID + " " + eventName);

        HandleFace(block, character, behaviorID, eventName);
        HandleGaze(block, character, behaviorID, eventName);
        HandleGesture(block, character, behaviorID, eventName);
        HandleHead(block, character, behaviorID, eventName);
        HandlePosture(block, character, behaviorID, eventName);
        HandleSpeech(block, character, behaviorID, eventName);

    }

    /// <summary>
    /// Handles all BMLblock related to the agent's face.
    /// </summary>
    /// <param name="block"></param>
    /// <param name="character"></param>
    /// <param name="behaviorID"></param>
    /// <param name="eventName"></param>
    protected virtual void HandleFace(BMLBlock block, GameObject character, string behaviorID, string eventName) {
        if(_DebugMode)
            Debug.Log($"{name} : Handling Face.");
        if(block is BMLFace) {
            BMLFace face = (BMLFace)block;
            m_FaceEvent.Invoke(face);
        }
        if(block is BMLFaceFacs) {
            BMLFaceFacs face = (BMLFaceFacs)block;
        }
        if(block is BMLFaceLexeme) {
            BMLFaceLexeme face = (BMLFaceLexeme)block;
        }
        if(block is BMLFaceShift) {
            BMLFaceShift face = (BMLFaceShift)block;
        }
    }

    /// <summary>
    /// Handles all BMLBlocks related to the agent's gaze.
    /// </summary>
    /// <param name="block"></param>
    /// <param name="character"></param>
    /// <param name="behaviorID"></param>
    /// <param name="eventName"></param>
    protected virtual void HandleGaze(BMLBlock block, GameObject character, string behaviorID, string eventName) {
        if(_DebugMode)
            Debug.Log($"{name} : Handling Gaze.");
        if(block is BMLGaze) {
            BMLGaze gaze = (BMLGaze)block;

            //GameObject target = GameObject.Find(gaze.target);
            //if(target != null) {
            //    HeadLookController controller = character.GetComponent<HeadLookController>();
            //    if(controller != null) {
            //        // setting parameter for behaviour
            //        controller.targetNode = target.transform;

            //        // setting parameter for callback
            //        controller.SetBMLNetParam(block.getCharacterId(), behaviorID, eventName);

            //        // add callback
            //        controller.OnBehaviourCompleted += SetTrigger;
            //    }
            //}

        }
        if(block is BMLGazeShift) {
            BMLGazeShift gazeShift = (BMLGazeShift)block;
        }
    }

    /// <summary>
    /// Handles all BMLBlocks related to the agent's gestures.
    /// </summary>
    /// <param name="block"></param>
    /// <param name="character"></param>
    /// <param name="behaviorID"></param>
    /// <param name="eventName"></param>
    private void HandleGesture(BMLBlock block, GameObject character, string behaviorID, string eventName) {
        if(_DebugMode)
            Debug.Log($"{name} : Handling Gesture.");
        if(block is BMLGesture) {
            BMLGesture gesture = (BMLGesture)block;

            //Animator animator = character.GetComponentInChildren<Animator>();

            //if(gesture.id == "gesture1")
            //    animator.SetTrigger("Show");


        } else if(block is BMLPointing) {
            BMLPointing pointing = (BMLPointing)block;

        }
    }

    /// <summary>
    /// Handles all BMLBlocks related to the agent's head.
    /// </summary>
    /// <param name="block"></param>
    /// <param name="character"></param>
    /// <param name="behaviorID"></param>
    /// <param name="eventName"></param>
    private void HandleHead(BMLBlock block, GameObject character, string behaviorID, string eventName) {
        if(_DebugMode)
            Debug.Log($"{name} : Handling Head.");
        if(block is BMLHead) {
            BMLHead head = (BMLHead)block;


        } else if(block is BMLHeadDirectionShift) {
            BMLHeadDirectionShift headDirectionShift = (BMLHeadDirectionShift)block;

        }
    }

    /// <summary>
    /// Handles all BMLBlocks related to the agent's posture.
    /// </summary>
    /// <param name="block"></param>
    /// <param name="character"></param>
    /// <param name="behaviorID"></param>
    /// <param name="eventName"></param>
    private void HandlePosture(BMLBlock block, GameObject character, string behaviorID, string eventName) {
        if(_DebugMode)
            Debug.Log($"{name} : Handling Posture.");
        if(block is BMLLocomotion) {
            BMLLocomotion locomotion = (BMLLocomotion)block;
        }
        if(block is BMLPosture) {
            BMLPosture posture = (BMLPosture)block;
            m_PostureEvent.Invoke(posture);
        }
        if(block is BMLPostureShift) {
            BMLPostureShift postureShift = (BMLPostureShift)block;

        }
        if(block is BMLStance) {
            BMLStance stance = (BMLStance)block;

        }
        if(block is BMLPose) {
            BMLPose pose = (BMLPose)block;

        }
    }

    /// <summary>
    /// Handles all BMLBlocks related to the agent's speech.
    /// </summary>
    /// <param name="block"></param>
    /// <param name="character"></param>
    /// <param name="behaviorID"></param>
    /// <param name="eventName"></param>
    private void HandleSpeech(BMLBlock block, GameObject character, string behaviorID, string eventName) {
        if(_DebugMode)
            Debug.Log($"{name} : Handling Speech.");
        if(block is BMLSpeech) {
            if(_DebugMode)
                Debug.Log($"{name} : Received a BlockSpeech ({ behaviorID} / {eventName}).");

            BMLSpeech speech = (BMLSpeech)block;
            m_SpeechEvent.Invoke(speech);
        }
    }

    //public void SetTrigger(BMLNetBehaviour obj, string characterId, string behaviorId, string eventName) {
    //    // remove the callback
    //    obj.OnBehaviourCompleted -= SetTrigger;

    //    // trigger BMLNet callback
    //    BmlRealizer.TriggerSyncPoint(behaviorId, eventName);
    //}

}
