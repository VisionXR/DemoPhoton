using UnityEngine;
using Photon.Pun;
using UnityEngine.UI;
using System.Collections;

public class GameManager : MonoBehaviourPunCallbacks
{
    public static GameManager instance;
    [SerializeField ]private Text debug;
    private GameObject Player1, Player2, p1, p2;
    [SerializeField] private GameObject CubeSpawner;

    private void Awake()
    {
        instance = this;
    }
    void Start()
    {
       
        if (PhotonNetwork.IsConnected && PhotonNetwork.IsMasterClient)
        {
            Player1 = PhotonNetwork.Instantiate("Player1Gun", new Vector3(0,0.5f,1), Quaternion.identity);
           
        }
        else if(PhotonNetwork.IsConnected && !PhotonNetwork.IsMasterClient)
        {
            Player2 = PhotonNetwork.Instantiate("Player2Gun", new Vector3(0, 0.5f, 1), Quaternion.identity);       
        }
        StartCoroutine(DoDelay());
    }
    

    [PunRPC]
    public void WhoseTurn(byte myParameter)
    {
       
        int id = myParameter;
        if (id == 1)
        {
            debug.text = PhotonNetwork.MasterClient.NickName + " Turn";       
            StartCoroutine(Delay(1));
            p1.SetActive(true);
            p2.SetActive(false);
        }
        else if(id == 2)
        {
            if (PhotonNetwork.IsMasterClient)
            {
                debug.text = PlayerPrefs.GetString("Client") + " Turn";
            }
            else
            {
                debug.text = PhotonNetwork.LocalPlayer.NickName + " Turn";
            }
            StartCoroutine(Delay(2));
            p2.SetActive(true);
            p1.SetActive(false);

        }
    
    }
    private IEnumerator Delay(int id)
    {
        yield return new WaitForSeconds(5);
        debug.text = "";
        if (id == 1 && PhotonNetwork.IsMasterClient)
        {
            CubeSpawner.GetComponent<CubeSpawner>().GenerateSpawnEvent(id);
        }
        else if(id == 2 && !PhotonNetwork.IsMasterClient)
        {
            CubeSpawner.GetComponent<CubeSpawner>().GenerateSpawnEvent(id);
        }
    }

    private IEnumerator DoDelay()
    {
        yield return new WaitForSeconds(1);
        p1 = GameObject.FindGameObjectWithTag("P1");
        p2 = GameObject.FindGameObjectWithTag("P2");
        if(PhotonNetwork.IsMasterClient)
        {
            WhoseTurnEvent(1);
        }
    }

    public void WhoseTurnEvent(int id)
    {
        PhotonView photonView = PhotonView.Get(this);     
        photonView.RPC("WhoseTurn", RpcTarget.All, (byte)id);
       
    }

    public void FireBullet(int id)
    {
        AudioManager.instance.onBulletSoundPlayed();
        GameObject bp = GameObject.Find("BulletPos");
        GameObject bullet = PhotonNetwork.Instantiate("Bullet", bp.transform.position, bp.transform.rotation);
        bullet.GetComponent<Rigidbody>().AddForce(bp.transform.forward * 40, ForceMode.VelocityChange);
        StartCoroutine(WaitFew(id));
    }

    private IEnumerator WaitFew(int id)
    {
        yield return new WaitForSeconds(5);
        PhotonView photonView = PhotonView.Get(this);
        photonView.RPC("WhoseTurn", RpcTarget.All, (byte)id);
    }
}
