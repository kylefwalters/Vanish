using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class topdown_Player : MonoBehaviour
{
    //the current speed of the player in respective axes
    private float currentSpeedx;
    private float currentSpeedy;
    //the target speed of the player in respective axes
    private float targetSpeedx;
    private float targetSpeedy;
    private float maxSpeed; //Affects max speed the player can reach
    private float speedBuff = 1.0f; //Speed buff (for items)
    public float speedMod = 2.0f; //Speed modifier for adjusting player speed
    public static float playerSize;
    //private float timeTillStep;
    //public float stepLength;

    private bool isMoving = false;
    private bool isHurt = false;
    private bool dying = false;
    private bool facingRL;
    //public static bool isVisible = true;
    [HideInInspector]
    public static bool isHit;

     Animator anim;
    public Animator childAnim;
    
    private Rigidbody2D rb;
    private Transform mousePos;
    //public GameObject footsteps;

    //[HideInInspector]
    public GameObject lastItem; //Last item to enter player collision range
    [Header("Items"), Tooltip("The duration of the candy powerup")]
    public float candyDuration = 3.0f;
    private float _candyDuration;

    void Start()
    {
        anim = gameObject.GetComponent<Animator>();
        rb = gameObject.GetComponent<Rigidbody2D>();
        playerSize = gameObject.GetComponent<Collider2D>().bounds.extents.x;
    }

    void Awake()
    {
        //Checks if powerup was in progress
        if(_candyDuration > 0)
        {
            StartCoroutine(Candy());
        }
    }

    void FixedUpdate()
    {
        #region Player Movement
        rb.MovePosition(rb.position + new Vector2(currentSpeedx, currentSpeedy) * Time.fixedDeltaTime);
        currentSpeedx = Mathf.MoveTowards(currentSpeedx, targetSpeedx * speedBuff * speedMod, maxSpeed * Time.deltaTime * speedBuff * speedMod);
        currentSpeedy = Mathf.MoveTowards(currentSpeedy, targetSpeedy * speedBuff * speedMod, maxSpeed * Time.deltaTime * speedBuff * speedMod);

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
        if (targetSpeedx > 0 && currentSpeedx < 0 && targetSpeedy > 0 && currentSpeedy < 0 || targetSpeedx < 0 && currentSpeedx > 0 && targetSpeedy < 0 && currentSpeedy > 0) //Decelerate slower if attempting to go in the opposite direction
        {
            maxSpeed = 30f;
        }
        else
        if (targetSpeedx == 0 && targetSpeedy == 0) //Decelerate slower if not pressing movement keys
        {
            maxSpeed = 30f;
        }
        else
        {
            maxSpeed = 40f;
        }
        #endregion

        //player footstep sounds
        #region Footsteps
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
        #endregion

        //cancels any velocity from collisions
        rb.velocity = Vector2.zero;

        //Check if Player is Idle
        if (currentSpeedx == 0 && currentSpeedy == 0)
        {

        }

        //player look at mouse Camera.main.ScreenToWorldPoint(Input.mousePosition)
        #region Player Rotation
        /*var pos = Camera.main.WorldToScreenPoint(transform.position);
        var dir = Input.mousePosition - pos;
        var angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
        if (!isHit)
        {
            transform.rotation = Quaternion.AngleAxis(angle - 90, Vector3.forward);
        }*/
        #endregion

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

    private void Update()
    {
        //Item pickup
        if (Input.GetKeyDown(KeyCode.Space) && lastItem != null)
        {
            //string itemName = lastItem.GetComponent<ItemBase>().itemType.ToString();
            ItemEffect(lastItem);
        }
    }

    #region Powerups
    void ItemEffect(GameObject item)
    {
        string itemName = item.name;
        Destroy(item);
        /*string nameEnding = itemName.Substring(itemName.Length - 9, itemName.Length);
        if(nameEnding.ToLowerInvariant()==" powerup")
            itemName = itemName.Substring(0, itemName.Length - 8).ToLowerInvariant();*/
        itemName = itemName.Substring(0, 5).ToLower();
        print(itemName);

        switch (itemName)
        {
            case "candy":
                //Effect for picking up item

                speedBuff *= 1.5f;
                _candyDuration = candyDuration;
                StartCoroutine(Candy());
                break;
            case null:
                break;
        }
    }

    IEnumerator Candy()
    {
        while(_candyDuration >= 0)
        {
            _candyDuration -= Time.deltaTime;
            yield return new WaitForSeconds(0.1f);
        }
        speedBuff /= 1.5f;
        yield return null;
    }
    #endregion
}
