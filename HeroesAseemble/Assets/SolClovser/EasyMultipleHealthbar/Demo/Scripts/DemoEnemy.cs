using UnityEngine;
using SolClovser.EasyMultipleHealthbar;

public class DemoEnemy : MonoBehaviour
{
    public int maxHp = 100;
    public int currentHp = 100;
    public Vector3 positionOffset = new Vector3(0, 2, 0);
    public int sortingLayer;
    public bool friend = false;
    int firstStart = 0;

    private HealthbarController _healthbar;
    private HeroesAssemble.CharacterController characterController;

    private void Awake()
    {
        characterController = GetComponent<HeroesAssemble.CharacterController>();
    }

    public void Start()
    {
        if (transform.tag == "Pokemon")
        {
            maxHp = characterController.health;

            currentHp = characterController.health;
        }

        RequestAHealthbar();
    }

    private void OnEnable()
    {
        if (transform.tag == "Goblin")
        {
            currentHp = maxHp;
        }
    }

    public void Update()
    {
        UpdateHealthbar(currentHp);
    }

    public void openHealtbar()
    {
        if (_healthbar)
        {
            _healthbar.healthBarSliderRectTransform.localScale = Vector3.one;
        }
    }

    public void hideHealtbar()
    {
        if (_healthbar)
        {
            _healthbar.healthBarSliderRectTransform.localScale = Vector3.zero;
        }
    }

    // How to request a healthbar
    private void RequestAHealthbar()
    {
        // Request a healthbar object from manager
        if (EasyMultipleHealthbar.Instance != null)
        {
            _healthbar = EasyMultipleHealthbar.Instance.RequestHealthbar(friend);
        }

        // Then setup hp values, which transform health bar should follow, and with how much offset
        if (_healthbar != null)
        {
            _healthbar.SetupUI(maxHp, currentHp, transform, positionOffset);
        }

        // Set the sorting layer to something higher if you want this healthbar to stay on top of other healthbars
        // You might need to increase the layer count in Easy Multiple Healthbar object.
        if (_healthbar != null)
        {
            _healthbar.SetSortingLayer(sortingLayer);
        }

        // You can use this if two healthbars are in same layer and they are overlapping.
        // _healthbar.MoveToBottomInLayerHierarchy();
    }

    // How to update the healthbar
    private void UpdateHealthbar(float currentValue)
    {
        if (_healthbar != null)
        {
            _healthbar.UpdateUI(currentValue);
        }
    }
}
