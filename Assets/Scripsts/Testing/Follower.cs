using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PathCreation;

public class Follower : MonoBehaviour
{
    public PathCreator[] pathCreator;

    [SerializeField]
    private float speed = 5;
    private float distanceTravelled;
    public bool isMoving;
    int index;

    // Start is called before the first frame update
    void Start()
    {
        isMoving = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.F))
        {
            index++;
            index %= pathCreator.Length;
            Debug.Log(index);
        }
        
        Movement(index);
    }

    private void Movement(int index)
    {
        if (isMoving)
        {
            distanceTravelled += speed * Time.deltaTime;
            transform.position = pathCreator[index].path.GetPointAtDistance(distanceTravelled);
            transform.rotation = pathCreator[index].path.GetRotationAtDistance(distanceTravelled);
        }
    }

}
