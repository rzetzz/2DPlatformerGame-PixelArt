using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Test : MonoBehaviour
{
    public Rigidbody2D rb;
    public float knockbackCOunter;
    
    float knock;
    bool move;
    bool jump;
    bool jumpClick;
    bool test;
    int count;
    private void Awake() {
        // inputControl = new PlayerInputSystem();
        // inputControl.Player.Jump.performed += ctx => Jump(true);
        // inputControl.Player.Jump.canceled += ctx => Jump(false);
        
    }
    // private void OnEnable() {
    //     inputControl.Enable();
    // }
    // private void OnDisable() {
    //     inputControl.Disable();
    // }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        // if(inputControl.Player.Jump.WasPerformedThisFrame()){
            
        //     jumpClick = true;
        // } else {
        //     jumpClick = false;
        // }
        // jumpClick = inputControl.Player.Jump.WasPerformedThisFrame();
       test = Input.GetButtonDown("Jump");
        if(jumpClick){
            count++;
        }
        Debug.Log(jumpClick +" "+ count);
        Debug.Log("Jump " + jump);
    // Debug.Log(inputControl.Player.Jump.WasPerformedThisFrame());
        
    }

    void Jump(bool cond){
        jump = cond;
       
    }
   
    private void FixedUpdate() {
        // if(!move){
        //     rb.velocity = new Vector2(2f , 0);
        // } else {
        //     rb.velocity = new Vector2(0,2f);
        // }
        
        
        // if(knock>0){
        //     rb.AddForce(new Vector2(4,rb.velocity.y), ForceMode2D.Impulse);
        // } else {
        //     rb.AddForce(Vector2.zero);
        // }
        
    }
    
}
