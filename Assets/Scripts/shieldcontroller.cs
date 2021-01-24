using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class shieldcontroller : MonoBehaviour
{
        //[SerializeField]を書くことによりpublicでなくてもInspectorから値を編集できます
    [SerializeField] 
    public const float Maxhp = 1000f; 
    public const float Minhp = 0;  //体力
    public float currentHp;
    public Slider slider;

    	void Start () {
		slider.maxValue = Maxhp;	// Sliderの最大値を敵キャラのHP最大値と合わせる
		currentHp = Maxhp;		// 初期状態はHP満タン
		slider.value = currentHp;	// Sliderの初期状態を設定（HP満タン）
	}

    

    //貫通する場合はTrigger系(どちらかにColliderのis triggerをチェック) 衝突しあうものはCollision系(ColliderとRigidbodyが必要)
    public void OnCollisionEnter(Collision collision)
    {

        //タグがEnemyBulletのオブジェクトが当たった時に{}内の処理が行われる
        if (collision.gameObject.tag == "EnemyBullet")    
        {
            //gameObject.GetComponent<EnemyBulletManager>()でEnemyBulletManagerスクリプトを参照し
            //.powerEnemy; でEnemyBulletManagerのpowerEnemyの値をゲット
            currentHp -= collision.gameObject.GetComponent<EnemyBulletManager>().powerEnemy;    
            Debug.Log("hit Player");  //コンソールにhit Playerが表示
            Debug.Log(currentHp);  //コンソールにhit Playerが表示
        }

        if (collision.gameObject.tag == "heal")    
        {
            //コンソールにhit Playerが表示
            //gameObject.GetComponent<EnemyBulletManager>()でEnemyBulletManagerスクリプトを参照し
            //.powerEnemy; でEnemyBulletManagerのpowerEnemyの値をゲット
            currentHp += collision.gameObject.GetComponent<ShieldHealManager>().heal;    
            Debug.Log("回復");
            Debug.Log(currentHp);
            
        }

                //体力が0以下になった時{}内の処理が行われる
        if (currentHp > Maxhp)     
       {
         currentHp = Maxhp;    
        Debug.Log(currentHp);
       }

        //体力が0以下になった時{}内の処理が行われる
        else if (currentHp <= 0)     
        {
            slider.value = currentHp; // Sliderに現在HPを適用
            Destroy(gameObject);  //ゲームオブジェクトが破壊される
            Debug.Log("GameOver");
        }
    }


    // Update is called once per frame
    void Update()
    {
                this.transform.position += new Vector3(0f, 0f, 0.02f);

        slider.value = currentHp; // Sliderに現在HPを適用
        //左移動
        if(Input.GetKey(KeyCode.A)){
            this.transform.Translate(-0.05f,0f,0f);
        }

        //上移動
        if(Input.GetKey(KeyCode.W)){
            this.transform.Translate(0f,0.05f,0f);
        }
        //右移動
        if(Input.GetKey(KeyCode.D)){
            this.transform.Translate(0.05f,0f,0f);
        }

        //下に移動
        if(Input.GetKey(KeyCode.S)){
            this.transform.Translate(0f,-0.05f,0f);
        }
    }
}
