using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UIElements;

namespace Player
{
    public class Player : MonoBehaviour
    {
            #region Publics

            //
    
            #endregion


            #region Unity API


            // Start is called once before the first execution of Update after the MonoBehaviour is created
            void Awake()
            {
                _rb = GetComponent<Rigidbody2D>();
            }
            
            private void FixedUpdate()
            {
                _rb.linearVelocityX = _dir * 2;
            }

            private void OnCollisionEnter2D(Collision2D collision)
            {
                if (collision.gameObject.layer == LayerMask.NameToLayer("Bonus"))
                {
                    _bonusActivated.Invoke();
                    collision.gameObject.SetActive(false);
                    collision.transform.position = new Vector3(1000, 1000, 0);
                }
            }

            #endregion
            

            #region Main Methods

            public void ChangeDirection()
            {
                _dir = -_dir;
            }
    
            #endregion

    
            #region Utils
    
            /* Fonctions privées utiles */
    
            #endregion
    
    
            #region Privates and Protected

            private Rigidbody2D _rb;
            private float _dir = 1.0f;
            [SerializeField] private UnityEvent _bonusActivated;

            #endregion
    }
}
