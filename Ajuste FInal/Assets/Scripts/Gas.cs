using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gas : MonoBehaviour
{
    [SerializeField] private AudioClip GAS_SOUND;
    
    void Start()
    {
        StartCoroutine(this.PlayGasSound());
    }

    void Update()
    {
        this.DestroyGas();
    }

    void DestroyGas(){
        if(GetComponentInParent<Exaustor>().leverPhase != GetComponentInParent<Exaustor>().variety && GetComponentInParent<Exaustor>().variety != 2){
            GetComponentInParent<Exaustor>().gasIsActive = false;
            Destroy(this.gameObject);
        }
    }

    IEnumerator PlayGasSound(){
        this.GetComponent<AudioSource>().PlayOneShot(this.GAS_SOUND);
        yield return new WaitForSeconds(0.5f);
        StartCoroutine(this.PlayGasSound());
    }
}
