using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameMaster : MonoBehaviour
{
    public static GameMaster instance;
    [HideInInspector] public Vector2 ultimoCheckpoint;
    [HideInInspector] public static bool playerColidiu;


    void Awake(){
        if(instance == null){
            instance = this;
            DontDestroyOnLoad(instance);
        }else{
            Destroy(gameObject);
        }
    }

}
