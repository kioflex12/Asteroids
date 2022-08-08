using System;
using Core.Gameplay.Enemies;
using UnityEngine;

namespace Core.Gameplay
{
    [RequireComponent(typeof(BoxCollider2D))]
    public class Bullet : MonoBehaviour
    {
        private float _speed;

        public void Init(float speed)
        {
            _speed = speed;

        }

        private void Update()
        {
            Move();
        }

        private void Move()
        {
            transform.Translate(Vector3.up * _speed * Time.deltaTime);
        }

        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.TryGetComponent<Enemy>(out var enemy))
            {
                enemy.Dead();
                Destroy(gameObject);
            }
        }

        private void OnBecameInvisible()
        {
            Destroy(gameObject);
        }
    }
}