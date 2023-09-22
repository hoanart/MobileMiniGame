using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerObject : MonoBehaviour
{
    [SerializeField]
    private AnchorGameObject anchor;
    [SerializeField]
    private bool trans;

    private void OnEnable()
    {

    }

    // Start is called before the first frame update
    void Start()
    {
        anchor = GetComponent<AnchorGameObject>();
    }

    // Update is called once per frame
    void Update()
    {
       
       
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        Debug.Log(col.tag);
        if (col.CompareTag("Player"))
        {
            Debug.Log("충돌");
            gameObject.SetActive(false);
        }

      
    }
}
