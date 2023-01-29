using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.Animations;
using UnityEngine.UI;


[RequireComponent(typeof(Rigidbody2D)), AddComponentMenu("Controller")]
public class Controller : MonoBehaviour
{
    #region Movement Modifiers
    [Header("--------------------Movement Modifiers--------------------")]
    public float speed = 500;
    public float jumpForce = 10;
    [Space(5)]
    public float boostForceX = 5;
    public float boostForceY = 10;
    #endregion
    
    [Header("--------------------Mobile Controls--------------------")]
    public GameObject moveButtons;
    [Space(10)]
    public GameObject jumpButtons;
    public GameObject diveButtons;
    #region Other stuff
    [Header("-------------------------Other stuff-------------------------")]
    public float boostCooldown = 5;
    [Space] public float groundDistance = 0.001f;
    public LayerMask groundLayer;
    public LayerMask enemyLayer;
    public GameObject spray;

    #endregion
    private float savedDirecrion;
    #region Private varibles
    ColorChange colorChange;
    public AudioSource audioSource;

    public AudioClip bounce;

    public Rigidbody2D rb;
    Transform groundCheck;
    Transform groundCheck2;
    Animator anim;

    float horizontalMovement = 0;
    float move = 0;
    public bool shouldMove;
    public bool shouldAirMove;
    bool DiveAllowed;
    bool isDiving = false;
    public bool canGetBounced = true;

    [HideInInspector] public bool freezeAll = false;
    public bool lockOnY = true;
    [HideInInspector] public bool lockOnX = false;

