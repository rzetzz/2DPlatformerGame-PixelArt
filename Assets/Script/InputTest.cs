using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
public class InputTest : MonoBehaviour
{
    
    private bool jump;
    public float hz;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    
    public void Move(InputAction.CallbackContext context){
        hz = context.ReadValue<Vector2>().x;
        Debug.Log(hz);
    }

    public void Jump(InputAction.CallbackContext context){
        jump = context.ReadValueAsButton();
        
    }
}

