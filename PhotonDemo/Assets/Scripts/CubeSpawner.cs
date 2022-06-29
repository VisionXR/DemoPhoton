using Photon.Pun;
using UnityEngine;

public class CubeSpawner : MonoBehaviourPun
{
    
    Rigidbody rb;
    [SerializeField] private float CubeForce = 13;
    [PunRPC]
    public void SpawnCube(byte id)
    {
       
        if(id == 1 && PhotonNetwork.IsMasterClient)
       {
            GameObject cube = PhotonNetwork.Instantiate("Cube", transform.position, transform.rotation);
            rb = cube.GetComponent<Rigidbody>();
            rb.AddForce(transform.forward * CubeForce, ForceMode.VelocityChange);
            cube.GetComponent<Renderer>().material.color = Color.red;
          
       }
        else if(id ==2 && !PhotonNetwork.IsMasterClient)
        {
            GameObject cube = PhotonNetwork.Instantiate("Cube", transform.position, transform.rotation);
            rb = cube.GetComponent<Rigidbody>();
            rb.AddForce(transform.forward * CubeForce, ForceMode.VelocityChange);
            cube.GetComponent<Renderer>().material.color = Color.blue;
        }

    }

    public void GenerateSpawnEvent(int id)
    {
        PhotonView photonView = PhotonView.Get(this);
        photonView.RPC("SpawnCube", RpcTarget.All, (byte)id);
    }
}
