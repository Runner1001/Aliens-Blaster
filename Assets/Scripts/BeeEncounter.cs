using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Random = UnityEngine.Random;

public class BeeEncounter : MonoBehaviour
{
    [SerializeField] List<Transform> lightnings;
    [SerializeField] float delayBeforeDamage = 2.5f;
    [SerializeField] float lightningAnimationTime = 2.5f;
    [SerializeField] float delayBetweenLightning = 2.5f;
    [SerializeField] float delayBetweenStrikes = 0.25f;
    [SerializeField] float lightningRadius = 1.0f;
    [SerializeField] LayerMask playerLayer;
    [SerializeField] int numberOfLightnings = 1;

    private Collider2D[] playerHitResults = new Collider2D[10];
    private List<Transform> activeLightnings;

    void OnEnable()
    {
        StartCoroutine(StartEncounter());
    }

    void OnValidate()
    {
        if (lightningAnimationTime <= delayBeforeDamage)
            delayBeforeDamage = lightningAnimationTime;

    }

    private IEnumerator StartEncounter()
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
        if(activeLightnings.Count >= lightnings.Count)
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
            var player = playerHitResults[i].GetComponent<Player>();
            if (player != null)
            {
                player.TakeDamage(Vector2.zero);
            }
        }
    }
}
