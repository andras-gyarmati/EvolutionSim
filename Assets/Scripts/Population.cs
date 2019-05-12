using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEngine;

namespace Assets.Scripts
{
    public class Population
    {
        private GameObject _gameObject;
        private List<Creature> _creatures;
        private Creature _currentCreature;
        private int _creatureIndex;


        public Population(GameObject parent)
        {
            _gameObject = new GameObject("Population");
            _gameObject.transform.SetParent(parent.transform);
            _creatures = new List<Creature>();

            for (var i = 0; i < Controller.PopulationSize; i++)
            {
                _creatures.Add(new Creature(_gameObject, Controller.PopulationSize));
            }
            _currentCreature = _creatures[0];
            _currentCreature.Activate();
        }

        private List<Creature> PickByFitness()
        {
            var picked = new List<Creature>();
            for (var j = 0; j < _creatures.Count; j++)
            {
                float sum = 0;
                foreach (Creature creature in _creatures)
                {
                    sum += creature.Fitness;
                }
                foreach (Creature c in _creatures)
                {
                    c.relProb = c.Fitness / sum;
                }

                var picker = Controller.Random.Next(100) / 100f;
                var index = 0;
                while (picker > 0 && index < _creatures.Count)
                {
                    picker -= _creatures[index].relProb;
                    index++;
                }
                index--;
                if (picker < 0)
                {
                    Creature creature = _creatures[index];
                    picked.Add(creature.NewOffspring(GetParent()));
                }
            }
            return picked;
        }

        private GameObject GetParent()
        {
            return _gameObject.transform.parent.gameObject;
        }

        public void Update()
        {
            _currentCreature.Update();
            if (_currentCreature.Age < Controller.LifeLength) return;
            _creatureIndex++;
            _currentCreature.Deactivate();
            if (_creatureIndex >= _creatures.Count) return;
            _currentCreature = _creatures[_creatureIndex];
            _currentCreature.Activate();
        }

        public void Reproduce()
        {
            _creatures = PickByFitness();
        }

        public bool AllSimulated()
        {
            return _creatureIndex == _creatures.Count - 1;
        }
    }
}