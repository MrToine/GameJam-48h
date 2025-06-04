using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using TMPro;
using UnityEngine.SceneManagement;

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
                float maxWidth = _publicGame.CameraWidth;
                float baseX = (-maxWidth / 2f) + 0.2f;
                float baseY = Camera.main.orthographicSize + 25f;

                float currentX = baseX;
                float currentY = baseY;

                float spaceX = 0.05f;
                float spaceY = 0.05f;

                _lastLineY = baseY;

                float brickWidth = 1f;
                float brickHeight = 1f;

                GameObject temp = _poolManager.SpawnObstacle(Vector3.one * 1000);
                Renderer tempRenderer = temp.GetComponentInChildren<Renderer>();
                if (tempRenderer != null)
                {
                    brickWidth = tempRenderer.bounds.size.x;
                    brickHeight = tempRenderer.bounds.size.y;
                }
                temp.SetActive(false);

                for (int i = 0; i < _nbObstacles; i++)
                {
                    GameObject obsGO;
                    GameObject obsObstacle = null;
                    GameObject obsBonus = null;

                    int randomSpawnBonus = Random.Range(0, 100);
                    bool isBonus = randomSpawnBonus < _probabilityBonus;

                    if (isBonus)
                    {
                        obsBonus = _poolManager.SpawnBonus(Vector3.one * 1000);
                        obsGO = obsBonus;
                        _nbBonusAsSpawned++;
                    }
                    else
                    {
                        obsObstacle = _poolManager.SpawnObstacle(Vector3.one * 1000);
                        obsGO = obsObstacle;
                    }

                    // Centrage parfait dans la case brique
                    obsGO.transform.position = new Vector3(
                        currentX + (brickWidth / 2),
                        currentY - (brickHeight / 2),
                        0f
                    );

                    currentX += brickWidth + spaceX;

                    if (currentX + brickWidth > baseX + maxWidth * 0.85f)
                    {
                        currentX = baseX;
                        currentY -= brickHeight + spaceY;
                        _lastLineY = currentY;
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
            }

            #endregion
    


            #region Main Methods

            public void QuitGame()
            {
                Application.Quit();
            }

            public void RestartLevel()
            {
                Time.timeScale = 1;
                SceneManager.LoadScene(SceneManager.GetActiveScene().name);
            }

            public void StartGame()
            {
                SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
            }

            public void GameOver()
            {
                Time.timeScale = 0;
                _menu.SetActive(true);
            }

            public void AddScore(int value)
            {
                Debug.Log("AddScore + " + value);
                _score += value;
                _scoreText.text = $"Score : {_score}";
                Debug.Log($"Score : {_score}");
            }
            
            public void BonusActivated()
            {
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
                float currentY = startY + pooledObjects[0].GetComponentInChildren<Renderer>().bounds.size.y + spaceY;
                float maxHeightInLine = 0f;

                foreach (var obj in pooledObjects)
                {
                    GameObject obs = obj.GetComponentInChildren<GameObject>();
                    if (!obs) continue;

                    Renderer renderer = obs.GetComponentInChildren<Renderer>();
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
                foreach (GameObject obs in _obstaclesList)
                {
                    Renderer renderer = obs.GetComponentInChildren<Renderer>();
                    if (!renderer)
                    {
                        continue;
                    }
                    obs.transform.position += Vector3.down * renderer.bounds.size.y;
                }
            }
    
            #endregion
    
    
            #region Privates and Protected

            private PoolSystem _poolManager;
            private PublicGame _publicGame;
            private float _timer;
            private List<GameObject> _obstaclesList = new List<GameObject>();
            private List<GameObject> _objectsInteractable = new List<GameObject>();
            private int _nbBonusAsSpawned = 0;
            private float _lastLineY = 0;
            private int _score;
        
            [SerializeField] private TMP_Text _scoreText;
            
            [Header("Briques (Nombre, delai entre chaque descente")]
            [SerializeField] private int _nbObstacles;
            [SerializeField] private float _linesDescentDelay = 10.0f;
            [Header("Bonus (Nombre, propabilité de spawn en %")]
            [SerializeField] private float _nbBonus;
            [SerializeField] private float _probabilityBonus;
            [SerializeField] private GameObject _menu;

            #endregion
    }
}
