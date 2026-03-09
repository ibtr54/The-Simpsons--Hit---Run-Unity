using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Narrator : MonoBehaviour
{

	private int n1;
	private int n2;
	private int n3;
	private int n4;
	private int n5;
	private float scaleYIL;
	public float velocityScaleYIL;
	
	private bool isLevel1Mission0Complete
    {
        get
        {
			return isLevel1Mission0Complete;
        }
        set
        {
			isLevel1Mission0Complete = value;
        }
    }
	
	public GameManager _gameManager;
	public GameObject coinsIndicator;
	private Animator _animatorPlayer;
	private Animator _animatorMarge;
	private Animator _animatorApu;
	public GameObject mainCamera;
	private GameObject player;
	private Player _player;
	private Player playerComponent;
	public GameObject marge;
	public GameObject apu;
	public GameObject carMain;
	//private string missionName;
	//private string instructions;
	public TextMeshProUGUI missionNameUI;
	public Image missionImageUI;
	public TextMeshProUGUI missionInstructionsUI;
	private bool isClickedContinueButton;
	public Button continueButton;
	private AudioSource _audioSource;
	public Animator canvasGeneralInstruction;
	private RectTransform _rTCanvasGI;
	public Image spriteMissionIcon;
	public GameObject targetObject;
	public Transform targetPosittonMission0_0;
	private GameObject targetObjectSpawned;
	public RectTransform instructionLabel;
	public TextMeshProUGUI generalInstructionsText;
	public GameObject missionCompleteSign;
	private Animator animatorMissionCompleteSign;
	public AudioClip[] popUpSound = new AudioClip[3];
	public AudioClip stageComplete;
	private GameObject exclamationShape;
	public GameObject mission0Tutorial;
	public GameObject mission0Instructions;
	

	[Header("Level 1 - Mission 0")]
	public AudioClip margeDialogM_0;
	public AudioClip homerDialogM_0;
	public AudioClip[] homerApuDialogM_0 = new AudioClip[3];
	public AudioClip congratsBart;
	public AudioClip bartCongratulation;
	public GameObject iceCream;
	public Transform iceCreamPosition;
	private GameObject iceCreamInstantied;
	private CollisionDetector collisionIceCream;
	
	private Animator animatormission0T;
	
	private Animator animatorMission0I;

	public Transform posCameraMarge;
	public Transform posCameraHomer;
	public GameObject cameraMission0Marge;
	public GameObject cameraMission0Homer;
	
	public Transform margePosttion;
	public Transform homerPosition;
	public GetInInt insideKwikMart;
	public GetInInt outsideKwikMart;
	public CollisionDetector homerApuTrigger;
	private bool missionStarted;
	private bool istalkApu;
	//private bool canShow;
	//private bool canUp;
	private bool isSodaIceCreamCollected
    {
        get
        {
			return isSodaIceCreamCollected;
        }
        set
        {
			isSodaIceCreamCollected = value;
        }
    }

	[Header("Level 1 - Mission 1")]
	public Transform posCameraMarge1;
	public Transform posCameraHomer1;
	public Transform positionTargetMission1;
	public AudioClip margeDialogM1_0;
	public AudioClip homerDialogM1_0;
	public AudioClip margeDialogM1_1;
	public AudioClip homerDialogM1_1;
	
	public AudioClip lisaDialogM1_0;
	public AudioClip homerDialogM1_2;
	public AudioClip lisaDialogM1_1;
	public AudioClip homerDialogM1_3;
	public AudioClip lisaDialogM1_2;
	public AudioClip homerDialogM1_4;
	public GameObject scienceProject;
	private GameObject scienceProjectInstantied;
	private CollisionDetector collisionScienceP;
	public Transform posScienceP;
	//[TextArea]
	//public string mission1 = "Mission 1";
	//public AudioClip margeDialog0M_1;
	//public AudioClip margeDialog1M_1;
	//public AudioClip homerDialog0M_1;
	//public AudioClip homerDialog1M_1;
	//public AudioClip endDialogM_1;
	//public GameObject cameraMission1Marge;
	//public GameObject cameraMission1Homer;
	//public GameObject carSkinner;
	//public GameObject startRace;
	//public GameObject endRace;
	//public GameObject lisa;
	//private Animator _animatorLisa;


	//[TextArea]
	//public string mission2 = "Mission 2";
	//public GameObject flanders;
	//private Animator _animatorFlanders;
	//public AudioSource startDialogM_2;

	// Start is called before the first frame update
	void Start()
    {
		scaleYIL = 0.0f;
		velocityScaleYIL = 0.4f;
		istalkApu = false;
		coinsIndicator.SetActive(true);
		mission0Tutorial.SetActive(true);
		mission0Instructions.SetActive(false);
		continueButton.onClick.AddListener(OnContinueButtonClicked);
		player = GameObject.FindGameObjectWithTag("Player");
		_player = player.GetComponent<Player>();
		_animatorPlayer = player.GetComponent<Animator>();
		_animatorMarge = marge.GetComponent<Animator>();
		_animatorApu = apu.GetComponent<Animator>();
		_audioSource = this.GetComponent<AudioSource>();
		_gameManager = GameObject.FindObjectOfType<GameManager>();
		instructionLabel.localScale = new Vector3(1, scaleYIL, 1);
		ResetNValues();
		_rTCanvasGI = canvasGeneralInstruction.gameObject.GetComponent<RectTransform>();
		playerComponent = player.GetComponent<Player>();
		animatorMissionCompleteSign = missionCompleteSign.GetComponent<Animator>();
		missionCompleteSign.SetActive(false);
		exclamationShape = GameObject.FindGameObjectWithTag("ExclamationShape");
		exclamationShape.SetActive(true);
		animatormission0T = mission0Tutorial.GetComponent<Animator>();
		animatorMission0I = mission0Instructions.GetComponent<Animator>();
		//collisionIceCream = iceCreamPosition.GetComponent<CollisionDetector>();

		ShowGeneralInstructions(_gameManager.margeIcon, "TALK TO MARGE");
		animatorMission0I.SetBool("DisappearMissionIns", false);
	}

	void Update()
	{

	}

	private void ShowMissionCompleteSign() {
		missionCompleteSign.SetActive(true);
		animatorMissionCompleteSign.Rebind();

	}

	public void ShowGeneralInstructions(Sprite missionIcon, string instructions) {
		
		spriteMissionIcon.gameObject.GetComponent<CanvasGroup>().alpha = 1;
		canvasGeneralInstruction.Rebind();
		canvasGeneralInstruction.SetBool("startShowing", false);
		canvasGeneralInstruction.SetBool("showUp", false);
		generalInstructionsText.text = instructions;
		spriteMissionIcon.sprite = missionIcon;
		canvasGeneralInstruction.SetBool("startShowing", true);
		canvasGeneralInstruction.SetBool("showUp", true);
		//_audioSource.PlayOneShot(popUpSound[Random.Range(0, 3)]);
		PlaySoundWithDelay(0.7f);
	}

	private void PlaySoundWithDelay(float seconds) {
		Invoke("pSound", seconds);
	}
	public void pSound() {
		_audioSource.PlayOneShot(popUpSound[Random.Range(0, 3)], 0.7f);
	}

	public void ShowGeneralInstructions(string instructions) {
		
		spriteMissionIcon.gameObject.GetComponent<CanvasGroup>().alpha = 0;
		canvasGeneralInstruction.Rebind();
		canvasGeneralInstruction.SetBool("startShowing", false);
		canvasGeneralInstruction.SetBool("showUp", false);
		generalInstructionsText.text = instructions;
		canvasGeneralInstruction.SetBool("startShowing", true);
		canvasGeneralInstruction.SetBool("showUp", true);
	}

	IEnumerator ShowMissionInstructions(string missionTitle, Sprite missionIcon, string missionDescription) {
		
		animatormission0T.Rebind();
		_player.canMove = false;
		_gameManager.radar.alpha = 0;
		animatormission0T.SetBool("ShowBlackScreen", true);
		yield return new WaitForSeconds(1.0f);
		mission0Instructions.SetActive(true);
		missionNameUI.text = missionTitle;
		missionInstructionsUI.text = missionDescription;
		missionImageUI.sprite = missionIcon;
		animatorMission0I.Rebind();
		animatorMission0I.SetBool("DisappearMissionIns", false);
		while (!isClickedContinueButton)
		{
			yield return null;
		}
		_player.canMove = true;
		_gameManager.radar.alpha = 1;
		animatorMission0I.SetBool("DisappearMissionIns", true);
		Debug.Log("DisappearMissionIns = true");
		animatormission0T.SetBool("ShowBlackScreen", false);
		animatormission0T.SetBool("isCutScene", false);
		//mission0Instructions.SetActive(false);
	}

	

	public IEnumerator Level1_Mission0() {
		ResetPosition();

		//Start Marge Dialog
		//canvasGeneralInstruction.gameObject.SetActive(false);
		animatormission0T.SetBool("isCutScene", true);
		exclamationShape.SetActive(false);
		_player.canMove = false;
		_gameManager.radar.alpha = 0;
		canvasGeneralInstruction.gameObject.GetComponent<CanvasGroup>().alpha = 0;
		coinsIndicator.SetActive(false);
		mainCamera.SetActive(false);
		_animatorMarge.SetBool("Mission 0", true);
		_animatorPlayer.SetBool("Mission 0", true);
        _gameManager.mission0TriggerComponent.gameObject.SetActive(false);
        cameraMission0Homer.transform.position = posCameraHomer.transform.position;
		cameraMission0Homer.transform.rotation = posCameraHomer.transform.rotation;
		cameraMission0Marge.transform.position = posCameraMarge.transform.position;
		cameraMission0Marge.transform.rotation = posCameraMarge.transform.rotation;

		cameraMission0Marge.SetActive(true);
		cameraMission0Homer.SetActive(false);
		_audioSource.PlayOneShot(margeDialogM_0);
			
		

		yield return new WaitForSeconds(margeDialogM_0.length);


		//Start Homer Dialog
		mainCamera.SetActive(false);
		cameraMission0Marge.SetActive(false);
		cameraMission0Homer.SetActive(true);
		_audioSource.PlayOneShot(homerDialogM_0);
		
		yield return new WaitForSeconds(homerDialogM_0.length+0.5f);

		mainCamera.SetActive(true);
		cameraMission0Marge.SetActive(false);
		cameraMission0Homer.SetActive(false);
		coinsIndicator.SetActive(true);
		//Start Animation
		//mission0Tutorial.SetActive(true);
		//missionName = "The Cola Caper";
		//instructions = "GO TO THE KWIK-E-MART AND PURCHASE\nSOME ICE CREAM AND COLA";

		
		StartCoroutine(ShowMissionInstructions("The Cola Caper", _gameManager.Mission0, "GO TO THE KWIK-E-MART AND PURCHASE\nSOME ICE CREAM AND COLA"));
		
		while (!isClickedContinueButton) {
			yield return null;
		}
		yield return null;
		
		//Finish Animation
		//mission0Tutorial.SetActive(false);

		canvasGeneralInstruction.gameObject.GetComponent<CanvasGroup>().alpha = 1;
		_animatorMarge.SetBool("Mission 0", false);
		_animatorPlayer.SetBool("Mission 0", false);
		
		ShowGeneralInstructions(/*_gameManager.sedanFamilyIcon,*/ "GET INTO YOUR VEHICLE");

		while (!playerComponent.isDriverMode) {
			yield return null;
		}
		ShowGeneralInstructions(_gameManager.kwikMartIcon, "DRIVE TO THE\nKWIK-E-MART");
		
		targetObjectSpawned = Instantiate(targetObject.gameObject, targetPosittonMission0_0.position, Quaternion.identity); //TODO: Create a gameobject and attach it to the Instantiate Function.
		
		while ((targetPosittonMission0_0.position-player.transform.position).sqrMagnitude>100) {
			//Debug.Log("Valor de colision en corrutina: "+ (targetObject.gameObject.transform.position - player.transform.position).sqrMagnitude);
			yield return null;
		}

		Destroy(targetObjectSpawned.gameObject);
		_audioSource.PlayOneShot(stageComplete);
		//Destroy(targetObject.gameObject); Destroying assets is not permitted to avoid data loss.
//		targetObject.SetActive(false);

		ShowGeneralInstructions(_gameManager.kwikMartIcon, "ENTER THE KWIK-E-MART");

		while (!insideKwikMart.isColliding) {
			yield return null;
		}
		
		yield return new WaitForSeconds(0.7f);
		ShowGeneralInstructions(_gameManager.apuIcon, "TALK TO APU");

		while (!homerApuTrigger.isColliding && !Input.GetKeyDown(KeyCode.Return)) {
			yield return null;
		}
		
		canvasGeneralInstruction.gameObject.GetComponent<CanvasGroup>().alpha = 0;
		_animatorPlayer.SetBool("Mission 0", true);
		_animatorApu.SetBool("Mission 0", true);
		_player.canMove = false;
		_audioSource.PlayOneShot(homerApuDialogM_0[0]);
		yield return new WaitForSeconds(homerApuDialogM_0[0].length);

		_audioSource.PlayOneShot(homerApuDialogM_0[1]);
		yield return new WaitForSeconds(homerApuDialogM_0[1].length);

		_audioSource.PlayOneShot(homerApuDialogM_0[2]);
		yield return new WaitForSeconds(homerApuDialogM_0[2].length);
		_player.canMove = true;
		_animatorPlayer.SetBool("Mission 0", false);
		_animatorApu.SetBool("Mission 0", false);
		canvasGeneralInstruction.gameObject.GetComponent<CanvasGroup>().alpha = 1;
		iceCreamInstantied = Instantiate(iceCream, iceCreamPosition.position, iceCreamPosition.rotation);

		collisionIceCream = iceCreamInstantied.GetComponent<CollisionDetector>();

		ShowGeneralInstructions(_gameManager.iceCream, "COLLECT THE ICE CREAM\nAND COLA");

		while (!collisionIceCream.isColliding) {
			yield return null;
		}

		Destroy(iceCreamInstantied.gameObject);

		ShowMissionCompleteSign();
		_gameManager._l1Mission0Complete = true;
		_audioSource.PlayOneShot(congratsBart);
		yield return new WaitForSeconds(congratsBart.length-0.5f);


		ShowGeneralInstructions(_gameManager.simpsonsHouse, "DRIVE TO THE SIMPSONS\nHOUSE");

		targetObjectSpawned = null;
		targetObjectSpawned = Instantiate(targetObject.gameObject, positionTargetMission1.position, Quaternion.identity);

		Debug.Log("D: "+ (positionTargetMission1.position - player.transform.position).sqrMagnitude);

		while((positionTargetMission1.position - player.transform.position).sqrMagnitude>100.0f){
			yield return null;

		}
		//yield return new WaitForSeconds(5.0f);
		Destroy(targetObjectSpawned.gameObject);
		exclamationShape.SetActive(true);
		_gameManager.mission1TriggerComponent.gameObject.SetActive(true);
		ShowGeneralInstructions(_gameManager.margeIcon, "TALK TO MARGE");
		_gameManager.isPlayerOnMission = false;
		//_gameManager.isPlayerOnMission = false;
		//Apu mission 0 mode



		//else if (!_audioSource.isPlaying && n2 > 0)
		//{
		//	cameraMission0Homer.SetActive(false);
		//	mainCamera.SetActive(true);
		//	//Show menu tutorial
		//	coinsIndicator.SetActive(false);
		//	mission0Tutorial.SetActive(true);
		//	missionName = "The Cola Caper";
		//	instructions = "GO TO THE KWIK-E-MART AND PURCHASE\nSOME ICE CREAM AND COLA";
		//	if (isClickedContinueButton)
		//	{
		//		coinsIndicator.SetActive(true);
		//		mission0Tutorial.SetActive(false);
		//		_animatorMarge.SetBool("Mission 0", false);
		//		_animatorPlayer.SetBool("Mission 0", false);
		//		//Instantie Apu
		//		if (!_audioSource.isPlaying && n3 == 0 && istalkApu)
		//		{
		//			StartCoroutine(playAudios(homerApuDialogM_0));
		//			//_audioSource.PlayOneShot(homerApuDialogM_0);

		//			n3++;
		//		}
		//		else if (!_audioSource.isPlaying && n3 > 0)
		//		{

		//			if (n4 == 0)
		//			{
		//				StopAllCoroutines();
		//				//Instantie IceCream and soda
		//				n4++;
		//			}
		//			else if (isSodaIceCreamCollected)
		//			{
		//				_audioSource.PlayOneShot(bartCongratulation);
		//				isLevel1Mission0Complete = true;
		//				ResetNValues();
		//				//Show Mission Complete
		//				//Start next Mission

		//			}


		//		}

		//	}
		//}
		//      }



	}

	public IEnumerator Level1_Mission1() {
		//ResetPosition(); TODO: Delete this function

		//Start Marge Dialog
		//canvasGeneralInstruction.gameObject.SetActive(false);
		isClickedContinueButton = false;
		animatormission0T.SetBool("isCutScene", true);
		exclamationShape.SetActive(false);
		_player.canMove = false;
		_gameManager.radar.alpha = 0;
		canvasGeneralInstruction.gameObject.GetComponent<CanvasGroup>().alpha = 0;
		coinsIndicator.SetActive(false);
		mainCamera.SetActive(false);
		_animatorMarge.SetBool("Mission 0", true);	//TODO: Change to animation for Mission1
		_animatorPlayer.SetBool("Mission 0", true); //TODO: Change to animation for Mission1
		cameraMission0Homer.transform.position = posCameraHomer1.transform.position;
		cameraMission0Homer.transform.rotation = posCameraHomer1.transform.rotation;
		cameraMission0Marge.transform.position = posCameraMarge1.transform.position;
		cameraMission0Marge.transform.rotation = posCameraMarge1.transform.rotation;
		_gameManager.mission1TriggerComponent.gameObject.SetActive(false);
		cameraMission0Marge.SetActive(true);
		_audioSource.PlayOneShot(margeDialogM1_0);
		yield return new WaitForSeconds(margeDialogM1_0.length);
		cameraMission0Marge.SetActive(false);
		
		cameraMission0Homer.SetActive(true);
		_audioSource.PlayOneShot(homerDialogM1_0);
		yield return new WaitForSeconds(homerDialogM1_0.length);
		cameraMission0Homer.SetActive(false);

		cameraMission0Marge.SetActive(true);
		_audioSource.PlayOneShot(margeDialogM1_1);
		yield return new WaitForSeconds(margeDialogM1_1.length);
		cameraMission0Marge.SetActive(false);

		cameraMission0Homer.SetActive(true);
		_audioSource.PlayOneShot(homerDialogM1_1);
		yield return new WaitForSeconds(homerDialogM1_1.length);
		cameraMission0Homer.SetActive(false);

		mainCamera.SetActive(true);

		StartCoroutine(ShowMissionInstructions("S-M-R-T", _gameManager.Mission0, "RACE PRINCIPAL SKINNER TO THE SCHOOL AND\nGIVE LISA HER SCIENCE PROJECT"));
		while (!isClickedContinueButton)
		{
			yield return null;
		}
		yield return null;

		canvasGeneralInstruction.gameObject.GetComponent<CanvasGroup>().alpha = 1;
		_animatorMarge.SetBool("Mission 0", false);
		_animatorPlayer.SetBool("Mission 0", false);

		ShowGeneralInstructions(_gameManager.scienceProject, "COLLECT LISA'S SCIENCE\nPROJECT");

		scienceProjectInstantied = Instantiate(scienceProject, posScienceP.position, posScienceP.rotation);
		collisionScienceP = scienceProjectInstantied.GetComponent<CollisionDetector>();

		while (!collisionScienceP.isColliding) {
			yield return null;
		}
		yield return null;
		Destroy(scienceProjectInstantied.gameObject);

		ShowGeneralInstructions(_gameManager.school, "DRIVE TO THE SCHOOL");




	}


	//void Level1_Mission1() {
	//	ResetPosition();
	//	_animatorMarge.SetBool("Mission 2", true);
	//	_animatorPlayer.SetBool("Mission 2", true);
	//	if (!_audioSource.isPlaying && n1 == 0)
	//	{

	//		_audioSource.PlayOneShot(margeDialog0M_1);
	//		//margeDialog0M_1.Play();
	//		n1++;
	//	}
	//	else if (!_audioSource && n1 > 0) {
	//		if(!_audioSource.isPlaying && n2 == 0)
	//           {
	//			_audioSource.PlayOneShot(homerDialog0M_1);
	//			//homerDialog0M_1.Play();
	//			n2++;
	//           }else if(!_audioSource.isPlaying && n2>0){
	//			if (!_audioSource.isPlaying && n3 == 0) {
	//				_audioSource.PlayOneShot(margeDialog1M_1);
	//				//margeDialog1M_1.Play();
	//				n3++;
	//			} else if (!_audioSource.isPlaying/*!margeDialog1M_1.isPlaying*/ && n3>0) {

	//			}
	//           }
	//	}


	//}

	void ResetNValues() {
		n1 = 0;
		n2 = 0;
		n3 = 0;
		n4 = 0;
		n5 = 0;
	}

	void ResetPosition()
    {
		marge.transform.position = margePosttion.position;
		marge.transform.rotation = margePosttion.rotation;
		player.transform.position = homerPosition.position;
		player.transform.rotation = homerPosition.rotation;
	}

	void OnContinueButtonClicked() {
		isClickedContinueButton = true;
	}

	IEnumerator playAudios(AudioClip[] audios) {
		foreach (AudioClip i in audios) {
			_audioSource.PlayOneShot(i);
			while (_audioSource.isPlaying) {
				yield return null;
			}
		}
	}

    // Update is called once per frame
    
}
