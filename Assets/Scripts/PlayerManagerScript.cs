using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManagerScript : MonoBehaviour {
    public Animator PlayerAnimator;
    public GameObject Player;
    public float PlayerMoveSpeed = 1.0f;
    public float PlayerRotateSpeed = 30f;
    public GameObject GunBarrelEnd;
    public LineRenderer lineRenderer;
    public float FireInterval = 0.2f;    // 两次开火的最小间隔（秒）
    public float LaserShowTime = 0.08f;  // 激光线的显示时长（秒）

    private float fireTimer = 0f;   // 距离下次可开火的时间
    private float laserTimer = 0f;  // 激光线剩余显示时间

    void Start() {
        // 初始隐藏激光线，只有开火时才短暂显示
        if (lineRenderer != null) lineRenderer.enabled = false;
    }

    // Update is called once per frame
    void Update() {
        float v_value = Input.GetAxis("Vertical");
        float h_value = Input.GetAxis("Horizontal");
        PlayerAnimator.SetFloat("PlayerMove", v_value);
        if (v_value >0.1||v_value<-0.1) {
            Player.transform.Translate(new Vector3(0,0,1)* v_value*PlayerMoveSpeed *Time.deltaTime,Space.Self);
        }
        if (h_value != 0) {
            Player.transform.Rotate(new Vector3(0,1,0)*h_value*PlayerRotateSpeed*Time.deltaTime,Space.Self);
        }

        fireTimer += Time.deltaTime;
        // 按下开火键且冷却结束时才射击一次，避免每帧重复扣血
        if (Input.GetButtonDown("Fire1") && fireTimer >= FireInterval) {
            Fire();
            fireTimer = 0f;
        }

        // 激光线到时自动隐藏
        if (lineRenderer != null && lineRenderer.enabled) {
            laserTimer -= Time.deltaTime;
            if (laserTimer <= 0f) lineRenderer.enabled = false;
        }
    }

    void Fire() {
        Ray ray = new Ray(GunBarrelEnd.transform.position, GunBarrelEnd.transform.forward);
        RaycastHit hitInfo;
        Vector3[] ps = new Vector3[2];
        ps[0] = Vector3.zero;
        ps[1] = Vector3.zero;
        if (Physics.Raycast(ray,out hitInfo)){
            // 世界坐标命中点转换成枪头局部坐标，供 LineRenderer 使用
            ps[1] = GunBarrelEnd.transform.InverseTransformPoint(hitInfo.point);
            // 直接判断命中物是否是怪物，不再依赖 tag（生成的预制体没有 monster 标签）
            MonsterScript ms = hitInfo.collider.GetComponent<MonsterScript>();
            if (ms != null) {
                ms.beAttach();
            }
        }
        lineRenderer.SetPositions(ps);
        lineRenderer.enabled = true;
        laserTimer = LaserShowTime;
    }
}
