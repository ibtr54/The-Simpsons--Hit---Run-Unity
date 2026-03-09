using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class duffTruckBoxes : MonoBehaviour
{
    public GameObject _duffTruckBoxes;
    private Animator _animatorBoxes;

    // Start is called before the first frame update
    void Start()
    {
        _animatorBoxes = _duffTruckBoxes.GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.tag == "Player") {
            if (Input.GetKeyDown(KeyCode.F)) {
                if (!(_animatorBoxes.GetBool("moveBoxes")))
                {
                    _animatorBoxes.SetBool("moveBoxes", true);
                }
                else if (_animatorBoxes.GetBool("moveBoxes")) {
                    _animatorBoxes.SetBool("moveBoxes", false);
                }
            }
        }
    }
}
