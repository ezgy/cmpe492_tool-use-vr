using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR;

public class AttachMotion : MonoBehaviour
{

    public bool attached = false;
    public Tool tool = null;
    public GameObject cube = null;
    GameObject oldParent;
    void Start()
    {
        //tool = transform.parent.parent.parent.gameObject as Tool;
    }

    // Update is called once per frame
    void Update()
    {
         if(attached)
        {
            if(SteamVR_Actions.bisectionInput_ToolTrigger.GetStateUp(SteamVR_Input_Sources.Any) && cube != null)
            {
                StartCoroutine(Drop(cube));
            }
            
        }
        /*if(attached)
        {
            if(!SteamVR_Actions.default_GrabPinch.active && cube != null)
            {
                cube.transform.SetParent(null);
                attached = false;
            }
        }*/
    }


    void OnTriggerStay(Collider other){
        //Debug.Log(other.gameObject.tag);
        if(other.gameObject.CompareTag("cube")){
            Pickup(other.gameObject);        
        }
    }

   public void Pickup(GameObject other)
    {
        if(SteamVR_Actions.bisectionInput_ToolTrigger.GetStateDown(SteamVR_Input_Sources.Any) &&  !attached && other.layer == 6)
        {
            attached = true;
            oldParent = other.transform.parent.gameObject;
            other.transform.SetParent(this.gameObject.transform);
            other.transform.position = this.gameObject.transform.position;
            Rigidbody rb = other.GetComponent<Rigidbody>();
            rb.useGravity = false;
            rb.isKinematic = true;
            cube = other.gameObject;
        }

    }   
    public IEnumerator Drop(GameObject cube)
    {
        cube.transform.SetParent(oldParent.transform);
        Rigidbody rb = cube.GetComponent<Rigidbody>();
        rb.useGravity = true;
        rb.isKinematic = false;
        attached = false;
        cube = null;
        yield return new WaitForSeconds(0.1f);
    }
}
