using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PathCreation;
using System;

public class BossController : MonoBehaviour
{
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

    [SerializeField]
    GameObject beeGameObjectPrefab;
    [SerializeField]
    Transform spawnLocation;

    // Start is called before the first frame update
    void Start()
    {
        isMoving = true;
        bossData.OnHealthChange += BossData_OnHealthChange;
        InvokeRepeating("StingerAttack", 2, stingerAttackRate);
    }

    private void BossData_OnHealthChange(object sender, BossData.OnHealthChangeEventArgs e)
    {
        if(e.health <= 240 && doOnce)
        {
            doOnce = false;
            isAttackable = false;
            SmashAttack(pathIndex);
        }
        if (e.health <= 190 && doOnce1)
        {
            doOnce1 = false;
            isAttackable = false;
            SmashAttack(pathIndex);
        }
        if (e.health <= 140 && doOnce2)
        {
            doOnce2 = false;
            isAttackable = false;
            SmashAttack(pathIndex);
        }
        if (e.health <= 100 && doOnce3)
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

        //Finds all the players and a random location
        GameObject[] players = GameObject.FindGameObjectsWithTag("PlayerAttack");
        int randomIndex = UnityEngine.Random.Range(0, players.Length -1);
        Vector3 myPos = transform.position;
        Vector3 playerPos = players[randomIndex].transform.position;
        Vector3 direction = Vector3.Normalize(playerPos - myPos);

        //shoot a projectile at that enemy
        GameObject newStinger = Instantiate(stingerPrefab, myPos, Quaternion.identity);
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
