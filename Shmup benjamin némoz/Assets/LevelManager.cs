using System.Collections;
using System.Collections.Generic;
using UnityEditor.ProjectWindowCallback;
using UnityEngine;
using UnityEngine.InputSystem.Interactions;

public class LevelManager : MonoBehaviour
{


    private List<GameObject> _enemyOnScreen = new List<GameObject>();

    [SerializeField] private List<Level> _levels;

    private Spawner _spawner;

    private int _actualLevel;

    // Start is called before the first frame update
    void Start()
    {
        _spawner = GetComponent<Spawner>();
    }

    // Update is called once per frame
    void Update()
    {
        //charge the next level
        if (_enemyOnScreen.Count == 0)
        {
            _spawner.ResetSpawn();

            foreach (LevelSpawnable spawnable in _levels[_actualLevel]._enemyList)
            {
                _spawner.Spawn(ref _enemyOnScreen,spawnable._number,spawnable._enemy);
            }
            
        }
    }
}
