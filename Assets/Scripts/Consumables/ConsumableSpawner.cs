using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConsumableSpawner : MonoBehaviour
{
    [SerializeField] private Vector3 TriggerSize = new Vector3(15, 15, 15);
    private Vector3 DebugBoxSize = Vector3.zero;

    [SerializeField] private GameObject Sword;
    [SerializeField] private GameObject Bow;
    [SerializeField] private GameObject HealthPotion;
    [SerializeField] private GameObject StrengthPotion;

    private void SpawnItem(Vector3 spawnLocation, GameObject obj)
    {
        GameObject item = Instantiate(obj);
        item.SetActive(false);
        item.transform.position = spawnLocation;
        if (item.transform.position == spawnLocation)
            item.SetActive(true);
        else
            StartCoroutine(RetrySetActive(item));
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
