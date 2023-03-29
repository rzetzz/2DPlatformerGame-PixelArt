using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.Universal;

public class LightController : MonoBehaviour
{
    public Light2D bgLight;
    bool ch = false;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (ch){
            bgLight.intensity = Mathf.Lerp(bgLight.intensity, 0.8f,0.2f * Time.deltaTime);
        } else {
             bgLight.intensity = Mathf.Lerp(bgLight.intensity, 1f,0.2f * Time.deltaTime);
        }
    }
    private void OnTriggerEnter2D(Collider2D other) {
        if (other.tag == "Player"){
            ch = !ch;
        }
        
    }
}
