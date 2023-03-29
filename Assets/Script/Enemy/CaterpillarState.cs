using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CaterpillarState : MonoBehaviour
{   
    public Transform parent;
    EnemyBehaviour doing;
    public Transform[] locPoint;
    int currentPoint;
    Animator setAnim;
    Rigidbody2D enemyRB;
    public bool isMove;
    public float speed;
    float moveDir;
    // Start is called before the first frame update
    void Start()
    {
        doing = GetComponent<EnemyBehaviour>();
        setAnim = GetComponent<Animator>();
        enemyRB = GetComponent<Rigidbody2D>();
        moveDir = speed;

        foreach (Transform isPoin in locPoint){
            isPoin.SetParent(null);
        }
        currentPoint = Random.Range(0,2);
    }

    // Update is called once per frame
    void Update()
    {
        
        if (Mathf.Abs(transform.position.x - locPoint[currentPoint].position.x) > .2f){
            if(transform.position.x > locPoint[currentPoint].position.x){
                doing.isFaceRight = false;
                moveDir = -speed;
                transform.localScale = Vector3.one;
            } else {
                doing.isFaceRight = true;
                moveDir = speed;
                transform.localScale = new Vector3(-1,1,1);
            }
        } else {
            enemyRB.velocity = Vector2.zero;
            currentPoint++;
            if (currentPoint >= locPoint.Length){
                currentPoint = 0;
            }
        }
        setAnim.SetBool("isMove",isMove); 
    }
    private void FixedUpdate() {
        if(isMove && !doing.isKnockback){
            Move();
        } else if(!isMove){
            enemyRB.velocity = Vector2.zero;
        }
    }

    public void Move(){
        enemyRB.velocity = new Vector2(moveDir, enemyRB.velocity.y);
    }

    public void StopMove(){
        isMove = false;
    }
    public void StartMove(){
        
        isMove = true;
        
        
    }
    public void nonSpeed(){
        isMove = false;
    }
}
