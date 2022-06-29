using UnityEngine;
using Photon.Pun;

public class CameraPosition : MonoBehaviourPunCallbacks
{
    public static CameraPosition instance;
    private void Awake()
    {
        instance = this;
    }

    public void SetCameraPosition()
    {
        if(PhotonNetwork.IsConnected && PhotonNetwork.IsMasterClient)
        {
            Camera.main.transform.position = new Vector3(0, 1, 0);
            Camera.main.transform.eulerAngles = new Vector3(0, 0, 0);
        }
        else if (PhotonNetwork.IsConnected && !PhotonNetwork.IsMasterClient)
        {
            Camera.main.transform.position = new Vector3(0, 1, 21);
            Camera.main.transform.eulerAngles = new Vector3(0, 180, 0);
        }
    }
}
