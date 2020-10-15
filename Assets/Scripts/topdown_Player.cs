using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class topdown_Player : MonoBehaviour
{
    private float currentSpeedx;
    private float currentSpeedy;
    private float targetSpeedx;
    private float targetSpeedy;
    private float maxSpeed;
    public static float playerSize;
    //private float timeTillStep;
    //public float stepLength;

    private bool isMoving = false;
    private bool isHurt = false;
    private bool dying = false;
    private bool facingRL;
    public static bool isVisible = true;
    public static bool isHit;

     Animator anim;
    public Animator childAnim;
    
    private Rigidbody2D rb;
    private Transform mousePos;
    //public GameObject footsteps;


    void Start()
    {
        anim = gameObject.GetComponent<Animator>();
        rb = gameObject.GetComponent<Rigidbody2D>();
        playerSize = gameObject.GetComponent<CircleCollider2D>().bounds.extents.x;
    }

    void FixedUpdate()
    {
        rb.MovePosition(rb.position + new Vector2(currentSpeedx, currentSpeedy) * Time.fixedDeltaTime);
        currentSpeedx = Mathf.MoveTowards(currentSpeedx, targetSpeedx, maxSpeed * Time.deltaTime);
        currentSpeedy = Mathf.MoveTowards(currentSpeedy, targetSpeedy, maxSpeed * Time.deltaTime);

        //Player X Movement
        if (Input.GetKey(KeyCode.LeftArrow) && !isHit || Input.GetKey(KeyCode.A) && !isHit)
        {
            if (!isHurt && !dying)
            {
                isMoving = true;
                //anim.SetBool("IsRunning", true);
                //childAnim.SetBool("isRunning", true);
                targetSpeedx = -6.5f;
            }
        }
        else
        if (Input.GetKey(KeyCode.RightArrow) && !isHit || Input.GetKey(KeyCode.D) && !isHit)
        {
            if (!isHurt && !dying)
            {
                isMoving = true;
                //anim.SetBool("IsRunning", true);
                //childAnim.SetBool("isRunning", true);
                targetSpeedx = 6.5f;
            }
        }
        else
        {
            targetSpeedx = 0;
        }
        
        //Player Y Movement
        if (Input.GetKey(KeyCode.UpArrow) && !isHit || Input.GetKey(KeyCode.W) && !isHit)
        {
            isMoving = true;
            anim.SetBool("IsRunning", true);
            //childAnim.SetBool("isRunning", true);
            targetSpeedy = 6.5f;
        } else if (Input.GetKey(KeyCode.DownArrow) && !isHit || Input.GetKey(KeyCode.S) && !isHit)
        {
            isMoving = true;

            anim.SetBool("IsRunning", true);
            //childAnim.SetBool("isRunning", true);
            targetSpeedy = -6.5f;
        } else
        {
            
            targetSpeedy = 0;
        }

        if (currentSpeedx == 0 && currentSpeedy == 0)
        {
            isMoving = false;
            //anim.SetBool("IsRunning", false);
            //childAnim.SetBool("isRunning", false);
        }

        //Player Acceleration
        if (targetSpeedx > 0 && currentSpeedx < 0 && targetSpeedy > 0 && currentSpeedy < 0 || targetSpeedx < 0 && currentSpeedx > 0 && targetSpeedy < 0 && currentSpeedy > 0)
        {
            maxSpeed = 50f;
        }
        else
        if (targetSpeedx == 0 && targetSpeedy == 0)
        {
            maxSpeed = 50f;
        }
        else
        {
            maxSpeed = 40f;
        }

        //player footstep sounds
        /*if (isMoving == true)
        {
            if (timeTillStep <= 0)
            {
                //Instantiate(footsteps, new Vector2(transform.position.x, transform.position.y), Quaternion.AngleAxis(0, Vector3.up));
                timeTillStep = stepLength;
            } else
            {
                timeTillStep -= Time.deltaTime;
            }
        }*/

        //cancels any velocity from collisions
        rb.velocity = Vector2.zero;

        //Check if Player is Idle
        if (currentSpeedx == 0 && currentSpeedy == 0)
        {

        }

        //player look at mouse Camera.main.ScreenToWorldPoint(Input.mousePosition)
        var pos = Camera.main.WorldToScreenPoint(transform.position);
        var dir = Input.mousePosition - pos;
        var angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
        if (!isHit)
        {
            transform.rotation = Quaternion.AngleAxis(angle - 90, Vector3.forward);
        }

        //Player Death
        if (isHit == true)
        {
            StartCoroutine(Death());
        }
        IEnumerator Death()
        {
            rb.constraints = RigidbodyConstraints2D.FreezeAll;
            yield return new WaitForSeconds(1.25f);
            Scene currentScene = SceneManager.GetActiveScene();
            isHit = false;
            SceneManager.LoadScene(currentScene.name);
        }
    }
}
