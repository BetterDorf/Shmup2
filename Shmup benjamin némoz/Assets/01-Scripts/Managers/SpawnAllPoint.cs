using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnAllPoint : MonoBehaviour
{
        [SerializeField] private GameObject mobsToSpawn;
        [SerializeField] private List<Transform> spawnPoints;

        private List<GameObject> Mobs;

        void Awake()
        {
            Mobs = new List<GameObject>();
        }


        // Start is called before the first frame update
        void Start()
        {
            for (int i = 0; i < spawnPoints.Count; i++)
            {
                Transform spawnPoint = spawnPoints[i];
               

                Mobs.Add(Instantiate(mobsToSpawn, spawnPoint));
            }
        }

        // Update is called once per frame
        void Update()
        {

        }
}

