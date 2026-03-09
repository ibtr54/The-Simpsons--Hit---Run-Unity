using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Vending_Machine : MonoBehaviour
{
    private int hp;
    private Animator playerAnimator;
    public GameObject coinsForVendigMachine;
    public GameObject coinsMovetoPlayerAutomatic;
    private Vector3 offsetY;
    private GameObject player;
    private bool canCalculate;
    // Start is called before the first frame update
    void Start()
    {
        hp = 10;
        offsetY = new Vector3(transform.position.x, transform.position.y + 1.5f,transform.position.z);
        canCalculate = false;
        player = GameObject.FindGameObjectWithTag("Player");
        playerAnimator = player.GetComponent<Animator>();
        
    }

    // Update is called once per frame
    void Update()
    {

        if (canCalculate) {
            Debug.Log(Vector3.Distance(this.transform.position, player.transform.position));
            if (Vector3.Distance(this.transform.position, player.transform.position) < 1)
            {
                
            }
        }
        
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.tag == "Ball_R")
        {
            Debug.Log("Pie Detectao");
            if (/*playerAnimator.GetCurrentAnimatorClipInfo(0)[0].clip.name == "Happy IdleKicking(1)" &&*/ Input.GetKeyDown(KeyCode.Z))
            {
                hp -= 4;

                if (hp < 0)
                {
                    for (int i = 0; i < 10; i++)
                    {
                        Instantiate(coinsForVendigMachine, offsetY, Quaternion.identity);

                    }
                    Destroy(this.gameObject);
                }
                else
                {
                    for (int i = 0; i < 4; i++)
                    {
                        Instantiate(coinsForVendigMachine, offsetY, Quaternion.identity);
                    }
                }

            }
        }
        
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "S_car")
        {
            hp -= 30;
            if (hp <= 0) {
                for (int i = 0; i < 10; i++)
                {
                    Instantiate(coinsMovetoPlayerAutomatic, offsetY, Quaternion.identity);

                }
                Destroy(this.gameObject);
            }
            
        }

    }

}
