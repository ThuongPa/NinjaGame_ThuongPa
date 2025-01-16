using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Effect : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        OnInit();
    }

    // Update is called once per frame
    private void OnInit()
    {
       Invoke(nameof(OnDespawn), 2f); 
    }
    private void OnDespawn()
    {
        Destroy(gameObject);
    }
   
}
