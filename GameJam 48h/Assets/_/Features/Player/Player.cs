using UnityEngine;
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

            private void Start()
            {
                //
            }

            // Update is called once per frame
            void Update()
            {
                //
            }

            private void FixedUpdate()
            {
                _rb.linearVelocityX = _dir * 2;
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

            #endregion
    }
}
