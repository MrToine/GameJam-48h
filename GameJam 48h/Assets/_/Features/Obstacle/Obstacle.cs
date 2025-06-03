using System;
using UnityEngine;
using UnityEngine.Pool;

namespace Obstacle
{
    public class Obstacle : MonoBehaviour
    {
        #region Publics

        public static float Width => _objectWidth;
        public static float Height => _objectHeight;
            
        #endregion


        #region Unity API


        // Start is called once before the first execution of Update after the MonoBehaviour is created
        void Awake()
        {
            _objectWidth = gameObject.GetComponent<SpriteRenderer>().bounds.size.x;
            _objectHeight = gameObject.GetComponent<SpriteRenderer>().bounds.size.y;
        }

        // Update is called once per frame
        void Update()
        {
            //
        }

        private void OnCollisionEnter2D(Collision2D other)
        {
            Debug.Log($"L'obstacle hit {other.gameObject.name}");
            _pool.Release(this);
        }

        #endregion
    


        #region Main Methods

        public void SetPool(IObjectPool<Obstacle> pool)
        {
            _pool = pool;
        }
    
        #endregion

    
        #region Utils
    
        /* Fonctions privées utiles */
    
        #endregion
    
    
        #region Privates and Protected

        private IObjectPool<Obstacle> _pool;
        private static float _objectWidth;
        private static float _objectHeight;

        #endregion
    }
}