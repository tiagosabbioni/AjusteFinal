using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fio : MonoBehaviour
{
    public ParticleSystem spark;
    public AudioClip CHOQUE_SOUND;
    public float startingTimer = 2f;
    public float currentTime = 0f;
    public float cooldown = 0f;
    public Transform botao;



    void Start()
    {
        currentTime = startingTimer;
        GetComponent<BoxCollider2D>().enabled = false;

    }

    
    void Update()
    {
        if(this.botao.GetComponent<Button>().isPressed == true)
        {
            Timer();
        }
            
    
    }

    void Timer()
    {
        currentTime -= 1 * Time.deltaTime;
        if(currentTime <= 0)
        {
            currentTime = 0;

            if(currentTime == 0)
            {
                cooldown += 1 * Time.deltaTime;
                GetComponent<BoxCollider2D>().enabled = false;
            }
        
        }
        if(cooldown >= startingTimer)
        {
            cooldown = startingTimer;
            currentTime = startingTimer;
        
            if(cooldown == startingTimer)
            {
                currentTime -= 1 * Time.deltaTime;
                CreateSpark();
                GetComponent<BoxCollider2D>().enabled = true;
                GetComponent<AudioSource>().PlayOneShot(CHOQUE_SOUND);
                cooldown = 0;
            }
        }
    
    }
    
    void CreateSpark()
    {
        spark.Play();
    }


}
