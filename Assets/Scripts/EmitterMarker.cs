using UnityEngine;
using System.Collections;

public class EmitterMarker : MonoBehaviour {
    
    private EmitterManager emiManager;

    void Start()
    {
        emiManager = FindObjectOfType<EmitterManager>();
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.tag == "Player")
        {
            emiManager.StartEmitters();
        }
    }
}
