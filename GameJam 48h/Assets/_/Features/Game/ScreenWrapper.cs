using UnityEngine;

namespace Game
{
    public class ScreenWrapper : MonoBehaviour
    {
            #region Publics

            //
    
            #endregion


            #region Unity API
            
            // Start is called once before the first execution of Update after the MonoBehaviour is created
            void Awake()
            {
                _camera = Camera.main;
                _publicGame =  GetComponent<PublicGame>();

                if (_camera != null)
                {
                    _publicGame.CameraHeight = _camera.orthographicSize * 2;
                    _publicGame.CameraWidth = _publicGame.CameraHeight * _camera.aspect;
                    _publicGame.CameraSize = _camera.orthographicSize;
                    _publicGame.CameraPosition = _camera.transform.position;

                }
            }

            private void Start()
            {
                    _leftBound = _camera.transform.position.x - _publicGame.CameraWidth / 2f;
                    _rightBound = _camera.transform.position.x + _publicGame.CameraWidth / 2f;
                
            }

            // Update is called once per frame
            private void Update()
            {
                Debug.Log($"Left: {_leftBound}, Right: {_rightBound}, PlayerX: {_player.position.x}");
                float buffer = 0.1f;
                float playerX = _player.position.x;
                
                if (playerX <= _leftBound)
                {
                    _player.position = new Vector2(_rightBound - buffer, _player.position.y);
                }
                else if (playerX >= _rightBound)
                {
                    _player.position = new Vector2(_leftBound + buffer, _player.position.y);
                }
            }

            #endregion
        


            #region Main Methods

            //
        
            #endregion

        
            #region Utils
        
            /* Fonctions privées utiles */
        
            #endregion
        
        
            #region Privates and Protected 

            private float _leftBound;
            private float _rightBound;
            private PublicGame _publicGame;
            
            [SerializeField] private Transform _player;
            [SerializeField] private Camera _camera;

            #endregion
    }
}
