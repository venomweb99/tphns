using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using Unity.Netcode;


public class PlayerController : NetworkBehaviour
{
    #region Variables
    [SerializeField]
    private float currentSpeed = 0.0f;
    private float maxSpeed = 10.0f;
    private float acceleration = 1.0f;
    private float dashForce = 20.0f;
    private float pushForce = 20.0f;
    private float pushUpForce = 20.0f;
    public float jumpForce = 10.0f;
    private float dashCD = 3.0f;
    private float dashTimer = 0.0f;
    public float attackCD = 1.0f;
    public float attackTimer = 0.0f;
    private NetworkVariable<Vector3> Position = new NetworkVariable<Vector3>();
    private NetworkVariable<Vector3> Rotation = new NetworkVariable<Vector3>();

    private Vector3 lastdir = Vector3.zero;
    private float compensationAngle = 45.0f;
    private float AxisMx;
    private float AxisMy;
   
    #region Pooling
    public List<GameObject> bullets;
    public GameObject bullet;
    public Transform m_instancePoint;
    public List<GameObject> activeBullets;
    #endregion
    #region Attack
    public GameObject attackHitbox;
    public bool isAttacking = false;
    public bool isAirborne = false;
    private bool isJumping = false;
    private bool isDashing = false;
    private bool isAttack = false;
    private bool once = false;
    #endregion
    #endregion
    // Start is called before the first frame update
    void Start()
    {
        /*
        bullets = new List<GameObject>();
        for(int i = 0;i< 20; i++)
        {
            GameObject obj = (GameObject)Instantiate(bullet);
            obj.SetActive(false);
            bullets.Add(obj);
        }
        activeBullets = new List<GameObject>();*/
        
        transform.position += new Vector3(0, 6, 0);
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if(!IsOwner){
            return;
        }
        AxisMy = Input.GetAxis("Vertical");
        AxisMx = Input.GetAxis("Horizontal");
        if(Input.GetButtonDown("Dash")){
            isDashing = true;
        }else{
            isDashing = false;
        }
        if(Input.GetButtonDown("Jump")){
            isJumping = true;
        }else{
            isJumping = false;
        }
        //check if BasicAttack is being pressed
        if (Input.GetButtonDown("BasicAttack"))
        {
            isAttack = true;
        }
        //Once you realease the button, cannot attack anymore unitl being pressed again
        if (Input.GetButtonUp("BasicAttack"))
        {
            isAttack = false;
        }
        
        
        if(IsHost){
            updateOnServerRpc(AxisMx, AxisMy, isJumping, isDashing, isAttack);
        }else{
            Debug.Log("client");
            if(once == false){
                once = true;
                int mult = 4;
                acceleration *= mult;
                maxSpeed *= mult;
            }
            
            updateOnServerRpc(AxisMx, AxisMy, isJumping, isDashing, isAttack);
            
        
        }
    }
    #region METHODS

    void updateThings(float amx, float amy, bool isJumping, bool isDashing, bool isAttack){
        float dt = Time.fixedDeltaTime;
        //raycast a green beam upside
        Debug.DrawRay(transform.position, Vector3.up * 10, Color.green);
        
        //move the player accelerating it exponentially
        currentSpeed += acceleration * dt;
        currentSpeed = maxSpeed;
        transform.Translate(amx * currentSpeed * dt, 0.0f, amy * currentSpeed * dt);
        
        

        //on jump input jump
        if (isJumping && !isAirborne)
        {
            isAirborne = true;
            GetComponent<Rigidbody>().AddForce(new Vector3(0, jumpForce, 0), ForceMode.Impulse);
            isJumping = false;
        }

        updateTimers();
        groundCheck();
        if(dashTimer > dashCD) dash();
        if(attackTimer > attackCD) basicAttack();
        //debugTests();

        //Shoot();
    }

    void updateTimers(){
        dashTimer += Time.deltaTime;
        attackTimer += Time.deltaTime;
    }
    
