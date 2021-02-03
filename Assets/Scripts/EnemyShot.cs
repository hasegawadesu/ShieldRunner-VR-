using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyShot : MonoBehaviour
{
    

    // 初回射撃時の待ち時間
    private float InstantiationTimer = 0f;
    // ターゲットオブジェクトの Transformコンポーネントを格納する変数
    public Transform target;//
    // オブジェクトの移動速度を格納する変数
    public float moveSpeed;//
    // オブジェクトが停止するターゲットオブジェクトとの距離を格納する変数
    public float stopDistance;//
    // オブジェクトがターゲットに向かって移動を開始する距離を格納する変数
    public float moveDistance;//

    //射出するオブジェクトをinspectorで設定と初期値
    [SerializeField, Tooltip("ThrowingObject")]
    private GameObject ThrowingObject=null;

    //標的のオブジェクトをinspectorで設定と初期値
    [SerializeField, Tooltip("TargetObject")]
    private GameObject TargetObject=null;

    //射出角度をinspectorで設定と初期値
    [SerializeField, Range(0F,90F), Tooltip("ThrowingAngle")]
    private float ThrowingAngle=0;
 
    //スタート時に起動するscript
    private void Start()
    {
    /*
    //射出オブジェクトと鑑賞しないように
     Collider collider = GetComponent<Collider>();
     if(collider != null)
     {
         //干渉しないようにisTriggerをつける 
         collider.isTrigger = true;
     }  
     */ 
    }

     // ゲーム実行中に毎フレーム実行する処理
    void Update()
    {
     //変数 targetPos を作成してターゲットオブジェクトの座標を格納
     Vector3 targetPos = target.position;//☆
     // 自分自身のY座標を変数 target のY座標に格納
     //（ターゲットオブジェクトのX、Z座標のみ参照）
     targetPos.y = transform.position.y;//☆
     // オブジェクトを変数 targetPos の座標方向に向かせる
     transform.LookAt(targetPos);//☆

     // 変数 distance を作成してオブジェクトの位置とターゲットオブジェクトの距離を格納
     float distance = Vector3.Distance(transform.position, target.position);//☆
     // オブジェクトとターゲットオブジェクトの距離判定
     // 変数 distance（ターゲットオブジェクトとオブジェクトの距離）が変数 moveDistance の値より小さければ
     // さらに変数 distance が変数 stopDistance の値よりも大きい場合
      


        if (this.transform.position.z-15 < target.transform.position.z)
        {
            Destroy(gameObject);
        }

        else if (distance < moveDistance && distance > stopDistance)//☆
        {
            ThrowingBall();
        }
    
        //        if(Input.GetMouseButtonDown(0))
        //      {
        //        //マウス左クリックでボールを射出する
         //      ThrowingBall();
        //}
    }
    //<summary>
    //ボールを射出する
    //</summary>
    private void ThrowingBall()
    {
        InstantiationTimer -= Time.deltaTime;
        if (ThrowingObject !=null && TargetObject !=null && InstantiationTimer <0)
        {
            //Ballオブジェクトの生成
            GameObject ball = Instantiate(ThrowingObject, this.transform.position, Quaternion.identity);
            InstantiationTimer =1f;
            
            //標的の座標

            //float x = Random.Range(-2.0f, 2.0f);
            //float y = Random.Range(-2.0f, 2.0f);
           Vector3 targetPosition = TargetObject.transform.position;

            //射出角度
            float angle = ThrowingAngle;

            //射出速度を算出
            Vector3 velocity = CalculateVelocity(this.transform.position, targetPosition, angle);

            //射出
            Rigidbody rid = ball.GetComponent<Rigidbody>();
            rid.AddForce(velocity * rid.mass, ForceMode.Impulse);
        }

        
            }

    //<summary>
    //標的に命中する射出速度の清算
    //</summary>
    //<param name="pointA">射出開始座標</param>
    //<param name="pointB">標的の座標</param>
    //<returns>射出速度</returns>
    private Vector3 CalculateVelocity(Vector3 pointA, Vector3 pointB, float angle)
    {
        //射出角をラジアンに変換
        float rad = angle * Mathf.PI / 180;

        //水平方向の距離ｘ
        float x = Vector2.Distance(new Vector2(pointA.x,pointA.z),new Vector2(pointB.x, pointB.z));
        
        //垂直方向の距離y
        float y = pointA.y - pointB.y;

        //斜方投射のこうしきを初速度について解く
        float speed = Mathf.Sqrt(-Physics.gravity.y * Mathf.Pow(x,2)/(2*Mathf.Pow(Mathf.Cos(rad),2)*(x * Mathf.Tan(rad)+y)));

        if(float.IsNaN(speed))
        {
            //条件を満たす初速を算出できなければVector3.zeroを返す
            return Vector3.zero;
        }
        else
        {
            return (new Vector3(pointB.x - pointA.x, x * Mathf.Tan(rad), pointB.z-pointA.z).normalized*speed);    
        }
    }
}