using UnityEngine;

namespace Assets.Scripts
{
    public class Obstacle
    {
        private GameObject _gameObject;
        

        public Obstacle(GameObject parent, Vector2 pos, Vector2 size)
        {
            _gameObject = new GameObject("Obstacle");
            _gameObject.transform.SetParent(parent.transform);
            _gameObject.transform.position = pos;
        }

    }
}