using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConsumableSpawner : MonoBehaviour
{
    [SerializeField] private LayerMask GroundLayer;

    [SerializeField] private Vector3 TriggerSize = new Vector3(15, 15, 15);
    private Vector3 DebugBoxSize = Vector3.zero;

    [SerializeField] private float _swordStartingStrength = 10f;
    private float _currentSwordStrength;

    [SerializeField] private GameObject Sword;
    //[SerializeField] private GameObject Bow;
    [SerializeField] private GameObject HealthPotion;
    [SerializeField] private GameObject StrengthPotion;

    private Coroutine swordSpawnTimer;
    //private Coroutine bowSpawnTimer;
    private Coroutine healthSpawnTimer;
    private Coroutine strengthSpawnTimer;

    private int SpawnAttempts = 0;

    public bool CanSpawn = false;

    private void Start()
    {
        _currentSwordStrength = _swordStartingStrength;
    }

    private void Update()
    {
        if(CanSpawn)
        {
            if (swordSpawnTimer == null)
                swordSpawnTimer = StartCoroutine(SpawnItemTimer(Sword, 60, 120));
            //if (bowSpawnTimer == null)
            //    bowSpawnTimer = StartCoroutine(SpawnItemTimer(Bow, 60, 120));
            if (healthSpawnTimer == null)
                healthSpawnTimer = StartCoroutine(SpawnItemTimer(HealthPotion, 5, 60));
            if (strengthSpawnTimer == null)
                strengthSpawnTimer = StartCoroutine(SpawnItemTimer(StrengthPotion, 5, 60));
        }
        else
        {
            if(swordSpawnTimer != null)
            {
                StopCoroutine(swordSpawnTimer);
                swordSpawnTimer = null;
            }
            //if (bowSpawnTimer != null)
            //{
            //    StopCoroutine(bowSpawnTimer);
            //    bowSpawnTimer = null;
            //}
            if (healthSpawnTimer != null)
            {
                StopCoroutine(healthSpawnTimer);
                healthSpawnTimer = null;
            }
            if(strengthSpawnTimer != null)
            {
                StopCoroutine(strengthSpawnTimer);
                strengthSpawnTimer = null;
            }
        }
    }

    private Vector3 CreateSpawnLocation()
    {
        for (; ;)
        {
            if(SpawnAttempts > 5)
            {
                Debug.LogError("Unable to spawn item. Quitting loop.");
                SpawnAttempts = 0;
                return Vector3.zero;
            }

            Vector3 _spawnLocation = new Vector3(transform.position.x + Random.Range(-TriggerSize.x / 2, TriggerSize.x / 2), 
                transform.position.y - (TriggerSize.y / 3), transform.position.z + Random.Range(-TriggerSize.z / 2, TriggerSize.z / 2));

            //Raycast down from this position
            RaycastHit hit;
            if (Physics.Raycast(_spawnLocation, -transform.up, out hit, TriggerSize.y, GroundLayer, QueryTriggerInteraction.Ignore))
            {
                _spawnLocation.y = hit.transform.position.y + 0.5f;
                SpawnAttempts = 0;
                return _spawnLocation;
            }
            else
            {
                Debug.Log("Trying to spawn again: " + ++SpawnAttempts);
                Debug.LogWarning("Spawn failure at: " + _spawnLocation);
            }

        }

    }

    private void SpawnItem(Vector3 spawnLocation, GameObject obj)
    {
        GameObject item = Instantiate(obj);
        item.SetActive(false);
        item.transform.position = spawnLocation;
        if (item.transform.position == spawnLocation)
            item.SetActive(true);
        else
            StartCoroutine(RetrySetActive(item));

        if (obj == Sword)
            item.GetComponent<WeaponController>().Damage = SetSwordStrength();  
    }

    private float SetSwordStrength()
    {
        if (_currentSwordStrength < 10f)
        {
            return 10f;
        }
        else
        {
            _currentSwordStrength += Random.Range(10f, 20f);
            return _currentSwordStrength;
        }
    }

    private IEnumerator SpawnItemTimer(GameObject _obj, float _lowestSpawnTime, float _longestSpawnTime)
    {
        for(; ; )
        {
            Vector3 spawnPos = CreateSpawnLocation();

            float waitTime = Random.Range(_lowestSpawnTime, _longestSpawnTime);
            yield return new WaitForSeconds(waitTime);

            if (spawnPos != Vector3.zero)
                SpawnItem(spawnPos, _obj);
            else
                Debug.LogError("Unable to spawn item: Location is: " + spawnPos);
        }
    }

    private IEnumerator RetrySetActive(GameObject obj)
    {
        yield return new WaitForSeconds(1f);
        obj.SetActive(true);
    }

    private void OnDrawGizmosSelected()
    {
        DebugBoxSize = new Vector3(TriggerSize.x, TriggerSize.y * 0.25f, TriggerSize.z);

        float CubeY = transform.position.y - (TriggerSize.y / 2);
        CubeY += DebugBoxSize.y / 2;

        Gizmos.DrawCube(new Vector3(transform.position.x, CubeY, transform.position.z), DebugBoxSize);
        Gizmos.DrawWireCube(transform.position, TriggerSize);
    }
}
