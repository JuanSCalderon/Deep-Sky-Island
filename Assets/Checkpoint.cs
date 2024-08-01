using UnityEngine;

public class Checkpoint : MonoBehaviour
{
    private void OnCollisionEnter(Collision collision)
    {

        if (collision.gameObject.CompareTag("Player"))
        {
            GameManager.Instance.lastCheckPointPos = transform.position;            
        }
    }
}
