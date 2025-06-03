using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

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
                _publicGame =  GetComponent<PublicGame>();
                _poolManager = gameObject.GetComponent<PoolSystem>();
            }

            private void Start()
            {
                float lineHeight = 0;
                float maxWidth = _publicGame.CameraWidth;
                float baseX = -maxWidth / 2f;
                float baseY = Camera.main.orthographicSize + 25f; // POSI Y DES ELEMENTS DE POOL
                float currentX = baseX;
                float currentY = baseY;
                float spaceX = 0.05f;
                float spaceY = 0.05f;
                float maxHeightInLine = 0f;
                _lastLineY = baseY;
                
                for (int i = 0; i < _nbObstacles; i++)
                {
                    int randomSpawnBonus = Random.Range(0, 100);
                    GameObject obsGO;
                    Obstacle.Obstacle obsObstacle = null;
                    Bonus.BonusObject obsBonus = null;

                    if (randomSpawnBonus >= _probabilityBonus && randomSpawnBonus <= 100)
                    {
                        Debug.Log("Création d'un obstacle");
                        obsObstacle = _poolManager.SpawnObstacle(new Vector3(1000, 1000, 0));
                        Debug.Log("OBS = "  + obsObstacle);
                        obsGO = obsObstacle.gameObject;
                        Debug.Log("OBSGO = " + obsGO);
                    }
                    else
                    {
                        Debug.Log("Création d'un bonus");
                        obsBonus = _poolManager.SpawnBonus(new Vector3(1000, 1000, 0));
                        Debug.Log("OBS = "  + obsBonus);
                        obsGO = obsBonus.gameObject;
                        obsBonus.GetComponent<Rigidbody2D>().linearVelocity = Vector2.zero;
                        Debug.Log("OBSGO = "  + obsGO);
                        _nbBonusAsSpawned++;
                    }
                    
                    Debug.Log("Spawn : " + obsGO);
                    
                    Renderer renderer = obsGO.GetComponent<Renderer>();
                    if (renderer == null) continue;

                    Vector3 size = renderer.bounds.size;
                    float width = size.x;
                    float height = size.y;

                    if (currentX + width > baseX + maxWidth)
                    {
                        currentX = baseX;
                        currentY -= maxHeightInLine + spaceY;
                        _lastLineY = currentY;
                        maxHeightInLine = 0f;
                    }
                    
                    obsGO.transform.position = new Vector3(currentX + width / 2, currentY - height / 2, 0);
                    currentX += width + spaceX;
                    if (height > maxHeightInLine)
                    {
                        maxHeightInLine = height;
                    }
                    if (obsObstacle != null)
                        _obstaclesList.Add(obsObstacle);
                    
                    _objectsInteractable.Add(obsGO);
                }
            }

            // Update is called once per frame
            void Update()
            {
                _timer -=  Time.deltaTime;
                if (_timer <= 0)
                {
                    DownTheLines();
                    _timer = _linesDescentDelay;
                }
                //CheckInactivesBricks();
                Debug.Log($"Il y a {_poolManager.GetAllInactiveObstacles().Count} briques désactivées");
            }

            #endregion
    


            #region Main Methods

            public void BonusActivated()
            {
                Debug.Log("Bonus activated");
                foreach (GameObject obsGO in _objectsInteractable)
                {
                    if (obsGO.transform.position.x >= _lastLineY + 0.1f &&
                        obsGO.transform.position.x <= _lastLineY - 0.1f)
                    {
                        obsGO.SetActive(false);
                    }
                }
            }
    
            #endregion

    
            #region Utils
            
            private void CheckInactivesBricks()
            {
                var pooledObjects = _poolManager.GetAllInactiveObstacles();
                if (pooledObjects.Count == 0) return;

                float maxWidth = _publicGame.CameraWidth;
                float baseX = -maxWidth / 2f;
                float spaceX = 0.05f;
                float spaceY = 0.05f;

                // Trouver la position Y la plus haute des briques existantes
                float startY = float.MinValue;
                foreach (var obs in _obstaclesList)
                {
                    if (obs != null && obs.gameObject.activeInHierarchy)
                    {
                        float y = obs.transform.position.y;
                        if (y > startY)
                        {
                            startY = y;
                        }
                    }
                }

                float currentX = baseX;
                float currentY = startY + pooledObjects[0].GetComponent<Renderer>().bounds.size.y + spaceY;
                float maxHeightInLine = 0f;

                foreach (var obj in pooledObjects)
                {
                    Obstacle.Obstacle obs = obj.GetComponent<Obstacle.Obstacle>();
                    if (!obs) continue;

                    Renderer renderer = obs.GetComponent<Renderer>();
                    if (!renderer) continue;

                    Vector3 size = renderer.bounds.size;
                    float width = size.x;
                    float height = size.y;

                    if (currentX + width > baseX + maxWidth)
                    {
                        currentX = baseX;
                        currentY += maxHeightInLine + spaceY;
                        maxHeightInLine = 0f;
                    }

                    obj.transform.position = new Vector3(currentX + width / 2, currentY - height / 2, 0);
                    currentX += width + spaceX;

                    if (height > maxHeightInLine)
                    {
                        maxHeightInLine = height;
                    }

                    obj.SetActive(true);
                    _obstaclesList.Add(obs);
                }
            }

            private void DownTheLines()
            {
                Debug.Log("Down the lines");
                foreach (Obstacle.Obstacle obs in _obstaclesList)
                {
                    obs.transform.position += Vector3.down * obs.GetComponent<Renderer>().bounds.size.y;
                }
            }
    
            #endregion
    
    
            #region Privates and Protected

            private PoolSystem _poolManager;
            private PublicGame _publicGame;
            private float _timer;
            private List<Obstacle.Obstacle> _obstaclesList = new List<Obstacle.Obstacle>();
            private List<GameObject> _objectsInteractable = new List<GameObject>();
            private int _nbBonusAsSpawned = 0;
            private float _lastLineY = 0;
            
            [Header("Briques (Nombre, delai entre chaque descente")]
            [SerializeField] private int _nbObstacles;
            [SerializeField] private float _linesDescentDelay = 10.0f;
            [Header("Bonus (Nombre, propabilité de spawn en %")]
            [SerializeField] private float _nbBonus;
            [SerializeField] private float _probabilityBonus;

            #endregion
    }
}
