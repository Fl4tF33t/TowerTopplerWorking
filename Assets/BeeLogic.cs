using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PathCreation;

public class BeeLogic : MonoBehaviour
{
    
    PathCreator pathCreator;
    [SerializeField]
    private float beeMovementSpeed = 5; 
    private float distanceTravelled;

    // Start is called before the first frame update
    void Start()
    {
        pathCreator = GameObject.Find("BeePath").GetComponent<PathCreator>();
    }

    // Update is called once per frame
    void Update()
    {
        distanceTravelled += beeMovementSpeed * Time.deltaTime;
        transform.position = pathCreator.path.GetPointAtDistance(distanceTravelled);
    }
}
