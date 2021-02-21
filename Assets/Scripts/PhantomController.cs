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
        offset = UnityEngine.Random.Range(1f, 3f);
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

        float targetradius = Mathf.Clamp(playerDistanceToBoss, innerRadius,  outerRadius);

        if (distanceToBoss > targetradius )
        {
            runFromBoss = false;

            if (distanceToBoss > targetradius + offset)
            {
                runToBoss = true;
            }
        }
        else if (distanceToBoss < targetradius)
        {
            runToBoss = false;

            if (distanceToBoss < targetradius )
            {
                runFromBoss = true;
                
            }
        }


        if( runFromBoss)
        {
            vectorToBoss = -vectorToBoss;
           
        }
        if (!runFromBoss && !runToBoss)
        {
            vectorToBoss = Vector3.zero;
        }


        _character.MoveXZ(vectorToBoss, true, _boss);
        if (_player.IsSwingingSword && distanceToBoss < 3F) 
        {
            _character.SwingSword();
        }
    }
}

