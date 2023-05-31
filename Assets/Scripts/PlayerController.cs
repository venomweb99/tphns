using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    #region Variables
    public float currentSpeed = 0.0f;
    public float maxSpeed = 10.0f;
    public float acceleration = 1.0f;
    public float dashForce = 20.0f;
    public float pushForce = 20.0f;
    public float pushUpForce = 20.0f;
    public float jumpForce = 10.0f;
    public float dashCD = 3.0f;
    public float dashTimer = 0.0f;
    public float attackCD = 1.0f;
    public float attackTimer = 0.0f;

    
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
    #endregion
    #endregion
    // Start is called before the first frame update
    void Start()
    {
        bullets = new List<GameObject>();
        for(int i = 0;i< 20; i++)
        {
            GameObject obj = (GameObject)Instantiate(bullet);
            obj.SetActive(false);
            bullets.Add(obj);
        }
        activeBullets = new List<GameObject>();
    }

    // Update is called once per frame
    void Update()
    {
        float dt = Time.deltaTime;
        AxisMy = Input.GetAxis("Vertical");
        AxisMx = Input.GetAxis("Horizontal");
        
        //move the player accelerating it exponentially
        currentSpeed += acceleration * dt;
        currentSpeed = Mathf.Min(currentSpeed, maxSpeed);
        transform.Translate(AxisMx * currentSpeed * dt, 0.0f, AxisMy * currentSpeed * dt);
        
        

        //on jump input jump
        if (Input.GetButtonDown("Jump") && !isAirborne)
        {
            isAirborne = true;
            GetComponent<Rigidbody>().AddForce(new Vector3(0, jumpForce, 0), ForceMode.Impulse);
        }

        updateTimers();
        groundCheck();
        if(dashTimer > dashCD) dash();
        if(attackTimer > attackCD) basicAttack();
        debugTests();

        Shoot();
    }
    #region METHODS

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


        if (Input.GetButtonDown("Dash"))
        {
            if(AxisMx > margin || AxisMx < -margin || AxisMy > margin || AxisMy < -margin){
                GetComponent<Rigidbody>().AddForce(transform.forward * isFwd * dashForce, ForceMode.Impulse);
                GetComponent<Rigidbody>().AddForce(newRight * isRight * dashForce, ForceMode.Impulse);
            }
            else{
                GetComponent<Rigidbody>().AddForce(lastdir * dashForce, ForceMode.Impulse);
            }
            
            dashTimer = 0;
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
        if (Input.GetButtonDown("BasicAttack"))
        {
            isAttacking = true;
        }
        //Once you realease the button, cannot attack anymore unitl being pressed again
        if (Input.GetButtonUp("BasicAttack"))
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
}
   
