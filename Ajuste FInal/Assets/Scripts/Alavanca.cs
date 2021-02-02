using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Alavanca : MonoBehaviour
{
    //Animator da Alavanca
    public Animator anim;
    public AudioClip LEVER_SOUND;
    public ParticleSystem spark;

    //Estágio de ativação da alavanca responsável por alternar os exaustores
    public int phase;

    // Start is called before the first frame update
    void Start()
    {
        this.phase = 1;
        this.anim = GetComponent<Animator>();
    }

    public void CreateSpark()
    {
        spark.Play();
    }
}
