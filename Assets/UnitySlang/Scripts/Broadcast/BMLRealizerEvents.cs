using BMLRealizer;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

// Regrouping BML related custom event classes.
// We use them to handle specific BMLBlocks data within unity.
// (Unity uses UnityEvent to pass data dynamically.)

[System.Serializable]
public class BMLSpeechEvent : UnityEvent<BMLSpeech> { }

[System.Serializable]
public class BMLFaceEvent : UnityEvent<BMLFace> { }

[System.Serializable]
public class BMLPostureEvent : UnityEvent<BMLPosture> { }
