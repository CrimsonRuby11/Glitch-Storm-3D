using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    [SerializeField]private float attackMoveDistance = 1f;
    [SerializeField]private float dashSpeed = 1f;
    [SerializeField]private float dashCooldown = 1f;
    [SerializeField]private float attackCooldown = 0.25f;
    [SerializeField]private float dashTime = 0.5f;
    [SerializeField]private float attackRange = 0f;
    [SerializeField]private float debugSpeed;

    [SerializeField]private List<GameObject> slashes;

    [SerializeField]private Transform attackPoint;
    [SerializeField]private LayerMask stormLayer;
    [SerializeField]private GameObject stormG;
    [SerializeField]private GameManager GM;
    [SerializeField]private MeterController meterController;

    public LayerMask enemyLayers;

    private Rigidbody rb;

    Vector3 dir;
    private int slashCount;
    private bool canAttack;
    private bool canDash;
    private bool isDashing;

    private bool isStormHit;
    

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        canAttack = true;
        canDash = true;
        isDashing = false;
        isStormHit= false;
        slashCount = 0;
    }

    
    void Update()
    {
        if(!meterController.isPaused()) {
            if(isStormHit) {
                return;
            }

            UpdateDir();
            UpdatePlayerFace();
        }
    }

    void FixedUpdate() {
        if(!meterController.isPaused()) {
            if(isDashing || isStormHit) {
                return;
            }

            DebugMovement();
        }
    }

    void DebugMovement() {
        float moveH = Input.GetAxisRaw("Horizontal");
        float moveV = Input.GetAxisRaw("Vertical");
        rb.velocity = new Vector3(moveH * debugSpeed, rb.velocity.y, moveV * debugSpeed);
    }

    void slash() {
        slashes[slashCount].SetActive(true);        
    }

    public void Attack(InputAction.CallbackContext context) {
        

        if(context.started && canAttack && !isDashing && !meterController.isPaused()) {
            StartCoroutine(AttackCR());

            Collider[] hitEnemies = Physics.OverlapSphere(attackPoint.position, attackRange, enemyLayers);

            foreach(Collider enemy in hitEnemies) {
                Debug.Log("ENEMY ATTACKED");
                Destroy(enemy.gameObject);
            }
        }

        
    }

    public void Dash(InputAction.CallbackContext context) {
        if(context.started && canDash && !meterController.isPaused()) {
            StartCoroutine(DashCR());

            meterController.startDashMeter();
        }
    }

    void UpdateDir() {
        if(Input.mousePosition.x > Screen.width/2) {
            if(Input.mousePosition.y > Screen.height/2) {
                dir = new Vector3(1, 0, 0).normalized;
                // dirnum = 0;
            }
            else {
                dir = new Vector3(0, 0, -1).normalized;
                // dirnum = 3;
            }
        }
        else {
            if(Input.mousePosition.y > Screen.height/2) {
                dir = new Vector3(0, 0, 1).normalized;
                // dirnum = 1;
            }
            else {
                dir = new Vector3(-1, 0, 0).normalized;
                // dirnum = 2;
            }
        }
    }

    void UpdatePlayerFace() {
        transform.forward = dir;
    }

    private IEnumerator AttackCR() {

        // Animate and Attack
        if(canAttack) {
            rb.MovePosition(transform.position + dir*attackMoveDistance);
            slash();
            canAttack = false;
        }

        yield return new WaitForSeconds(attackCooldown);
        canAttack = true;
        slashes[slashCount++].SetActive(false);
        if(slashCount == 3) slashCount = 0;
        
    }

    private IEnumerator DashCR() {

        canDash = false;
        isDashing = true;

        rb.velocity = transform.forward * dashSpeed;
        meterController.setDashIcon(true);
        
        yield return new WaitForSeconds(dashTime);

        isDashing = false;
        rb.velocity = Vector3.zero;

        yield return new WaitForSeconds(dashCooldown);

        canDash = true;
        meterController.setDashIcon(false);
        
    }

    public void setCanDash(bool b) {
        canDash = b;
    }

}

