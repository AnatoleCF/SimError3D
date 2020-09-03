using System.Text;
using System;
using System.Net;
using UnityEngine;
using System.Net.Sockets;
using System.Threading;

/// <summary>
/// Simple socket listener.
/// </summary>
public class BroadcastReceiver : IDisposable {

    public delegate void AddOnMessageReceivedDelegate(string message, IPEndPoint remoteEndpoint);
    public event AddOnMessageReceivedDelegate MessageReceived;

    private void OnMessageReceivedEvent(string message, IPEndPoint remoteEndpoint) {
        if(MessageReceived != null)
            MessageReceived(message, remoteEndpoint);
    }

    private Thread _ReadThread;
    private UdpClient _Socket;


    public void Receive(int port) {
        // Create thread for reading UDP messages
        _ReadThread = new Thread(new ThreadStart(delegate {
            try {
                _Socket = new UdpClient(port);
                Debug.LogFormat("[Broadcast] Receiving on port {0}", port);
            } catch(Exception err) {
                Debug.LogError(err.ToString());
                return;
            }
            while(true) {
                Debug.Log($"[Broadcast] Waiting for response from port {port}...");
                try {
                    // receive bytes
                    IPEndPoint anyIP = new IPEndPoint(IPAddress.Any, 0);
                    byte[] data = _Socket.Receive(ref anyIP);

                    // encode UTF8-coded bytes to text format
                    string message = Encoding.UTF8.GetString(data);
                    OnMessageReceivedEvent(message, anyIP);
                    Debug.Log($"[Broadcast] Message received from port {port} : \n" + message);
                } catch(Exception err) {
                    Debug.LogError(err.ToString());
                }
            }
        }));
        _ReadThread.IsBackground = true;
        _ReadThread.Start();
    }

    public void Dispose() {
        if(_ReadThread.IsAlive) {
            _ReadThread.Abort();
        }
        if(_Socket != null) {
            _Socket.Close();
            _Socket = null;
        }
    }
}
