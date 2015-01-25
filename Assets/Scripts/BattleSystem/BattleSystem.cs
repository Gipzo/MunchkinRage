using UnityEngine;
using System.Collections;

public class BattleSystem : MonoBehaviour {
	
	public bool isBattle;
	public bool isActive;

    public Transform target;

    public RoomInfo room;

	public DoorScript[] Doors;
	public int currentDoor = 0;

    private float distance1, distance2, distance3;
	public Transform level;
	float attackTimeout=0.0f;
	float maxAttackTimeout = 0.5f;


	bool inFight = false;
	bool playerAttack = false;

	EnemyBattle eb = null;
	CharcterBattle cb = null;


    void Start() {

        distance1 = 0;
        distance2 = 0;
        distance3 = 0;

    }

	void StartFight() {
		cb = GetComponent ("CharcterBattle") as CharcterBattle;
		eb = target.gameObject.GetComponent ("EnemyBattle") as EnemyBattle;
		inFight = true;

		/*
Шанс 1 юнита начать первым = ловкость 1 юнита / (ловкость 1 юнита + ловкость 2 юнита), если шанс не сработал, то первым начинает 2 юнит.

		 */
		int enemyAgility = (target.gameObject.GetComponent ("EnemyState") as EnemyState).agility;
		int playerAgility = (GetComponent("CharcterState") as CharcterState).agility;

		if (playerAgility > enemyAgility) {
		 	if (((float)playerAgility/((float)playerAgility+(float)enemyAgility)) < UnityEngine.Random.Range(0f,1f)) {
				playerAttack=true;
			}else{
				playerAttack=false;
			}
		} else {
			
			if (((float)enemyAgility/((float)playerAgility+(float)enemyAgility)) < UnityEngine.Random.Range(0f,1f)) {
				playerAttack=false;
			}else{
				playerAttack=true;
			}
		}



		Debug.Log("Fight!");
	}

	public GameObject GetDamage() {
		GameObject prefab = (level.gameObject.GetComponent("LevelGenerator") as LevelGenerator).damage;
		return Instantiate (prefab) as GameObject;
	}

	void AddText(string text, Vector3 pos, Color color, bool stay=false) {
		GameObject damageObj = GetDamage();
		if (!stay)
			(damageObj.GetComponent ("FloatText") as FloatText).enabled = true;
		damageObj.transform.position = pos ;
		Debug.Log(damageObj.gameObject.transform.Find ("Text").gameObject.name);
		(damageObj.gameObject.GetComponentsInChildren<UnityEngine.UI.Text>()[0] as UnityEngine.UI.Text).text=text;
		(damageObj.gameObject.GetComponentsInChildren<UnityEngine.UI.Text>()[0] as UnityEngine.UI.Text).color=color;
	}

	void Attack() {
		int damage = 0;
		bool crit = false;
		bool bd = false;

		bool evasion = false;
		bool block = false;

		EnemyState es = target.gameObject.GetComponent("EnemyState") as EnemyState;
		CharcterState cs = GetComponent ("CharcterState") as CharcterState;


		if (playerAttack) {
			damage = cb.AttackDMG();
			crit = cb.AttackCrit();
		 	bd = cb.AttackBD();
			if (crit) {
				AddText("Crit!", transform.position, Color.red);
			}

			evasion = false;//es.agility < UnityEngine.Random.Range(0,100);
			block =false;
			if (es.perck == EnemyState.enTypePerk.Block) {
				block = 50<UnityEngine.Random.Range(0,100);
			}
			if (evasion) {
				AddText("evade", target.position, Color.white);
			}else{
				

				if (block && bd) {
					AddText("break!", (transform.position+target.position)/2.0f, Color.white);
					block=false;
				}
				if (block) {
					AddText("block", (transform.position+target.position)/2.0f, Color.white);
					es.curHealth-=damage/2;
					AddText((damage/2).ToString(), target.position, Color.green);
				}else{
					es.curHealth-=damage;
					AddText((damage).ToString(), target.position,Color.green);
				}
				AddText(damage.ToString(), target.position, Color.green);
			}

			if (es.curHealth<0.0f) {

				GameObject.Destroy(target.gameObject);
				target=null;
				inFight=false;
				room.HasEnemy=false;
			}

		}else{
			damage = eb.AttackDMG();
			crit = eb.AttackCrit();
			bd = eb.AttackBD();
			
			AddText(damage.ToString(), transform.position, Color.red);
			
			if (crit) {
				AddText("Crit!", target.position, Color.red);
			}

			evasion = cs.agility < UnityEngine.Random.Range(0,100);
			block = cs.blockChance < UnityEngine.Random.Range(0,100);
			
			if (evasion) {
				AddText("miss", transform.position, Color.white);
			}else{
				if (block && bd) {
					AddText("break!", (transform.position+target.position)/2.0f, Color.white);
					block=false;
				}
				if (block) {
					AddText("block", (transform.position+target.position)/2.0f, Color.red);
					cs.curHealth-=damage/2;
					AddText((damage/2).ToString(), transform.position, Color.red);
				}else{
					cs.curHealth-=damage;
					AddText((damage).ToString(), transform.position, Color.red);
				}
				AddText(damage.ToString(), target.position, Color.red);
			}
			
			if (cs.curHealth<0.0f) {

				GameObject.Find("dead").transform.position = new Vector3(
					transform.position.x,
					transform.position.y,
					-10.0f
					);
				(GameObject.Find("dead").GetComponent("ToMenuScript") as ToMenuScript).act=true;
				isActive=false;
				target=null;
				inFight=false;
			}

		}
		string attacker = "Player";
		if (!playerAttack) attacker = "Enemy";
		Debug.Log(string.Format("{0} attacks for {1} crit:{2} destroyblock:{3}",attacker, damage.ToString(), crit.ToString(), bd.ToString()));
		playerAttack = ! playerAttack;
	}

