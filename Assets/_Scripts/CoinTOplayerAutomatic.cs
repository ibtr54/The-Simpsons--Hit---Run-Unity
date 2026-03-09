using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinTOplayerAutomatic : MonoBehaviour
{
    public float coinSpeed;
    public float secondsForDestroy;
    private bool canJourney;
    private GameObject player;
   // private Animator _animator;
    public GameManager _gameManager;
    private Vector3 offsetPositionPlayer;
    private Rigidbody _rigidbody;
    private int randomDirection;
    private Collider _collider;
    void Start()
    {
        //startTime = Time.time;
        player = GameObject.FindGameObjectWithTag("S_car");
        canJourney = false;
        //_animator = GetComponent<Animator>();
        _gameManager = FindObjectOfType<GameManager>();
        _rigidbody = GetComponent<Rigidbody>();
        _collider = GetComponent<Collider>();
        _rigidbody.AddForce(Vector3.up * Random.Range(0.5f, 2.0f) * 7, ForceMode.Impulse);
        randomDirection = Random.Range(0, 2);

        if (randomDirection == 0)
        {
            _rigidbody.AddForce(Vector3.left * Random.Range(0.5f, 2.0f) * 10, ForceMode.Impulse);
        }
        else if (randomDirection == 1)
        {
            _rigidbody.AddForce(Vector3.right * Random.Range(0.5f, 2.0f) * 10, ForceMode.Impulse);
        }
        else if (randomDirection == 2)
        {
            _rigidbody.AddForce(Vector3.forward * Random.Range(0.5f, 2.0f) * 10, ForceMode.Impulse);
        }
       // _animator.speed = 2.7f;
        //canJourney = true;
        
    }

    // Update is called once per frame
    void Update()
    {
      
        StartCoroutine(DestroyCoinWithoutTouch());
        StartCoroutine(CoinTaked(0.4f));
        if (canJourney)
        {
            StopCoroutine(CoinTaked(0.4f));
            transform.position = Vector3.MoveTowards(transform.position, player.transform.position, coinSpeed * Time.deltaTime);
           
        }
    }



    IEnumerator DestroyCoinWithoutTouch()
    {
        yield return new WaitForSeconds(16.0f);
        //Destroy(this.gameObject);
        Destroy(transform.parent.gameObject);
    }

    IEnumerator CoinTaked(float seconds)
    {
       yield return new WaitForSeconds(seconds);
        canJourney = true;
        _rigidbody.isKinematic = true;
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player" || other.tag == "S_car")
        {
            _rigidbody.isKinematic = true;
            _gameManager.coins++;
            //Destroy(this.gameObject);
            Destroy(transform.parent.gameObject);
            //    //Debug.Log("canJourney is true");

        }
    }

    private void OnCollisionEnter(Collision other)
    {


        if (other.gameObject.tag == "Player" || other.gameObject.tag == "S_car")
        {
            _rigidbody.isKinematic = true;
            _gameManager.coins++;
            //Destroy(this.gameObject);
            Destroy(transform.parent.gameObject);
            //    //Debug.Log("canJourney is true");

        }
        //else if (other.tag == "S_car") {
        //    //_animator.speed = 0.0f;
        //    //Debug.Log("Moneda con autoooooo");
        //    //_gameManager.coins++;
        //    //Destroy(this.gameObject);
        //}
    }



    //private void OnTriggerExit(Collider other)
    //{
    //    StopCoroutine(CoinTaked(secondsForDestroy));
    //}
}
