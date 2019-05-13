using System;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;

namespace Assets.Scripts
{
    public class Controller : MonoBehaviour
    {
        public static int PopulationSize;
        public static float MutationRate;
        public static float LifeLength;
        public static System.Random Random;
        public static float TimeScale;

        private Environment _environment;
        private TextMeshProUGUI _populationText;

        private void Start()
        {
            PopulationSize = 3;
            MutationRate = 0.05f;
            LifeLength = 5f;
            TimeScale = 3;
            Random = new System.Random();
            _environment = new Environment(gameObject);
            _populationText = gameObject.GetComponentInChildren<TextMeshProUGUI>();
        }

        private void FixedUpdate()
        {
            _environment.Update();
            _populationText.text = $"population: {_environment.PopulationIndex}";
            Time.timeScale = TimeScale;
        }

        public static float MapValue(float value, float min, float max, float targetMin, float targetMax)
        {
            return targetMin + (targetMax - targetMin) * ((value - min) / (max - min));
        }

        public static List<T> CloneList<T>(List<T> listToClone) where T : ICloneable
        {
            return listToClone.Select(item => (T)item.Clone()).ToList();
        }
    }
}
