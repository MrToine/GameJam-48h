using UnityEngine;

namespace Game
{
    public class PublicGame : MonoBehaviour
    {
            #region Publics

            //
    
            #endregion


            #region Unity API


            // Start is called once before the first execution of Update after the MonoBehaviour is created
            void Awake()
            {
                //
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
                //
            }

            #endregion
    


            #region Main Methods

            public float CameraWidth
            {
                get => _cameraWidth;
                set => _cameraWidth = value;
            }

            public float CameraHeight
            {
                get => _cameraHeight;
                set => _cameraHeight = value;
            }

            public float CameraSize
            {
                get => _cameraSize;
                set => _cameraSize = value;
            }

            public Vector2 CameraPosition
            {
                get => _cameraPosition;
                set => _cameraPosition = value;
            }

            #endregion

    
            #region Utils
    
            /* Fonctions privées utiles */
    
            #endregion
    
    
            #region Privates and Protected


            private float _cameraHeight;
            private float _cameraWidth;
            private float _cameraSize;
            private Vector2 _cameraPosition;

            #endregion
    }
}
