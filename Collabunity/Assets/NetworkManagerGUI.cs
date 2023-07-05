using System.Net;
using System.Net.Sockets;
using Unity.Netcode;
using Unity.Netcode.Transports.UTP;
using UnityEngine;

namespace HelloWorld
{
    public class NetworkManagerGUI : MonoBehaviour
    {
        public string connectedIP;
        void OnGUI()
        {
            GUI.skin.label.fontSize = 50;
            GUI.skin.button.fontSize = 50;
            GUI.skin.textField.fontSize = 50;
            GUILayout.BeginArea(new Rect(100, 100, 1000, 1000));
            if (!NetworkManager.Singleton.IsClient && !NetworkManager.Singleton.IsServer)
            {
                StartButtons();
            }
            else
            {
                StatusLabels();
            }

            GUILayout.EndArea();
        }

        void StartButtons()
        {
            if (GUILayout.Button("Host", GUILayout.Height(100))) { NetworkManager.Singleton.StartHost(); }
            if (GUILayout.Button("Client", GUILayout.Height(100)))
            {
                Debug.Log(connectedIP);
                NetworkManager.Singleton.GetComponent<UnityTransport>().ConnectionData.Address = connectedIP;
                NetworkManager.Singleton.StartClient();
            }
            if (GUILayout.Button("Server", GUILayout.Height(100))) { NetworkManager.Singleton.StartServer(); }
            GUILayout.Label("For clients, enter an IP to connect:", GUILayout.Height(100));
            connectedIP = GUILayout.TextField(connectedIP, 15, GUILayout.Height(100));
        }

        void StatusLabels()
        {
            var mode = NetworkManager.Singleton.IsHost ?
                "Host" : NetworkManager.Singleton.IsServer ? "Server" : "Client";

            GUILayout.Label("Transport: " +
                NetworkManager.Singleton.NetworkConfig.NetworkTransport.GetType().Name);
            GUILayout.Label("Mode: " + mode);
            GUILayout.Label("Connected IP Address: " + ((NetworkManager.Singleton.IsHost || NetworkManager.Singleton.IsServer) ? getIP() : connectedIP));
        }

        public string getIP()
        {
            var host = Dns.GetHostEntry(Dns.GetHostName());
            foreach (var ip in host.AddressList)
            {
                if (ip.AddressFamily == AddressFamily.InterNetwork)
                {
                    return ip.ToString();
                }
            }
            throw new System.Exception("No network adapters with an IPv4 address in the system!");
        }
    }
}