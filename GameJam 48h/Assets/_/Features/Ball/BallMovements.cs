using Game;
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
                Invoke(nameof(SetRandomTrajectory), 1f);
            }

            private void OnCollisionEnter2D(Collision2D collision)
            {
                Debug.Log(collision.gameObject.name);
                if (collision.gameObject.layer == LayerMask.NameToLayer("DeathZone"))
                {
                    Debug.Log("GameOver");
                    _gameManager.GameOver();
                }
            }

            #endregion
            

            #region Main Methods

            // 
    
            #endregion

    
            #region Utils

            private void SetRandomTrajectory()
            {
                Vector2 force = Vector2.zero;
                force.x = Random.Range(-1f, 1f);
                force.y = -1f;
                
                _rb.AddForce(force.normalized * (_speed + _acceleration));
            }
    
            #endregion
    
    
            #region Privates and Protected

            private Rigidbody2D _rb;
            [SerializeField] private PublicGame _publicGame;
            [SerializeField] private float _speed = 500f;
            [SerializeField] private float _acceleration = 5f;
            [SerializeField] private GameManager _gameManager;
            
            #endregion
    }
}
