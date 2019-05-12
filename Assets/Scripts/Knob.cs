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

        private SpriteRenderer _spriteRenderer;
        private CircleCollider2D _circleCollider2D;

        public float Density;
        public float Friction;
        public float Restitution;

        private float _radius;

        public Knob(GameObject parent)
        {
            GameObject = new GameObject("Knob");
            GameObject.transform.SetParent(parent.transform);
            GameObject.transform.position = new Vector2(Random.Range(0, 1f), Random.Range(0, 1f));
            _pairs = new List<Knob>();
            Rigidbody = GameObject.AddComponent<Rigidbody2D>();
            Rigidbody.bodyType = RigidbodyType2D.Dynamic;

            _spriteRenderer = GameObject.AddComponent<SpriteRenderer>();
            _spriteRenderer.sprite = Resources.Load<Sprite>("1px");
            _spriteRenderer.drawMode = SpriteDrawMode.Sliced;
            _radius = 0.5f;
            _spriteRenderer.size = new Vector2(_radius, _radius);
            _spriteRenderer.color = Color.red;

            _circleCollider2D = GameObject.AddComponent<CircleCollider2D>();
            _circleCollider2D.radius = _radius;

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