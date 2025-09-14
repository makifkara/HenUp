using UnityEngine;

public class SinglePlatform : MonoBehaviour
{
    bool used = false;
    PlatformSpawner ps;
    private void Start()
    {
        ps = GameManager.Instance.gameObject.GetComponent<PlatformSpawner>();
    }
    public bool IsUsed()
    {
        return used;
    }
    public void UsedIt()
    {
        used = true;
        ps.PutThisPlatformBack(this.gameObject);
        Debug.Log("Single Platform put back!!");
        used = false;
    }
    void Update()
    {



    }

}
