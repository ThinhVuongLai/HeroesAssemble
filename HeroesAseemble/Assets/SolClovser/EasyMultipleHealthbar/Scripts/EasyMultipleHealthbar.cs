using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SolClovser.EasyMultipleHealthbar
{   
    public class EasyMultipleHealthbar : MonoBehaviour 
    {
        public static EasyMultipleHealthbar Instance { get; private set; }

        [Header("Settings")] 
        [Tooltip("All health bars will be contained and operated in this canvas.")]
        public RectTransform containerCanvas;
    
        
        [Tooltip("Health bar prefab that will be used to create all health bars.")]
        public GameObject healthbarPrefab;
        public GameObject healthbarPrefabFriend;
        [Tooltip("Set it to approximately maximum number of health bars will be used at any time.")]
        public int healthbarPoolSize;

        [Tooltip("How many layers will be utilized. Minimum is 1.")]
        [Min(1)]
        public int sortingLayersCount;
        
        public Transform[] SortingLayers { get; private set; }

        private List<HealthbarController> _healthBarsList = new List<HealthbarController>();
        private List<HealthbarController> _healthBarsListFriend = new List<HealthbarController>();
        private Queue<HealthbarController> _healthBarPool;
        private Queue<HealthbarController> _healthBarPoolFriend;

        #region Mono Methods

        private void Awake () 
        {
            CreateInstance();
            
            CreateSortingLayers();
            CreateHealthbarPool();
        }
        #endregion
     
        #region Initialization
        
        // Create MultipleHealthbarManager instance.
        // Only one instance will be live at any given time and duplicates will be destroyed.
        private void CreateInstance()
        {
            if(Instance == null)
            {
                Instance = this;			
            }
            else if(Instance != this)
            {
                Debug.LogWarning("Only one Multiple Healthbar Manager game object can exist at a time. Destroying duplicates!");
                Destroy(gameObject);
                return;
            }

           // DontDestroyOnLoad(gameObject);
        }
        #endregion

        #region Private Methods
        
        private void CreateSortingLayers()
        {
            SortingLayers = new Transform[sortingLayersCount];
            
            for (int i = 0; i < sortingLayersCount; i++)
            {
                GameObject tempObject = new GameObject("Sorting Layer " + i,typeof(RectTransform));
                
                tempObject.transform.SetParent(containerCanvas, false);
                SortingLayers[i] = tempObject.transform;
            }
        }
        
        // Create a pool of health bars for later use
        // Instantiating objects at run-time is expensive,
        // because of that we will create our health bars at the awake once and use them whenever we want later
        private void CreateHealthbarPool()
        {
            _healthBarPool = new Queue<HealthbarController>();
            
            for (int i = 0; i < healthbarPoolSize; i++)
            {
                AddHealthbarToQueue();
              
            
            }
            _healthBarPoolFriend = new Queue<HealthbarController>();
            for (int i = 0; i <50 ; i++)
            {
            
                if (i % 2 == 0)
                {
                    AddHealthbarToQueueFriend();
                }

            }
        }

        // Add a healthbar to queue
        private void AddHealthbarToQueueFriend()
        {
            GameObject tempObject = Instantiate(healthbarPrefabFriend, Vector3.zero, healthbarPrefabFriend.transform.rotation);

            tempObject.transform.SetParent(SortingLayers[0]);

            HealthbarController healthBar = tempObject.GetComponent<HealthbarController>();

            _healthBarsListFriend.Add(healthBar);
            _healthBarPoolFriend.Enqueue(healthBar);
        }
        private void AddHealthbarToQueue()
        {
            GameObject tempObject = Instantiate(healthbarPrefab, Vector3.zero, healthbarPrefab.transform.rotation);

            tempObject.transform.SetParent(SortingLayers[0]);

            HealthbarController healthBar = tempObject.GetComponent<HealthbarController>();
            
            _healthBarsList.Add(healthBar);
            _healthBarPool.Enqueue(healthBar);
        }

        #endregion
        
        #region Public Methods
        /// <summary>
        /// Request a new health bar
        /// </summary>
        /// <returns></returns>
        public HealthbarController RequestHealthbar(bool friend)
        {
            // Expand the queue if no health bars left in it
            if(_healthBarPool.Count <= 0 && !friend)
            {
        
                    AddHealthbarToQueue();
                
                
            }
            if (_healthBarPoolFriend.Count<=0 && friend)
            {
                
                    AddHealthbarToQueueFriend();
                

            }
            HealthbarController healthBarToReturn;
            if (!friend)
            {
                healthBarToReturn = _healthBarPool.Dequeue();
            }
            else
            {
                healthBarToReturn = _healthBarPoolFriend.Dequeue();
            }
          

            return healthBarToReturn;
        }

        /// <summary>
        /// Return the healthbar to the pool
        /// </summary>
        /// <param name="healthbarToReturn"></param>
        public void ReturnHealthbar(HealthbarController healthbarToReturn)
        {
            healthbarToReturn.gameObject.SetActive(false);
            _healthBarPool.Enqueue(healthbarToReturn);
        }

        public void ReturnHealthbarFriend(HealthbarController healthbarToReturn)
        {
            healthbarToReturn.gameObject.SetActive(false);
            _healthBarPoolFriend.Enqueue(healthbarToReturn);
        }

        /// <summary>
        /// Collect all health bars. It should be used when you are finish with health bars. Example; before loading a new level 
        /// </summary>
        public void ReturnAllHealthbars(bool friend)
        {
            if (!friend)
            {
                for (int i = 0; i < _healthBarsList.Count; i++)
                {
                    ReturnHealthbar(_healthBarsList[i]);
                }
            }
            else  {

                for (int i = 0; i < _healthBarsListFriend.Count; i++)
                {
                    ReturnHealthbarFriend(_healthBarsListFriend[i]);
                }

            }
          
           
        }
        #endregion
    }
}
