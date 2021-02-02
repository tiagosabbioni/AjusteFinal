using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlataformaQueCai : MonoBehaviour
{

    void Start()
    {

    }

    void Update()
    {
        
    }

    IEnumerator Cai(){
        GetComponent<Animator>().SetBool("aberta", true);
        yield return new WaitForSeconds(1f);
        GetComponent<BoxCollider2D>().enabled = false;
        yield return new WaitForSeconds(5f);
        GetComponent<Animator>().SetBool("aberta", false);
        yield return new WaitForSeconds(1f);
        GetComponent<BoxCollider2D>().enabled = true;
    }

    void OnCollisionEnter2D(Collision2D collider){
        if(collider.gameObject.CompareTag("Player")){
            StartCoroutine(Cai());
        }
    }
}
