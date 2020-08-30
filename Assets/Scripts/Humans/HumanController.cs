using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

namespace HumanStateManagement
{

    public class HumanController : MonoBehaviour
    {

        //Range for how often a new human comes in to the store
        public float minNextHuman = 10.0f;
        public float maxNextHuman = 25.0f;

        public float nextHumanToSpawn;
        public Transform humanSpawnLocation;

        public static HumanController Instance;

        public GameObject[] prefabs;

        void Awake()
        {
            if (Instance == null)
            {
                Instance = this;
            }
        }

        // Start is called before the first frame update
        void Start()
        {
            nextHumanToSpawn = Random.Range(minNextHuman, maxNextHuman);
        }

        // Update is called once per frame
        void Update()
        {
            if (Time.time > nextHumanToSpawn)
            {
                SpawnHuman(); //TODO: Set Up an object pool for humans
                nextHumanToSpawn = Time.time + Random.Range(minNextHuman, maxNextHuman);
            }
        }

        public void SpawnHuman()
        {
            GameObject humanPrefab = prefabs[Random.Range(0, prefabs.Length)];
            GameObject newHuman = Instantiate(humanPrefab, humanSpawnLocation.position, Quaternion.identity);
        }

        public void OnHumanEaten(Human human)
        {
            //Not exactly sure what we need to do here...
        }
    }

}
