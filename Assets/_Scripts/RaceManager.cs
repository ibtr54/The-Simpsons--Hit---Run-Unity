using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RaceManager : MonoBehaviour
{
    private Player _player;
    public static RaceManager Instance { get; private set; }
    [Header("Level 1 Races")]
    public List<Transform> checkpointsMission1Level1;
    public List<Transform> checkpointsExtraRace1Level1;
    private int numPlayers;
    public List<GameObject> NPCRaceCar;
    public List<GameObject> SpawnedNPCRaceCar;
    public Transform originPosRace;
    private int numLap;
    private bool raceMod;
    private bool startMode;    
    private float offsetX;
    private float offsetZ;
    public int columnNumber;

    private void Awake()
    {
        if ( Instance != null && Instance != this ) {
            Destroy( this );
            return;
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        _player = GameObject.FindGameObjectWithTag( "Player" ).GetComponent<Player>();
        offsetX = 1.0f;
        offsetZ = 1.0f;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void InitializeRace( int race, string NPCRacers, bool raceMode ) {
        if ( raceMode ) InitializeRace( race, NPCRacers, raceMode, 0 );
    }

    void StartRace() {
        foreach ( GameObject raceCar in SpawnedNPCRaceCar ) {
            raceCar.GetComponent<CarRace>().canStart = true;
        }
    }

    void InitializeRace(int race, string NPCRacers, bool raceMode, int numberLaps) {
        Vector3 currentOffset = new Vector3(0, 0, 0);
        numPlayers = NPCRacers.Length;
        char[] charNPCRacers;
        charNPCRacers = NPCRacers.ToCharArray();
        switch (race) {
            case 1:
                originPosRace = checkpointsMission1Level1[1]; // here we get the first race's checkpoint
                break;

            default:
                return;

        }


        for (int i = 0; i < numPlayers; ++i ) {
                switch ( charNPCRacers[i] ) {
                    case 's':
                        SpawnedNPCRaceCar[i] = Instantiate( NPCRaceCar[0], originPosRace.position + currentOffset, checkpointsMission1Level1[0].rotation );
                        SpawnedNPCRaceCar[i].GetComponent<CarRace>().raceMode = raceMode; // if race mode is true the race will be linear, otherwise the race will have laps to complete.
                    break;
                    default:
                        return;
                }

                if ( i+1 % columnNumber == 0 ) { 
                    
                    currentOffset = originPosRace.position;
                    if (i != 0) {
                        currentOffset += offsetZ * -originPosRace.forward * Mathf.Floor( ((i + 1) / columnNumber) );
                    }
                }
                currentOffset += offsetX * originPosRace.right;

                
            }    
            

        
        
    }
    
}
