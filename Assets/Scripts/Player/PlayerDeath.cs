using System;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerDeath : MonoBehaviour
{
    //[SerializeField] private GameObject deathParticle;
    [SerializeField] private bool playerCanDie;
    [SerializeField] private float playerLifetime;
    [SerializeField] private Renderer playerRenderer;
    public float timeRemaining { get; private set; }
    private Rigidbody rb;
    private Collider playerCollider;
    private PlayerMovement playerMovement;

    private bool isDead = false;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer == 3) // 3 means harmfullobstacles
        {
            HandleDeath();
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        // Catches solid objects without "Is Trigger" checked
        if (collision.gameObject.layer == 3)
        {
            HandleDeath();
        }
    }

    private void HandleDeath()
    {
        if (isDead || !playerCanDie)
        {
            return;
        }

        Debug.Log("death");
        isDead = true;
        rb.useGravity = false;
        rb.linearVelocity = Vector3.zero;
        playerCollider.enabled = false;
        playerRenderer.enabled = false;
        // do something to hide player?

        //Debug.Log(deathParticle == null ? "NULL REF" : "Ref OK, playing");

        //Instantiate(deathParticle, this.transform.position, Quaternion.identity);

        playerMovement.deathThisFrame = true;

        StartCoroutine(WaitAndRespawn());
    }



    private IEnumerator WaitAndRespawn()
    {
        yield return new WaitForEndOfFrame();

        Respawn();

        //recorder.StartNewRecording();
    }



    private void Respawn()
    {
        isDead = false;


        rb.linearVelocity = Vector3.zero;


        rb.useGravity = true;
        playerCollider.enabled = true;
        playerRenderer.enabled = true;

        timeRemaining = playerLifetime;

        //GameEventsManager.Instance.GoalReached();

        //PausedControl.Instance.TogglePause();
        //SceneManager.LoadScene("Level1"); // currentlevel
    }

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
        playerCollider = GetComponent<Collider>();
        playerMovement = GetComponent<PlayerMovement>();
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

        timeRemaining = playerLifetime;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
