using UnityEngine;

public class BallLauncher : MonoBehaviour
{
    [Header("発射するボールのプレハブ")]
    public GameObject ballPrefab;

    [Header("着弾までの時間（秒）")]
    public float flightTime = 1.8f;

    void Update()
    {
        // エンターキーで発射！
        if (Input.GetKeyDown(KeyCode.Return))
        {
            ShootBall();
        }
    }

    void ShootBall()
    {
        if (ballPrefab == null)
        {
            Debug.LogError("Ball Prefabがセットされていません！");
            return;
        }

        // 1. ボールを生成
        GameObject ball = Instantiate(ballPrefab, transform.position, Quaternion.identity);
        Rigidbody ballRb = ball.GetComponent<Rigidbody>();

        // 2. 自分のコートのランダムな目標地点を決める
        // ※ここの数字を自分のコートの座標に合わせて微調整してください
        float randomX = Random.Range(-1.7f, 14f);
        float randomZ = Random.Range(-19.9f, 1.5f);
        Vector3 targetPoint = new Vector3(randomX, 0f, randomZ);

        // 3. 必要な初速を物理計算で出す
        Vector3 startPoint = transform.position;
        float vx = (targetPoint.x - startPoint.x) / flightTime;
        float vz = (targetPoint.z - startPoint.z) / flightTime;
        float gravity = Physics.gravity.y;
        float vy = (targetPoint.y - startPoint.y - 0.5f * gravity * flightTime * flightTime) / flightTime;

        // 4. 速度をセット
        ballRb.linearVelocity = new Vector3(vx, vy, vz);
    }
}