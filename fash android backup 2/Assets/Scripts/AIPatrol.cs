using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIPatrol : MonoBehaviour
{
    public float speed;
    private bool movingRight = true;
    public Transform groundDetection;
    public Transform wallDetection;
    public LayerMask groundLayer;
    ColorChange colorChange;

    private void Awake()
    {
        colorChange = GameObject.Find("Fash").GetComponent<ColorChange>();
    }
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
}


