using UnityEngine;

public class DestroyCube : MonoBehaviour
{

    public void OnCollisionEnter(Collision collision)
    {
        if(collision.collider.gameObject.tag == "Ground")
        {
           // Destroy(gameObject);
        }
    }
}
