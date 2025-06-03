using UnityEngine;
using UnityEngine.Pool;

namespace Bonus
{
    public class BonusObject : MonoBehaviour
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
            
            public void SetPool(IObjectPool<BonusObject> pool)
            {
                _pool = pool;
            }
    
            #endregion

    
            #region Utils
    
            /* Fonctions privées utiles */
    
            #endregion
    
    
            #region Privates and Protected
        
            private IObjectPool<BonusObject> _pool;

            #endregion
    }
}
