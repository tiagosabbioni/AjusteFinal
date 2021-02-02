using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tobey : MonoBehaviour
{
    public GameObject engrenagem;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (this.engrenagem.GetComponent<Objeto>().isInserted){
            this.GetComponent<SpriteRenderer>().enabled = true;
            this.transform.Rotate (new Vector3(0, 0, 5));
        }
    }
}
