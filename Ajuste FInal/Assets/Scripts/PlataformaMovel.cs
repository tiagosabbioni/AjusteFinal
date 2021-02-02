using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlataformaMovel : MonoBehaviour
{


    private bool moveRight = true;
    [SerializeField] private int platformSpeed;
    private float startingPos;
    [SerializeField] private float negativeFinalPos;
    [SerializeField] private bool horizontal, vertical;
    

    void Start()
    {
        if( horizontal ){
            startingPos = this.transform.position.x;
        }else if( vertical ){
            startingPos = this.transform.position.y;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if ( horizontal ){
            HorizontalMovement();
        }else if ( vertical ){
            VerticalMovement();
        }
            
    }

    void HorizontalMovement(){
        if ( transform.position.x <= startingPos ){
            moveRight = true  ;
        }
        if ( transform.position.x >= negativeFinalPos ){
            moveRight = false;
        }

        if(transform.position.x <= startingPos){
            GetComponent<Animator>().SetBool("direita", true);
        }else if(transform.position.x >= negativeFinalPos - 0.05f){
            GetComponent<Animator>().SetBool("direita", false);
        }

        if ( moveRight ){
            transform.position = new Vector2 (transform.position.x + platformSpeed * Time.deltaTime, transform.position.y);
        }else{
            transform.position = new Vector2 (transform.position.x - platformSpeed * Time.deltaTime, transform.position.y);
        }
    }

    void VerticalMovement(){
        if (platformSpeed >  0 && transform.position.y >= startingPos){
            platformSpeed = platformSpeed * -1;
        }
        else if (platformSpeed < 0 && transform.position.y <= negativeFinalPos){
            platformSpeed = platformSpeed * -1;
        }

        if(transform.position.y <= negativeFinalPos){
            GetComponent<Animator>().SetBool("direita", true);
        }else if(transform.position.y >= startingPos){
            GetComponent<Animator>().SetBool("direita", false);
        }
        //Move o objeto em um eixo, baseado também no tempo entre um frame e outro, multiplicado pelo valor da velocidade determinado no Inspector
        transform.position = new Vector3(transform.position.x, transform.position.y + (platformSpeed * Time.deltaTime), transform.position.z);
    }
}