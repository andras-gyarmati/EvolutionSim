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

        private Vector2 startPos;

        public PhysicsMaterial2D PhysicsMaterial2D;

        private LineRenderer _lineRenderer;
        private CircleCollider2D _circleCollider2D;

        public float Density;
        public float Friction;
        public float Restitution;

        int circleResolution = 10;
        private float _radius;

        public Knob(GameObject parent, float radius, float bounciness, float friction)
        {

            _radius = radius;
            GameObject = new GameObject("Knob");
            GameObject.transform.SetParent(parent.transform);
            GameObject.transform.position = new Vector2(Random.Range(0, 2f), Random.Range(0, 2f));
            startPos = GameObject.transform.position;
            _pairs = new List<Knob>();
            Rigidbody = GameObject.AddComponent<Rigidbody2D>();
            Rigidbody.bodyType = RigidbodyType2D.Dynamic;

            PhysicsMaterial2D = new PhysicsMaterial2D("KnobPM");
            PhysicsMaterial2D.bounciness = bounciness;
            PhysicsMaterial2D.friction = friction;

            var spriteRenderer = GameObject.AddComponent<SpriteRenderer>();
            spriteRenderer.sprite = Resources.Load<Sprite>("1px");
            spriteRenderer.drawMode = SpriteDrawMode.Sliced;
            spriteRenderer.size = new Vector2(_radius, _radius);
            spriteRenderer.color = Color.red;

            //_lineRenderer = GameObject.AddComponent<LineRenderer>();
            //_lineRenderer.positionCount = circleResolution;
            //var material = Resources.Load<Material>("Knob");
            //var lineRendererMaterial = material;
            //_lineRenderer.material = lineRendererMaterial;
            //_lineRenderer.startWidth = 0.1f;
            //_lineRenderer.endWidth = 0.1f;
            //SetLinePos();

            _circleCollider2D = GameObject.AddComponent<CircleCollider2D>();
            _circleCollider2D.radius = _radius;

            Deactivate();
        }

        //private void SetLinePos()
        //{
        //    var vector = Vector2.right * _radius;
        //    for (int i = 0; i < circleResolution; i++)
        //    {
        //        _lineRenderer.SetPosition(i, vector);
        //        Quaternion.AngleAxis(360f / circleResolution, Vector3.back);
        //    }
        //}

        public void Deactivate()
        {
            GameObject.SetActive(false);
        }

        public void Activate()
        {
            GameObject.transform.position = startPos;
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