using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Coin : MonoBehaviour
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
    void Start()
    {
        //startTime = Time.time;
        
        canJourney = false;
        _animator = GetComponent<Animator>();
        _gameManager = FindObjectOfType<GameManager>();
    }

    // Update is called once per frame
    void Update()
    {
        if (canJourney) {


            offsetPositionPlayer = new Vector3(player.transform.position.x, player.transform.position.y + 1.0f, player.transform.position.z);
            _animator.speed = 0.0f;
            transform.position = Vector3.Slerp(transform.position,offsetPositionPlayer,coinSpeed*Time.deltaTime);
            StartCoroutine(DestroyCoin(secondsForDestroy));
            
            //transform._position = Vector3.MoveTowards(transform._position, player.transform._position, 1.0f * Time.deltaTime);
            //Vector3 direction = new Vector3(transform._position.x + 5.0f * Time.deltaTime, transform._position.y, transform._position.z);
            //transform._position = direction;
            
        }
    }

    IEnumerator DestroyCoin(float seconds) {
        yield return new WaitForSeconds(seconds);
        _gameManager.coins++;
        Destroy(this.gameObject);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player" || other.tag == "S_car")
        {
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
        StopCoroutine(DestroyCoin(secondsForDestroy));
    }
}
