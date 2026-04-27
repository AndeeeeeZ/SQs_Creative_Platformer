using System.Collections;
using UnityEngine;

public class FloorMonsterSpawner : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private Transform player;
    [SerializeField] private Transform leftPoint;
    [SerializeField] private Transform rightPoint;
    [SerializeField] private MonsterMover monsterPrefab;

    [Header("Spawn Settings")]
    [SerializeField] private float checkInterval = 1f;
    [SerializeField] private float spawnChance = 0.1f;

    [Header("Floor Detection")]
    [SerializeField] private float floorTolerance = 1.0f;

    private MonsterMover currentMonster;

    private void Start()
    {
        StartCoroutine(SpawnLoop());
    }

    private IEnumerator SpawnLoop()
    {
        while (true)
        {
            yield return new WaitForSeconds(checkInterval);

            // Skip if player is not on this floor
            if (!IsPlayerOnThisFloor()) continue;

            // Skip if a monster already exists
            if (currentMonster != null) continue;

            // 10% chance
            if (Random.value < spawnChance)
            {
                SpawnMonster();
            }
        }
    }

    private bool IsPlayerOnThisFloor()
    {
        // Compare Y only to determine if player is on this floor
        return Mathf.Abs(player.position.y - leftPoint.position.y) < floorTolerance;
    }

    private void SpawnMonster()
    {
        // Compare ONLY X distance (since hallway is 1D)
        float leftDist = Mathf.Abs(player.position.x - leftPoint.position.x);
        float rightDist = Mathf.Abs(player.position.x - rightPoint.position.x);

        Transform spawnPoint = leftDist > rightDist ? leftPoint : rightPoint;

        currentMonster = Instantiate(monsterPrefab, spawnPoint.position, Quaternion.identity);

        currentMonster.Initialize(
            player,
            leftPoint.position.x,
            rightPoint.position.x,
            OnMonsterRemoved
        );
    }

    private void OnMonsterRemoved()
    {
        // Clear reference when monster is gone
        currentMonster = null;
    }
}