    void setlastdir(){
        float margin = 0.1f;
        if(AxisMx > margin) lastdir = transform.right;
        if(AxisMx < -margin) lastdir = -transform.right;
        if(AxisMy > margin) lastdir = transform.forward;
        if(AxisMy < -margin) lastdir = -transform.forward;
    }
    void dash()
    {

        float angleToFix = compensationAngle * AxisMx;
        Vector3 newRight = Quaternion.AngleAxis(angleToFix, Vector3.up) * transform.right;

        float isRight = 0;
        float margin = 0.1f;
        if(AxisMx > margin) isRight = 1;
        if(AxisMx < -margin) isRight = -1;

        float isFwd = 0;
        if(AxisMy > margin) isFwd = 1;
        if(AxisMy < -margin) isFwd = -1;


        if (isDashing)
        {
            if(AxisMx > margin || AxisMx < -margin || AxisMy > margin || AxisMy < -margin){
                GetComponent<Rigidbody>().AddForce(transform.forward * isFwd * dashForce, ForceMode.Impulse);
                GetComponent<Rigidbody>().AddForce(newRight * isRight * dashForce, ForceMode.Impulse);
            }
            else{
                GetComponent<Rigidbody>().AddForce(lastdir * dashForce, ForceMode.Impulse);
            }
            
            dashTimer = 0;
            isDashing = false;
        }
        
    }
    void Shoot()
    {
        if (Input.GetKeyDown(KeyCode.Q))
        {
            for(int i = 0; i < bullets.Count; i++)
            {
                if (!bullets[i].activeInHierarchy)
                {
                    bullets[i].transform.position = new Vector3(m_instancePoint.position.x, m_instancePoint.position.y + 1.0f, m_instancePoint.position.z);
                   // bullets[i].transform.position.y = m_instancePoint.position.y + 10.0f; 
                    bullets[i].transform.rotation = Quaternion.identity;
                    bullets[i].SetActive(true);
                }
            }
        }
    }
    void basicAttack()
    {
        //check if BasicAttack is being pressed
        if (isAttack)
        {
            isAttacking = true;
        }else
        {
            isAttacking = false;
        }


    }

    void groundCheck()
    {
        //check if the object below self is tagged ground
        if (Physics.Raycast(transform.position-new Vector3(0,1.05f,0), -transform.up, 0.3f))
        {
            Debug.Log("Grounded");
                isAirborne = false;

            GetComponent<Hook>().StopGrapple();
        }

    }

    void debugTests(){
        //raycast to transform forward in red
        Debug.DrawRay(transform.position, transform.forward * 10, Color.red);
        //raycast to transform right in green
        Debug.DrawRay(transform.position, transform.right * 10, Color.green);
        //raycast to transform down in blue
        Debug.DrawRay(transform.position-new Vector3(0,1.05f,0), -transform.up * 0.3f, Color.blue);
    }

    public Vector3 CalculateJumpVelocity(Vector3 startPoint, Vector3 endPoint, float trajectoryHeight)
    {
        float gravity = Physics.gravity.y;
        float displacementeY = endPoint.y - startPoint.y;
        Vector3 displacementeXZ = new Vector3(endPoint.x - startPoint.x, 0f, endPoint.z - startPoint.z);

        Vector3 velociryY = Vector3.up * Mathf.Sqrt(-2 * gravity * trajectoryHeight);
        Vector3 velocityXZ = displacementeXZ / (Mathf.Sqrt(-2 * trajectoryHeight / gravity)
            + Mathf.Sqrt(2 * (displacementeY - trajectoryHeight)/gravity));

        return velocityXZ + velociryY;
    }

    public void JumpToPosition(Vector3 targetPosition, float trajetoryHeight)
    {
        velocityToSet = CalculateJumpVelocity(transform.position, targetPosition, trajetoryHeight);
        Invoke(nameof(SetVelocity), 0.1f);
    }

    private Vector3 velocityToSet;

    private void SetVelocity()
    {
        GetComponent<Rigidbody>().velocity = velocityToSet * 2.5f;
    }
    #endregion

    [ClientRpc]
    void updateOnClientRpc(float amx, float amy, bool isJumping, bool isDashing, bool isAttack){
        updateOnServerRpc(amx, amy, isJumping, isDashing, isAttack);
        Debug.Log("ClientRpc updated");
    }

    [ServerRpc(RequireOwnership = false)]
    void updateOnServerRpc(float amx, float amy, bool isJumping, bool isDashing, bool isAttack){
        
        updateThings(amx, amy, isJumping, isDashing, isAttack);
        Debug.Log("ServerRpc updated");
    }

    
}
   
