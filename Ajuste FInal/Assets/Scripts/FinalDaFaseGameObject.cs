using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class FinalDaFaseGameObject : MonoBehaviour
{
    
    public string nomeDaCena;
    public float tempoDaCutscene;

    void Start()
    {

    }

    void Update()
    {
        if (Time.timeSinceLevelLoad >= tempoDaCutscene){
            SceneManager.LoadScene(nomeDaCena);
        }
    }
}