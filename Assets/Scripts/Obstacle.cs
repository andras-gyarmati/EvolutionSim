using UnityEngine;

namespace Assets.Scripts
{
    public class Obstacle
    {
        private GameObject _gameObject;
        private SpriteRenderer _spriteRenderer;
        private BoxCollider2D _boxCollider2D;
        private Rigidbody2D _rigidbody2D;

        public Obstacle(GameObject parent, Vector2 pos, Vector2 size)
        {
            _gameObject = new GameObject("Obstacle");
            _gameObject.transform.SetParent(parent.transform);
            _gameObject.transform.position = pos;

            _spriteRenderer = _gameObject.AddComponent<SpriteRenderer>();
            _spriteRenderer.sprite = Resources.Load<Sprite>("1px");
            _spriteRenderer.drawMode = SpriteDrawMode.Sliced;
            _spriteRenderer.size = size;
            _spriteRenderer.color = Color.green;

            _boxCollider2D = _gameObject.AddComponent<BoxCollider2D>();
            _boxCollider2D.size = size;

            _rigidbody2D = _gameObject.AddComponent<Rigidbody2D>();
            _rigidbody2D.bodyType = RigidbodyType2D.Static;
        }
    }
}