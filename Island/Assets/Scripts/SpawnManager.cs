using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{   
    // instantiate balloonPrefab as gameObject
    [SerializeField]
    private GameObject _balloonPrefab;

    // variables for time delay and lifetime
    private float _delay = 3f;
    private bool _alive = true;

    // Start Coroutine
    void Start()
    {
        StartCoroutine(SpawnSystem());    
    }

    // when player dies set _alive to false (and consequently stop spawning)
    public void onPlayerDeath()
    {
        _alive = false;
    }

    /*
     * SpanSystem()
     * while alive is true, instantiate balloons that are spawned randomly below the player.
     */
    IEnumerator SpawnSystem()
    {
        while (_alive)
        {
            Instantiate(_balloonPrefab, new Vector3(Random.Range(-20f, 20f), -10f, Random.Range(-20f, 20f)), Quaternion.identity, this.transform);
            yield return new WaitForSeconds(_delay);
        }
        yield return null;  
    }
}
