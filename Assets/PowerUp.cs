using UnityEngine;

public class PowerUp : MonoBehaviour
{
    [SerializeField] private float power;
    [SerializeField] private Vector3 powerUpVector;
    [SerializeField] private PowerUpType type;
    int counter = 0;
    public enum PowerUpType
    {
        Spring,
        Tramboline,
        Rocket,

    }
    void OnTriggerEnter2D(Collider2D collision)
    {

    }
    void OnTriggerExit2D(Collider2D collision)
    {
        if (counter != 0)
        {
            return;
        }
        if (collision.gameObject.tag == "Player")
        {
            PlayerMovement pm;
            collision.gameObject.TryGetComponent<PlayerMovement>(out pm);
            if (pm != null)
            {
                pm.PowerItUp(powerUpVector * power);
                Debug.Log("You got effected by a " + type.ToString());
                counter++;
                GetComponent<Collider2D>().enabled = false;
                transform.localScale = new Vector3(transform.localScale.x, transform.localScale.y * 2, transform.localScale.z);
                AudioManager.Instance.PlaySFX(AudioManager.SFXClip.spring);
            }
        }
    }
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }
}
