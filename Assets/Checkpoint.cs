using UnityEngine;

public class Checkpoint : MonoBehaviour
{
    private void OnCollisionEnter(Collision collision)
    {

        if (collision.gameObject.CompareTag("Player"))
        {
            AudioManager.Instance.PlayMusic("GamePlayMusic2");
            GameManager.Instance.lastCheckPointPos = transform.position;
        }
    }
}
