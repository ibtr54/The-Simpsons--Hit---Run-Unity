using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DinamicObject : MonoBehaviour
{
    private Rigidbody _rigidbody;
    public float secondsYieledBeforeDestroy;
    public GameObject coins;

    // Start is called before the first frame update
    void Start()
    {
        _rigidbody = this.GetComponent<Rigidbody>();
        _rigidbody.isKinematic = true;
    }


    private void OnCollisionEnter(Collision collision)
    {
        
        if (collision.gameObject.CompareTag("S_car")) {
            _rigidbody.isKinematic = false;
            
            StartCoroutine(CountDownToDestroy(secondsYieledBeforeDestroy));
        }
    }

    IEnumerator CountDownToDestroy(float seconds) {
        int c = 0;
        while (c > 3) {
            Instantiate(coins, this.transform.position, Quaternion.identity);
            c++; //C++
        }   
        yield return new WaitForSeconds(seconds);
        Destroy(this.gameObject);
    }


}
