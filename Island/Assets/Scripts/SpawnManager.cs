using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    [SerializeField]
    private GameObject _balloonPrefab;

    private float _delay = 3f;
    private bool _alive = true;

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(SpawnSystem());    
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void onPlayerDeath()
    {
        _alive = false;
    }

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