    void Update() {
		if (attackTimeout > 0.0f) {
			attackTimeout-=Time.deltaTime;
		}

		if (!isActive)
			return;
		
		SelectNearestTarget ();


			if (room.HasEnemy) {
				if (Vector2.Distance (gameObject.transform.position, target.transform.position) > 3.2f) {
					GoToTarget ();
				}else{
					if (attackTimeout<=0.1f) {
						attackTimeout=maxAttackTimeout;
						if (!inFight) {
							StartFight();
						}else{
						
							Attack ();
						}
					}
				}
				return;
			}

			if (room.HasChest) {
				if (Vector2.Distance (gameObject.transform.position, target.transform.position) > 3.2f) {
					GoToTarget ();
				}else{
					
					room.HasChest=false;
					GameObject.Destroy(target.gameObject);
					target=null;
				}
				return;
			}

		if (currentDoor<Doors.Length) {
			target = Doors[currentDoor].transform;
		}else{
			target = GameObject.Find("Exit").transform;
		}

		if (Vector3.Distance (gameObject.transform.position, 
		                      new Vector3(
			target.transform.position.x,
			target.transform.position.y,
			transform.position.z
			)) > 0.2f) {
			GoToTarget();
		}else{
			if (currentDoor>=Doors.Length) {

				isActive = false;

				Debug.Log("EndLevel");

				(GameObject.Find ("Level").GetComponent("LevelGenerator") as LevelGenerator).NextLevel();


			}else{
				RoomInfo room1 = (Doors[currentDoor].Room1.gameObject.GetComponent("RoomInfo")as RoomInfo);
				RoomInfo room2 = (Doors[currentDoor].Room2.gameObject.GetComponent("RoomInfo")as RoomInfo);
				RoomInfo targetRoom = room1;
				if (room1.Open) {
					targetRoom=room2;
				}
				targetRoom.OpenRoom(this.gameObject);
				room=targetRoom;
				if (targetRoom.HasEnemy || targetRoom.HasChest) {
					isBattle=true;
					target=null;
				}
				currentDoor++;
				
			}
			//isBattle=true;
		}
    
    }

    void SelectNearestTarget() {
		if (room.HasEnemy) {
			target = room.transformEnemy[0].transform;
			return;
		}
		if (room.HasChest) {
			target = room.gameObject.transform.Find("Chest").transform;
			return;
		}
		if (currentDoor < Doors.Length) {
			target = Doors [currentDoor].transform;
		} else {
			target = GameObject.Find("Exit").transform;
			(gameObject.GetComponent("CharcterState") as CharcterState).curHealth=222;
		}
    }



    void GoToTarget() {

        transform.position = Vector3.MoveTowards(transform.position, 
		                                  new Vector3(
			target.transform.position.x,target.transform.position.y,transform.position.z), 15f*Time.deltaTime);
    }

}
