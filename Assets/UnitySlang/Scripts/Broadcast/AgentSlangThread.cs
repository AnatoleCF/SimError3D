using Sirenix.OdinInspector;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Threading;
using UnityEngine;

/// <summary>
/// A simple AgentSlang autorun called from within Unity.
/// Starts AgentSlang when the Scene starts.
/// NOTE : Penser à changer le chemin vers AgentSlang pour la version Editor.
/// </summary>
public class AgentSlangThread : MonoBehaviour {

    private Process process;

    [SerializeField] private bool _AutoLaunch = false;

    //[SerializeField] private string _AgentSlangFolder = "C:/Users/Anatole/Desktop/AgentSlang/AgentSlang/bin";
    [SerializeField] private string _ConfigName = "config-simerror-1";
    [SerializeField] private string _ProfileName = "profile1";

    private void Start() {
        if(_AutoLaunch) {
//#if UNITY_EDITOR
//            process = Process.Start("CMD.exe", $"/C UnitySlang.bat {_AgentSlangFolder} {_ConfigName} {_ProfileName}");
//#else
//            process = Process.Start("CMD.exe", $"/C UnitySlang.bat AgentSlang/bin {_ConfigName} {_ProfileName}");
//#endif
            process = Process.Start("CMD.exe", $"/C UnitySlang.bat AgentSlang/bin {_ConfigName} {_ProfileName}");
        }
    }

    private void OnDisable() {
        KillProcess();
    }



    [Button]
    private void KillProcess() {

        Process.Start("CMD.exe", "/C UnitySlangExit.bat");
    }
}
