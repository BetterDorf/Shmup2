using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
        //charge the next level if no enemies are left
        if (_enemyOnScreen.Count == 0)
        {
            //Check if we finished all level
            if (_actualLevel >= _levels.Count)
            {
                AllLevelsCleared();
                return;
            }

            //Reset the spawn points
            _spawner.ResetSpawn();

            //spawn each enemy in the level
            foreach (LevelSpawnable spawnable in _levels[_actualLevel]._enemyList)
            {
                _spawner.Spawn(ref _enemyOnScreen,spawnable._number,spawnable._enemy);
            }

            _actualLevel++;
        }

        //Cleanup the list of enemies on the screen (remove all null elements)
        _enemyOnScreen.RemoveAll(x => !x);
    }

    void AllLevelsCleared()
    {
        //Temporary before we have the boss
        if (GameManager.instance)
            GameManager.instance.Win();

        //No need to spawn more levels
        this.enabled = false;
    }
}
