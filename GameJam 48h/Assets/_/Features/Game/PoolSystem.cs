using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;

namespace Game
{
    public class PoolSystem : MonoBehaviour
    {
        public PoolSystem(float ObjectWidth, float ObjectHeight)
        {
            m_objectWidth = ObjectWidth;
            m_objectHeight = ObjectHeight;
        }

        public float m_objectWidth;
        public float m_objectHeight;
        
        #region Publics

        //
    
        #endregion


        #region Unity API


        // Start is called once before the first execution of Update after the MonoBehaviour is created
        void Awake()
        {
            _obstaclePool = new ObjectPool<GameObject>(
                CreateObstacle,
                OnGetFromPool,
                OnReleaseToPool,
                OnDestroyObstacle,
                maxSize: 9999
            );
            
            _bonusPool = new ObjectPool<GameObject>(
                CreateBonus,
                OnGetFromBonusPool,
                OnReleaseToBonusPool,
                OnDestroyBonus,
                maxSize: 9999
            );

            _poolBonus = new List<GameObject>();
            _pool = new List<GameObject>();
        }

        #endregion
        

        #region Main Methods

        public GameObject SpawnObstacle(Vector2 position)
        {
            GameObject obstacle = _obstaclePool.Get();
            obstacle.transform.position = position;
            _pool.Add(obstacle.gameObject);
            
            return obstacle;
        }
        
        public GameObject SpawnBonus(Vector2 position)
        {
            GameObject bns = _bonusPool.Get();
            bns.transform.position = position;
            _poolBonus.Add(bns.gameObject);
            
            return bns;
        }

        public List<GameObject> GetAllInactiveObstacles()
        {
            List<GameObject> inactiveList = new List<GameObject>();

            foreach (GameObject obs in _pool)
            {
                if (!obs.activeInHierarchy)
                {
                    inactiveList.Add(obs);
                }
            }
            
            return inactiveList;
        }
    
        #endregion

    
        #region Utils

        private GameObject CreateBonus()
        {
            GameObject bonus = Instantiate(_bonusPrefab);
            bonus.GetComponentInChildren<Bonus.BonusObject>().SetPool(_bonusPool);
            return bonus;
        }

        private GameObject CreateObstacle()
        {
            GameObject obs = Instantiate(_objectPrefab);
            obs.GetComponentInChildren<Game.Obstacle>().SetPool(_obstaclePool);
            return obs;
        }

        private void OnGetFromPool(GameObject obstacle)
        {
            obstacle.gameObject.SetActive(true);
        }

        private void OnReleaseToPool(GameObject obstacle)
        {
            obstacle.gameObject.SetActive(false);
        }
        
        private void OnGetFromBonusPool(GameObject bns)
        {
            bns.gameObject.SetActive(true);
        }

        private void OnReleaseToBonusPool(GameObject bns)
        {
            bns.gameObject.SetActive(false);
        }

        private void OnDestroyObstacle(GameObject instance)
        {
            Destroy(instance.gameObject);
        }
        
        private void OnDestroyBonus(GameObject instance)
        {
            Destroy(instance.gameObject);
        }
    
        #endregion
    
    
        #region Privates and Protected

        private IObjectPool<GameObject> _obstaclePool;
        private IObjectPool<GameObject> _bonusPool;
        private List<GameObject> _pool;
        private List<GameObject> _poolBonus;
        [SerializeField] private GameObject _objectPrefab;
        [SerializeField] private GameObject _bonusPrefab;
        //[SerializeField] private int _maxObstacles;

        #endregion
    }
}
