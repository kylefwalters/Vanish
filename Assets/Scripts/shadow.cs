using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class shadow : MonoBehaviour
{

    Animator anim;
    public Animator childAnim;
    // Start is called before the first frame update
    void Start()
    {
        anim = gameObject.GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.LeftArrow) || Input.GetKey(KeyCode.A) )
        {
            
               
                anim.SetBool("IsRunningLeft", true);
                anim.SetBool("IsRunningRight", false);
                //childAnim.SetBool("isRunning", true);
                
            
        }
        else
        if (Input.GetKey(KeyCode.RightArrow) || Input.GetKey(KeyCode.D) )
        {
            
               
                anim.SetBool("IsRunningRight", true);
                anim.SetBool("IsRunningLeft", false);
                //childAnim.SetBool("isRunning", true);
                
            
        }
        else
        {
            anim.SetBool("IsRunningLeft", false);
            anim.SetBool("IsRunningRight", false);
           
        }

        //Player Y Movement
        if (Input.GetKey(KeyCode.UpArrow) || Input.GetKey(KeyCode.W) )
        {
            
            anim.SetBool("IsRunningUp", true);
            //childAnim.SetBool("isRunning", true);
            
        }
        else if (Input.GetKey(KeyCode.DownArrow)  || Input.GetKey(KeyCode.S))
        {
            

            anim.SetBool("IsRunningDown", true);
            //childAnim.SetBool("isRunning", true);
           
        }
        else
        {
            anim.SetBool("IsRunningUp", false);
            anim.SetBool("IsRunningDown", false);
         
        }
    }
}
