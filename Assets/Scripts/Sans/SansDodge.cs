using Unity.VisualScripting;
using UnityEngine;

public class SansDodge : MonoBehaviour
{
    public int sansDodgeAmt;
    public bool isDodging;
    [SerializeField] private Transform dodgeLocation;
    [SerializeField] private float dodgeSpeed;
    [SerializeField] private GameObject parentObject;
    private int floorCount = -1;

    public void StartDodge()
    {
        if (isDodging) return; 

        floorCount++;
        parentObject = GameObject.Find($"Ground_{floorCount}");

        if (parentObject == null)
        {
            Debug.LogError($"Ground_{floorCount} not found!");
            floorCount--; 
            return;
        }

        dodgeLocation = FindChildWithTag(parentObject.transform, "Sans-Pos");
        transform.SetParent(parentObject.transform);
        isDodging = true;
    }

    private void DodgePlayer()
    {
        if (!isDodging) return;
        transform.position = Vector3.Lerp(transform.position, dodgeLocation.position, dodgeSpeed * Time.deltaTime);
        if (Vector3.Distance(transform.position, dodgeLocation.position) < 0.01f)
        {
            transform.position = dodgeLocation.position; 
            isDodging = false;
            Debug.Log($"[DodgePlayer] Arrived, isDodging=false at frame={Time.frameCount}");
        }
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
        isDodging = false;
        transform.SetParent(parentObject.transform);
    }

    // Update is called once per frame
    void Update()
    {
        DodgePlayer();
    }
}
