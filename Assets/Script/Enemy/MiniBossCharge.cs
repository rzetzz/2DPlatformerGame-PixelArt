using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MiniBossCharge : MonoBehaviour
{
    Transform player;
    public float turnSpeed = 0.2f;
    Quaternion targetRot;
    Quaternion rot;
    // Start is called before the first frame update
    void Start()
    {
        player = PlayerStats.instance.transform;
    }

    // Update is called once per frame
    void Update()
    {
        transform.localScale = transform.parent.localScale;
    }

    public void getPosition(){
        Vector3 direction = transform.position - player.position;
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        targetRot = Quaternion.AngleAxis(angle, Vector3.forward);
        transform.rotation = targetRot;
        // transform.rotation = Quaternion.Slerp(transform.rotation, targetRot, turnSpeed * Time.deltaTime);
    }
    public void setDisable(){
        gameObject.SetActive(false);
    }
}
