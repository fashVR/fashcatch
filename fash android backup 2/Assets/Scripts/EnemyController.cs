using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
/*public class EnemyController : MonoBehaviour
{
     public float speed;
    private bool movingRight = true;
    public Transform groundDetection;
    public Transform wallDetection;
    public LayerMask groundLayer;

    void Update()
    {
        transform.Translate(Vector2.right * speed * Time.deltaTime);
        RaycastHit2D groundInfo = Physics2D.Raycast(groundDetection.position, Vector2.down, 2f);
        RaycastHit2D wallInfo = Physics2D.Raycast(wallDetection.position, Vector2.right, 2f, groundLayer);
        if (groundInfo.collider == false || wallInfo.collider == true)
        {
            if (movingRight)
            {
                transform.eulerAngles = new Vector3(0, -180, 0);
                movingRight = false;
            }
            else
            {
                transform.eulerAngles = new Vector3(0, 0, 0);
                movingRight = true;
            }
        }
    }
}*/

public class EnemyController : MonoBehaviour
{
    public float walkSpeed;
    public Transform groundDetection;
    public Transform wallDetection;
    public LayerMask groundLayer;
    public LayerMask pathLayer;
    public LayerMask fashLayer;
    public Rigidbody2D rb;
    ColorChange colorChange;
    Animator anim;
    Controller controller;
    [HideInInspector] public bool freezeAll = false;
    private Canvas canvas;
    private GameObject fashParent;
    public Transform enemyDetection;
    public LayerMask enemyLayer;
    bool isDaying;
    public SpriteRenderer[] lights = new SpriteRenderer[2];
    AudioSource audioSource;
    CircleWipeController circleWipe;

    [HideInInspector]
    public bool mustPatrol;

    private void Start()
    {
        mustPatrol = true;
    }
    private void Awake()
    {
        colorChange = GameObject.Find("Fash").GetComponent<ColorChange>();
        anim = GameObject.Find("Fash").GetComponent<Animator>();
        controller = GameObject.Find("FashParent").GetComponent<Controller>();
        canvas = GameObject.Find("GameUI").GetComponent<Canvas>();
        fashParent = GameObject.Find("FashParent");
        circleWipe = Camera.main.GetComponent<CircleWipeController>();
        audioSource = GetComponent<AudioSource>();

    }
    void Update()
    {
        
        if (freezeAll)
        {
            rb.constraints = RigidbodyConstraints2D.FreezeAll;
        }
        else
        {
            rb.constraints = RigidbodyConstraints2D.FreezeRotation;
        }
        if (mustPatrol)
        {
            Patrol();
        }
        RaycastHit2D groundInfo = Physics2D.Raycast(groundDetection.position, Vector2.down, 2f, groundLayer);
        RaycastHit2D pathInfo = Physics2D.Raycast(groundDetection.position, Vector2.down, 2f, pathLayer);
        RaycastHit2D pathWallInfo = Physics2D.Raycast(wallDetection.position, Vector2.right, 2f, pathLayer);
        RaycastHit2D wallInfo = Physics2D.Raycast(wallDetection.position, Vector2.right, 2f, groundLayer);
        RaycastHit2D fashInfo = Physics2D.Raycast(wallDetection.position, Vector2.right, 2f, fashLayer);
        RaycastHit2D fash2Info = Physics2D.Raycast(wallDetection.position, Vector2.up, 4f, fashLayer);
        RaycastHit2D enemyInfo = Physics2D.Raycast(enemyDetection.position, Vector2.right, 2f, enemyLayer);
        if (groundInfo.collider == false && pathInfo.collider == false || wallInfo.collider == true || pathWallInfo.collider == true)
        {
            Flip();
        }
        if (fashInfo.collider == true || fash2Info.collider == true)
        {
            if (colorChange.isCamouflaged == false && !isDaying)
            {
                PlayerPrefs.SetString("LastLevel", SceneManager.GetActiveScene().name);
                controller.canGetBounced = false;

                isDaying = true;
                freezeAll = true;
                controller.jumpButtons.SetActive(false);
                controller.moveButtons.SetActive(false);
                controller.diveButtons.SetActive(false);
                controller.freezeAll = true;
                controller.freezeAll = false;
                controller.shouldMove = false;
                if (controller.isGrounded() == false)
                {
                    //fashParent.transform.position = new Vector3(fashParent.transform.position.x, transform.position.y + 0.1f, fashParent.transform.position.z);
                }
                Vector2 fashValues = fashParent.transform.localScale;
                Vector2 enemyValues = gameObject.transform.localScale;
                if (enemyValues.x < 0 && fashValues.x < 0 || enemyValues.x > 0 && fashValues.x > 0)
                {
                    fashParent.transform.localScale = new Vector2(fashParent.transform.localScale.x * -1, fashParent.transform.localScale.y);
                    
                }
                controller.lockOnY = false;
                if(fashParent.transform.localScale.x > 0)
                {
                    fashParent.transform.position = new Vector3(gameObject.transform.position.x - 7, transform.position.y, fashParent.transform.position.z);

                    controller.rb.velocity = new Vector2(-5f, 15f);
                }
                if (fashParent.transform.localScale.x < 0)
                {
                    Debug.Log("Ahoi");
                    fashParent.transform.position = new Vector3(gameObject.transform.position.x + 7f, transform.position.y, fashParent.transform.position.z);

                    controller.rb.velocity = new Vector2(5f, 15f);
                }

                audioSource.Play();
                circleWipe.enabled = true;
                anim.SetBool("Detected", true);

                foreach(SpriteRenderer sp in lights)
                {
                    sp.color = Color.red;
                }
            }
        }
    }

    
    void Patrol()
    {
        rb.velocity = new Vector2(walkSpeed * Time.fixedDeltaTime, rb.velocity.y);
    }

    void Flip()
    {
        mustPatrol = false;
        transform.localScale = new Vector2(transform.localScale.x * -1, transform.localScale.y);
        walkSpeed *= -1;
        mustPatrol = true;
    }
}

