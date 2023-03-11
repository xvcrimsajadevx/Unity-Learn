using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AnimalHunger : MonoBehaviour
{
    [SerializeField] private Collider AnimalCollider;

    [SerializeField] private Slider hungerSlider;
    [SerializeField] private int maxHunger;

    private int currentHunger = 0;

    // Start is called before the first frame update
    void Start()
    {
        hungerSlider.maxValue = maxHunger;
        hungerSlider.value = currentHunger;
        hungerSlider.fillRect.gameObject.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Food")
        {
            FeedAnimal();
            Destroy(other.gameObject);
        }
    }

    public void FeedAnimal()
    {
        currentHunger++;

        hungerSlider.fillRect.gameObject.SetActive(true);
        hungerSlider.value = currentHunger;

        if (currentHunger >= maxHunger)
        {
            Destroy(gameObject, 0.1f);

            gameObject.GetComponentInParent<ScoreManager>().ManageScore(maxHunger);
        }
    }
}
