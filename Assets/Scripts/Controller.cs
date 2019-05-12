using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Assets.Scripts
{
    public class Controller : MonoBehaviour
    {
        public static int PopulationSize;
        public static float MutationRate;
        public static float LifeLength;
        public static System.Random Random;

        private Environment _environment;

        private void Start()
        {
            PopulationSize = 500;
            MutationRate = 0.05f;
            Random = new System.Random();
            _environment = new Environment(gameObject);
        }

        public static float RandomFloat(int max)
        {
            return Random.Next(max * 100) / 100f;
        }

        public static float RandomFloat(int min, int max)
        {
            return Random.Next(min * 100, max * 100) / 100f;
        }

        private void FixedUpdate()
        {
            _environment.Update();
        }

        private void Update()
        {

        }

        public static float MapValue(float value, float min, float max, float targetMin, float targetMax) //todo test
        {
            return targetMin + (targetMax - targetMin) * ((value - min) / (max - min));
        }

        public static List<T> CloneList<T>(List<T> listToClone) where T : ICloneable
        {
            return listToClone.Select(item => (T)item.Clone()).ToList();
        }
    }
}