    public bool isGrounded()
    {
        if (Physics2D.Raycast(groundCheck.position, Vector2.down, groundDistance, groundLayer) || Physics2D.Raycast(groundCheck2.position, Vector2.down, groundDistance, groundLayer))
        {
            return true;
        }
        else
        {
            return false;
        }
    }



    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "flower")
        {
            if (canGetBounced)
            {
                audioSource.clip = bounce;
                audioSource.Play();
                lockOnY = true;
                lockOnY = false;
                rb.velocity = new Vector2(rb.velocity.x, 40f);
            }

        }
       
    }
    Vector3 dir;
    #endregion

    private void Awake()
    {
        colorChange = GetComponentInChildren<ColorChange>();

        rb = GetComponent<Rigidbody2D>();

        anim = GetComponentInChildren<Animator>();
        groundCheck = GameObject.Find("Ground Check").GetComponent<Transform>();
        groundCheck2 = GameObject.Find("Ground Check2").GetComponent<Transform>();
        
    }

    private void Update()
    {
        if (Input.GetKeyDown("space"))
        {
            if(jumpButtons.activeInHierarchy == true)
            {
                Jump();
            }
            if (DiveAllowed == true)
            {
                Dive();
            }
        }
        #region Lock On Y
        if (lockOnY)
        {
            rb.constraints = RigidbodyConstraints2D.FreezePositionY | RigidbodyConstraints2D.FreezeRotation;
        }
        else
        {
            rb.constraints = RigidbodyConstraints2D.FreezeRotation;
        }
        #endregion

        #region Lock On X
        if (lockOnX)
        {
            rb.constraints = RigidbodyConstraints2D.FreezePositionX | RigidbodyConstraints2D.FreezeRotation;
        }
        else
        {
            rb.constraints = RigidbodyConstraints2D.FreezeRotation;
        }
        #endregion

        #region Lock On Y
        if (freezeAll)
        {
            rb.constraints = RigidbodyConstraints2D.FreezeAll;
        }
        else
        {
            rb.constraints = RigidbodyConstraints2D.FreezeRotation;
        }
        #endregion

        #region Grounded Stuff
        if (isGrounded())
        {
            DiveAllowed = true;
            anim.SetBool("Jump", false);
            jumpButtons.SetActive(true);
            diveButtons.SetActive(false);
        }
        else
        {
            jumpButtons.SetActive(false);
            diveButtons.SetActive(true);
            anim.SetBool("Jump", true);
            lockOnY = false;
        }
        #endregion

        #region Walk Anim
        if (horizontalMovement != 0)
        {
            anim.SetBool("Walk", true);
        }
        else
        {
            anim.SetBool("Walk", false);
        }
        #endregion
    }

    private void FixedUpdate()
    {
        Move();
       
    }

    void Move()
    {
        if (shouldMove)
        {
            #region Main Stuff
            move = (horizontalMovement * speed) * Time.deltaTime;

            if (isGrounded())
            {
                rb.velocity = new Vector2(move, rb.velocity.y);
            }
            else
            {
                jumpButtons.SetActive(false);
  
                if(rb.velocity.x >= 0 && horizontalMovement < 0)
                {
                    rb.AddForce(transform.right * -300f); 
                }
                if (rb.velocity.x <= 0 && horizontalMovement > 0)
                {
                    rb.AddForce(transform.right * 300f);
                }
                if (rb.velocity.x < 10 && rb.velocity.x > -10 && horizontalMovement < 0)
                {
                    rb.AddForce(transform.right * -300f);
                }
                if (rb.velocity.x < 10 && rb.velocity.x > -10 && horizontalMovement > 0)
                {
                    rb.AddForce(transform.right * 300f);
                }

            }           
            #endregion

            #region Set Direction
            if (!isDiving)
            {
                if (horizontalMovement < 0)
                {
                    transform.localScale = new Vector3(-1, 1, 1);
                }
                else if (horizontalMovement > 0)
                {
                    transform.localScale = new Vector3(1, 1, 1);
                }
            }
            #endregion
        }
    }

   

    public void Move(int speed)
    {
        horizontalMovement = speed;
    }

    public void Jump()
    {
        if (isDiving == false) 
        {
            
            lockOnY = false;
            if (isGrounded())
            {
                savedDirecrion = gameObject.transform.localScale.x;
                jumpButtons.SetActive(false);
                diveButtons.SetActive(true);

                rb.velocity = new Vector2(rb.velocity.x, jumpForce);

                StartCoroutine(AfterJump());
            }
        }
        
    }

    IEnumerator AfterJump()
    {
        yield return new WaitForSeconds(0.3f);
        while (true)
        {
            yield return new WaitForEndOfFrame();
            if (isGrounded())
            {
                yield return new WaitForSeconds(0.2f);

                jumpButtons.SetActive(true);
                diveButtons.SetActive(false);

                lockOnY = true;

                anim.SetBool("Idle", true);
                yield return new WaitForSeconds(0.2f);
    
                break;
            }
        }
    }

    #region Dive
    public void Dive()
    {
        dir = transform.localScale;
        if (DiveAllowed)
        {

            if (!isGrounded())
            {
                DiveAllowed = false;
                isDiving = true;
                anim.SetBool("Dive", true);
                diveButtons.SetActive(false);
                rb.velocity.Set(0f, 0f);
                if (horizontalMovement > 0)
                {
                    //rb.velocity = new Vector2((rb.velocity.x + boostForceX), (rb.velocity.y + boostForceY));
                    rb.velocity = new Vector2((12f), (15f));
                }
                else if (horizontalMovement < 0)
                {
                    //rb.velocity = new Vector2((rb.velocity.x - boostForceX), (rb.velocity.y + boostForceY));
                    rb.velocity = new Vector2((-12f), (15f));
                }
                jumpButtons.SetActive(false);
                StartCoroutine(AfterBoost());
            }
        }
        
    }

    IEnumerator AfterBoost()
    {
        
        yield return new WaitUntil(isGrounded);
        anim.SetBool("Dive", false);
        shouldMove = false;
        yield return new WaitForSeconds(1);        
        isDiving = false;
        shouldMove = true;
        jumpButtons.SetActive(true);     
    }
    #endregion

    
}
