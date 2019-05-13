using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts
{
    public class Environment
    {
        private GameObject _gameObject;
        private List<Obstacle> _obstacles;
        private Population _population;
        public int PopulationIndex;

        public Environment(GameObject parent)
        {
            _gameObject = new GameObject("Environment");
            _gameObject.transform.SetParent(parent.transform);

            _obstacles = new List<Obstacle>();
            _obstacles.Add(new Obstacle(_gameObject, new Vector2(0, -4.5f), new Vector2(50, 1)));

            _population = new Population(_gameObject);
            PopulationIndex = 0;
        }

        public void Update()
        {
            _population.Update();
            if (_population.AllSimulated())
            {
                _population.Reproduce();
                PopulationIndex++;
            }
        }

    }
}