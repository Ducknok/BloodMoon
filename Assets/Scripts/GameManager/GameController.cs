using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour
{
    Vector2 startPoint;
    private Health h;
    public GameObject gameOverUI;
    private Rigidbody2D rb;
    [SerializeField] private Behaviour[] be;


    void Start()
    {
        startPoint = transform.position;
        h = GetComponent<Health>();
        rb = GetComponent<Rigidbody2D>();
    }
    
    private void Update()
    {
        if(h.currentHealth <= 0)
        {
            gameOverUI.SetActive(true);
        }
       
    }
    IEnumerator Respawn(float duration)
    {
        rb.velocity = new Vector2(0, 0);
        yield return new WaitForSeconds(duration);
        transform.position = startPoint;
        h.currentHealth = h.startingHeath;
        h.maxHealth = h.startingHeath;
        h.dead = false;
        foreach(Behaviour b in be)
        {
            b.enabled = true;
        }
    }
    public void Restart()
    {
        SceneManager.LoadSceneAsync(SceneManager.GetActiveScene().buildIndex);
    }
}
