using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChunkController : MonoBehaviour
{
    //public GameObject p1_default;
    public GameObject part1_a;
    public GameObject part2_a;
    public GameObject part3_a;
    public GameObject part4_a;
    public GameObject part5_a;
    public GameObject part6_a;
    public GameObject part7_a;
    public GameObject part8_a;
    //private PartTrigger _partDefault;
    private PartTrigger _part1;
    private PartTrigger _part2;
    private PartTrigger _part3;
    private PartTrigger _part4;
    private PartTrigger _part5;
    private PartTrigger _part6;
    private PartTrigger _part7;
    private PartTrigger _part8;

    private int activeChunk;

    public int getActiveChunck
    {
        get
        {
            return activeChunk;
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        activeChunk = 0x01;
        //_partDefault = p1_default.GetComponent<PartTrigger>();
        _part1 = part1_a.GetComponent<PartTrigger>();
        _part2 = part2_a.GetComponent<PartTrigger>();
        _part3 = part3_a.GetComponent<PartTrigger>();
        _part4 = part4_a.GetComponent<PartTrigger>();
        _part5 = part5_a.GetComponent<PartTrigger>();
        _part6 = part6_a.GetComponent<PartTrigger>();
        _part7 = part7_a.GetComponent<PartTrigger>();
        _part8 = part8_a.GetComponent<PartTrigger>();


    }

    // Update is called once per frame
    void Update()
    {


        if (_part1.getTriggerEnter)
        {
            activeChunk = 0x01;

        }else if (_part2.getTriggerEnter)
        {
            
            activeChunk = 0x02;
            //Debug.Log("Active Chunk:" + activeChunk);
        }else if (_part3.getTriggerEnter)
        {
            activeChunk = 0x03;
        }
        else if (_part4.getTriggerEnter)
        {
            activeChunk = 0x04;
        }
        else if (_part5.getTriggerEnter)
        {
            activeChunk = 0x05;
        }
        else if (_part6.getTriggerEnter)
        {
            activeChunk = 0x06;
        }
        else if (_part7.getTriggerEnter)
        {
            activeChunk = 0x07;
        }
        else if (_part8.getTriggerEnter)
        {
            activeChunk = 0x08;
        }

    }



}
