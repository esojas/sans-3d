using UnityEngine;

public class SansDodge : MonoBehaviour
{
    public int sansDodgeAmt;
    public bool isDodging;
    [SerializeField] private Transform dodgeLocation;
    [SerializeField] private float dodgeSpeed;
    [SerializeField] private GameObject parentObject;

    private void DodgePlayer() 
    {
        if (!isDodging) return;
        transform.SetParent(parentObject.transform);
        transform.position = Vector3.Lerp(transform.position, dodgeLocation.position, dodgeSpeed*Time.deltaTime);
        Debug.LogWarning("DODGING!");
    }

    private void CheckSansPosition()
    {
        if (transform.position == dodgeLocation.position) isDodging = false;
    }

    Transform FindChildWithTag(Transform parent, string tag)
    {
        foreach(Transform child in parent)
        {
            if (child.CompareTag(tag)) return child;

            Transform found = FindChildWithTag(child, tag);
            if(found != null) return found;
        }
        return null;
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        //DodgePlayer();
    }

    // Update is called once per frame
    void Update()
    {
        CheckSansPosition();
        DodgePlayer();
    }
}
