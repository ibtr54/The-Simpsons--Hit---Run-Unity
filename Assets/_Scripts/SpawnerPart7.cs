using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnerPart7 : MonoBehaviour
{
    public GameObject part2;
    public GameObject part7;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            part2.SetActive(false);
            part7.SetActive(true);
        }
    }
}
