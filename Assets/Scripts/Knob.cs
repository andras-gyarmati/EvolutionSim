using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts
{
    public class Knob
    {
        public GameObject GameObject;
        private List<Knob> _pairs;
        public int PairCount => _pairs.Count;
        public Vector2 Pos => GameObject.transform.position;
        public Rigidbody2D Rigidbody;
        public float Density;
        public float Friction;
        public float Restitution;

        public Knob(GameObject parent)
        {
            GameObject = new GameObject("Knob");
            GameObject.transform.SetParent(parent.transform);
            _pairs = new List<Knob>();
            Rigidbody = GameObject.AddComponent<Rigidbody2D>();
            Deactivate();
        }

        public void Deactivate()
        {
            GameObject.SetActive(false);
        }

        public void Activate()
        {
            GameObject.SetActive(true);
        }

        public void AddPair(Knob other)
        {
            _pairs.Add(other);
        }

        public bool HasPair(Knob knob)
        {
            return _pairs.Contains(knob);
        }
    }
}