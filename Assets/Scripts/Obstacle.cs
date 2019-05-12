using UnityEngine;

namespace Assets.Scripts
{
    public class Obstacle
    {
        private GameObject _gameObject;
        private SpriteRenderer _spriteRenderer;

        public Obstacle(GameObject parent, Vector2 pos, Vector2 size)
        {
            _gameObject = new GameObject("Obstacle");
            _gameObject.transform.SetParent(parent.transform);
            _gameObject.transform.position = pos;
            _gameObject.transform.localScale = size;
            _spriteRenderer = _gameObject.AddComponent<SpriteRenderer>();
            _spriteRenderer.sprite = Resources.Load<Sprite>("1px");
            _spriteRenderer.color = Color.green;

        }
    }
}