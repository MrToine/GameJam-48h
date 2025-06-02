using UnityEngine;

namespace Ball
{
    public class BallMovements : MonoBehaviour
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
                // Donner une vitesse initiale à la balle (diagonal vers le haut-droite)
                Vector2 initialDirection = new Vector2(1f, -1f).normalized;
                _rb.linearVelocity = initialDirection * _speedBall;
            }

            private void FixedUpdate()
            {
                Debug.DrawLine(transform.position, transform.position + (Vector3)_rb.linearVelocity, Color.red);
            }

            private void OnCollisionEnter2D(Collision2D collision)
            {
                
                Vector2 normal = collision.contacts[0].normal;
                Vector2 currentVelocity = _rb.linearVelocity;
                
                Vector2 reflectedVelocity = Vector2.Reflect(currentVelocity, normal);
                
                float minAngleRad = Mathf.Deg2Rad * 15f;

                if (Mathf.Abs(reflectedVelocity.x) < minAngleRad || Mathf.Abs(reflectedVelocity.y) < minAngleRad)
                {
                    float newX = reflectedVelocity.x >= 0 ? Mathf.Sin(minAngleRad) : -Mathf.Sin(minAngleRad);
                    float newY = reflectedVelocity.y >= 0 ? Mathf.Cos(minAngleRad) : -Mathf.Cos(minAngleRad);
                    reflectedVelocity = new Vector2(newX, newY);
                }
                
                reflectedVelocity = reflectedVelocity.normalized * _speedBall;
                
                _rb.linearVelocity = reflectedVelocity;
            }

            #endregion
            

            #region Main Methods

            // 
    
            #endregion

    
            #region Utils
    
            /* Fonctions privées utiles */
    
            #endregion
    
    
            #region Privates and Protected

            private Rigidbody2D _rb;

            [SerializeField] private float _speedBall = 1.5f;
            
            #endregion
    }
}
