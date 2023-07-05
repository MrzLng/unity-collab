using Unity.Netcode;
using UnityEngine;

namespace HelloWorld
{
    public class NetworkPlayerManager : NetworkBehaviour
    {
        public NetworkVariable<Vector3> Position = new NetworkVariable<Vector3>();

        
    }
}