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
            
                _cameraHeight = _camera.orthographicSize * 2;
                _cameraWidth = _cameraHeight * _camera.aspect;

                _leftBound = _camera.transform.position.x - _cameraWidth / 2f;
                _rightBound = _camera.transform.position.x + _cameraWidth / 2f;
            }

            // Update is called once per frame
            private void Update()
            {
                if (_player.position.x < _leftBound)
                {
                    _player.position = new Vector2(_rightBound,  _player.position.y);
                }

                if (_player.position.x > _rightBound)
                {
                    _player.position = new Vector2(_leftBound,  _player.position.y);
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

            private float _cameraHeight;
            private float _cameraWidth;

            private float _leftBound;
            private float _rightBound;
            
            [SerializeField] private Transform _player;
            [SerializeField] private Camera _camera;

            #endregion
    }
}
