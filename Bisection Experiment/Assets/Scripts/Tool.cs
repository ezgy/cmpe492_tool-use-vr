using UnityEngine;
using Valve.VR;
using System.Collections;

public class Tool: MonoBehaviour 
{
    
    //public InteractableCube _currentCube = null;
    //public List<InteractableCube> _contactInteractables = new List<InteractableCube>();

     [HideInInspector]
    public Transform attachmentPoint;  
    public GameObject cube = null;
    public bool attached = false; 
    public Rigidbody rb;

    void Awake()
    {
        attachmentPoint = transform.GetChild(0).GetChild(5).GetChild(0);
        GameObject tool = transform.GetChild(0).gameObject;
        tool.SetActive(true);
        cube = null;
        rb = GetComponent<Rigidbody>();
    }

    void Update()
    {
        /*if(SteamVR_Actions.bisectionInput_ToolTrigger.GetStateDown(SteamVR_Input_Sources.Any) ){
            Debug.Log("bisection trigger");
        }*/
        if(attached)
        {
            if(!SteamVR_Actions.bisectionInput_ToolTrigger.GetStateDown(SteamVR_Input_Sources.Any) && cube != null)
            {
                StartCoroutine(Drop());
            }
            
        }
        if(SteamVR_Actions.bisectionInput_ToolTrigger.GetStateDown(SteamVR_Input_Sources.Any) && cube != null)
        {
               
        }
        
    }

    public void Pickup(GameObject other)
    {
        if(SteamVR_Actions.bisectionInput_ToolTrigger.GetStateDown(SteamVR_Input_Sources.Any))
        {
            Debug.Log("grab edecem");
            Debug.Log(other.layer);
            if(other.layer == 6 && !attached)
            {
                attached = true;
                other.transform.SetParent(attachmentPoint);
            }
        }
        Debug.Log("pikapa geldiniz");

        
        
    }

    public IEnumerator Drop()
    {
        cube.transform.SetParent(null);
        attached = false;
        yield return new WaitForSeconds(0.1f);
    }

    public InteractableCube getNearestCube()
    {
        return null;
    }

/*
    public void OnCollisionEnter(Collision collision){
        rb.constraints = RigidbodyConstraints.FreezeRotationY;
        Debug.Log(collision.gameObject.name);
    }

    public void OnCollisionExit(Collision collision){
        rb.constraints = RigidbodyConstraints.None;
        Debug.Log("leave");
    }
    */
}