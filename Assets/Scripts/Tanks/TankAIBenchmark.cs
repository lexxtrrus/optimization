using System;
using System.Collections;
using System.Collections.Generic;
using Tanks;
using UnityEngine;
using Random = UnityEngine.Random;

public class TankAIBenchmark : MonoBehaviour
{
    [SerializeField] private Transform player;
    AITank[] tanks;
    public int numberOfTanks;
    public GameObject tankPrefab;

    private void Start()
    {
        tanks = new AITank[numberOfTanks];
        for (int i = 0; i < numberOfTanks; i++)
        {
            var pos = new Vector3(Random.Range(-50, 50), 0, Random.Range(-50, 50));
            Instantiate(tankPrefab, pos, Quaternion.identity).AddComponent<AITank>().Init(player, i, this);
        }
    }

    private void Update()
    {
        foreach (var tank in tanks)
        {
            if(tank) tank.Tick();
        }
    }

    public void Add(int index, AITank ai)
    {
        tanks[index] = ai;
    }

    public void Remove(int index)
    {
        tanks[index] = null;
    }
}
