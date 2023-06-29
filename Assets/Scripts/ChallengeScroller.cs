using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChallengeScroller : MonoBehaviour
{

    public float scrollSpeed = 5.0f;
    public GameObject[] challenges;
    public float frequency = 0.5f;
    float counter = 0.0f;
    public Transform challengeSpawnPoint;
    // Start is called before the first frame update
    void Start(){
        GenerateRandomChallenge();
    }

    // Update is called once per frame
    void Update(){
        if(counter <= 0.0f){
            GenerateRandomChallenge();
        } else {
            counter -= Time.deltaTime * frequency; 
        }

        GameObject currentChild;
        for (int i = 0; i < transform.childCount; i++){
            currentChild = transform.GetChild(i).gameObject;
            ScrollChallenge(currentChild);
            if(currentChild.transform.position.x <= -15.0f ){
                Destroy(currentChild);
            }
        }
    }

    void ScrollChallenge(GameObject currentChallenge) {
        currentChallenge.transform.position -= Vector3.right * scrollSpeed * Time.deltaTime;
    }

    void GenerateRandomChallenge() {
        GameObject newChallenge = Instantiate( challenges[Random.Range(0,challenges.Length)], 
            challengeSpawnPoint.position,
            Quaternion.identity
        );
        newChallenge.transform.parent = transform;
        counter = 1.0f;
    }
}
