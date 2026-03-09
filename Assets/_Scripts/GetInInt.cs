using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class GetInInt : MonoBehaviour
{
    // Start is called before the first frame update
    private GameObject Player;
    private GameManager _gameManager;
    public Camera mainCamera;
    public Camera inDoorCamera;
    private FollowPlayerInDoors _followPlayer;
    public Transform posStartPlayerInDoor;
    public Transform posEndPlayerOutDoor;
    public CameraController _cameraController;
    public Animator animatorCutOut;
    private AnimationClip[] cutOutClips;
    

    [HideInInspector]
    public bool isColliding;

    void Start()
    {
        inDoorCamera.gameObject.SetActive(false);
        Player = GameObject.FindGameObjectWithTag("Player");
        cutOutClips = animatorCutOut.runtimeAnimatorController.animationClips;
        _gameManager = GameObject.FindObjectOfType<GameManager>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerExit(Collider other)
    {
        isColliding = false;
    }

    private void OnTriggerEnter(Collider other)
    {
        isColliding = true;
    }

    private void OnTriggerStay(Collider other)
    {
        isColliding = true;
        if (other.CompareTag("Player") && Input.GetKeyDown(KeyCode.Return) && !_cameraController.isPlayerInDoors)
        {
            //StartCoroutine(CutOut());
            StartCoroutine(CutOutIn());

        }
        else if (other.CompareTag("Player") && Input.GetKeyDown(KeyCode.Return) && _cameraController.isPlayerInDoors) {
            //StartCoroutine(CutOut());
            StartCoroutine(CutOutOut());
        }
    }

   

    IEnumerator CutOutIn() {
        animatorCutOut.SetTrigger("CutOut");
        _gameManager.radar.alpha = 0;
        yield return new WaitForSeconds(cutOutClips[0].length + 0.5f);
        Player.transform.position = posStartPlayerInDoor.transform.position;
        mainCamera.gameObject.SetActive(false);
        inDoorCamera.gameObject.SetActive(true);
        _followPlayer = inDoorCamera.gameObject.GetComponentInParent<FollowPlayerInDoors>();
        _followPlayer.canMove = true;
        _cameraController.isPlayerInDoors = true;
        animatorCutOut.SetTrigger("CutOut");
    }

    IEnumerator CutOutOut() {
        animatorCutOut.SetTrigger("CutOut");
        _gameManager.radar.alpha = 1;
        yield return new WaitForSeconds(cutOutClips[0].length + 0.5f);
        Player.transform.position = posEndPlayerOutDoor.transform.position;
        mainCamera.gameObject.SetActive(true);
        inDoorCamera.gameObject.SetActive(false);
        _followPlayer = inDoorCamera.gameObject.GetComponentInParent<FollowPlayerInDoors>();
        _followPlayer.canMove = false;
        _followPlayer.ResetPos();
        _cameraController.isPlayerInDoors = false;
        animatorCutOut.SetTrigger("CutOut");
    }
}
