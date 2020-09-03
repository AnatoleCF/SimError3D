
using AssetPackage;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Xml;
using UnityEngine;

public class AgentSlangBroadcastReceiver : MonoBehaviour {
    public static AgentSlangBroadcastReceiver Instance;

    [Header("Parameters")]
    [SerializeField] private bool _DebugMode = false;
    [SerializeField] private bool _AutoMode = false;
    
    [SerializeField] private int _Port = 4010;

    BroadcastReceiver receiver;

    RageBMLRealizer BmlRealizer = new RageBMLRealizer();

    #region MonoBehaviour

    private void Awake() {
        Instance = this;
    }
    
    private void OnEnable() {
        if(_AutoMode)
            BeginReceive();
    }

    private void OnDisable() {
        EndReceive();
    }

    void Update() {
        BmlRealizer.Update(Time.deltaTime);
    }

    #endregion

    #region Broadcast

    /// <summary>
    /// Starts receiving messages from the MARCSocket.
    /// </summary>
    public void BeginReceive() {
        receiver = new BroadcastReceiver();
        receiver.MessageReceived += OnMessageReceived;
        receiver.Receive(_Port);
    }

    /// <summary>
    /// Ends the message reception.
    /// </summary>
    public void EndReceive() {
        receiver.Dispose();
    }

    /// <summary>
    /// Callback called when a messsage is received.
    /// Parses and processes the MarcBML received.
    /// </summary>
    /// <param name="message"></param>
    /// <param name="remoteEndPoint"></param>
    private void OnMessageReceived(string message, IPEndPoint remoteEndPoint) {
        string cleanedBml = MarcBMLFormater.ProcessMarcBML(message);
        BmlRealizer.ParseFromString(cleanedBml);
    }

    /// <summary>
    /// Registers the given BMLListener to the message receiver.
    /// </summary>
    /// <param name="listener"></param>
    public void RegisterBmlListener(BMLListener listener) {
        listener.BmlRealizer = BmlRealizer;
        BmlRealizer.OnSyncPointCompleted += listener.SyncPointCompleted;

        if(_DebugMode)
            Debug.Log($"Registering {listener.name}.");
    }

    #endregion

}
