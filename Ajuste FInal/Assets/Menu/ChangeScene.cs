using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ChangeScene : MonoBehaviour
{
    public string nomeDaCena;
    public void trocarCena(){
        SceneManager.LoadScene(nomeDaCena);
    }
    void Start()
    {
        
    }

    void Update()
    {
        
    }
}
