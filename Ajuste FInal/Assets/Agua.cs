using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Agua : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(PlayWaterSound());
    }

    // Update is called once per frame
    void Update()
    {
        if(!transform.parent.GetComponent<Jato>().soltandoAgua){
            Destroy(this.gameObject);
        }
    }
    IEnumerator PlayWaterSound(){
        yield return new WaitForSeconds(0.5f);
        StartCoroutine(PlayWaterSound());
    }
}
