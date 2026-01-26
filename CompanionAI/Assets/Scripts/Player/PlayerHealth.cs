using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class PlayerHealth : MonoBehaviour
{
    public int maxHealth = 100;
    public int currentHealth;

    public Image healthFill;
    public float feedbackDuration = 1f;

    Coroutine colorRoutine;

    void Start()
    {
        currentHealth = maxHealth;
        UpdateBar(Color.green);
    }

    public void TakeDamage(int damage)
    {
        currentHealth -= damage;
        currentHealth = Mathf.Clamp(currentHealth, 0, maxHealth);

        ShowFeedback(Color.red);
    }

    public void Heal(int amount)
    {
        currentHealth += amount;
        currentHealth = Mathf.Clamp(currentHealth, 0, maxHealth);

        ShowFeedback(Color.blue);
    }

    void ShowFeedback(Color color)
    {
        if (colorRoutine != null)
            StopCoroutine(colorRoutine);

        colorRoutine = StartCoroutine(ColorRoutine(color));
    }

    IEnumerator ColorRoutine(Color color)
    {
        UpdateBar(color);
        yield return new WaitForSeconds(feedbackDuration);
        UpdateBar(Color.green);
    }

    void UpdateBar(Color color)
    {
        healthFill.fillAmount = (float)currentHealth / maxHealth;
        healthFill.color = color;
    }
}
