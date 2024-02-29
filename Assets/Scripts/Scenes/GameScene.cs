using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameScene : BaseScene
{
    [SerializeField] Monster monsterPrefab;
    [SerializeField] Transform spawnPoint;
    [SerializeField] int count;

    public override IEnumerator LoadingRoutine()
    {
        Debug.Log("GameScene Load");
        // fake loading
        yield return new WaitForSecondsRealtime(0.5f);
        Manager.Scene.SetLoadingBarValue(0.7f);
        Debug.Log("Player Spawn");
        yield return new WaitForSecondsRealtime(0.5f);
        Manager.Scene.SetLoadingBarValue(0.9f);
        Debug.Log("Ready ObjectPool");

        /*for (int i = 0; i < count; i++)
        {
            Vector2 randomOffset = Random.insideUnitCircle * 3;
            Vector3 spawnPos = spawnPoint.position + new Vector3(randomOffset.x, 0, randomOffset.y);
            Monster monster = Instantiate(monsterPrefab, spawnPos, Quaternion.identity);

            yield return new WaitForSecondsRealtime(0.5f);
        }*/

        yield return new WaitForSecondsRealtime(0.5f);
        Debug.Log("Loading complete");
    }

    public void ToTitleScene()
    {
        Manager.Scene.LoadScene("TitleScene");
    }
}
