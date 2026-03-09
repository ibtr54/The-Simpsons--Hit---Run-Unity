using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPC_Controller : MonoBehaviour
{

    //cars
    public List<Transform> CheckpointsCar;

    //Person
    public List<Transform> CheckpointsNPCPart1_1;
    public List<Transform> CheckpointsNPCPart1_2;
    public List<Transform> CheckpointsNPCPart1_3;
    //Part 2
    public List<Transform> CheckpointsNPCPart2_1;
    public List<Transform> CheckpointsNPCPart2_2;
    public List<Transform> CheckpointsNPCPart2_3;
    //Part 3
    public List<Transform> CheckpointsNPCPart3_1;
    public List<Transform> CheckpointsNPCPart3_2;
    public List<Transform> CheckpointsNPCPart3_3;
    public List<Transform> CheckpointsNPCPart3_4;
    //Part 4
    public List<Transform> CheckpointsNPCPart4_1;
    public List<Transform> CheckpointsNPCPart4_2;
    public List<Transform> CheckpointsNPCPart4_3;
    //Part5
    public List<Transform> CheckpointsNPCPart5_1;
    public List<Transform> CheckpointsNPCPart5_2;
    //Part6
    public List<Transform> CheckpointsNPCPart6_1;
    public List<Transform> CheckpointsNPCPart6_2;
    public List<Transform> CheckpointsNPCPart6_3;
    public List<Transform> CheckpointsNPCPart6_4;
    //Part7
    public List<Transform> CheckpointsNPCPart7_1;
    public List<Transform> CheckpointsNPCPart7_2;
    //part8
    public List<Transform> CheckpointsNPCPart8_1;
    public List<Transform> CheckpointsNPCPart8_2;
    public List<Transform> CheckpointsNPCPart8_3;

    public List<GameObject> NPCPersons;
    public List<GameObject> NPCCountryPersons;
    public List<GameObject> NPCCars;

    public int activeChunk;
    public GameObject chunkController;
    private ChunkController _chController;
    private const int max_NPCPersons = 15;
    public int spawnedNPCPart1;
    public int spawnedNPCPart2;
    public int spawnedNPCPart3;
    public int spawnedNPCPart4;
    public int spawnedNPCPart5;
    public int spawnedNPCPart6;
    public int spawnedNPCPart7;
    public int spawnedNPCPart8;
    public int spawnedCarPart1;
    public int spawnedCarWorld;
    private GameObject npcInstance;
    private int leftRightLaneRandom;
    private const int maxSpawnedCarPart1 = 3;
    private const int maxSpawnedCarPChunk = 9;
    private int positionCar;
    private Vector3 vectorDirectionTargetCar;
    private Quaternion rotationCar;
    private int randomCar;
    private GameObject carInstantie;
    private CarAI carNPC;
    private Vector3 vectorToPlayer;
    private GameObject player;
    private int maxWhileExecution;

    // Start is called before the first frame update
    void Start()
    {
        //activeChunk = 1;
        spawnedNPCPart1 = 0;
        spawnedNPCPart2 = 0;
        spawnedNPCPart3 = 0;
        spawnedNPCPart4 = 0;
        spawnedNPCPart5 = 0;
        spawnedNPCPart6 = 0;
        spawnedNPCPart7 = 0;
        spawnedNPCPart8 = 0;
        spawnedCarPart1 = 0;
        spawnedCarWorld = 0;
        maxWhileExecution = 100;
        player = GameObject.FindGameObjectWithTag("Player");
        _chController = chunkController.GetComponent<ChunkController>();
        SpawnCarOnStart();
    }

    // Update is called once per frame
    void Update()
    {
        activeChunk = _chController.getActiveChunck;

        if (spawnedNPCPart1 <= max_NPCPersons && spawnedNPCPart2 <= max_NPCPersons && spawnedNPCPart3 <= max_NPCPersons && spawnedNPCPart4 <= max_NPCPersons && spawnedNPCPart5 <= max_NPCPersons && spawnedNPCPart6 <= max_NPCPersons && spawnedNPCPart7 <= max_NPCPersons && spawnedNPCPart8 <= max_NPCPersons)
        {
            GenerateNPCPerson();
        }

        if (spawnedCarWorld <= maxSpawnedCarPChunk)
        {
            GenerateNPCCars();
        }
    }

    void SpawnCarAtAtoBOnStart(int positionA, int positionB)
    {
        positionCar = Random.Range(positionA, positionB);


        int im = 0;
        while (CheckpointsCar[positionCar].gameObject.GetComponent<CollisionDetector>().isColliding /*|| vectorToPlayer.sqrMagnitude>7000*/)
        {
            if (im > maxWhileExecution) break;
            if (positionCar >= CheckpointsCar.Count - 2) positionCar = 0;
            if (CheckpointsCar[positionCar].gameObject.GetComponent<CollisionDetector>().isColliding) positionCar += 2;
            //if (7000 < vectorToPlayer.sqrMagnitude) /*positionCar = Random.Range(positionA, positionB)*/ positionCar++;
            im++;
        }

        //vectorToPlayer = player.transform.position - CheckpointsCar[positionCar].position;

        if ((positionCar + 1) < CheckpointsCar.Count - 1 /*&& vectorToPlayer.sqrMagnitude > 18000*/)
        {
            vectorDirectionTargetCar = CheckpointsCar[positionCar + 1].position - CheckpointsCar[positionCar].position;
            rotationCar = Quaternion.LookRotation(vectorDirectionTargetCar, Vector3.up);
            carInstantie = Instantiate(NPCCars[randomCar], CheckpointsCar[positionCar].position, rotationCar);
            carNPC = carInstantie.GetComponentInChildren<CarAI>();
            carNPC.position = positionCar;
            spawnedCarWorld++;
        }
        else
        {
            Debug.Log("Index out of bounds, car will not be instantied");
        }


        //spawnedCarPart1++;
    }


    void SpawnCarAtAtoB(int positionA, int positionB)
    {
        positionCar =Random.Range(positionA, positionB);
        
        
        int im = 0;
        while (CheckpointsCar[positionCar].gameObject.GetComponent<CollisionDetector>().isColliding /*|| vectorToPlayer.sqrMagnitude>7000*/)
        {
            if (im > maxWhileExecution) break;
            if (positionCar >= CheckpointsCar.Count - 2) positionCar = 0;
            if (CheckpointsCar[positionCar].gameObject.GetComponent<CollisionDetector>().isColliding) positionCar+=2;
            //if (7000 < vectorToPlayer.sqrMagnitude) /*positionCar = Random.Range(positionA, positionB)*/ positionCar++;
            im++;
        }

        vectorToPlayer = player.transform.position - CheckpointsCar[positionCar].position;

        if ((positionCar + 1) < CheckpointsCar.Count - 1 && vectorToPlayer.sqrMagnitude > 18000)
        {
            vectorDirectionTargetCar = CheckpointsCar[positionCar + 1].position - CheckpointsCar[positionCar].position;
            rotationCar = Quaternion.LookRotation(vectorDirectionTargetCar, Vector3.up);
            carInstantie = Instantiate(NPCCars[randomCar], CheckpointsCar[positionCar].position, rotationCar);
            carNPC = carInstantie.GetComponentInChildren<CarAI>();
            carNPC.position = positionCar;
            spawnedCarWorld++;
        }
        else {
            Debug.Log("Index out of bounds, car will not be instantied");
        }
       
        
        //spawnedCarPart1++;
    }



    void SpawnCarAtAtoB(int positionA, int positionB,int exA,int exB) {
        
        int[] exNumber = new int[exB-exA];
        if (exB - exA > 0) {
            //foreach (int i in exNumber)
            //{
            //    exNumber[i] = exA;
            //    exA++;
            //}
            for(int i = 0; i < exNumber.Length; i++)
            {
                exNumber[i] = exA;
                exA++;
            }
        }
        
        positionCar = RandomExceptList(positionA, positionB, exNumber);/*Random.Range(positionA, positionB)*/
        
        
        int im = 0;
        while (CheckpointsCar[positionCar].gameObject.GetComponent<CollisionDetector>().isColliding /*|| vectorToPlayer.sqrMagnitude> 7000*/)
        {
            if (im > maxWhileExecution) break;
            if (positionCar>=CheckpointsCar.Count-2)positionCar=0;
            if(CheckpointsCar[positionCar].gameObject.GetComponent<CollisionDetector>().isColliding)positionCar+=2;
            //if(7000 < vectorToPlayer.sqrMagnitude) /*positionCar = RandomExceptList(positionA, positionB, exNumber);*/ positionCar++;
            im++;
        }
        vectorToPlayer = player.transform.position - CheckpointsCar[positionCar].position;
        if ((positionCar + 1) < CheckpointsCar.Count - 1 && vectorToPlayer.sqrMagnitude>18000)
        {
            vectorDirectionTargetCar = CheckpointsCar[positionCar + 1].position - CheckpointsCar[positionCar].position;
            rotationCar = Quaternion.LookRotation(vectorDirectionTargetCar, Vector3.up);
            carInstantie = Instantiate(NPCCars[randomCar], CheckpointsCar[positionCar].position, rotationCar);
            carNPC = carInstantie.GetComponentInChildren<CarAI>();
            carNPC.position = positionCar;
            spawnedCarWorld++;
        }
        else {
            Debug.Log("Index out of bounds, car will not be instantied");
        }
        
        //spawnedCarPart1++;
    }

    void SpawnCarAtAtoB(int positionA, int positionB, int exA, int exB,int exC,int exD)
    {

        int[] exNumber = new int[exB - exA];
        if (exB - exA > 0)
        {
            //foreach (int i in exNumber)
            //{
            //    exNumber[i] = exA;
            //    exA++;
            //}
            for (int i = 0; i < exNumber.Length; i++)
            {
                exNumber[i] = exA;
                exA++;
            }
        }

        positionCar = RandomExceptList(positionA, positionB, exNumber,exC,exD);/*Random.Range(positionA, positionB)*/
        //  Debug.Log(positionCar + " - " + (positionCar + 1));
        
        int im = 0;
        while (CheckpointsCar[positionCar].gameObject.GetComponent<CollisionDetector>().isColliding /*|| vectorToPlayer.sqrMagnitude> 7000*/)
        {
            if (im > maxWhileExecution) break;
            if (positionCar >= CheckpointsCar.Count - 2) positionCar = 0;
            if (CheckpointsCar[positionCar].gameObject.GetComponent<CollisionDetector>().isColliding) positionCar += 2;
            //if(7000 < vectorToPlayer.sqrMagnitude) /*positionCar = RandomExceptList(positionA, positionB, exNumber);*/ positionCar++;
            im++;
        }
        //Debug.Log("line: 206 "+positionCar + " - " + (positionCar + 1));
        vectorToPlayer = player.transform.position - CheckpointsCar[positionCar].position;
        if ((positionCar + 1) < CheckpointsCar.Count - 1 && vectorToPlayer.sqrMagnitude > 18000)
        {
            vectorDirectionTargetCar = CheckpointsCar[positionCar + 1].position - CheckpointsCar[positionCar].position;
            rotationCar = Quaternion.LookRotation(vectorDirectionTargetCar, Vector3.up);
            carInstantie = Instantiate(NPCCars[randomCar], CheckpointsCar[positionCar].position, rotationCar);
            carNPC = carInstantie.GetComponentInChildren<CarAI>();
            carNPC.position = positionCar;
            spawnedCarWorld++;
        }
        else {
            Debug.Log("Index out of bounds, car will not be instantied");
        }
       
        //spawnedCarPart1++;
    }

    void SpawnCarOnStart() {
        for (int i = 0; i < maxSpawnedCarPart1; i++) {
            leftRightLaneRandom = Random.Range(0, 2);
            randomCar = Random.Range(0, NPCCars.Count);
            if (leftRightLaneRandom == 0)
            {
                //positionCar = Random.Range(83, 101);
                //vectorDirectionTargetCar = CheckpointsCar[positionCar + 1].position - CheckpointsCar[positionCar].position;
                //rotationCar = Quaternion.LookRotation(vectorDirectionTargetCar, Vector3.up);
                //carInstantie = Instantiate(NPCCars[randomCar], CheckpointsCar[positionCar].position, rotationCar);
                //carNPC = carInstantie.GetComponentInChildren<CarAI>();
                //carNPC.position = positionCar;
                
                SpawnCarAtAtoBOnStart(83, 101);
                spawnedCarPart1++;
            }
            else
            {
                //positionCar = Random.Range(324, 338);
                //vectorDirectionTargetCar = CheckpointsCar[positionCar + 1].position - CheckpointsCar[positionCar].position;
                //rotationCar = Quaternion.LookRotation(vectorDirectionTargetCar, Vector3.up);
                //carInstantie = Instantiate(NPCCars[randomCar], CheckpointsCar[positionCar].position, rotationCar);
                //carNPC = carInstantie.GetComponentInChildren<CarAI>();
                //carNPC.position = positionCar;
                //spawnedCarPart1++;
                SpawnCarAtAtoBOnStart(324, 338);
                spawnedCarPart1++;
            }
        }
        
    }

    void GenerateNPCCars() {


        leftRightLaneRandom = Random.Range(0, 2);
        randomCar = Random.Range(0, NPCCars.Count);
        switch (activeChunk)
        {
            case 1:
                if (leftRightLaneRandom == 0)
                {
                   
                    SpawnCarAtAtoB(63, 118,83,100);
                    
                }
                else {
                   
                    SpawnCarAtAtoB(308, 361,323,338);
                }
                    break;
            case 2:
                int lrl = Random.Range(0, 2);
                if (leftRightLaneRandom == 0)
                {
                    if (lrl == 0)
                    {
                        SpawnCarAtAtoB(83, 130, 100, 118);
                    }
                    else {
                        SpawnCarAtAtoB(83, 170, 100, 130);
                    }
                   
                }
                else {
                    if (lrl == 0)
                    {
                        SpawnCarAtAtoB(257, 338, 296, 323);
                    }
                    else {
                        SpawnCarAtAtoB(296, 338, 307, 323);
                    }
                    
                }
                break;
            case 3:
                if (leftRightLaneRandom == 0)
                {
                    SpawnCarAtAtoB(100, 194, 118, 131,159,171);
                }
                else {
                    SpawnCarAtAtoB(231, 323, 257, 267,296,307);
                }
                break;
            case 4:
                if (leftRightLaneRandom == 0)
                {
                    SpawnCarAtAtoB(119, 214, 131, 158, 171, 200);
                }
                else {
                    SpawnCarAtAtoB(215, 307, 226, 256, 269, 295);
                }
                break;
            case 5:
                SpawnCarAtAtoB(131, 295, 202, 225);
                break;

            case 6:
                if (leftRightLaneRandom == 0)
                {
                    SpawnCarAtAtoB(331, 385, 340, 362);
                }
                else {
                    SpawnCarAtAtoB(37, 91, 54, 84);
                }
                break;
            case 7:
                if (leftRightLaneRandom == 0)
                {
                    SpawnCarAtAtoB(0, 82, 36, 54);
                }
                else {
                    SpawnCarAtAtoB(339, 420, 371, 390);
                }
                break;
            case 8:
                if (leftRightLaneRandom == 0)
                {
                    SpawnCarAtAtoB(38, 54);
                }
                else {
                    SpawnCarAtAtoB(371, 390);
                }
                //SpawnCarAtAtoB(38,389,54,371);
                break;
                
        }
    }

    public static int RandomExceptList(int min, int max, int[] forbiddenElements)
    {
        bool numberAllowed = true;
        //for (int i = 0; i < forbiddenElements.Length; i++)
        //{
        //    if (forbiddenElements[i] < min || forbiddenElements[i] > max)
        //    {
        //        string temp;
        //        if (forbiddenElements[i] < min) {
        //            temp = forbiddenElements[i] + " < " + min;
        //        }
        //        else {
        //            temp = forbiddenElements[i] + " > " + min;
        //        }
        //        Debug.Log("All forbidden numbers have to be inside given range." + temp);
        //        return min;
        //    }
        //    for (int j = i + 1; j < forbiddenElements.Length; j++)
        //    {
        //        if (forbiddenElements[i] == forbiddenElements[j])
        //        {
        //            Debug.Log("Forbidden numbers have to be different between each other.");
        //            return min;
        //        }
        //    }
        //}
        if (max < min)
        {
            Debug.Log("Minimum limit has to be lower than maximum limit...");
            return min;
        }
        if (forbiddenElements.Length > (max - min))
        {
            Debug.Log("You can't forbid so many numbers.");
            return min;
        }
        if (forbiddenElements.Length >= 1)
        {
           while (true)
           {
                numberAllowed = true;
                int r = Random.Range(min, max);
                for (int i = 0; i < forbiddenElements.Length; i++)
                {
                    if (r == forbiddenElements[i])
                        numberAllowed = false;
                }
                if (numberAllowed )
                    return r;
           }
        }
        else
        {
            int r = Random.Range(min, max);
            return r;
        }
    }

    public static int RandomExceptList(int min, int max, int[] forbiddenElements, int exC, int exD)
    {
        bool numberAllowed = true;
        //for (int i = 0; i < forbiddenElements.Length; i++)
        //{
        //    if (forbiddenElements[i] < min || forbiddenElements[i] > max)
        //    {
        //        string temp;
        //        if (forbiddenElements[i] < min)
        //        {
        //            temp = forbiddenElements[i] + " < " + min;
        //        }
        //        else
        //        {
        //            temp = forbiddenElements[i] + " > " + min;
        //        }
        //        Debug.Log("All forbidden numbers have to be inside given range." + temp);
        //        return min;
        //    }
        //    for (int j = i + 1; j < forbiddenElements.Length; j++)
        //    {
        //        if (forbiddenElements[i] == forbiddenElements[j])
        //        {
        //            Debug.Log("Forbidden numbers have to be different between each other.");
        //            return min;
        //        }
        //    }
        //}
        if (max < min)
        {
            Debug.Log("Minimum limit has to be lower than maximum limit...");
            return min;
        }
        if (forbiddenElements.Length > (max - min))
        {
            Debug.Log("You can't forbid so many numbers.");
            return min;
        }
        if (forbiddenElements.Length >= 1)
        {
            int maxWhileExecution = 100;
            int k=0;
            while (k<maxWhileExecution)
            {
                numberAllowed = true;
                int r = Random.Range(min, max);
                for (int i = 0; i < forbiddenElements.Length; i++)
                {
                    if (r == forbiddenElements[i])
                        numberAllowed = false;
                }
                if (numberAllowed && (r<exC || r>exD))
                    return r;
                k++;
            }
        }
        else
        {
            int r = Random.Range(min, max);
            return r;
        }
        return 0;
    }

    void GenerateNPCPerson()
    {
        int subpart;
        int positionSubPart;
        int randomNPC;
        GameObject npc;
        NPCPerson NPCComponent;
        NPCPerson clone;
        
        if (activeChunk == 1)
        {
            
            subpart = UnityEngine.Random.Range(1, 4);

            switch (subpart)
            {
                case 1:
                    positionSubPart = UnityEngine.Random.Range(1, CheckpointsNPCPart1_1.Count - 1);
                    randomNPC = UnityEngine.Random.Range(0, NPCPersons.Count);
                    npc = NPCPersons[randomNPC]; // Asignamos el prefab en lugar del componente
                    NPCComponent = npc.GetComponentInChildren<NPCPerson>();

                    // Ahora, instanciamos el prefab y obtenemos una referencia al componente NPCPerson en el objeto instanciado
                    npcInstance = Instantiate(npc, CheckpointsNPCPart1_1[positionSubPart].position, Quaternion.identity);
                    NPCComponent = npcInstance.GetComponentInChildren<NPCPerson>();

                    NPCComponent.checkpointPart = 1;
                    NPCComponent.checkpointSubPart = 1;
                    NPCComponent._position = positionSubPart;
                    spawnedNPCPart1++;
                    break;

                case 2:
                    positionSubPart = UnityEngine.Random.Range(1, CheckpointsNPCPart1_2.Count - 1);
                    randomNPC = UnityEngine.Random.Range(0, NPCPersons.Count);
                    npc = NPCPersons[randomNPC]; // Asignamos el prefab en lugar del componente
                    NPCComponent = npc.GetComponentInChildren<NPCPerson>();

                    // Ahora, instanciamos el prefab y obtenemos una referencia al componente NPCPerson en el objeto instanciado
                    npcInstance = Instantiate(npc, CheckpointsNPCPart1_2[positionSubPart].position, Quaternion.identity);
                    NPCComponent = npcInstance.GetComponentInChildren<NPCPerson>();

                    NPCComponent.checkpointPart = 1;
                    NPCComponent.checkpointSubPart = 2;
                    NPCComponent._position = positionSubPart;
                    spawnedNPCPart1++;
                    break;
                case 3:
                    positionSubPart = UnityEngine.Random.Range(1, CheckpointsNPCPart1_3.Count - 1);
                    randomNPC = UnityEngine.Random.Range(0, NPCPersons.Count);
                    npc = NPCPersons[randomNPC]; // Asignamos el prefab en lugar del componente
                    NPCComponent = npc.GetComponentInChildren<NPCPerson>();

                    // Ahora, instanciamos el prefab y obtenemos una referencia al componente NPCPerson en el objeto instanciado
                    npcInstance = Instantiate(npc, CheckpointsNPCPart1_3[positionSubPart].position, Quaternion.identity);
                    NPCComponent = npcInstance.GetComponentInChildren<NPCPerson>();

                    NPCComponent.checkpointPart = 1;
                    NPCComponent.checkpointSubPart = 3;
                    NPCComponent._position = positionSubPart;
                    spawnedNPCPart1++;
                    break;

            }

        }else if (activeChunk == 2)
        {
            //Debug.Log("Pendiente");
            subpart = UnityEngine.Random.Range(1, 4);
            switch (subpart)
            {
                case 1:
                    positionSubPart = UnityEngine.Random.Range(1, CheckpointsNPCPart2_1.Count - 1);
                    randomNPC = UnityEngine.Random.Range(0, NPCPersons.Count);
                    npc = NPCPersons[randomNPC]; // Asignamos el prefab en lugar del componente
                    NPCComponent = npc.GetComponentInChildren<NPCPerson>();

                    // Ahora, instanciamos el prefab y obtenemos una referencia al componente NPCPerson en el objeto instanciado
                    npcInstance = Instantiate(npc, CheckpointsNPCPart2_1[positionSubPart].position, Quaternion.identity);
                    NPCComponent = npcInstance.GetComponentInChildren<NPCPerson>();

                    NPCComponent.checkpointPart = 2;
                    NPCComponent.checkpointSubPart = 1;
                    NPCComponent._position = positionSubPart;
                    spawnedNPCPart2++;
                    break;

                case 2:
                    positionSubPart = UnityEngine.Random.Range(1, CheckpointsNPCPart2_2.Count - 1);
                    randomNPC = UnityEngine.Random.Range(0, NPCPersons.Count);
                    npc = NPCPersons[randomNPC]; // Asignamos el prefab en lugar del componente
                    NPCComponent = npc.GetComponentInChildren<NPCPerson>();

                    // Ahora, instanciamos el prefab y obtenemos una referencia al componente NPCPerson en el objeto instanciado
                    npcInstance = Instantiate(npc, CheckpointsNPCPart2_2[positionSubPart].position, Quaternion.identity);
                    NPCComponent = npcInstance.GetComponentInChildren<NPCPerson>();

                    NPCComponent.checkpointPart = 2;
                    NPCComponent.checkpointSubPart = 2;
                    NPCComponent._position = positionSubPart;
                    spawnedNPCPart2++;
                    break;

                case 3:
                    positionSubPart = UnityEngine.Random.Range(1, CheckpointsNPCPart2_3.Count - 1);
                    randomNPC = UnityEngine.Random.Range(0, NPCPersons.Count);
                    npc = NPCPersons[randomNPC]; // Asignamos el prefab en lugar del componente
                    NPCComponent = npc.GetComponentInChildren<NPCPerson>();

                    // Ahora, instanciamos el prefab y obtenemos una referencia al componente NPCPerson en el objeto instanciado
                    npcInstance = Instantiate(npc, CheckpointsNPCPart2_3[positionSubPart].position, Quaternion.identity);
                    NPCComponent = npcInstance.GetComponentInChildren<NPCPerson>();

                    NPCComponent.checkpointPart = 2;
                    NPCComponent.checkpointSubPart = 3;
                    NPCComponent._position = positionSubPart;
                    spawnedNPCPart2++;
                    break;
            }


        }else if (activeChunk == 3)
        {
            subpart = UnityEngine.Random.Range(1, 5);
            switch (subpart)
            {
                case 1:
                    positionSubPart = UnityEngine.Random.Range(1, CheckpointsNPCPart3_1.Count - 1);
                    randomNPC = UnityEngine.Random.Range(0, NPCPersons.Count);
                    npc = NPCPersons[randomNPC]; // Asignamos el prefab en lugar del componente
                    NPCComponent = npc.GetComponentInChildren<NPCPerson>();

                    // Ahora, instanciamos el prefab y obtenemos una referencia al componente NPCPerson en el objeto instanciado
                    npcInstance = Instantiate(npc, CheckpointsNPCPart3_1[positionSubPart].position, Quaternion.identity);
                    NPCComponent = npcInstance.GetComponentInChildren<NPCPerson>();

                    NPCComponent.checkpointPart = 3;
                    NPCComponent.checkpointSubPart = 1;
                    NPCComponent._position = positionSubPart;
                    spawnedNPCPart3++;
                    break;

                case 2:
                    positionSubPart = UnityEngine.Random.Range(1, CheckpointsNPCPart3_2.Count - 1);
                    randomNPC = UnityEngine.Random.Range(0, NPCPersons.Count);
                    npc = NPCPersons[randomNPC]; // Asignamos el prefab en lugar del componente
                    NPCComponent = npc.GetComponentInChildren<NPCPerson>();

                    // Ahora, instanciamos el prefab y obtenemos una referencia al componente NPCPerson en el objeto instanciado
                    npcInstance = Instantiate(npc, CheckpointsNPCPart3_2[positionSubPart].position, Quaternion.identity);
                    NPCComponent = npcInstance.GetComponentInChildren<NPCPerson>();

                    NPCComponent.checkpointPart = 3;
                    NPCComponent.checkpointSubPart = 2;
                    NPCComponent._position = positionSubPart;
                    spawnedNPCPart3++;
                    break;

                case 3:
                    positionSubPart = UnityEngine.Random.Range(1, CheckpointsNPCPart3_3.Count - 1);
                    randomNPC = UnityEngine.Random.Range(0, NPCPersons.Count);
                    npc = NPCPersons[randomNPC]; // Asignamos el prefab en lugar del componente
                    NPCComponent = npc.GetComponentInChildren<NPCPerson>();

                    // Ahora, instanciamos el prefab y obtenemos una referencia al componente NPCPerson en el objeto instanciado
                    npcInstance = Instantiate(npc, CheckpointsNPCPart3_3[positionSubPart].position, Quaternion.identity);
                    NPCComponent = npcInstance.GetComponentInChildren<NPCPerson>();

                    NPCComponent.checkpointPart = 3;
                    NPCComponent.checkpointSubPart = 3;
                    NPCComponent._position = positionSubPart;
                    spawnedNPCPart3++;
                    break;

                case 4:
                    positionSubPart = UnityEngine.Random.Range(1, CheckpointsNPCPart3_4.Count - 1);
                    randomNPC = UnityEngine.Random.Range(0, NPCPersons.Count);
                    npc = NPCPersons[randomNPC]; // Asignamos el prefab en lugar del componente
                    NPCComponent = npc.GetComponentInChildren<NPCPerson>();

                    // Ahora, instanciamos el prefab y obtenemos una referencia al componente NPCPerson en el objeto instanciado
                    npcInstance = Instantiate(npc, CheckpointsNPCPart3_4[positionSubPart].position, Quaternion.identity);
                    NPCComponent = npcInstance.GetComponentInChildren<NPCPerson>();

                    NPCComponent.checkpointPart = 3;
                    NPCComponent.checkpointSubPart = 4;
                    NPCComponent._position = positionSubPart;
                    spawnedNPCPart3++;
                    break;
            }
        }else if(activeChunk == 4)
        {
            subpart = UnityEngine.Random.Range(1, 4);
            switch (subpart)
            {
                case 1:
                    positionSubPart = UnityEngine.Random.Range(1, CheckpointsNPCPart4_1.Count - 1);
                    randomNPC = UnityEngine.Random.Range(0, NPCPersons.Count);
                    npc = NPCPersons[randomNPC]; // Asignamos el prefab en lugar del componente
                    NPCComponent = npc.GetComponentInChildren<NPCPerson>();

                    // Ahora, instanciamos el prefab y obtenemos una referencia al componente NPCPerson en el objeto instanciado
                    npcInstance = Instantiate(npc, CheckpointsNPCPart4_1[positionSubPart].position, Quaternion.identity);
                    NPCComponent = npcInstance.GetComponentInChildren<NPCPerson>();

                    NPCComponent.checkpointPart = 4;
                    NPCComponent.checkpointSubPart = 1;
                    NPCComponent._position = positionSubPart;
                    spawnedNPCPart4++;
                    break;
                case 2:
                    positionSubPart = UnityEngine.Random.Range(1, CheckpointsNPCPart4_2.Count - 1);
                    randomNPC = UnityEngine.Random.Range(0, NPCPersons.Count);
                    npc = NPCPersons[randomNPC]; // Asignamos el prefab en lugar del componente
                    NPCComponent = npc.GetComponentInChildren<NPCPerson>();

                    //Now, instatie the prefab and get the reference to the NPCPerson Component on the instancied object
                    npcInstance = Instantiate(npc, CheckpointsNPCPart4_2[positionSubPart].position, Quaternion.identity);
                    NPCComponent = npcInstance.GetComponentInChildren<NPCPerson>();

                    NPCComponent.checkpointPart = 4;
                    NPCComponent.checkpointSubPart = 2;
                    NPCComponent._position = positionSubPart;
                    spawnedNPCPart4++;
                    break;
                case 3:
                    positionSubPart = UnityEngine.Random.Range(1, CheckpointsNPCPart4_3.Count - 1);
                    randomNPC = UnityEngine.Random.Range(0, NPCPersons.Count);
                    npc = NPCPersons[randomNPC];
                    NPCComponent = npc.GetComponentInChildren<NPCPerson>();
                    //Now, instatie the prefab and get the reference to the NPCPerson Component on the instancied object
                    npcInstance = Instantiate(npc, CheckpointsNPCPart4_3[positionSubPart].position, Quaternion.identity);
                    NPCComponent = npcInstance.GetComponentInChildren<NPCPerson>();

                    NPCComponent.checkpointPart = 4;
                    NPCComponent.checkpointSubPart = 3;
                    NPCComponent._position = positionSubPart;
                    spawnedNPCPart4++;
                    break;
            }
        }else if(activeChunk == 5)
        {
            subpart = UnityEngine.Random.Range(1, 3);
            switch (subpart)
            {
                case 1:
                    positionSubPart = UnityEngine.Random.Range(1, CheckpointsNPCPart5_1.Count - 1);
                    randomNPC = UnityEngine.Random.Range(0, NPCPersons.Count);
                    npc = NPCPersons[randomNPC]; // Asignamos el prefab en lugar del componente
                    NPCComponent = npc.GetComponentInChildren<NPCPerson>();

                    //Now, instatie the prefab and get the reference to the NPCPerson Component on the instancied object
                    npcInstance = Instantiate(npc, CheckpointsNPCPart5_1[positionSubPart].position, Quaternion.identity);
                    NPCComponent = npcInstance.GetComponentInChildren<NPCPerson>();

                    NPCComponent.checkpointPart = 5;
                    NPCComponent.checkpointSubPart = 1;
                    NPCComponent._position = positionSubPart;
                    spawnedNPCPart5++;
                    break;
                case 2:
                    positionSubPart = UnityEngine.Random.Range(1, CheckpointsNPCPart5_2.Count - 1);
                    randomNPC = UnityEngine.Random.Range(0, NPCPersons.Count);
                    npc = NPCPersons[randomNPC]; // Asignamos el prefab en lugar del componente
                    NPCComponent = npc.GetComponentInChildren<NPCPerson>();

                    
                    npcInstance = Instantiate(npc, CheckpointsNPCPart5_2[positionSubPart].position, Quaternion.identity);
                    NPCComponent = npcInstance.GetComponentInChildren<NPCPerson>();

                    NPCComponent.checkpointPart = 5;
                    NPCComponent.checkpointSubPart = 2;
                    NPCComponent._position = positionSubPart;
                    spawnedNPCPart5++;
                    break;
            }
        }else if(activeChunk == 6)
        {
            subpart = UnityEngine.Random.Range(1, 5);
            switch (subpart)
            {
                case 1:
                    positionSubPart = UnityEngine.Random.Range(1, CheckpointsNPCPart6_1.Count - 1);
                    randomNPC = UnityEngine.Random.Range(0, NPCPersons.Count);
                    npc = NPCPersons[randomNPC]; // Asignamos el prefab en lugar del componente
                    NPCComponent = npc.GetComponentInChildren<NPCPerson>();

                    // Ahora, instanciamos el prefab y obtenemos una referencia al componente NPCPerson en el objeto instanciado
                    npcInstance = Instantiate(npc, CheckpointsNPCPart6_1[positionSubPart].position, Quaternion.identity);
                    NPCComponent = npcInstance.GetComponentInChildren<NPCPerson>();

                    NPCComponent.checkpointPart = 6;
                    NPCComponent.checkpointSubPart = 1;
                    NPCComponent._position = positionSubPart;
                    spawnedNPCPart6++;
                    break;
                case 2:
                    positionSubPart = UnityEngine.Random.Range(1, CheckpointsNPCPart6_2.Count - 1);
                    randomNPC = UnityEngine.Random.Range(0, NPCPersons.Count);
                    npc = NPCPersons[randomNPC]; // Asignamos el prefab en lugar del componente
                    NPCComponent = npc.GetComponentInChildren<NPCPerson>();

                    // Ahora, instanciamos el prefab y obtenemos una referencia al componente NPCPerson en el objeto instanciado
                    npcInstance = Instantiate(npc, CheckpointsNPCPart6_2[positionSubPart].position, Quaternion.identity);
                    NPCComponent = npcInstance.GetComponentInChildren<NPCPerson>();

                    NPCComponent.checkpointPart = 6;
                    NPCComponent.checkpointSubPart = 2;
                    NPCComponent._position = positionSubPart;
                    spawnedNPCPart6++;
                    break;
                case 3:
                    positionSubPart = UnityEngine.Random.Range(1, CheckpointsNPCPart6_3.Count - 1);
                    randomNPC = UnityEngine.Random.Range(0, NPCPersons.Count);
                    npc = NPCPersons[randomNPC]; // Asignamos el prefab en lugar del componente
                    NPCComponent = npc.GetComponentInChildren<NPCPerson>();

                    // Ahora, instanciamos el prefab y obtenemos una referencia al componente NPCPerson en el objeto instanciado
                    npcInstance = Instantiate(npc, CheckpointsNPCPart6_3[positionSubPart].position, Quaternion.identity);
                    NPCComponent = npcInstance.GetComponentInChildren<NPCPerson>();

                    NPCComponent.checkpointPart = 6;
                    NPCComponent.checkpointSubPart = 3;
                    NPCComponent._position = positionSubPart;
                    spawnedNPCPart6++;
                    break;
                case 4:
                    positionSubPart = UnityEngine.Random.Range(1, CheckpointsNPCPart6_4.Count - 1);
                    randomNPC = UnityEngine.Random.Range(0, NPCPersons.Count);
                    npc = NPCPersons[randomNPC]; // Asignamos el prefab en lugar del componente
                    NPCComponent = npc.GetComponentInChildren<NPCPerson>();

                    // Ahora, instanciamos el prefab y obtenemos una referencia al componente NPCPerson en el objeto instanciado
                    npcInstance = Instantiate(npc, CheckpointsNPCPart6_4[positionSubPart].position, Quaternion.identity);
                    NPCComponent = npcInstance.GetComponentInChildren<NPCPerson>();

                    NPCComponent.checkpointPart = 6;
                    NPCComponent.checkpointSubPart = 4;
                    NPCComponent._position = positionSubPart;
                    spawnedNPCPart6++;
                    break;
            }
        }else if(activeChunk == 7)
        {
            subpart = UnityEngine.Random.Range(1, 3);
            switch (subpart)
            {
                case 1:
                    positionSubPart = UnityEngine.Random.Range(1, CheckpointsNPCPart7_1.Count - 1);
                    randomNPC = UnityEngine.Random.Range(0, NPCCountryPersons.Count);
                    npc = NPCCountryPersons[randomNPC]; // Asignamos el prefab en lugar del componente
                    NPCComponent = npc.GetComponentInChildren<NPCPerson>();

                    // Ahora, instanciamos el prefab y obtenemos una referencia al componente NPCPerson en el objeto instanciado
                    npcInstance = Instantiate(npc, CheckpointsNPCPart7_1[positionSubPart].position, Quaternion.identity);
                    NPCComponent = npcInstance.GetComponentInChildren<NPCPerson>();

                    NPCComponent.checkpointPart = 7;
                    NPCComponent.checkpointSubPart = 1;
                    NPCComponent._position = positionSubPart;
                    spawnedNPCPart7++;
                    break;
                case 2:
                    positionSubPart = UnityEngine.Random.Range(1, CheckpointsNPCPart7_2.Count - 1);
                    randomNPC = UnityEngine.Random.Range(0, NPCCountryPersons.Count);
                    npc = NPCCountryPersons[randomNPC]; // Asignamos el prefab en lugar del componente
                    NPCComponent = npc.GetComponentInChildren<NPCPerson>();

                    // Ahora, instanciamos el prefab y obtenemos una referencia al componente NPCPerson en el objeto instanciado
                    npcInstance = Instantiate(npc, CheckpointsNPCPart7_2[positionSubPart].position, Quaternion.identity);
                    NPCComponent = npcInstance.GetComponentInChildren<NPCPerson>();

                    NPCComponent.checkpointPart = 7;
                    NPCComponent.checkpointSubPart = 2;
                    NPCComponent._position = positionSubPart;
                    spawnedNPCPart7++;
                    break;
            }
        }else if(activeChunk == 8)
        {
            subpart = UnityEngine.Random.Range(1, 4);

            switch (subpart)
            {
                case 1:
                    positionSubPart = UnityEngine.Random.Range(1, CheckpointsNPCPart8_1.Count - 1);
                    randomNPC = UnityEngine.Random.Range(0, NPCCountryPersons.Count);
                    npc = NPCCountryPersons[randomNPC]; // Asignamos el prefab en lugar del componente
                    NPCComponent = npc.GetComponentInChildren<NPCPerson>();

                    // Ahora, instanciamos el prefab y obtenemos una referencia al componente NPCPerson en el objeto instanciado
                    npcInstance = Instantiate(npc, CheckpointsNPCPart8_1[positionSubPart].position, Quaternion.identity);
                    NPCComponent = npcInstance.GetComponentInChildren<NPCPerson>();

                    NPCComponent.checkpointPart = 8;
                    NPCComponent.checkpointSubPart = 1;
                    NPCComponent._position = positionSubPart;
                    spawnedNPCPart8++;
                    break;

                case 2:
                    positionSubPart = UnityEngine.Random.Range(1, CheckpointsNPCPart8_2.Count - 1);
                    randomNPC = UnityEngine.Random.Range(0, NPCCountryPersons.Count);
                    npc = NPCCountryPersons[randomNPC]; // Asignamos el prefab en lugar del componente
                    NPCComponent = npc.GetComponentInChildren<NPCPerson>();

                    // Ahora, instanciamos el prefab y obtenemos una referencia al componente NPCPerson en el objeto instanciado
                    npcInstance = Instantiate(npc, CheckpointsNPCPart8_2[positionSubPart].position, Quaternion.identity);
                    NPCComponent = npcInstance.GetComponentInChildren<NPCPerson>();

                    NPCComponent.checkpointPart = 8;
                    NPCComponent.checkpointSubPart = 2;
                    NPCComponent._position = positionSubPart;
                    spawnedNPCPart8++;
                    break;
                case 3:
                    positionSubPart = UnityEngine.Random.Range(1, CheckpointsNPCPart8_3.Count - 1);
                    randomNPC = UnityEngine.Random.Range(0, NPCCountryPersons.Count);
                    npc = NPCCountryPersons[randomNPC]; // Asignamos el prefab en lugar del componente
                    NPCComponent = npc.GetComponentInChildren<NPCPerson>();

                    // Ahora, instanciamos el prefab y obtenemos una referencia al componente NPCPerson en el objeto instanciado
                    npcInstance = Instantiate(npc, CheckpointsNPCPart8_3[positionSubPart].position, Quaternion.identity);
                    NPCComponent = npcInstance.GetComponentInChildren<NPCPerson>();

                    NPCComponent.checkpointPart = 8;
                    NPCComponent.checkpointSubPart = 3;
                    NPCComponent._position = positionSubPart;
                    spawnedNPCPart8++;
                    break;

            }
        }


    }

}
