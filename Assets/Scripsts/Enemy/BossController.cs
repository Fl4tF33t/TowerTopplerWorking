using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PathCreation;
using System;

public class BossController : MonoBehaviour
{
    AudioSource audioSource;
    public AudioClip stingerAudio;
    public AudioClip stageBreaking;
    

    public event EventHandler OnSmashAttack;
    public PathCreator[] pathCreator;

    //the bases that will be used to disappear
    [SerializeField]
    GameObject[] bases;
    int baseIndex = 0;

    //Movement Variables
    [SerializeField]
    int pathIndex;
    [SerializeField]
    private float bossMovementSpeed = 5;
    private float distanceTravelled;
    private bool isMoving;

    //StingerAttackVariables
    [SerializeField]
    private float stingerAttackRate = 30f;
    [SerializeField]
    private GameObject stingerPrefab;
    [SerializeField]
    private float stingerSpeed = 5f;

    //DropAttackVariables
    [SerializeField]
    private Transform dropSpot;
    [SerializeField]
    BossData bossData;
    public bool isSmashing = false;
    public bool isStaging = false;
    bool doOnce = true;
    bool doOnce1 = true;
    bool doOnce2 = true;
    bool doOnce3 = true;

    public static bool isAttackable = true;

    //Stage areas
    [SerializeField]
    private Transform stage;
    public Transform stingerSpawnLocation;

    [SerializeField]
    GameObject beeGameObjectPrefab;
    [SerializeField]
    Transform spawnLocation;
    float timer;

    // Start is called before the first frame update
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        isMoving = true;
        bossData.OnHealthChange += BossData_OnHealthChange;
        InvokeRepeating("StingerAttack", 2, stingerAttackRate);
        timer = 300;
    }

    private void BossData_OnHealthChange(object sender, BossData.OnHealthChangeEventArgs e)
    {
        if(e.health <= 260 && doOnce)
        {
            doOnce = false;
            isAttackable = false;
            SmashAttack(pathIndex);
        }
        if (e.health <= 140 && doOnce1)
        {
            doOnce1 = false;
            isAttackable = false;
            SmashAttack(pathIndex);
        }
        if (e.health <= 50 && doOnce2)
        {
            doOnce2 = false;
            isAttackable = false;
            SmashAttack(pathIndex);
        }
        if (e.health <= 10 && doOnce3 && timer<0)
        {
            doOnce3 = false;
            isAttackable = false;
            SmashAttack(pathIndex);
        }

        
    }

    // Update is called once per frame
    void Update()
    {
        Movement(pathIndex);
        timer -= Time.deltaTime;
    }

    private void Movement(int index)
    {
        if (isMoving)
        {
            distanceTravelled += bossMovementSpeed * Time.deltaTime;
            transform.position = pathCreator[index].path.GetPointAtDistance(distanceTravelled);
            //transform.rotation = pathCreator[index].path.GetRotationAtDistance(distanceTravelled);
        }

        if (isSmashing)
        {
            transform.position = Vector3.Lerp(transform.position, dropSpot.transform.position, Time.deltaTime);
            
        }

        if (isStaging)
        {
            transform.position = Vector3.Lerp(transform.position, stage.transform.position, 5 * Time.deltaTime);

        }
    }
    private void StingerAttack() //uses a tag system, the players need to have a player tag
    {
        //stops the movement
        StartCoroutine(AttackStart(2f));

        audioSource.PlayOneShot(stingerAudio);
        //Finds all the players and a random location
        GameObject[] players = GameObject.FindGameObjectsWithTag("PlayerAttack");
        int randomIndex = UnityEngine.Random.Range(0, players.Length -1);
        Vector3 myPos = transform.position;
        Vector3 playerPos = players[randomIndex].transform.position;
        Vector3 direction = Vector3.Normalize(playerPos - stingerSpawnLocation.transform.position);
        float directionRot = 90f;
        if(playerPos.x < 0)
        {
            directionRot = -90;
        }else
        {
            directionRot = 90;        
        }


        //shoot a projectile at that enemy
        GameObject newStinger = Instantiate(stingerPrefab, stingerSpawnLocation.transform.position, Quaternion.Euler(0, 0, directionRot));
        Rigidbody stingerRB = newStinger.GetComponent<Rigidbody>();
        stingerRB.AddForce(direction * stingerSpeed);

    }

    private void SmashAttack(int index)
    {
        isMoving = false;
        isSmashing = true;
        CancelInvoke("StingerAttack");
        StartCoroutine(StartSmashAttack());
    }

    IEnumerator StartSmashAttack()
    {
        OnSmashAttack?.Invoke(this, EventArgs.Empty);

        yield return new WaitForSeconds(2f);
        isSmashing = false;
        isStaging = true;
        isAttackable = true;
        Destroy(bases[baseIndex].gameObject);
        audioSource.PlayOneShot(stageBreaking);
        baseIndex++;
        yield return new WaitForSeconds(2f);
        Instantiate(beeGameObjectPrefab, spawnLocation.transform.position, Quaternion.identity);
        isStaging = false;
        pathIndex++;
        pathIndex %= pathCreator.Length;
        isMoving = true;
        
    }

    private IEnumerator AttackStart(float timeDelay)
    {
        isMoving = !isMoving;
        yield return new WaitForSeconds(timeDelay);
        isMoving = !isMoving;
    }
  
}
