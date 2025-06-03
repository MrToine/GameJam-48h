using System;
using UnityEngine;
using UnityEngine.Pool;
using UnityEngine.Events;

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

        private void OnCollisionEnter2D(Collision2D other)
        {
            if (other.gameObject.layer != LayerMask.NameToLayer("Default") ||
                other.gameObject.layer != LayerMask.NameToLayer("PieceTrajectory")
                )
            {
                ActivateAtlas();
                Invoke(nameof(DeactivateAtlas), 1);
                gameObject.SetActive(false);
                _onHit.Invoke();
            }
        }
        
        private void ActivateAtlas()
        {
            int i = 0;
            foreach (Transform child in _parent.transform)
            {
                Debug.Log("ACTIVATION GO : " + child.name);
                child.gameObject.SetActive(true);

                Rigidbody2D rb = child.GetComponent<Rigidbody2D>();
                if (rb != null)
                {
                    rb.simulated = true;

                    Vector2 forceDirection = i == 0 ? Vector2.left : Vector2.right;
                    rb.linearVelocity = forceDirection.normalized * UnityEngine.Random.Range(2f, 4f);
                }
                i++;
            }
        }

        private void DeactivateAtlas()
        {
            foreach (Transform child in _parent.transform)
            {
                child.gameObject.SetActive(false);
            }
        }

        #endregion
    


        #region Main Methods

        public void SetPool(IObjectPool<GameObject> pool)
        {
            _pool = pool;
        }
    
        #endregion

    
        #region Utils
    
        /* Fonctions privées utiles */
    
        #endregion
    
    
        #region Privates and Protected

        private IObjectPool<GameObject> _pool;
        private static float _objectWidth;
        private static float _objectHeight;

        [SerializeField] private UnityEvent _onHit;
        [SerializeField] private GameObject _parent;
        
        [Header("Gestion (Délai avant destruction des restes de brique)")]
        [SerializeField] private float _delayToDestroy;

        #endregion
    }
}