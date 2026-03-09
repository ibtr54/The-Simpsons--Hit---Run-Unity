using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowPlayerInDoors : MonoBehaviour
{

    public bool canMove;
    private GameObject player;
    public GameObject startPos;
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
    }

    public void ResetPos() {
        this.transform.position = startPos.transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        if (canMove)
        {
            this.transform.position = player.transform.position;
        }
        
    }
}
