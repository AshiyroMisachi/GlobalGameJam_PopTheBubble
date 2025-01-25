using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(Collider))]
public class S_Interrupteur : MonoBehaviour
{
    public UnityEvent buttonAction;
    public Vector3 activePosition;

    private void Start()
    {
        GetComponent<Collider>().isTrigger = true;
    }


    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("Test");
        buttonAction.Invoke();
        GetComponent<Collider>().enabled = false;
        transform.localPosition = activePosition;
    }
}