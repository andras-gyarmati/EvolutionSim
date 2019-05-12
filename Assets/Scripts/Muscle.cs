using UnityEngine;

namespace Assets.Scripts
{
    public class Muscle
    {
        private GameObject _gameObject;
        private Knob k1;
        private Knob k2;
        public float frequencyHz;
        public float dampingRatio;
        private DistanceJoint2D distanceJoint;
        private float distance;
        private float moveScale;
        private float timer; //??
        private LineRenderer _lineRenderer;

        public Muscle(GameObject parent, Knob k1, Knob k2, float frequencyHz, float dampingRatio)
        {
            _gameObject = new GameObject("Muscle");
            _gameObject.transform.SetParent(parent.transform);
            this.k1 = k1;
            this.k2 = k2;
            this.frequencyHz = frequencyHz;
            this.dampingRatio = dampingRatio;

            moveScale = 0.7f;
            distance = Vector2.Distance(k1.Pos, k2.Pos);

            distanceJoint = k1.GameObject.AddComponent<DistanceJoint2D>();
            distanceJoint.distance = distance;
            distanceJoint.connectedBody = k2.Rigidbody;

            _lineRenderer = _gameObject.AddComponent<LineRenderer>();
            var lineRendererMaterial = Resources.Load<Material>("Line");
            _lineRenderer.material = lineRendererMaterial;
            _lineRenderer.startWidth = 0.1f;
            _lineRenderer.endWidth = 0.1f;
            SetLinePos();

            timer = Random.Range(0, 1f);
        }

        private void SetLinePos()
        {
            _lineRenderer.SetPosition(0, k1.Pos);
            _lineRenderer.SetPosition(1, k2.Pos);
        }

        public void Update()
        {
            distanceJoint.distance = Controller.MapValue(Mathf.Sin(timer), -1, 1, distance * moveScale, distance / moveScale);
            SetLinePos();
            timer += 0.1f;
        }

    }
}