using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class GameManager : MonoBehaviour
{
    public GameObject coinsUI;
    private TextMeshProUGUI _textMesh;
    public MissionTrigger mission0TriggerComponent;
    public MissionTrigger mission1TriggerComponent;
    public Narrator _narrator;
    public int coins;
    public CanvasGroup radar;
    public GameObject pauseMenu;

    [Header("Pause Menu")]
    public Button continueButton;

    [Header("Characters")]
    public Sprite margeIcon;
    public Sprite apuIcon;

    [Header("Vehicles")]
    public Sprite sedanFamilyIcon;

    [Header("Location")]
    public Sprite simpsonsHouse;
    public Sprite kwikMartIcon;
    public Sprite school;

    [Header("Object")]
    public Sprite iceCream;
    public Sprite scienceProject;
    [Header("Missions Icons")]
    public Sprite Mission0;


    [HideInInspector]
    public bool isPlayerOnMission;

    //Level 1 Mission
    [HideInInspector]
    public bool _l1Mission0Complete;
    public bool _l1Mission1Complete;
    public bool _l1Mission2Complete;
    public bool _l1Mission3Complete;
    public bool _l1Mission4Complete;
    


    // Start is called before the first frame update
    void Start()
    {   
        coins = 0;
        _textMesh = coinsUI.GetComponent<TextMeshProUGUI>();
        isPlayerOnMission = false;
        pauseMenu.SetActive(false);
        continueButton.onClick.AddListener(ResumeGame);
        mission1TriggerComponent.gameObject.SetActive(false);
        //_narrator.ShowGeneralInstructions(margeIcon, "TALK TO MARGE");
    }

    // Update is called once per frame
    void Update()
    {
        _textMesh.text = "" + coins;
        if (Input.GetKeyDown(KeyCode.Return)&& mission0TriggerComponent.isColliding&&!isPlayerOnMission&&!_l1Mission0Complete) {
            StartCoroutine(_narrator.Level1_Mission0());
            isPlayerOnMission = true;
        }

        if (Input.GetKeyDown(KeyCode.Return) && mission1TriggerComponent.isColliding && !isPlayerOnMission && _l1Mission0Complete) {
            StartCoroutine(_narrator.Level1_Mission1());
            isPlayerOnMission = true;
        }

        //if (!isPlayerOnMission) {
         //   StopAllCoroutines();
        //}

        if (Input.GetKeyDown(KeyCode.Escape)) {
            if (pauseMenu.activeSelf)
            {
                ResumeGame();
            }
            else {
                PauseGame();
            }
        }
    }

    private void PauseGame() {
        Time.timeScale = 0;
        pauseMenu.SetActive(true);
    }

    private void ResumeGame() {
        pauseMenu.SetActive(false);
        Time.timeScale = 1;
        
    }

}
