using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PhantomController : MonoBehaviour
{
    [SerializeField] private Character _boss;
    [SerializeField] private Character _player;
    private Character _character;
    private bool targetSystemEngaged = false;
    private bool blockingEngaged = false;
    [SerializeField] private float innerRadius = 2F;
    [SerializeField] private float outerRadius = 2F;
    private bool runToBoss = false;
    private bool runFromBoss = false;
    private float offset = 2F;

    private void Start()
    {
        _character = GetComponent<Character>();
    }

    void Update()
    {
        CalculateMove();

    }

    private void CalculateMove()
    {
        Vector3 vectorToBoss = _boss.transform.position - _character.transform.position;
        float distanceToBoss = Vector3.Magnitude(vectorToBoss);
        Vector3 playerVectorToBoss = _boss.transform.position - _player.transform.position;
        float playerDistanceToBoss = Vector3.Magnitude(playerVectorToBoss);

        if (distanceToBoss > playerDistanceToBoss + 2f)
        {
            runToBoss = true;
}
        if (distanceToBoss > playerDistanceToBoss - 1f && distanceToBoss < playerDistanceToBoss +1f )
        {
            runFromBoss = false;
            runToBoss = false;
            vectorToBoss = Vector3.zero;
        }
        if (distanceToBoss < playerDistanceToBoss - 2f)
        {
            runFromBoss = true;
        }

        if( runFromBoss)
        {
            vectorToBoss = -vectorToBoss;
           
        }
        _character.MoveXZ(vectorToBoss, true, _boss);
        if(_player.IsSwingingSword)
        {
            _character.SwingSword();
        }
    }
}

