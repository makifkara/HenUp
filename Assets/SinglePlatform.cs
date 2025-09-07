using UnityEngine;

public class SinglePlatform : MonoBehaviour
{
    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            PlatformSpawner ps = GameManager.Instance.gameObject.GetComponent<PlatformSpawner>();
            if (ps != null)
            {
                if (collision.gameObject.GetComponent<Rigidbody2D>().linearVelocityY < 0)
                {
                    ps.PutThisPlatformBack(this.gameObject);
                    Debug.Log("Single Platform put back!!");
                }


            }
        }
    }
}
