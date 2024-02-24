using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StormController : MonoBehaviour
{
    public LayerMask playerLayer;

    [SerializeField]private PlayerController playerC;   
    [SerializeField]private float stormSpeed;
    [SerializeField]private GameManager GM;

    private Vector3 stormVel;
    private Rigidbody rb;
    private bool isPlayerHit;
    // Start is called before the first frame update
    void Start()
    {
        stormVel = new Vector3(stormSpeed, 0, 0);
        rb = GetComponent<Rigidbody>();
        isPlayerHit = false;
    }

    // Update is called once per frame
    void Update()
    {
        // Move storm
        moveStorm();
    }

    void moveStorm() {
        if(!isPlayerHit) {
            rb.velocity = stormVel;
        }
        else {
            rb.velocity = Vector3.zero;
        }
    }
    
    private void OnTriggerEnter(Collider other) {
        if(other.tag == "Player") {
            stormHit();
        }
    }

    private void stormHit() {
        isPlayerHit = true;
        GM.isStageFailed = true;
        Debug.Log("STORM HIT!!");
    }
}
