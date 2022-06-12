using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Text.RegularExpressions;
using System.Linq;

public class CubeSpawn : MonoBehaviour
{

    public GameObject objects ;
    public GameObject papers;
    public GameObject spawnPaper;
    public GameObject attachmentPoint;

    [HideInInspector]
    GameObject child;

    [HideInInspector]
    GameObject paper;

    [HideInInspector]
    int currChild;

    public int phase = 0;

    [HideInInspector]
    public int phase3_index = 0;

    public int phase5_index = 0;

    [HideInInspector]
    public int[] phase3order = {13, 7,1 , 14, 8, 2, 15,9,3,16,10,4,17,11,5,18,12,6};
    
    public int[] phase5order = {3,6,2,9,1,7,5,12,15,8,4,18,10,11,13,16,17,14};
    public int threecount = 0;
    public int[] prime =  {2,3,5,7,11,13,17};

    void Start()
    {
        currChild = 18;
        child = objects.transform.GetChild(currChild-1).gameObject;
        paper = papers.transform.GetChild(currChild-1).gameObject;
        child.SetActive(true);
        phase =1;
    }

    void Update() //phase 1 ve 2 kod içinde, diğerlerine geçişi unityden yapıyorum. koda da ekleyebiliriz ama elle yapmak rahat geldi 
    {
       if(phase == 1 && Vector3.Distance(paper.transform.position,child.transform.position)<=0.05f && (currChild > 0 ) && child.transform.position.y < 1.04f){
           StartCoroutine(SpawnNextCube());
        }
        if(phase == 2 && currChild < 0 && currChild>= -18&& Vector3.Distance(spawnPaper.transform.position,child.transform.position)<=0.05f && child.transform.position.y < 1.04f ){
            StartCoroutine(CollectNextCube());
        }
        if( phase == 3 && (phase3_index == 0 || Vector3.Distance(paper.transform.position,child.transform.position)<=0.05f && (phase3_index<18) && child.transform.position.y < 1.04f)){
           StartCoroutine(SpawnLeftToRight());
        }
        if(phase == 4 && phase3_index >= 0 && Vector3.Distance(spawnPaper.transform.position,child.transform.position)<=0.05f && child.transform.position.y < 1.04f ){
            StartCoroutine(CollectRightToLeft());
        }
       if(phase == 5  && (phase5_index == 0 || Vector3.Distance(paper.transform.position,child.transform.position)<=0.05f && (phase5_index<18) && child.transform.position.y < 1.04f)){
           StartCoroutine(Spawn19());
       }
       if(phase == 6 && Vector3.Distance(spawnPaper.transform.position,child.transform.position)<=0.05f && child.transform.position.y < 1.04f){
           child = attachmentPoint.GetComponent<AttachMotion>().cube;
           StartCoroutine(CollectDiv());
       }
    }


    public IEnumerator SpawnNextCube(){
        currChild --;
        if(currChild!=0){
            child = objects.transform.GetChild(currChild-1).gameObject;
            paper = papers.transform.GetChild(currChild-1).gameObject;
            child.SetActive(true);
        }
        else{
            currChild--; //only here if 0 -> need to start collecting from 1
            phase = 2;
        }
        //Debug.Log("Spawn" + currChild);
        yield return new WaitForSeconds(1f);
    }

    public IEnumerator CollectNextCube(){
        child.SetActive(false);
        currChild --;
        //Debug.Log("Collect" +  currChild);
        //child = objects.transform.GetChild(-1 + (-1* currChild)).gameObject;
        string name = "Cube ("+(-1* currChild)+")";
        child = objects.transform.Find(name).gameObject;
        //child.SetActive(false);
        //Debug.Log(child.name);
            
        yield return new WaitForSeconds(1f);
    }

    public IEnumerator SpawnLeftToRight(){
        //Debug.Log("spawn index" + phase3_index);
        int child_num = phase3order[phase3_index];
        string name = "Cube ("+child_num+")";
        string pname = "Numbered Paper (" + child_num + ")";
        child = objects.transform.Find(name).gameObject;
        paper = papers.transform.Find(pname).gameObject;
        child.SetActive(true);
        //Debug.Log(child.name);
        //Debug.Log(paper.name);
        phase3_index++;
        yield return new WaitForSeconds(1f);
    }

    public IEnumerator CollectRightToLeft(){
        child.SetActive(false);
        phase3_index --;
        if(phase3_index>=0){
            int child_num = phase3order[phase3_index];
            string name = "Cube ("+child_num+")";
            child = objects.transform.Find(name).gameObject;
        }
        yield return new WaitForSeconds(1f);
    }

    public IEnumerator Spawn19(){
        int child_num = phase5order[phase5_index];
        string name = "Cube ("+child_num+")";
        string pname = "Numbered Paper (" + (19-child_num) + ")";
        child = objects.transform.Find(name).gameObject;
        paper = papers.transform.Find(pname).gameObject;
        child.SetActive(true);
        //Debug.Log(child.name);
        //Debug.Log(paper.name);
        phase5_index++;
        yield return new WaitForSeconds(1f);
    }
    public IEnumerator CollectDiv(){
        int child_num = Int32.Parse(Regex.Match(child.name, @"\d+").Value);
        
        if(child_num%3==0 && threecount < 6){
            child.SetActive(false);
            threecount++;
        }
        
        else if(prime.Contains(child_num) && threecount >= 6){
            child.SetActive(false);
            threecount++;
        }
        yield return new WaitForSeconds(1f);
    }
}
