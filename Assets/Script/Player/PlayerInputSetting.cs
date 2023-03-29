using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
public class PlayerInputSetting : MonoBehaviour
{
    public static PlayerInputSetting instance;
    // PlayerInputSystem inputControl;
    public float horizontal;
    public Vector2 moveDir;
    public Vector2 axis;
    Vector2 aim;
    public bool jump;
    public bool attack;
    public bool jumpClick;
    public bool dashClick;
    public bool attackClick;
    int count;
    PlayerInput playerInput;
    Vector3 mousePos;
    Vector3 mouseAim;
    Vector3 theMouse;
    public Transform lightning;
    //  private void Awake() {
        
    //     instance = this;
    //     inputControl = new PlayerInputSystem();
        
    //     inputControl.Player.Jump.performed += ctx => Jump(true);
    //     inputControl.Player.Jump.canceled += ctx => Jump(false);

    //     inputControl.Player.Move.performed += ctx => Movement(ctx.ReadValue<Vector2>());
    //     inputControl.Player.Move.canceled += ctx => Movement(Vector2.zero);

    //     inputControl.Player.Aim.performed += ctx => Axis(ctx.ReadValue<Vector2>());

    //     inputControl.Player.Attack.performed += ctx => Attack(true);
    //     inputControl.Player.Attack.canceled += ctx => Attack(false);
    // }
    // private void OnEnable() {
    //     inputControl.Enable();
        
    // }
    // private void OnDisable() {
    //     inputControl.Disable();
    // }
    
    // private void Update() {
        
        
    //     jumpClick = inputControl.Player.Jump.WasPerformedThisFrame();
    //     dashClick = inputControl.Player.Dash.WasPerformedThisFrame();
    //     attackClick = inputControl.Player.Attack.WasPerformedThisFrame();

    //     if(jumpClick){
    //         count++;
    //     }

    //     Debug.Log(inputControl.controlSchemes);
    //     InputSystem.onDeviceChange +=
    //     (device, change) =>
    //     {
    //     switch (change)
    //     {
    //         case InputDeviceChange.Added:
    //             Debug.Log("Device added: " + device);
    //             break;
    //         case InputDeviceChange.Removed:
    //             Debug.Log("Device removed: " + device);
    //             break;
    //         case InputDeviceChange.ConfigurationChanged:
    //             Debug.Log("Device configuration changed: " + device);
    //             break;
    //     }
    // };
    // }

    void Jump(bool cond){
        jump = cond;
       
    }
    void Attack(bool cond){
        attack = cond;
       
    }
    void Movement(Vector2 direction){
        moveDir = direction;
    }
    void Axis(Vector2 direction){
        aim = direction;
    }
    private void Awake() {
        instance = this;
        playerInput = GetComponent<PlayerInput>();
    }
    private void Update() {
        theMouse = new Vector3(Mouse.current.position.ReadValue().x, Mouse.current.position.ReadValue().y, Camera.main.transform.position.z);
        // mousePos = Camera.main.ScreenToWorldPoint(theMouse);
        mousePos = Camera.main.WorldToScreenPoint(lightning.transform.position);
        Vector3 temps = mousePos;
        mousePos.z = Camera.main.nearClipPlane;
        // mouseAim = mousePos - lightning.transform.position;
        
        mouseAim = new Vector2(theMouse.x - mousePos.x, theMouse.y - mousePos.y);
        mouseAim.Normalize();
        var jumper = playerInput.actions["Jump"];
        jumpClick = jumper.WasPerformedThisFrame();

        var attack = playerInput.actions["Attack"];
        attackClick = attack.WasPerformedThisFrame();

        var dash = playerInput.actions["Dash"];
        dashClick = dash.WasPerformedThisFrame();
        
        if(playerInput.currentControlScheme.Equals("Keyboard&Mouse")){
            
            axis = new Vector2(mouseAim.x, mouseAim.y);
        } else {
            axis = aim;
        }

        if(jumpClick){
            count++;
        }
        
       
        // Debug.Log(axis);
        // pos = Camera.main.ScreenToViewportPoint(Mouse.current.position.ReadValue());
        
       
        
    }

    public void Aiming(InputAction.CallbackContext ctx){
        Axis(ctx.ReadValue<Vector2>());
        
    }
    public void Moving(InputAction.CallbackContext ctx){
        Movement(ctx.ReadValue<Vector2>());
    }
    public void Jumping(InputAction.CallbackContext ctx){
        if(ctx.performed){
            Jump(true);
        } else if (ctx.canceled){
            Jump(false);
        }
    }
    public void Attacking(InputAction.CallbackContext ctx){
        if(ctx.performed){
            Attack(true);
        } else if(ctx.canceled){
            Attack(false);
        }
    }
    
}
