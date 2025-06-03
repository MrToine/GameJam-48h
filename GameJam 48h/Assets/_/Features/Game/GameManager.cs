using UnityEngine;

namespace Game
{
    public class GameManager : MonoBehaviour
    {
            #region Publics

            //
    
            #endregion


            #region Unity API


            // Start is called once before the first execution of Update after the MonoBehaviour is created
            void Awake()
            {
                _poolManager = gameObject.GetComponent<PoolSystem>();
            }

            private void Start()
            {
                float baseY = 6.7f;
                float baseX = -3;
                for (int i = 0; i < _nbObstacles; i++)
                {
                    _poolManager.SpawnObstacle(new Vector3(baseX + i, baseY, 0));
                }
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

            // 
    
            #endregion

    
            #region Utils
    
            /* Fonctions privées utiles */
    
            #endregion
    
    
            #region Privates and Protected

            private PoolSystem _poolManager;
            [SerializeField] private int _nbObstacles;

            #endregion
    }
}
