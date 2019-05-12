using UnityEngine;

namespace Assets.Scripts
{
    public class Muscle
    {
        private Knob k1;
        private Knob k2;
        public float frequencyHz;
        public float dampingRatio;
        private DistanceJoint2D distanceJoint;
        private float distance;
        private float moveScale;
        private float timer; //??

        public Muscle(Knob k1, Knob k2, float frequencyHz, float dampingRatio)
        {
            this.k1 = k1;
            this.k2 = k2;
            this.frequencyHz = frequencyHz;
            this.dampingRatio = dampingRatio;

            moveScale = 0.7f;
            distance = Vector2.Distance(k1.Pos, k2.Pos);

            distanceJoint = k1.GameObject.AddComponent<DistanceJoint2D>();
            distanceJoint.distance = distance;
            distanceJoint.connectedBody = k2.Rigidbody;

            timer = Controller.RandomFloat(1);
        }

        public void Update()
        {
            distanceJoint.distance = Controller.MapValue(Mathf.Sin(timer), -1, 1, distance * moveScale, distance / moveScale);
            timer += 0.1f;
        }

    }
}