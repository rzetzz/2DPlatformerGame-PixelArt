using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FmodPlayer : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void PlayFootsteps(string path){
        FMOD.Studio.EventInstance footsteps = FMODUnity.RuntimeManager.CreateInstance(path);
        footsteps.start();
        footsteps.release();

    }
    
}
