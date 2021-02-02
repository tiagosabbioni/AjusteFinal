using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Exaustor : MonoBehaviour
{
    //COMPONENTES
        private AudioSource audioS;
        private Animator anim;

    //SONS
        [SerializeField] private AudioClip BLOW_SOUND;

    //BOOLEANS
        public bool gasIsActive;

    //VALORES
        public int variety;
        [HideInInspector] public int leverPhase;
    
    //GAMEOBJECTS
        [SerializeField] private GameObject lever;
        [SerializeField] private GameObject GAS;
    
    void Start()
    {
        this.gasIsActive = false; 
        this.audioS = GetComponent<AudioSource>();
        this.anim = GetComponent<Animator>(); 
    }

    void Update()
    {
        this.BlowGasLever();
            this.anim.SetBool("soltandoGas", gasIsActive);
    }

    private void BlowGasLever()
    {
        this.leverPhase = this.lever.GetComponent<Alavanca>().phase;
        if(!this.gasIsActive && this.leverPhase == this.variety || !this.gasIsActive && this.variety == 2){
            StartCoroutine(this.Gas());
        }

    }


    IEnumerator Gas()
    {
        this.gasIsActive = true;
        yield return new WaitForSeconds(0.7f);
        if(this.transform.rotation == Quaternion.Euler(0f, 0f, 0f)){
            GameObject gasSpawn = Instantiate(this.GAS, new Vector3(this.transform.position.x, this.transform.position.y - 1.5f, this.transform.position.z), Quaternion.Euler(0f, 0f, 0f));
            gasSpawn.transform.parent = this.transform;
        }else if(this.transform.rotation == Quaternion.Euler(0f, 0f, 90f)){
            GameObject gasSpawn = Instantiate(this.GAS, new Vector3(this.transform.position.x + 1.5f, this.transform.position.y, this.transform.position.z), Quaternion.Euler(0f, 0f, 90));
            gasSpawn.transform.parent = this.transform;
        }else if(this.transform.rotation == Quaternion.Euler(0f, 0f, 180f)){
            GameObject gasSpawn = Instantiate(this.GAS, new Vector3(this.transform.position.x, this.transform.position.y + 1.5f, this.transform.position.z), Quaternion.Euler(0f, 0f, 180));
            gasSpawn.transform.parent = this.transform;
        }else if(this.transform.rotation == Quaternion.Euler(0f, 0f, 270f)){
            GameObject gasSpawn = Instantiate(this.GAS, new Vector3(this.transform.position.x - 1.5f, this.transform.position.y, this.transform.position.z), Quaternion.Euler(0f, 0f, 270));        
            gasSpawn.transform.parent = this.transform;    
        }
        this.audioS.PlayOneShot(this.BLOW_SOUND);
    }
}
