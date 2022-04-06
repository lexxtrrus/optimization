using System;
using UnityEngine;

public interface ITickable
{
    void Tick();
    void Subscribe();
    void Unsubscribe();
}

namespace Tanks
{
    public class AITank: MonoBehaviour, ITickable
    {
        private Transform player;
        private Transform enemy;
        private int index;
        private TankAIBenchmark tankAIBenchmark;

        public void Init(Transform player, int index, TankAIBenchmark tankAIBenchmark)
        {
            enemy = transform;
            this.player = player;
            this.index = index;
            this.tankAIBenchmark = tankAIBenchmark;

            Subscribe();
        }
        
        public void Tick()
        {
            enemy.LookAt(player);
            enemy.Translate(0, 0, 0.05f);
        }

        public void Subscribe()
        {
            tankAIBenchmark.Add(index, this);
        }

        public void Unsubscribe()
        {
            tankAIBenchmark.Remove(index);
        }

        private void OnDestroy()
        {
            Unsubscribe();
        }
    }
}