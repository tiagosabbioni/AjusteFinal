using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Checkpoint : MonoBehaviour
{

    private GameMaster gameMaster;

    void Start(){
        gameMaster = GameObject.FindGameObjectWithTag("GM").GetComponent<GameMaster>();
    }


    void OnTriggerEnter2D(Collider2D collider){
        if(collider.CompareTag("Player")){
            GameMaster.playerColidiu = true;
            gameMaster.ultimoCheckpoint = this.transform.position;
        }
    }

}
