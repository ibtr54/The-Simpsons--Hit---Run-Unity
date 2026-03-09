using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Coin_Vending_Machine : MonoBehaviour
{
    // Start is called before the first frame update
    // private float startTime;
    //  public float journeyToPlayerTime;
    public float coinSpeed;
    public float secondsForDestroy;
    private bool canJourney;
    private GameObject player;
    private Animator _animator;
    public GameManager _gameManager;
    private Vector3 offsetPositionPlayer;
    private Rigidbody _rigidbody;
    private int randomDirection;
    private Collider _collider;
    void Start()
    {
        //startTime = Time.time;

        canJourney = false;
        _animator = GetComponent<Animator>();
        _gameManager = FindObjectOfType<GameManager>();
        _rigidbody = GetComponent<Rigidbody>();
        _collider = GetComponent<Collider>();
        _rigidbody.AddForce(Vector3.up * Random.Range(0.5f, 2.0f)*7,ForceMode.Impulse);
        randomDirection = Random.Range(0, 2);

        if (randomDirection == 0)
        {
            _rigidbody.AddForce(Vector3.left * Random.Range(0.5f, 2.0f) * 100, ForceMode.Impulse);
        }
        else if (randomDirection == 1)
        {
            _rigidbody.AddForce(Vector3.right * Random.Range(0.5f, 2.0f) * 100, ForceMode.Impulse);
        }
        else if (randomDirection == 2)
        {
            _rigidbody.AddForce(Vector3.forward * Random.Range(0.5f, 2.0f) * 100, ForceMode.Impulse);
        }
        _animator.speed = 2.7f;
    }

    // Update is called once per frame
    void Update()
    {
        //if (Input.GetKeyDown(KeyCode.K)) {
        //    _rigidbody.AddForce(Vector3.up * Random.Range(0.5f, 2.0f));
        //    randomDirection = Random.Range(0, 2);
        //        if (randomDirection == 0)
        //    {
        //        _rigidbody.AddForce(Vector3.left * Random.Range(0.5f, 2.0f) * 10);
        //    }
        //    else if (randomDirection == 1)
        //    {
        //        _rigidbody.AddForce(Vector3.right * Random.Range(0.5f, 2.0f) * 10);
        //    }
        //    else if (randomDirection == 2)
        //    {
        //        _rigidbody.AddForce(Vector3.forward * Random.Range(0.5f, 2.0f) * 10);
        //    }
        //}

        StartCoroutine(DestroyCoinWithoutTouch());
        
        if (canJourney)
        {
            StopCoroutine(DestroyCoinWithoutTouch());
            _collider.isTrigger = true;
            offsetPositionPlayer = new Vector3(player.transform.position.x, player.transform.position.y + 1.0f, player.transform.position.z);
            _animator.speed = 0.0f;
            transform.position = Vector3.Slerp(transform.position, offsetPositionPlayer, coinSpeed * Time.deltaTime);
            StartCoroutine(CoinTaked(secondsForDestroy));

            //transform._position = Vector3.MoveTowards(transform._position, player.transform._position, 1.0f * Time.deltaTime);
            //Vector3 direction = new Vector3(transform._position.x + 5.0f * Time.deltaTime, transform._position.y, transform._position.z);
            //transform._position = direction;

        }
    }

    

    IEnumerator DestroyCoinWithoutTouch() {
        yield return new WaitForSeconds(6.0f);
        //Destroy(this.gameObject);
        Destroy(transform.parent.gameObject);
    }

    IEnumerator CoinTaked(float seconds)
    {
        yield return new WaitForSeconds(seconds);
        _gameManager.coins++;
        //Destroy(this.gameObject);
        Destroy(transform.parent.gameObject);
    }
    private void OnCollisionEnter(Collision other)
    {
        

        if (other.gameObject.tag == "Player" || other.gameObject.tag == "S_car")
        {
            _rigidbody.isKinematic = true;
            player = other.gameObject;
            Debug.Log("canJourney is true");
            canJourney = true;
        }
        //else if (other.tag == "S_car") {
        //    //_animator.speed = 0.0f;
        //    //Debug.Log("Moneda con autoooooo");
        //    //_gameManager.coins++;
        //    //Destroy(this.gameObject);
        //}
    }

    

    private void OnTriggerExit(Collider other)
    {
        StopCoroutine(CoinTaked(secondsForDestroy));
    }
}
