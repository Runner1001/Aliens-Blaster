using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Random = UnityEngine.Random;

public class BeeEncounter : MonoBehaviour, ITakeDamage
{
    [SerializeField] List<Transform> lightnings;
    [SerializeField] float delayBeforeDamage = 2.5f;
    [SerializeField] float lightningAnimationTime = 2.5f;
    [SerializeField] float delayBetweenLightning = 2.5f;
    [SerializeField] float delayBetweenStrikes = 0.25f;
    [SerializeField] float lightningRadius = 1.0f;
    [SerializeField] LayerMask playerLayer;
    [SerializeField] int numberOfLightnings = 1;
    [SerializeField] GameObject bee;
    [SerializeField] Animator beeAnimator;
    [SerializeField] Rigidbody2D beeRigidBody;
    [SerializeField] Transform[] beeDestinations;
    [SerializeField] float minIdleTime = 1.0f;
    [SerializeField] float maxIdleTime = 2.0f;
    [SerializeField] GameObject beeLaser;
    [SerializeField] int maxHealth = 10;
    [SerializeField] Water water;
    [SerializeField] Collider2D groundCollider;

    private Collider2D[] playerHitResults = new Collider2D[10];
    private List<Transform> activeLightnings;
    private bool shotStarted = false;
    private bool shotFinished = false;
    int currentHealth;

    void OnEnable()
    {
        currentHealth = maxHealth;
        StartCoroutine(StartLightning());
        StartCoroutine(StartMovement());
        var wrapper = GetComponentInChildren<ShootAnimationWrapper>();
        wrapper.OnShoot += () => shotStarted = true;
        wrapper.OnReload += () => shotFinished = true;
    }

    void OnValidate()
    {
        if (lightningAnimationTime <= delayBeforeDamage)
            delayBeforeDamage = lightningAnimationTime;

    }

    private IEnumerator StartMovement()
    {
        beeLaser.SetActive(false);
        GrabBag<Transform> grabBag = new GrabBag<Transform>(beeDestinations);

        while (true)
        {
            var destination = grabBag.Grab();
            if (destination == null)
            {
                Debug.LogError("No destination found for bee movement.");
                yield break;
            }

            beeAnimator.SetBool("Move", true);

            while (Vector2.Distance(bee.transform.position, destination.position) > 0.1f)
            {
                bee.transform.position = Vector2.MoveTowards(bee.transform.position, destination.position, Time.deltaTime);
                yield return null;
            }

            beeAnimator.SetBool("Move", false);

            yield return new WaitForSeconds(Random.Range(minIdleTime, maxIdleTime));
            beeAnimator.SetTrigger("Fire");

            yield return new WaitUntil(() => shotStarted);
            shotStarted = false;
            beeLaser.SetActive(true);

            yield return new WaitUntil(() => shotFinished);
            shotFinished = false;
            beeLaser.SetActive(false);
        }
    }

    private IEnumerator StartLightning()
    {
        foreach (var lightning in lightnings)
        {
            lightning.gameObject.SetActive(false);
        }

        activeLightnings = new List<Transform>();
        while (true)
        {
            for (int i = 0; i < numberOfLightnings; i++)
            {
                yield return SpawnNewLightning();
            }

            yield return new WaitUntil(() => activeLightnings.All(t => !t.gameObject.activeSelf));
            activeLightnings.Clear();
        }
    }

    private IEnumerator SpawnNewLightning()
    {
        if (activeLightnings.Count >= lightnings.Count)
        {
            Debug.LogError("All lightnings are already active. Cannot spawn new lightning.");
            yield break;
        }

        int index = Random.Range(0, lightnings.Count);
        var lightningIndex = lightnings[index];

        while (activeLightnings.Contains(lightningIndex))
        {
            index = Random.Range(0, lightnings.Count);
            lightningIndex = lightnings[index];
        }

        StartCoroutine(ShowLightning(lightningIndex));
        activeLightnings.Add(lightningIndex);

        yield return new WaitForSeconds(delayBetweenStrikes);
    }

    private IEnumerator ShowLightning(Transform lightning)
    {
        lightning.gameObject.SetActive(true);
        yield return new WaitForSeconds(delayBeforeDamage);
        DamagePlayersInRange(lightning);
        yield return new WaitForSeconds(lightningAnimationTime - delayBeforeDamage);
        lightning.gameObject.SetActive(false);
        yield return new WaitForSeconds(delayBetweenLightning);
    }

    private void DamagePlayersInRange(Transform lightning)
    {
        int hits = Physics2D.OverlapCircleNonAlloc(
            lightning.position, lightningRadius,
            playerHitResults, playerLayer);

        for (int i = 0; i < hits; i++)
        {
            var player = playerHitResults[i].GetComponent<PlayerAIO>();
            if (player != null)
            {
                player.TakeDamage(Vector2.zero);
            }
        }
    }

    public void TakeDamage()
    {
        currentHealth--;
        if(currentHealth == maxHealth / 2)
        {
            StartCoroutine(ToggleFlood(true));
        }
        if (currentHealth <= 0)
        {
            StopAllCoroutines();
            StartCoroutine(ToggleFlood(false));
            beeAnimator.SetBool("Dead", true);
            beeRigidBody.bodyType = RigidbodyType2D.Dynamic;
            foreach(var collider in bee.GetComponentsInChildren<Collider2D>())
            {
                collider.gameObject.layer = LayerMask.NameToLayer("Dead");
            }
        }
        else
        {
            beeAnimator.SetTrigger("Hit");
        }
    }

    private IEnumerator ToggleFlood(bool enableFlood)
    {
        float initialWaterY = water.transform.position.y;
        float targetWaterY = enableFlood ? initialWaterY + 1f : initialWaterY - 1f;
        float duration = 1f;
        float elapsedTime = 0f;
        while(elapsedTime < duration)
        {
            elapsedTime += Time.deltaTime;
            float progress = elapsedTime / duration;
            float y = Mathf.Lerp(initialWaterY, targetWaterY, progress);
            var destination = new Vector3(water.transform.position.x, y, water.transform.position.z);
            water.transform.position = destination;
            yield return null;
        }
        groundCollider.enabled = !enableFlood;
        water.SetSpeed(enableFlood ? 5f : 0f);
    }

    [ContextMenu("Half Health")]
    private void HalfHealth()
    {
        currentHealth = maxHealth / 2;
        currentHealth++;
        TakeDamage();
    }

    [ContextMenu("Kill")]
    private void Kill()
    {
        currentHealth = 1;
        TakeDamage();
    }

    [ContextMenu("Full Health")]
    private void FullHealth()
    {
        currentHealth = maxHealth;
    }
}
