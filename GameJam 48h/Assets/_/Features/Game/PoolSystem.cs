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
            _obstaclePool = new ObjectPool<Obstacle.Obstacle>(
                CreateObstacle,
                OnGetFromPool,
                OnReleaseToPool,
                OnDestroyObstacle,
                maxSize: 9999
            );

            _pool = new List<GameObject>();
        }

        #endregion
        

        #region Main Methods

        public Obstacle.Obstacle SpawnObstacle(Vector2 position)
        {
            Obstacle.Obstacle obstacle = _obstaclePool.Get();
            obstacle.transform.position = position;
            _pool.Add(obstacle.gameObject);
            
            return obstacle;
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

        private Obstacle.Obstacle CreateObstacle()
        {
            Obstacle.Obstacle obs = Instantiate(_objectPrefab);
            obs.SetPool(_obstaclePool);
            return obs;
        }

        private void OnGetFromPool(Obstacle.Obstacle obstacle)
        {
            obstacle.gameObject.SetActive(true);
        }

        private void OnReleaseToPool(Obstacle.Obstacle obstacle)
        {
            obstacle.gameObject.SetActive(false);
        }

        private void OnDestroyObstacle(Obstacle.Obstacle obstacle)
        {
            Destroy(obstacle.gameObject);
        }
    
        #endregion
    
    
        #region Privates and Protected

        private IObjectPool<Obstacle.Obstacle> _obstaclePool;
        private List<GameObject> _pool;
        [SerializeField] private Obstacle.Obstacle _objectPrefab;
        //[SerializeField] private int _maxObstacles;

        #endregion
    }
}
