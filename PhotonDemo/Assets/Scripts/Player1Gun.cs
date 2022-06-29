using UnityEngine;
using Photon.Pun;

public class Player1Gun : MonoBehaviourPunCallbacks
{
    private float AngleIncrement = 5f;
    public override void OnEnable()
    {
        Inputmanager.instance.ButtonClicked += OnButtonClicked;
    }

    public override void OnDisable()
    {
        Inputmanager.instance.ButtonClicked -= OnButtonClicked;
    }

    public void OnButtonClicked(int Buttonid)
    {
        if (PhotonNetwork.IsMasterClient)
        {
            if (Buttonid == 3)
            {
                transform.rotation = Quaternion.Euler(transform.eulerAngles + new Vector3(0, -AngleIncrement, 0));
            }
            else if (Buttonid == 4)
            {
                transform.rotation = Quaternion.Euler(transform.eulerAngles + new Vector3(0, +AngleIncrement, 0));
            }
            else if (Buttonid == 1)
            {
                transform.rotation = Quaternion.Euler(transform.eulerAngles + new Vector3(-AngleIncrement, 0, 0));
            }
            else if (Buttonid == 2)
            {
                transform.rotation = Quaternion.Euler(transform.eulerAngles + new Vector3(AngleIncrement, 0, 0));
            }
            else if(Buttonid == 5)
            {

                GameManager.instance.FireBullet(2);
            }
        }
    }
   
}
