using System;
using System.Collections;
using System.Security.Cryptography;
using CodeStage.AntiCheat.Storage;
using ExitGames.Client.Photon;
using Il2CppSystem;
using MelonLoader;
using UnhollowerBaseLib;
using UnityEngine;
using UnityEngine.AI;
using Harmony;

namespace NEC_Console
{
	public sealed class NecConsole : MelonMod
	{
		private Transform PT
		{
			get
			{
				return UnityEngine.Object.FindObjectOfType<WeaponPickUp>().transform.root.gameObject.transform;
			}
		}

		private Transform FPS
		{
			get
			{
				return UnityEngine.Object.FindObjectOfType<FPScontroller>().transform.root.gameObject.transform;
			}
		}

		private static Bot[] Bots
		{
			get
			{
				Bot[] result;
				if (PhotonNetwork.room != null)
				{
					result = UnityEngine.Object.FindObjectsOfType<Bot>();
				}
				else
				{
					result = null;
				}
				return result;
			}
		}

		private static BossBot[] Boss
		{
			get
			{
				BossBot[] result;
				if (PhotonNetwork.room != null)
				{
					result = UnityEngine.Object.FindObjectsOfType<BossBot>();
				}
				else
				{
					result = null;
				}
				return result;
			}
		}

		private static CustardBot[] Cbot
		{
			get
			{
				CustardBot[] result;
				if (PhotonNetwork.room != null)
				{
					result = UnityEngine.Object.FindObjectsOfType<CustardBot>();
				}
				else
				{
					result = null;
				}
				return result;
			}
		}

		private static Bullet[] bt
		{
			get
			{
				Bullet[] result;
				if (PhotonNetwork.room != null)
				{
					result = UnityEngine.Object.FindObjectsOfType<Bullet>();
				}
				else
				{
					result = null;
				}
				return result;
			}
		}

		public override void OnUpdate()
		{
			if (Input.GetKeyDown(KeyCode.F3))
			{
				this.Use = !this.Use;
			}
			UnityEngine.Object.FindObjectOfType<UpdaterV2>().AGOIMDFCHAN = true;
			if (PhotonNetwork.room != null)
			{
				if (this.n)
				{
					string text = NecConsole.RandomCode(10, true, true, true);
					ObscuredPrefs.SetString("ZWName01", text);
					PhotonNetwork.player.NickName = text;
					PhotonNetwork.networkingPeer.PlayerName = text;
				}
				if (this.dall)
				{
					foreach (PhotonPlayer photonPlayer in PhotonNetwork.playerList)
					{
						if (photonPlayer.ID != PhotonNetwork.player.ID)
						{
							this.SetHPforInternet(float.MaxValue, photonPlayer);
						}
					}
				}
				if (this.KIck)
				{
					string name = PhotonNetwork.player.name;
					string text2 = NecConsole.RandomCode(100, true, true, true);
					string name2 = NecConsole.RandomCode(100, true, true, true);
					PhotonNetwork.player.name = name2;
					GameObject gameObject = GameObject.Find("__Room");
					if (gameObject)
					{
						PhotonView component = gameObject.GetComponent<PhotonView>();
						Il2CppSystem.Object[] arr = new Il2CppSystem.Object[]
						{
							text2,
							"Team B"
						};
						component.RPC("MBMLNBNADID", PhotonTargets.All, arr);
					}
					PhotonNetwork.player.name = name;
					string log = NecConsole.RandomCode(100, true, true, true);
					string what = NecConsole.RandomCode(100, true, true, true);
					this.SendLog(log, what);
				}
				if (this.Hp)
				{
					foreach (PhotonPlayer photonPlayer2 in PhotonNetwork.networkingPeer.mPlayerListCopy)
					{
						Il2CppSystem.Single single = default(Il2CppSystem.Single);
						single.m_value = -1f;
						Il2CppReferenceArray<Il2CppSystem.Object> parameters = new Il2CppReferenceArray<Il2CppSystem.Object>(new Il2CppSystem.Object[]
						{
							single.BoxIl2CppObject(),
							photonPlayer2
						});
						GameObject gameObject2 = GameObject.Find(photonPlayer2.NickName);
						if (gameObject2)
						{
							gameObject2.GetComponent<PhotonView>().RPC("LLPGPMIJOBA", PhotonTargets.All, parameters);
						}
					}
				}
				if (this.fx)
				{
					string value = NecConsole.RandomCode(2, true, false, false);
					this.ss = System.Convert.ToSingle(value);
					this.SetPlayerHP(this.ss);
				}
				if (Input.GetKeyDown(KeyCode.B))
				{
					UnityEngine.Object.SetName(UnityEngine.Object.FindObjectOfType<RoomMultiplayerMenu>().CBKEAFJFMAH, "NetworkPlayer2");
					UnityEngine.Object.FindObjectOfType<RoomMultiplayerMenu>().RespawnPlayer2();
				}
				if (this.FPS.gameObject != null)
				{
					if (Input.GetKeyDown(KeyCode.Q))
					{
						this.Clip = !this.Clip;
					}
					if (this.Clip)
					{
						this.FPS.GetComponent<CharacterController>().enabled = false;
						if (Input.GetKey(KeyCode.W))
						{
							this.FPS.position += Camera.main.transform.forward * this.speed * Time.deltaTime;
						}
						if (Input.GetKey(KeyCode.S))
						{
							this.FPS.position += Camera.main.transform.forward * -this.speed * Time.deltaTime;
						}
						if (Input.GetKey(KeyCode.Space))
						{
							this.FPS.position += Camera.main.transform.up * this.speed * Time.deltaTime;
						}
						if (Input.GetKey(KeyCode.LeftControl))
						{
							this.FPS.position += Camera.main.transform.up * -this.speed * Time.deltaTime;
						}
						if (Input.GetKey(KeyCode.D))
						{
							this.FPS.position += Camera.main.transform.right * this.speed * Time.deltaTime;
						}
						if (Input.GetKey(KeyCode.A))
						{
							this.FPS.position += Camera.main.transform.right * -this.speed * Time.deltaTime;
						}
					}
					else
					{
						this.FPS.GetComponent<CharacterController>().enabled = true;
					}
				}
				if (Input.GetKeyDown(KeyCode.Y))
				{
					this.god = !this.god;
				}
				if (this.god)
				{
					this.pro += Time.deltaTime * this.spe;
					if (this.pro >= 360f)
					{
						this.pro -= 360f;
					}
					float x = this.PT.position.x + this.a * Mathf.Cos(this.pro);
					float y = this.PT.position.y + this.a * Mathf.Sin(this.pro);
					float z = this.PT.position.z + this.a * Mathf.Cos(this.pro);
					GameObject gameObject3 = PhotonNetwork.NOOU("SUR/BossShadow", new Vector3(x, y, z), default(Quaternion), 0);
					gameObject3.GetComponent<PhotonView>().RPC("PCOJDBFOIGH", PhotonTargets.All, new Il2CppSystem.Object[1]);
					PhotonNetwork.Destroy(gameObject3);
					foreach (PhotonPlayer photonPlayer3 in PhotonNetwork.otherPlayers)
					{
						GameObject.Find(photonPlayer3.name);
						if (Vector3.Distance(gameObject3.transform.position, this.PT.position) <= this.pro)
						{
							this.SetHPforInternet(float.MaxValue, photonPlayer3);
						}
						this.SetHPforInternet(0f, photonPlayer3);
					}
				}
				if (this.PT != null)
				{
					if (Input.GetKey(KeyCode.C))
					{
						if (this.PT.gameObject)
						{
							this.ts = true;
							this.p();
						}
					}
					else
					{
						this.ts = false;
						this.p();
					}
				}
				if (this.wp)
				{
					WeaponManager weaponManager = UnityEngine.Object.FindObjectOfType<WeaponManager>();
					if (weaponManager != null)
					{
						for (int i = 0; i < weaponManager.ABEDKBPCLPH.Count; i++)
						{
							if (weaponManager.ABEDKBPCLPH[i].LOFNJKEFKEB != null)
							{
								weaponManager.ABEDKBPCLPH[i].LOFNJKEFKEB.bulletsLeft = int.MaxValue;
								weaponManager.ABEDKBPCLPH[i].LOFNJKEFKEB.patchedClips = int.MaxValue;
							}
							if (weaponManager.ABEDKBPCLPH[i].LFGOANBMKKA != null)
							{
								weaponManager.ABEDKBPCLPH[i].LFGOANBMKKA.bulletsLeft = int.MaxValue;
								weaponManager.ABEDKBPCLPH[i].LFGOANBMKKA.patchedClips = int.MaxValue;
							}
							if (weaponManager.ABEDKBPCLPH[i].MAJALNJMGLF != null)
							{
								weaponManager.ABEDKBPCLPH[i].MAJALNJMGLF.ammoCount = int.MaxValue;
							}
						}
					}
				}
				if (this.lf)
				{
					WeaponManager weaponManager2 = UnityEngine.Object.FindObjectOfType<WeaponManager>();
					if (weaponManager2 != null)
					{
						for (int j = 0; j < weaponManager2.ABEDKBPCLPH.Count; j++)
						{
							weaponManager2.ABEDKBPCLPH[j].GILMKIPFFLM = false;
						}
					}
				}
				else
				{
					WeaponManager weaponManager3 = UnityEngine.Object.FindObjectOfType<WeaponManager>();
					if (weaponManager3 != null)
					{
						for (int k = 0; k < weaponManager3.ABEDKBPCLPH.Count; k++)
						{
							weaponManager3.ABEDKBPCLPH[k].GILMKIPFFLM = true;
						}
					}
				}
				if (this.ms)
				{
					foreach (Bullet bullet in NecConsole.bt)
					{
						if (bullet != null)
						{
							bullet.GOAMBJOIJKP = int.MaxValue;
						}
					}
				}
				if (this.pb)
				{
					foreach (Bullet bullet2 in NecConsole.bt)
					{
						if (bullet2 != null)
						{
							bullet2.MGKBIMCILBK = false;
						}
					}
				}
				if (this.gss)
				{
					WeaponManager weaponManager4 = UnityEngine.Object.FindObjectOfType<WeaponManager>();
					if (weaponManager4 != null)
					{
						for (int l = 0; l < weaponManager4.ABEDKBPCLPH.Count; l++)
						{
							if (weaponManager4.ABEDKBPCLPH[l].LOFNJKEFKEB != null)
							{
								weaponManager4.ABEDKBPCLPH[l].LOFNJKEFKEB.fireRate = 0.009f;
								weaponManager4.ABEDKBPCLPH[l].LFGOANBMKKA.fireRate = 0.009f;
							}
							weaponManager4.ABEDKBPCLPH[l].HMHINNFMJBF.recoilPower = 0f;
							if (weaponManager4.ABEDKBPCLPH[l].MAJALNJMGLF != null)
							{
								weaponManager4.ABEDKBPCLPH[l].MAJALNJMGLF.initialSpeed = 500;
								weaponManager4.ABEDKBPCLPH[l].MAJALNJMGLF.waitBeforeReload = 0f;
								weaponManager4.ABEDKBPCLPH[l].MAJALNJMGLF.reloadTime = 0.1f;
								weaponManager4.ABEDKBPCLPH[l].MAJALNJMGLF.shotDelay = 0f;
							}
						}
					}
				}
				else
				{
					WeaponManager weaponManager5 = UnityEngine.Object.FindObjectOfType<WeaponManager>();
					if (weaponManager5 != null)
					{
						for (int num = 0; num < weaponManager5.ABEDKBPCLPH.Count; num++)
						{
							if (weaponManager5.ABEDKBPCLPH[num].LOFNJKEFKEB != null)
							{
								weaponManager5.ABEDKBPCLPH[num].LOFNJKEFKEB.fireRate = 0.1f;
								weaponManager5.ABEDKBPCLPH[num].LFGOANBMKKA.fireRate = 0.1f;
							}
							weaponManager5.ABEDKBPCLPH[num].HMHINNFMJBF.recoilPower = 0f;
							if (weaponManager5.ABEDKBPCLPH[num].MAJALNJMGLF != null)
							{
								weaponManager5.ABEDKBPCLPH[num].MAJALNJMGLF.initialSpeed = 20;
								weaponManager5.ABEDKBPCLPH[num].MAJALNJMGLF.reloadTime = 0.5f;
								weaponManager5.ABEDKBPCLPH[num].MAJALNJMGLF.waitBeforeReload = 0.5f;
								weaponManager5.ABEDKBPCLPH[num].MAJALNJMGLF.shotDelay = 0f;
							}
						}
					}
				}
				if (this.sl)
				{
					UnityEngine.Object.FindObjectOfType<FPScontroller>().CDJMJCNGEMN.fallDamageMultiplier = 0f;
				}
				if (this.nc)
				{
					MelonCoroutines.Start(this.ForC());
				}
				if (UnityEngine.Object.FindObjectOfType<Volume>().gameObject != null)
				{
					if (UnityEngine.Object.FindObjectOfType<Volume>().LGNAJPCNIKE[2].options._size != 38)
					{
						Volume.option option = new Volume.option();
						option.optionName = "蓝色工人";
						option.image = null;
						option.resourcePath = "sur/worker_bot";
						UnityEngine.Object.FindObjectOfType<Volume>().LGNAJPCNIKE[2].options.Add(option);
						Volume.option option2 = new Volume.option();
						option2.optionName = "士兵";
						option2.image = null;
						option2.resourcePath = "sur/soldier_ranged_bot";
						UnityEngine.Object.FindObjectOfType<Volume>().LGNAJPCNIKE[2].options.Add(option2);
					}
					if (UnityEngine.Object.FindObjectOfType<Volume>().LGNAJPCNIKE[3].options._size != 38)
					{
						Volume.option option3 = new Volume.option();
						option3.optionName = "蓝色工人";
						option3.image = null;
						option3.resourcePath = "sur/worker_bot";
						UnityEngine.Object.FindObjectOfType<Volume>().LGNAJPCNIKE[3].options.Add(option3);
						Volume.option option4 = new Volume.option();
						option4.optionName = "士兵";
						option4.image = null;
						option4.resourcePath = "sur/soldier_ranged_bot";
						UnityEngine.Object.FindObjectOfType<Volume>().LGNAJPCNIKE[3].options.Add(option4);
					}
				}
			}
		}

		public override void OnGUI()
		{
			if (this.Use)
			{
				Cursor.visible = true;
				Cursor.lockState = CursorLockMode.None;
				this.MainMenu();
			}
			if (this.msa)
			{
				Cursor.visible = true;
				Cursor.lockState = CursorLockMode.None;
				this.Use = false;
				this.Monster();
			}
			if (this.sssa)
			{
				Cursor.visible = true;
				Cursor.lockState = CursorLockMode.None;
				this.Use = false;
				this.saas = true;
				this.RoomMenu();
			}
			if (this.so)
			{
				Cursor.visible = true;
				Cursor.lockState = CursorLockMode.None;
				this.Use = false;
				this.sopp = true;
				this.NameMenu();
			}
			if (this.fork)
			{
				Cursor.visible = true;
				Cursor.lockState = CursorLockMode.None;
				this.Use = false;
				this.pk = true;
				this.LIstP();
			}
			if (this.MM)
			{
				Cursor.visible = true;
				Cursor.lockState = CursorLockMode.None;
				this.Use = false;
				this.MapMenu();
			}
			if (this.mini)
			{
				Cursor.visible = true;
				Cursor.lockState = CursorLockMode.None;
				this.Use = false;
				this.mini2 = true;
				this.WeanponMenu();
			}
			if (this.k)
			{
				Cursor.visible = true;
				Cursor.lockState = CursorLockMode.None;
				this.Use = false;
				this.PlayerMenu();
			}
		}

		public void MainMenu()
		{
			if (this.Use)
			{
				GUI.Box(new Rect(10f, 10f, 200f, 740f), "<color=red>Nec</color>控制台");
				GUI.color = Color.grey;
				if (GUI.Button(new Rect(10f, 30f, 200f, 20f), "<color=grey>刷新房间属性</color>"))
				{
					UnityEngine.Object.FindObjectOfType<RoomMultiplayerMenu>().Start();
				}
				if (GUI.Button(new Rect(10f, 50f, 200f, 20f), "<color=orange>放怪菜单</color>"))
				{
					this.msa = !this.msa;
				}
				if (GUI.Button(new Rect(10f, 70f, 200f, 20f), "<color=white>房间菜单</color>"))
				{
					this.sssa = true;
				}
				if (GUI.Button(new Rect(10f, 90f, 200f, 20f), "<color=yellow>名字菜单</color>"))
				{
					this.so = true;
				}
				if (GUI.Button(new Rect(10f, 110f, 200f, 20f), "<color=red>针对菜单</color>"))
				{
					this.fork = true;
				}
				if (GUI.Button(new Rect(10f, 130f, 200f, 20f), "<color=green>武器菜单</color>"))
				{
					this.mini = true;
				}
				if (GUI.Button(new Rect(10f, 150f, 200f, 20f), "<color=red>玩家菜单</color>"))
				{
					this.k = !this.k;
				}
				if (GUI.Button(new Rect(10f, 170f, 200f, 20f), "<color=yellow>地图菜单</color>"))
				{
					this.MM = !this.MM;
				}
				GUI.Label(new Rect(10f, 190f, 50f, 20f), "飞行速度" + this.speed.ToString(), "s");
				this.speed = GUI.HorizontalSlider(new Rect(10f, 210f, 200f, 20f), this.speed, 0f, 125f);
				if (GUI.Button(new Rect(10f, 230f, 200f, 25f), "<color=red>摧毁一切</color>"))
				{
					PhotonNetwork.networkingPeer.DestroyAll(false);
				}
				if (GUI.Button(new Rect(10f, 255f, 200f, 25f), "<color=red>摧毁所有实体</color>"))
				{
					foreach (PhotonPlayer photonPlayer in PhotonNetwork.playerList)
					{
						PhotonNetwork.networkingPeer.DestroyPlayerObjects(photonPlayer.ID, false);
						PhotonNetwork.networkingPeer.DestroyPlayerObjects(0, false);
					}
				}
				if (GUI.Button(new Rect(10f, 280f, 200f, 25f), "<color=red>摧毁摔落伤害</color>" + this.sl.ToString()))
				{
					this.sl = !this.sl;
				}
			}
		}

		public void SetPlayerHP(float HP)
		{
			PlayerDamage component = this.PT.gameObject.GetComponent<PlayerDamage>();
			if (component)
			{
				component.JPCFPICJKFI = HP;
				component.Awake();
			}
		}

		public void Monster()
		{
			GUI.Box(new Rect(10f, 10f, 400f, 740f), "<color=orange>放怪菜单</color>");
			GUI.color = Color.grey;
			if (GUI.Button(new Rect(10f, 30f, 200f, 20f), "<color=orange>搜集播音员</color>"))
			{
				GameObject ad = PhotonNetwork.NOOU("coop/announcer", this.PT.position + this.PT.forward * 7f, new Quaternion(180f, 0f, 0f, 0f), 0);
				this.DSB(ad);
			}
			if (GUI.Button(new Rect(10f, 50f, 200f, 20f), "<color=orange>搜集新生儿</color>"))
			{
				GameObject ad2 = PhotonNetwork.NOOU("coop/newborn", this.PT.position + this.PT.forward * 7f, new Quaternion(180f, 0f, 0f, 0f), 0);
				this.DSB(ad2);
			}
			if (GUI.Button(new Rect(10f, 70f, 200f, 20f), "<color=orange>搜集一代丁</color>"))
			{
				GameObject ad3 = PhotonNetwork.NOOU("coop/tinkywinkyold", this.PT.position + this.PT.forward * 7f, new Quaternion(180f, 0f, 0f, 0f), 0);
				this.DSB(ad3);
			}
			if (GUI.Button(new Rect(10f, 90f, 200f, 20f), "<color=orange>搜集丁</color>"))
			{
				GameObject ad4 = PhotonNetwork.NOOU("coop/tinkywinky", this.PT.position + this.PT.forward * 7f, new Quaternion(180f, 0f, 0f, 0f), 0);
				this.DSB(ad4);
			}
			if (GUI.Button(new Rect(10f, 110f, 200f, 20f), "<color=orange>搜集二阶段拉拉</color>"))
			{
				GameObject ad5 = PhotonNetwork.NOOU("coop/laalaa", this.PT.position + this.PT.forward * 7f, new Quaternion(180f, 0f, 0f, 0f), 0);
				this.DSB(ad5);
			}
			if (GUI.Button(new Rect(10f, 130f, 200f, 20f), "<color=orange>搜集二阶段波</color>"))
			{
				GameObject ad6 = PhotonNetwork.NOOU("coop/po", this.PT.position + this.PT.forward * 7f, new Quaternion(180f, 0f, 0f, 0f), 0);
				this.DSB(ad6);
			}
			if (GUI.Button(new Rect(10f, 150f, 200f, 20f), "<color=orange>搜集鱼迪</color>"))
			{
				GameObject ad7 = PhotonNetwork.NOOU("coop/dipsy", this.PT.position + this.PT.forward * 7f, new Quaternion(180f, 0f, 0f, 0f), 0);
				this.DSB(ad7);
			}
			if (GUI.Button(new Rect(10f, 170f, 200f, 20f), "<color=orange>搜集守护者</color>"))
			{
				GameObject ad8 = PhotonNetwork.NOOU("coop/whitetubby", this.PT.position + this.PT.forward * 7f, new Quaternion(180f, 0f, 0f, 0f), 0);
				this.DSB(ad8);
			}
			if (GUI.Button(new Rect(10f, 190f, 200f, 20f), "<color=orange>搜集冒牌货</color>"))
			{
				GameObject ad9 = PhotonNetwork.NOOU("coop/imposter", this.PT.position + this.PT.forward * 7f, new Quaternion(180f, 0f, 0f, 0f), 0);
				this.DSB(ad9);
			}
			if (GUI.Button(new Rect(10f, 210f, 200f, 20f), "<color=orange>奶昔</color>"))
			{
				GameObject ad10 = PhotonNetwork.NOOU("custard", this.PT.position + this.PT.forward * 7f, new Quaternion(180f, 0f, 0f, 0f), 0);
				this.DSB(ad10);
			}
			if (GUI.Button(new Rect(10f, 230f, 200f, 20f), "<color=orange>土司</color>"))
			{
				GameObject ad11 = PhotonNetwork.NOOU("toast", this.PT.position + this.PT.forward * 7f, new Quaternion(180f, 0f, 0f, 0f), 0);
				this.DSB(ad11);
			}
			if (GUI.Button(new Rect(10f, 250f, 200f, 20f), "<color=orange>蝴蝶实体</color>"))
			{
				GameObject gameObject = PhotonNetwork.NOOU("flycambutterfly", this.PT.position + this.PT.forward * 7f, new Quaternion(180f, 0f, 0f, 0f), 0);
				UnityEngine.Object.Destroy(gameObject.GetComponent<FPSMouseLook>());
				UnityEngine.Object.Destroy(gameObject.GetComponent<Camera>());
				this.DSB(gameObject);
			}
			if (GUI.Button(new Rect(10f, 270f, 200f, 20f), "<color=orange>摄像头实体</color>"))
			{
				GameObject gameObject2 = PhotonNetwork.NOOU("flycam", this.PT.position + this.PT.forward * 7f, new Quaternion(180f, 0f, 0f, 0f), 0);
				UnityEngine.Object.Destroy(gameObject2.GetComponent<FPSMouseLook>());
				UnityEngine.Object.Destroy(gameObject2.GetComponent<Camera>());
				this.DSB(gameObject2);
			}
			if (GUI.Button(new Rect(10f, 290f, 200f, 20f), "<color=orange>玩家实体</color>"))
			{
				GameObject gameObject3 = PhotonNetwork.NOOU("networkplayer2", this.PT.position + this.PT.forward * 7f, new Quaternion(0f, 0f, 0f, 0f), 0);
				UnityEngine.Object.Destroy(gameObject3.GetComponent<FPSMouseLook>());
				UnityEngine.Object.Destroy(gameObject3.GetComponent<FPSinput>());
				UnityEngine.Object.Destroy(gameObject3.GetComponent<FPSSoundController>());
				UnityEngine.Object.Destroy(gameObject3.GetComponent<WeaponPickUp>());
				gameObject3.transform.FindChild("LookObject").gameObject.active = false;
				gameObject3.transform.FindChild("PLAYER_MODEL").gameObject.active = true;
				gameObject3.transform.FindChild("HUD").gameObject.active = false;
				this.DSB(gameObject3);
			}
			if (GUI.Button(new Rect(210f, 30f, 200f, 20f), "<color=orange>生存播音员</color>"))
			{
				GameObject ad12 = PhotonNetwork.NOOU("sur/bossannouncer", this.PT.position + this.PT.forward * 7f, new Quaternion(180f, 0f, 0f, 0f), 0);
				this.DSB(ad12);
			}
			if (GUI.Button(new Rect(210f, 50f, 200f, 20f), "<color=orange>生存新生儿</color>"))
			{
				GameObject ad13 = PhotonNetwork.NOOU("sur/newborn_bot", this.PT.position + this.PT.forward * 7f, new Quaternion(180f, 0f, 0f, 0f), 0);
				this.DSB(ad13);
			}
			if (GUI.Button(new Rect(210f, 70f, 200f, 20f), "<color=orange>生存蓝色工人</color>"))
			{
				GameObject ad14 = PhotonNetwork.NOOU("sur/worker_bot", this.PT.position + this.PT.forward * 7f, new Quaternion(180f, 0f, 0f, 0f), 0);
				this.DSB(ad14);
			}
			if (GUI.Button(new Rect(210f, 90f, 200f, 20f), "<color=orange>生存丁</color>"))
			{
				GameObject ad15 = PhotonNetwork.NOOU("sur/bosstinky", this.PT.position + this.PT.forward * 7f, new Quaternion(180f, 0f, 0f, 0f), 0);
				this.DSB(ad15);
			}
			if (GUI.Button(new Rect(210f, 110f, 200f, 20f), "<color=orange>生存二阶段拉拉</color>"))
			{
				GameObject ad16 = PhotonNetwork.NOOU("sur/bosslaalaa", this.PT.position + this.PT.forward * 7f, new Quaternion(180f, 0f, 0f, 0f), 0);
				this.DSB(ad16);
			}
			if (GUI.Button(new Rect(210f, 130f, 200f, 20f), "<color=orange>生存二阶段波</color>"))
			{
				GameObject ad17 = PhotonNetwork.NOOU("sur/bosspo", this.PT.position + this.PT.forward * 7f, new Quaternion(180f, 0f, 0f, 0f), 0);
				this.DSB(ad17);
			}
			if (GUI.Button(new Rect(210f, 150f, 200f, 20f), "<color=orange>生存鱼迪</color>"))
			{
				GameObject ad18 = PhotonNetwork.NOOU("sur/bossdipsy", this.PT.position + this.PT.forward * 7f, new Quaternion(180f, 0f, 0f, 0f), 0);
				this.DSB(ad18);
			}
			if (GUI.Button(new Rect(210f, 170f, 200f, 20f), "<color=orange>生存守护者</color>"))
			{
				GameObject ad19 = PhotonNetwork.NOOU("sur/BossWhiteTubby", this.PT.position + this.PT.forward * 7f, new Quaternion(180f, 0f, 0f, 0f), 0);
				this.DSB(ad19);
			}
			if (GUI.Button(new Rect(210f, 190f, 200f, 20f), "<color=orange>生存冒牌货</color>"))
			{
				GameObject ad20 = PhotonNetwork.NOOU("sur/bossimposter", this.PT.position + this.PT.forward * 7f, new Quaternion(180f, 0f, 0f, 0f), 0);
				this.DSB(ad20);
			}
			if (GUI.Button(new Rect(210f, 210f, 200f, 20f), "<color=orange>生存士兵</color>"))
			{
				GameObject ad21 = PhotonNetwork.NOOU("sur/soldier_ranged_bot", this.PT.position + this.PT.forward * 7f, new Quaternion(180f, 0f, 0f, 0f), 0);
				this.DSB(ad21);
			}
			if (GUI.Button(new Rect(210f, 230f, 200f, 20f), "<color=orange>旧版玩家实体</color>"))
			{
				PhotonNetwork.room.MasterClientId = -1;
				GameObject gameObject4 = PhotonNetwork.NOOU("networkplayer", this.PT.position + this.PT.forward * 7f, new Quaternion(0f, 0f, 0f, 0f), 0);
				UnityEngine.Object.Destroy(gameObject4.GetComponent<FPSMouseLook>());
				UnityEngine.Object.Destroy(gameObject4.GetComponent<FPSinput>());
				UnityEngine.Object.Destroy(gameObject4.GetComponent<FPSSoundController>());
				UnityEngine.Object.Destroy(gameObject4.GetComponent<WeaponPickUp>());
				gameObject4.transform.FindChild("LookObject").gameObject.active = false;
				gameObject4.transform.FindChild("PLAYER_MODEL").gameObject.active = true;
				gameObject4.transform.FindChild("HUD").gameObject.active = false;
				this.DSB(gameObject4);
			}
			if (GUI.Toggle(new Rect(10f, 310f, 200f, 20f), this.ai, "无ai"))
			{
				this.ai = true;
			}
			else
			{
				this.ai = false;
			}
			if (GUI.Toggle(new Rect(10f, 330f, 200f, 20f), this.ragdoll, "布娃娃"))
			{
				this.ragdoll = true;
			}
			else
			{
				this.ragdoll = false;
			}
			if (GUI.Toggle(new Rect(10f, 350f, 200f, 20f), this.collect, "观赏"))
			{
				this.collect = true;
			}
			else
			{
				this.collect = false;
			}
			if (GUI.Toggle(new Rect(10f, 370f, 200f, 20f), this.controll, "扮演"))
			{
				this.controll = true;
			}
			else
			{
				this.controll = false;
			}
			if (GUI.Toggle(new Rect(210f, 310f, 200f, 20f), this.sb, "天旋地转"))
			{
				this.sb = true;
			}
			else
			{
				this.sb = false;
			}
			if (GUI.Button(new Rect(10f, 390f, 400f, 20f), "<color=blue>更改自己怪物为蓝队</color>"))
			{
				foreach (PhotonPlayer photonPlayer in PhotonNetwork.networkingPeer.mPlayerListCopy)
				{
					int id = PhotonNetwork.player.ID;
					PhotonNetwork.player.InternalChangeLocalID(photonPlayer.ID);
					Bot[] bots = NecConsole.Bots;
					for (int i = 0; i < bots.Length; i++)
					{
						bots[i].FLMFBIEMKED = 0;
					}
					CustardBot[] cbot = NecConsole.Cbot;
					for (int i = 0; i < cbot.Length; i++)
					{
						cbot[i].FLMFBIEMKED = 0;
					}
					BossBot[] boss = NecConsole.Boss;
					for (int i = 0; i < boss.Length; i++)
					{
						boss[i].FLMFBIEMKED = 0;
					}
					PhotonNetwork.player.InternalChangeLocalID(id);
				}
			}
			if (GUI.Button(new Rect(10f, 410f, 400f, 20f), "<color=red>更改自己怪物为红队</color>"))
			{
				Bot[] bots = NecConsole.Bots;
				for (int i = 0; i < bots.Length; i++)
				{
					bots[i].FLMFBIEMKED = 1;
				}
				CustardBot[] cbot = NecConsole.Cbot;
				for (int i = 0; i < cbot.Length; i++)
				{
					cbot[i].FLMFBIEMKED = 1;
				}
				BossBot[] boss = NecConsole.Boss;
				for (int i = 0; i < boss.Length; i++)
				{
					boss[i].FLMFBIEMKED = 1;
				}
			}
			if (GUI.Button(new Rect(10f, 430f, 400f, 20f), "<color=orange>杀死全部生存怪物</color>"))
			{
				foreach (BossBot bossBot in UnityEngine.Object.FindObjectsOfType<BossBot>())
				{
					bossBot.FD0002(float.MaxValue);
				}
				foreach (Bot bot in UnityEngine.Object.FindObjectsOfType<Bot>())
				{
					bot.FD0002(float.MaxValue);
				}
			}
			if (GUI.Button(new Rect(10f, 450f, 200f, 20f), "<color=orange>全部生存怪物高血量</color>"))
			{
				foreach (BossBot bossBot2 in UnityEngine.Object.FindObjectsOfType<BossBot>())
				{
					bossBot2.FD0002(-2.14E+09f);
				}
				foreach (Bot bot2 in UnityEngine.Object.FindObjectsOfType<Bot>())
				{
					bot2.FD0002(-2.14E+09f);
				}
			}
			if (GUI.Button(new Rect(210f, 450f, 200f, 20f), "<color=orange>全部生存怪物无敌</color>"))
			{
				foreach (BossBot bossBot3 in UnityEngine.Object.FindObjectsOfType<BossBot>())
				{
					bossBot3.FD0002(float.NaN);
				}
				foreach (Bot bot3 in UnityEngine.Object.FindObjectsOfType<Bot>())
				{
					bot3.FD0002(float.NaN);
				}
			}
			if (GUI.Button(new Rect(10f, 470f, 400f, 20f), "<color=orange>-返回</color>"))
			{
				this.msa = false;
				this.Use = true;
				this.MainMenu();
			}
		}

		public void RoomMenu()
		{
			if (this.saas)
			{
				GUI.Box(new Rect(10f, 10f, 200f, 740f), "<color=white>房间菜单</color>");
				GUI.color = Color.grey;
				if (PhotonNetwork.room != null)
				{
					if (GUI.Button(new Rect(10f, 270f, 200f, 20f), "<color=white>-返回</color>"))
					{
						this.saas = false;
						this.Use = true;
						this.MainMenu();
						this.sssa = false;
					}
					this.RoomProperties = PhotonNetwork.room.CustomProperties;
					GUI.Label(new Rect(10f, 30f, 200f, 940f), "<color=white>房间名字：</color>" + PhotonNetwork.room.nameField, "s");
					if (GUI.Button(new Rect(10f, 50f, 100f, 20f), "<color=white>沙盒模式</color>") && GameObject.Find("__Room"))
					{
						this.SendLog("房间已改为", "沙盒模式");
						GameObject gameObject = GameObject.Find("__Room");
						gameObject.GetComponent<SurvivalMechanics>().enabled = false;
						gameObject.GetComponent<ClassicMechanics>().enabled = false;
						this.SetGameMode("SBX", this.RoomProperties);
					}
					if (GUI.Button(new Rect(110f, 50f, 100f, 20f), "<color=white>生存模式</color>"))
					{
						GameObject.Find("__Room").GetComponent<SurvivalMechanics>().enabled = true;
						this.SetGameMode("SUR", this.RoomProperties);
						this.SendLog("房间已改为", "生存模式");
					}
					if (GUI.Button(new Rect(10f, 70f, 100f, 20f), "<color=white>搜集模式</color>"))
					{
						this.SendLog("房间已改为", "搜集模式");
						GameObject gameObject2 = GameObject.Find("__Room");
						gameObject2.GetComponent<SurvivalMechanics>().enabled = false;
						gameObject2.GetComponent<ClassicMechanics>().enabled = true;
						this.SetGameMode("COOP", this.RoomProperties);
					}
					if (GUI.Button(new Rect(110f, 70f, 100f, 20f), "<color=white>感染模式</color>"))
					{
						this.SendLog("房间已改为", "感染模式");
						GameObject gameObject3 = GameObject.Find("__Room");
						gameObject3.GetComponent<ClassicMechanics>().enabled = false;
						gameObject3.GetComponent<SurvivalMechanics>().enabled = false;
						this.SetGameMode("INF", this.RoomProperties);
					}
					if (GUI.Button(new Rect(10f, 90f, 100f, 20f), "<color=white>团队竞赛模式</color>"))
					{
						this.SendLog("房间已改为", "TDM模式");
						GameObject gameObject4 = GameObject.Find("__Room");
						gameObject4.GetComponent<ClassicMechanics>().enabled = false;
						gameObject4.GetComponent<SurvivalMechanics>().enabled = false;
						this.SetGameMode("TDM", this.RoomProperties);
					}
					if (GUI.Button(new Rect(110f, 90f, 100f, 20f), "<color=white>对抗模式</color>"))
					{
						this.SendLog("房间已改为", "对抗模式");
						GameObject gameObject5 = GameObject.Find("__Room");
						gameObject5.GetComponent<ClassicMechanics>().enabled = false;
						gameObject5.GetComponent<SurvivalMechanics>().enabled = false;
						this.SetGameMode("VS", this.RoomProperties);
					}
					if (GUI.Button(new Rect(110f, 110f, 100f, 20f), "<color=white>队伍：</color>" + PhotonNetwork.player.CustomProperties["TeamName"].ToString()))
					{
						if (PhotonNetwork.player.CustomProperties["TeamName"].ToString() == "Team A")
						{
							UnityEngine.Object.FindObjectOfType<RoomMultiplayerMenu>().BHNKAEKPIOA = "TDM";
							UnityEngine.Object.FindObjectOfType<RoomMultiplayerMenu>().SwapTeams("Team B");
							UnityEngine.Object.FindObjectOfType<RoomMultiplayerMenu>().Start();
							UnityEngine.Object.FindObjectOfType<RoomMultiplayerMenu>().RespawnPlayer2();
							this.SendLog(PhotonNetwork.player.NickName + "加入了<color=red>Team B</color>", null);
						}
						else
						{
							UnityEngine.Object.FindObjectOfType<RoomMultiplayerMenu>().BHNKAEKPIOA = "SBX";
							UnityEngine.Object.FindObjectOfType<RoomMultiplayerMenu>().SwapTeams("Team A");
							UnityEngine.Object.FindObjectOfType<RoomMultiplayerMenu>().Start();
							UnityEngine.Object.FindObjectOfType<RoomMultiplayerMenu>().RespawnPlayer2();
							this.SendLog(PhotonNetwork.player.NickName + "加入了Team A", null);
						}
					}
					if (GUI.Button(new Rect(210f, 110f, 100f, 20f), "<color=white>回放模式</color>"))
					{
						this.RoomProperties["GM001'"] = "THR";
						PhotonNetwork.room.SetCustomProperties(this.RoomProperties, null, false);
						PhotonNetwork.room.SetPropertiesListedInLobby(PhotonNetwork.networkingPeer.enterRoomParamsCache.RoomOptions.CRPFL);
						this.SendLog("房间已改为", "回放模式");
					}
					if (GUI.Button(new Rect(10f, 130f, 200f, 20f), "<color=white>真房主</color>"))
					{
						PhotonNetwork.networkingPeer.RemovePlayer(PhotonNetwork.player.ID, PhotonNetwork.masterClient);
						PhotonNetwork.SetMasterClient(PhotonNetwork.player);
						MelonCoroutines.Start(this.SetMaster(PhotonNetwork.player));
						this.SendLog("房主已切换为 " + PhotonNetwork.player.NickName, null);
					}
					if (GUI.Button(new Rect(10f, 150f, 200f, 20f), "<color=white>假房主</color>" + PhotonNetwork.isMasterClient.ToString()))
					{
						if (!PhotonNetwork.isMasterClient)
						{
							PhotonNetwork.room.MasterClientId = PhotonNetwork.player.ID;
						}
						else
						{
							PhotonNetwork.room.MasterClientId = 1;
						}
					}
					this.Maxs = (int)PhotonNetwork.room.maxPlayersField;
					if (GUI.Button(new Rect(10f, 170f, 200f, 20f), "<color=white>房间人数限制：</color>" + this.Maxs.ToString()))
					{
						if (this.Maxs != 12)
						{
							PhotonNetwork.room.MaxPlayers = 12;
							this.SendLog("房间人数限制已改为", "12");
						}
						else
						{
							PhotonNetwork.room.MaxPlayers = 6;
							this.SendLog("房间人数限制已改为", "6");
						}
					}
					if (GUI.Button(new Rect(10f, 190f, 200f, 20f), "<color=white>生成假房间</color>"))
					{
						GameObject gameObject6 = PhotonNetwork.NOOU("__Room", new Vector3(0f, 0f, 0f), new Quaternion(0f, 0f, 0f, 1f), 0);
						UnityEngine.Object.SetName(GameObject.Find("__Room(Clone)"), "__Room");
						gameObject6.GetComponent<RoomMultiplayerMenu>().Awake();
					}
					if (GUI.Button(new Rect(10f, 210f, 200f, 20f), "<color=white>摧毁房间</color>"))
					{
						PhotonNetwork.Destroy(GameObject.Find("__Room"));
						PhotonNetwork.Destroy(GameObject.Find("__Room(Clone)"));
					}
					if (GUI.Button(new Rect(10f, 230f, 200f, 20f), "<color=white>退出</color>"))
					{
						Application.LoadLevel("MainMenu");
					}
					if (GUI.Toggle(new Rect(10f, 250f, 200f, 20f), this.KIck, "刷屏"))
					{
						this.KIck = true;
						return;
					}
					this.KIck = false;
					return;
				}
				else
				{
					GUI.Label(new Rect(10f, 30f, 200f, 20f), "<color=white>无任何信息</color>", "？");
					if (GUI.Button(new Rect(10f, 50f, 200f, 20f), "<color=white>生成假房间</color>"))
					{
						PhotonNetwork.NOOU("__Room", new Vector3(0f, 0f, 0f), new Quaternion(0f, 0f, 0f, 1f), 0);
						UnityEngine.Object.SetName(GameObject.Find("__Room(Clone)"), "__Room");
					}
					if (GUI.Button(new Rect(10f, 70f, 200f, 20f), "<color=white>摧毁房间</color>"))
					{
						PhotonNetwork.networkingPeer.DestroyAll(false);
						PhotonNetwork.Destroy(GameObject.Find("__Room"));
						PhotonNetwork.Destroy(GameObject.Find("__Room(Clone)"));
					}
					if (GUI.Button(new Rect(10f, 90f, 200f, 20f), "<color=white>退出</color>"))
					{
						PhotonNetwork.Disconnect();
						PhotonNetwork.LeaveRoom();
						PhotonNetwork.networkingPeer.Disconnect();
						PhotonNetwork.networkingPeer.LeftRoomCleanup();
						PhotonNetwork.networkingPeer.OpLeave();
						PhotonNetwork.LoadLevel(1);
						PhotonNetwork.room.IsLocalClientInside = false;
					}
					if (GUI.Button(new Rect(10f, 110f, 200f, 20f), "<color=white>-返回</color>"))
					{
						this.saas = false;
						this.Use = true;
						this.MainMenu();
						this.sssa = false;
					}
				}
			}
		}

		public void MapMenu()
		{
			GUI.Box(new Rect(10f, 10f, 200f, 740f), "<color=yellow>地图菜单</color>");
			GUI.color = Color.yellow;
			if (PhotonNetwork.room != null)
			{
				this.RoomProperties = PhotonNetwork.room.CustomProperties;
				if (GUI.Button(new Rect(10f, 30f, 200f, 20f), "MainMenu"))
				{
					this.GFH("MainMenu");
					this.SetMapInServer("MainMenu", this.RoomProperties);
				}
				if (GUI.Button(new Rect(10f, 50f, 200f, 20f), "Custard Facility (Day)"))
				{
					this.GFH("Custard Facility (Day)");
					this.SetMapInServer("Custard Facility (Day)", this.RoomProperties);
				}
				if (GUI.Button(new Rect(10f, 70f, 200f, 20f), "Custard Facility (Dusk)"))
				{
					this.GFH("Custard Facility (Dusk)");
					this.SetMapInServer("Custard Facility (Dusk)", this.RoomProperties);
				}
				if (GUI.Button(new Rect(10f, 90f, 200f, 20f), "Custard Facility (Night)"))
				{
					this.GFH("Custard Facility (Night)");
					this.SetMapInServer("Custard Facility (Night)", this.RoomProperties);
				}
				if (GUI.Button(new Rect(10f, 110f, 200f, 20f), "Blue Rooms"))
				{
					this.SetMapInServer("Blue Rooms", this.RoomProperties);
				}
				if (GUI.Button(new Rect(10f, 130f, 200f, 20f), "Blue Room"))
				{
					this.GFH("Blue Room");
					this.SetMapInServer("Blue Room", this.RoomProperties);
				}
				if (GUI.Button(new Rect(10f, 150f, 200f, 20f), "Mountains"))
				{
					this.GFH("Mountains");
					this.SetMapInServer("Mountains", this.RoomProperties);
				}
				if (GUI.Button(new Rect(10f, 170f, 200f, 20f), "(S2) Training Maze"))
				{
					this.GFH("(S2) Training Maze");
					this.SetMapInServer("(S2) Training Maze", this.RoomProperties);
				}
				if (GUI.Button(new Rect(10f, 190f, 200f, 20f), "(S2) Lake"))
				{
					this.GFH("(S2) Lake");
					this.SetMapInServer("(S2) Lake", this.RoomProperties);
				}
				if (GUI.Button(new Rect(10f, 210f, 200f, 20f), "Lake"))
				{
					this.GFH("Lake");
					this.SetMapInServer("Lake", this.RoomProperties);
				}
				if (GUI.Button(new Rect(10f, 230f, 200f, 20f), "Training Maze"))
				{
					this.GFH("Training Maze");
					this.SetMapInServer("Training Maze", this.RoomProperties);
				}
				if (GUI.Button(new Rect(10f, 250f, 200f, 20f), "Banned"))
				{
					this.GFH("Banned");
					this.SetMapInServer("Banned", this.RoomProperties);
				}
				if (GUI.Toggle(new Rect(10f, 270f, 200f, 20f), this.load, "加载"))
				{
					this.load = true;
				}
				else
				{
					this.load = false;
				}
			}
			if (GUI.Button(new Rect(10f, 290f, 200f, 20f), "返回"))
			{
				this.MM = false;
				this.Use = true;
				this.MainMenu();
			}
		}

		public void GFH(string MaoName)
		{
			if (this.load)
			{
				Application.LoadLevel(MaoName);
			}
		}

		public void NameMenu()
		{
			if (this.sopp)
			{
				GUI.Box(new Rect(10f, 10f, 200f, 740f), "<color=white>名字菜单</color>");
				GUI.color = Color.grey;
				if (GUI.Button(new Rect(10f, 310f, 200f, 20f), "<color=white>-返回</color>"))
				{
					this.sopp = false;
					this.Use = true;
					this.MainMenu();
					this.so = false;
				}
				if (PhotonNetwork.room != null && this.PT.gameObject)
				{
					int blinnbpdfig = this.PT.gameObject.GetComponent<PlayerNetworkController>().BLINNBPDFIG;
					if (blinnbpdfig == 0)
					{
						this.NameC = "<color=white>白色</color>";
					}
					if (blinnbpdfig == 2)
					{
						this.NameC = "<color=green>绿色</color>";
					}
					if (blinnbpdfig == 3)
					{
						this.NameC = "<color=cyan>蓝色</color>";
					}
					if (blinnbpdfig == 7)
					{
						this.NameC = "<color=black>黑色</color>";
					}
					if (blinnbpdfig == 9)
					{
						this.NameC = "<color=orange>橙色</color>";
					}
					if (blinnbpdfig == 10)
					{
						this.NameC = "<color=purple>紫色</color>";
					}
					if (blinnbpdfig == 16)
					{
						this.NameC = "<color=red>红色</color>";
					}
					if (blinnbpdfig == -1)
					{
						this.NameC = "无名";
					}
					if (GUI.Button(new Rect(10f, 30f, 200f, 20f), "<color=white>白色</color>"))
					{
						this.PT.gameObject.GetComponent<PlayerNetworkController>().BLINNBPDFIG = 0;
						ObscuredPrefs.SetString("PlayerType01", "0");
					}
					if (GUI.Button(new Rect(10f, 50f, 200f, 20f), "<color=green>绿色</color>"))
					{
						this.PT.gameObject.GetComponent<PlayerNetworkController>().BLINNBPDFIG = 2;
						ObscuredPrefs.SetString("PlayerType01", "2");
					}
					if (GUI.Button(new Rect(10f, 70f, 200f, 20f), "<color=cyan>蓝色</color>"))
					{
						this.PT.gameObject.GetComponent<PlayerNetworkController>().BLINNBPDFIG = 3;
						ObscuredPrefs.SetString("PlayerType01", "3");
					}
					if (GUI.Button(new Rect(10f, 90f, 200f, 20f), "<color=black>黑色</color>"))
					{
						this.PT.gameObject.GetComponent<PlayerNetworkController>().BLINNBPDFIG = 7;
					}
					if (GUI.Button(new Rect(10f, 110f, 200f, 20f), "<color=orange>橙色</color>"))
					{
						this.PT.gameObject.GetComponent<PlayerNetworkController>().BLINNBPDFIG = 9;
						ObscuredPrefs.SetString("PlayerType01", "9");
					}
					if (GUI.Button(new Rect(10f, 130f, 200f, 20f), "<color=purple>紫色</color>"))
					{
						this.PT.gameObject.GetComponent<PlayerNetworkController>().BLINNBPDFIG = 10;
						ObscuredPrefs.SetString("PlayerType01", "10");
					}
					if (GUI.Button(new Rect(10f, 150f, 200f, 20f), "<color=red>红色</color>"))
					{
						this.PT.gameObject.GetComponent<PlayerNetworkController>().BLINNBPDFIG = 16;
						ObscuredPrefs.SetString("PlayerType01", "16");
					}
					if (GUI.Button(new Rect(10f, 170f, 200f, 20f), "<color=white>无PlayerName</color>"))
					{
						this.PT.gameObject.GetComponent<PlayerNetworkController>().BLINNBPDFIG = -1;
						ObscuredPrefs.SetString("PlayerType01", "-1");
					}
					if (GUI.Button(new Rect(10f, 190f, 200f, 20f), "<color=white>游客名字</color>"))
					{
						string str = System.Convert.ToInt32(NecConsole.RandomCode(3, true, false, false)).ToString();
						this.PT.gameObject.GetComponent<PlayerNetworkController>().BLINNBPDFIG = 0;
						ObscuredPrefs.SetString("ZWName01", "Player " + str);
						PhotonNetwork.player.NickName = "Player " + str;
						PhotonNetwork.networkingPeer.PlayerName = "Player " + str;
					}
					Cursor.visible = true;
					Cursor.lockState = CursorLockMode.None;
					GUI.Label(new Rect(10f, 250f, 200f, 20f), string.Concat(new string[]
					{
						"<color=white>名字:",
						PhotonNetwork.player.NickName,
						" | 颜色:",
						this.NameC,
						"</color>"
					}), "6");
					this.Name = GUI.TextField(new Rect(10f, 270f, 200f, 20f), this.Name, 666);
					if (GUI.Button(new Rect(10f, 290f, 200f, 20f), "<color=white>设置名字</color>"))
					{
						ObscuredPrefs.SetString("ZWName01", this.Name);
						PhotonNetwork.player.NickName = this.Name;
						PhotonNetwork.networkingPeer.PlayerName = this.Name;
					}
					if (GUI.Toggle(new Rect(10f, 210f, 200f, 20f), this.nc, "随机名字颜色"))
					{
						this.nc = true;
					}
					else
					{
						this.nc = false;
					}
					if (GUI.Toggle(new Rect(10f, 230f, 200f, 20f), this.n, "随机名字"))
					{
						this.n = true;
						return;
					}
					this.n = false;
				}
			}
		}

		public void LIstP()
		{
			if (this.pk)
			{
				GUILayout.BeginArea(new Rect(410f, 30f, 400f, 700f));
				GUILayout.BeginVertical("Box", new GUILayoutOption[0]);
				GUILayout.Label("<b><color=Red>针对菜单</color></b>", new GUILayoutOption[0]);
				GUILayout.Label("<b><color=grey>所有在线玩家:</color></b>", new GUILayoutOption[0]);
				this.Vector5 = GUILayout.BeginScrollView(this.Vector5, new GUILayoutOption[0]);
				foreach (PhotonPlayer photonPlayer in PhotonNetwork.playerList)
				{
					GUILayout.BeginHorizontal("Box", new GUILayoutOption[0]);
					GUILayout.Label("[" + photonPlayer.ID.ToString() + "]-" + photonPlayer.NickName, new GUILayoutOption[0]);
					if (GUILayout.RepeatButton("<color=yellow>摧毁</color>", new GUILayoutOption[0]))
					{
						PhotonNetwork.networkingPeer.DestroyPlayerObjects(photonPlayer.ID, false);
					}
					if (GUILayout.RepeatButton("<color=yellow>死亡</color>", new GUILayoutOption[0]))
					{
						this.SetHPforInternet(float.MaxValue, photonPlayer);
					}
					if (GUILayout.RepeatButton("<color=yellow>Nan血</color>", new GUILayoutOption[0]))
					{
						this.SendLog(photonPlayer.NickName, " 无敌了");
						this.SetHPforInternet(float.NaN, photonPlayer);
					}
					if (GUILayout.Button("<color=yellow>附身</color>", new GUILayoutOption[0]) && PhotonNetwork.room != null)
					{
						PhotonNetwork.networkingPeer.DestroyPlayerObjects(PhotonNetwork.player.ID, false);
						PhotonNetwork.networkingPeer.RemovePlayer(photonPlayer.ID, photonPlayer);
						PhotonNetwork.player.InternalCacheProperties(photonPlayer.CustomProperties);
						PhotonNetwork.networkingPeer.ChangeLocalID(photonPlayer.ID);
						PhotonNetwork.player.InternalChangeLocalID(photonPlayer.ID);
						PhotonNetwork.networkingPeer.PlayerName = photonPlayer.NickName;
						ObscuredPrefs.SetString("ZWName01", photonPlayer.NickName);
					}
					if (GUILayout.Button("<color=yellow>死后复活</color>", new GUILayoutOption[0]) && PhotonNetwork.room != null)
					{
						GameObject.Find("__Room").GetComponent<PhotonView>().RPC("MLHPLGJCGJM", photonPlayer, new Il2CppReferenceArray<Il2CppSystem.Object>(0L));
					}
					if (GUILayout.Button("<color=yellow>幽灵</color>", new GUILayoutOption[0]) && PhotonNetwork.room != null)
					{
						string text = PhotonNetwork.room.customProperties["GM001'"].ToString();
						ExitGames.Client.Photon.Hashtable customProperties = PhotonNetwork.room.CustomProperties;
						customProperties["GM001'"] = "SUR";
						PhotonNetwork.room.SetCustomProperties(customProperties, null, false);
						this.SetHPforInternet(float.MaxValue, photonPlayer);
						customProperties["GM001'"] = text;
						PhotonNetwork.room.SetCustomProperties(customProperties, null, false);
					}
					if (GUILayout.Button("<color=yellow>生成</color>", new GUILayoutOption[0]) && PhotonNetwork.room != null)
					{
						PhotonNetwork.networkingPeer.DestroyPlayerObjects(photonPlayer.ID, false);
						int id = PhotonNetwork.player.ID;
						PhotonNetwork.player.InternalChangeLocalID(photonPlayer.ID);
						GameObject gameObject = PhotonNetwork.NOOU(this.MN, this.PT.position + this.PT.forward * 7f, new Quaternion(0f, 0f, 0f, 0f), 0);
						PhotonNetwork.player.InternalChangeLocalID(id);
						if (gameObject.tag == "FlyCam")
						{
							UnityEngine.Object.Destroy(gameObject.GetComponent<FlareLayer>());
							UnityEngine.Object.Destroy(gameObject.GetComponent<FPSMouseLook>());
							UnityEngine.Object.Destroy(gameObject.GetComponent<Camera>());
						}
						if (gameObject.tag == "monster")
						{
							gameObject.transform.FindChild("Main Camera").gameObject.SetActive(false);
						}
						if (gameObject.tag == "Player")
						{
							gameObject.transform.FindChild("HUD").gameObject.SetActive(false);
							gameObject.transform.FindChild("LookObject").gameObject.SetActive(false);
						}
					}
					if (GUILayout.RepeatButton("<color=yellow>无限死亡</color>", new GUILayoutOption[0]) && PhotonNetwork.room != null && photonPlayer.ID != PhotonNetwork.player.ID)
					{
						for (int i = 0; i < 150; i++)
						{
							this.SetHPforInternet(float.MaxValue, photonPlayer);
							int id2 = PhotonNetwork.player.ID;
							PhotonNetwork.player.InternalChangeLocalID(photonPlayer.ID);
							GameObject gameObject2 = PhotonNetwork.NOOU("networkplayer2", new Vector3(0f, 0f, 0f), new Quaternion(0f, 0f, 0f, 0f), 0);
							PhotonNetwork.player.InternalChangeLocalID(id2);
							gameObject2.SetActive(false);
						}
					}
					if (GUILayout.RepeatButton("<color=yellow>踢</color>", new GUILayoutOption[0]) && PhotonNetwork.room != null)
					{
						string text2 = PhotonNetwork.room.customProperties["GM001'"].ToString();
						int id3 = PhotonNetwork.player.ID;
						PhotonNetwork.player.InternalChangeLocalID(photonPlayer.ID);
						GameObject gameObject3 = PhotonNetwork.NOOU("networkplayer", new Vector3(0f, 0f, 0f), new Quaternion(0f, 0f, 0f, 0f), 0);
						PhotonNetwork.player.InternalChangeLocalID(id3);
						gameObject3.SetActive(false);
						ExitGames.Client.Photon.Hashtable customProperties2 = PhotonNetwork.room.CustomProperties;
						customProperties2["GM001'"] = "COOP";
						PhotonNetwork.room.SetCustomProperties(customProperties2, null, false);
						GameObject.Find("__Room").GetComponent<PhotonView>().RPC("GANGONMCLJL", photonPlayer, new Il2CppReferenceArray<Il2CppSystem.Object>(0L));
						GameObject.Find("__Room").GetComponent<PhotonView>().RPC("EGEOPEKDAND", photonPlayer, new Il2CppReferenceArray<Il2CppSystem.Object>(0L));
						customProperties2["GM001'"] = text2;
						PhotonNetwork.room.SetCustomProperties(customProperties2, null, false);
					}
					if (GUILayout.RepeatButton("<color=yellow>崩溃</color>", new GUILayoutOption[0]) && PhotonNetwork.room != null && photonPlayer.ID != PhotonNetwork.player.ID)
					{
						int id4 = PhotonNetwork.player.ID;
						PhotonNetwork.player.InternalChangeLocalID(photonPlayer.ID);
						PhotonNetwork.NOOU("sur/bossscythetubby", new Vector3(0f, 0f, 0f), new Quaternion(0f, 0f, 0f, 0f), 0);
						PhotonNetwork.player.InternalChangeLocalID(id4);
					}
					if (GUILayout.RepeatButton("<color=yellow>修改名字</color>", new GUILayoutOption[0]) && PhotonNetwork.room != null && photonPlayer.ID != PhotonNetwork.player.ID)
					{
						photonPlayer.IsLocal = true;
						int id5 = photonPlayer.ID;
						int id6 = PhotonNetwork.player.ID;
						if (ObscuredPrefs.GetString("ZWName01", "") == null)
						{
							string nickName = PhotonNetwork.player.NickName;
							PhotonNetwork.player.InternalChangeLocalID(photonPlayer.ID);
							PhotonNetwork.networkingPeer.PlayerName = this.ON;
							PhotonNetwork.networkingPeer.SendPlayerName();
							photonPlayer.NickName = this.ON;
							photonPlayer.nameField = this.ON;
							PhotonNetwork.player.InternalChangeLocalID(id6);
							PhotonNetwork.networkingPeer.PlayerName = nickName;
						}
						else
						{
							string @string = ObscuredPrefs.GetString("ZWName01", "");
							PhotonNetwork.player.InternalChangeLocalID(photonPlayer.ID);
							PhotonNetwork.networkingPeer.PlayerName = this.ON;
							PhotonNetwork.networkingPeer.SendPlayerName();
							photonPlayer.NickName = this.ON;
							photonPlayer.nameField = this.ON;
							PhotonNetwork.player.InternalChangeLocalID(id6);
							PhotonNetwork.networkingPeer.PlayerName = @string;
						}
						photonPlayer.InternalChangeLocalID(PhotonNetwork.player.ID);
						photonPlayer.InternalChangeLocalID(id5);
					}
					GUILayout.EndHorizontal();
				}
				if (GUILayout.Button("<color=yellow>全体玩家循环血量</color>" + this.Hp.ToString(), new GUILayoutOption[0]))
				{
					this.Hp = !this.Hp;
				}
				if (GUILayout.Button("<color=yellow>杀掉其他玩家</color>" + this.dall.ToString(), new GUILayoutOption[0]))
				{
					this.dall = !this.dall;
				}
				if (GUILayout.Button("<color=yellow>重生所有玩家</color>", new GUILayoutOption[0]))
				{
					GameObject.Find("__Room").GetComponent<PhotonView>().RPC("JBGEDMBIHKF", PhotonTargets.All, new Il2CppReferenceArray<Il2CppSystem.Object>(0L));
				}
				if (GUILayout.Button("<color=yellow>随机感染（仅感染模式）</color>", new GUILayoutOption[0]))
				{
					GameObject.Find("__Room").GetComponent<PhotonView>().RPC("GIHBIFJDOMC", PhotonTargets.All, new Il2CppReferenceArray<Il2CppSystem.Object>(0L));
				}
				this.ON = GUILayout.TextArea(this.ON, 256, new GUILayoutOption[0]);
				this.MN = GUILayout.TextArea(this.MN, 256, new GUILayoutOption[0]);
				if (GUILayout.Button("返回", new GUILayoutOption[0]))
				{
					this.pk = false;
					this.Use = true;
					this.MainMenu();
					this.fork = false;
				}
				GUILayout.EndScrollView();
				GUILayout.EndVertical();
				GUILayout.EndArea();
			}
		}

		public void WeanponMenu()
		{
			if (this.mini2)
			{
				GUILayout.BeginArea(new Rect(10f, 30f, 400f, 700f));
				GUILayout.BeginVertical("Box", new GUILayoutOption[0]);
				GUILayout.Label("<b><color=green>武器菜单</color></b>", new GUILayoutOption[0]);
				this.Vector5 = GUILayout.BeginScrollView(this.Vector5, new GUILayoutOption[0]);
				if (GUI.Button(new Rect(0f, 550f, 400f, 20f), "返回"))
				{
					this.mini = false;
					this.Use = true;
					this.MainMenu();
					this.mini2 = false;
				}
				if (GUI.Button(new Rect(0f, 450f, 400f, 20f), "武器无限子弹:" + this.wp.ToString()))
				{
					this.wp = !this.wp;
				}
				if (GUI.Button(new Rect(0f, 470f, 400f, 20f), "武器全自动:" + this.lf.ToString()))
				{
					this.lf = !this.lf;
				}
				if (GUI.Button(new Rect(0f, 490f, 400f, 20f), "武器高射速:" + this.gss.ToString()))
				{
					this.gss = !this.gss;
				}
				if (GUI.Button(new Rect(0f, 510f, 400f, 20f), "子弹秒杀:" + this.ms.ToString()))
				{
					this.ms = !this.ms;
				}
				if (GUI.Button(new Rect(0f, 530f, 400f, 20f), "子弹屏蔽:" + this.pb.ToString()))
				{
					this.pb = !this.pb;
				}
				if (this.PT.gameObject != null)
				{
					if (GUILayout.Button("<color=green>XIX</color>", new GUILayoutOption[0]))
					{
						this.give("XIX");
					}
					if (GUILayout.Button("<color=green>XIX II</color>", new GUILayoutOption[0]))
					{
						this.give("XIX II");
					}
					if (GUILayout.Button("<color=green>AKM</color>", new GUILayoutOption[0]))
					{
						this.give("AKM");
					}
					if (GUILayout.Button("<color=green>RPG</color>", new GUILayoutOption[0]))
					{
						this.give("RPG");
					}
					if (GUILayout.Button("<color=green>M40A3</color>", new GUILayoutOption[0]))
					{
						this.give("M40A3");
					}
					if (GUILayout.Button("<color=green>MCS870</color>", new GUILayoutOption[0]))
					{
						this.give("MCS870");
					}
					if (GUILayout.Button("<color=green>M249-Saw</color>", new GUILayoutOption[0]))
					{
						this.give("M249-Saw");
					}
					if (GUILayout.Button("<color=green>Camera</color>", new GUILayoutOption[0]))
					{
						this.give("Camera");
					}
					if (GUILayout.Button("<color=green>Torch</color>", new GUILayoutOption[0]))
					{
						this.give("Torch");
					}
					if (GUILayout.Button("<color=green>Flashlight</color>", new GUILayoutOption[0]))
					{
						this.give("Flashlight");
					}
					if (GUILayout.Button("<color=green>M4A1</color>", new GUILayoutOption[0]))
					{
						PhotonNetwork.room.masterClientId = -1;
						PhotonNetwork.Destroy(this.PT.gameObject);
						PhotonNetwork.NOOU("networkplayer", this.PT.position + this.PT.forward * 7f, new Quaternion(0f, 0f, 0f, 0f), 0, null).transform.FindChild("LookObject").transform.FindChild("Main Camera").transform.FindChild("Weapon Camera").transform.FindChild("WeaponManager").transform.FindChild("Shorty").gameObject.SetActive(false);
						this.give("M4A1");
					}
					if (GUILayout.Button("<color=green>弹簧刀</color>", new GUILayoutOption[0]))
					{
						WeaponScript component = UnityEngine.Object.FindObjectOfType<WeaponManager>().transform.Find("Knife").GetComponent<WeaponScript>();
						this.give("Knife");
						component.FFCLOMAOFHO = "BallisticKnife";
					}
					if (GUILayout.Button("<color=green>手雷</color>", new GUILayoutOption[0]))
					{
						WeaponScript component2 = UnityEngine.Object.FindObjectOfType<WeaponManager>().transform.Find("Knife").GetComponent<WeaponScript>();
						this.give("Knife");
						component2.FFCLOMAOFHO = "M67";
					}
					if (GUILayout.Button("<color=green>卡宾枪</color>", new GUILayoutOption[0]))
					{
						WeaponScript component3 = UnityEngine.Object.FindObjectOfType<WeaponManager>().transform.Find("MCS870").GetComponent<WeaponScript>();
						this.give("MCS870");
						component3.FFCLOMAOFHO = "STW-25";
					}
					if (GUILayout.Button("<color=green>榴弹炮</color>", new GUILayoutOption[0]))
					{
						WeaponScript component4 = UnityEngine.Object.FindObjectOfType<WeaponManager>().transform.Find("RPG").GetComponent<WeaponScript>();
						this.give("RPG");
						component4.FFCLOMAOFHO = "M79";
					}
					if (GUILayout.Button("<color=green>鸟狙</color>", new GUILayoutOption[0]))
					{
						WeaponScript component5 = UnityEngine.Object.FindObjectOfType<WeaponManager>().transform.Find("M40A3").GetComponent<WeaponScript>();
						this.give("M40A3");
						component5.FFCLOMAOFHO = "Blaser R93 ";
					}
					if (GUILayout.Button("<color=green>冲锋枪</color>", new GUILayoutOption[0]))
					{
						WeaponScript component6 = UnityEngine.Object.FindObjectOfType<WeaponManager>().transform.Find("MP5N").GetComponent<WeaponScript>();
						this.give("MP5N");
						component6.FFCLOMAOFHO = "MP5KA4 ";
					}
					if (GUILayout.Button("<color=green>雷m顿</color>", new GUILayoutOption[0]))
					{
						WeaponScript component7 = UnityEngine.Object.FindObjectOfType<WeaponManager>().transform.Find("Shorty").GetComponent<WeaponScript>();
						this.give("Shorty");
						component7.FFCLOMAOFHO = "M87T";
					}
				}
				GUILayout.EndScrollView();
				GUILayout.EndVertical();
				GUILayout.EndArea();
			}
		}

		public void PlayerMenu()
		{
			GUI.Box(new Rect(10f, 10f, 200f, 740f), "<color=red>玩家菜单</color>");
			GUI.color = Color.red;
			if (GUI.Button(new Rect(10f, 270f, 200f, 20f), "-返回"))
			{
				this.k = false;
				this.Use = true;
				this.MainMenu();
			}
			if (this.PT.gameObject != null)
			{
				if (GUI.Button(new Rect(10f, 30f, 200f, 20f), "<color=yellow>负血量无敌</color>"))
				{
					this.SetPlayerHP(-999999f);
					UnityEngine.Object.Destroy(this.PT.gameObject.GetComponent<PlayerDamage>());
				}
				if (GUI.Button(new Rect(10f, 50f, 200f, 20f), "血量随机" + this.fx.ToString()))
				{
					this.fx = !this.fx;
				}
				GUI.Label(new Rect(10f, 70f, 200f, 20f), "跳跃高度:" + this.PT.GetComponent<FPScontroller>().AIPEDHNNCLM.baseHeight.ToString(), "6");
				this.ty = GUI.TextField(new Rect(10f, 90f, 200f, 20f), this.ty, 666);
				if (GUI.Button(new Rect(10f, 110f, 200f, 20f), "应用"))
				{
					this.PT.GetComponent<FPScontroller>().AIPEDHNNCLM.baseHeight = System.Convert.ToSingle(this.ty);
				}
				GUI.Label(new Rect(10f, 130f, 200f, 20f), "玩家血量:" + this.PT.GetComponent<PlayerDamage>().EAOGKANHLAN.ToString(), "6");
				this.HS = GUI.TextField(new Rect(10f, 150f, 200f, 20f), this.HS, 666);
				if (GUI.Button(new Rect(10f, 170f, 200f, 20f), "应用"))
				{
					this.PT.GetComponent<PlayerDamage>().JPCFPICJKFI = System.Convert.ToSingle(this.HS);
					this.PT.GetComponent<PlayerDamage>().EAOGKANHLAN = System.Convert.ToSingle(this.HS);
				}
				GUI.Label(new Rect(10f, 190f, 200f, 20f), "上半身动作ID:" + this.PT.GetComponent<PlayerNetworkController>().BHNPCAMHNFM.ToString(), "6");
				this.dz2 = GUI.TextField(new Rect(10f, 210f, 150f, 20f), this.dz2, 666);
				if (this.dzc)
				{
					this.color = "<color=yellow>";
					MelonCoroutines.Start(this.DZ2(System.Convert.ToInt32(this.dz2)));
				}
				else
				{
					this.color = "<color=red>";
				}
				if (GUI.Button(new Rect(160f, 210f, 50f, 20f), this.color + "锁定</color>"))
				{
					this.dzc = !this.dzc;
				}
				GUI.Label(new Rect(10f, 230f, 200f, 20f), "下半身动作ID:" + this.PT.GetComponent<PlayerNetworkController>().DOAPAILCLNN.ToString(), "6");
				this.dz1 = GUI.TextField(new Rect(10f, 250f, 150f, 20f), this.dz1, 666);
				if (this.dz)
				{
					this.color = "<color=yellow>";
					MelonCoroutines.Start(this.DZ(System.Convert.ToInt32(this.dz1)));
				}
				else
				{
					this.color = "<color=red>";
				}
				if (GUI.Button(new Rect(160f, 250f, 50f, 20f), this.color + "锁定</color>"))
				{
					this.dz = !this.dz;
				}
			}
		}

		public void SetHPforInternet(float HP, PhotonPlayer p)
		{
			Il2CppSystem.Single single = default(Il2CppSystem.Single);
			single.m_value = HP;
			Il2CppReferenceArray<Il2CppSystem.Object> parameters = new Il2CppReferenceArray<Il2CppSystem.Object>(new Il2CppSystem.Object[]
			{
				single.BoxIl2CppObject(),
				p
			});
			GameObject.Find(p.NickName).GetComponent<PhotonView>().RPC("LLPGPMIJOBA", PhotonTargets.All, parameters);
		}

		public void give(string wn)
		{
			WeaponManager weaponManager = UnityEngine.Object.FindObjectOfType<WeaponManager>();
			WeaponScript component = weaponManager.transform.Find(wn).GetComponent<WeaponScript>();
			weaponManager.ABEDKBPCLPH.Add(component);
		}

		public void SendLog(string log, string what)
		{
			GameObject gameObject = GameObject.Find("__Room");
			if (gameObject)
			{
				PhotonView component = gameObject.GetComponent<PhotonView>();
				Il2CppSystem.Object[] array = new Il2CppSystem.Object[4];
				array[0] = log;
				array[2] = what;
				Il2CppSystem.Object[] arr = array;
				component.RPC("networkAddMessage", PhotonTargets.All, arr);
			}
		}

		public void p()
		{
			if (this.ts)
			{
				this.PT.GetComponent<FPScontroller>().GBEPIFHEPBH = true;
				return;
			}
			this.PT.GetComponent<FPScontroller>().GBEPIFHEPBH = false;
		}

		public IEnumerator SetMaster(PhotonPlayer p)
		{
			yield return new WaitForSeconds(3f);
			PhotonNetwork.networkingPeer.AddNewPlayer(p.ID, p);
			yield break;
		}

		public IEnumerator ForC()
		{
			int randsize = new System.Random(System.DateTime.Now.Millisecond).Next(0, 16);
			this.PT.gameObject.GetComponent<PlayerNetworkController>().BLINNBPDFIG = randsize;
			yield return new WaitForSeconds(3f);
			yield break;
		}

		public IEnumerator DZ(int sb)
		{
			this.PT.GetComponent<PlayerNetworkController>().DOAPAILCLNN = sb;
			yield return new WaitForSeconds(1f);
			yield break;
		}

		public IEnumerator DZ2(int sb)
		{
			this.PT.GetComponent<PlayerNetworkController>().BHNPCAMHNFM = sb;
			yield return new WaitForSeconds(1f);
			yield break;
		}

		public void SetGameMode(string GM, ExitGames.Client.Photon.Hashtable properties1)
		{
			PhotonNetwork.room.MasterClientId = PhotonNetwork.player.ID;
			UnityEngine.Object.FindObjectOfType<RoomMultiplayerMenu>().BHNKAEKPIOA = GM;
			properties1["GM001'"] = GM;
			PhotonNetwork.room.SetCustomProperties(properties1, null, false);
			UnityEngine.Object.FindObjectOfType<RoomMultiplayerMenu>().Start();
			UnityEngine.Object.FindObjectOfType<RoomMultiplayerMenu>().RespawnPlayer2();
			PhotonNetwork.room.SetPropertiesListedInLobby(PhotonNetwork.networkingPeer.enterRoomParamsCache.RoomOptions.CRPFL);
			foreach (PhotonPlayer targetPlayer in PhotonNetwork.playerList)
			{
				GameObject.Find("__Room").GetComponent<PhotonView>().RPC("EGEOPEKDAND", targetPlayer, new Il2CppReferenceArray<Il2CppSystem.Object>(0L));
			}
		}

		public void SetMapInServer(string MN, ExitGames.Client.Photon.Hashtable properties1)
		{
			properties1["MN002'"] = MN;
			PhotonNetwork.room.SetCustomProperties(properties1, null, false);
			PhotonNetwork.room.SetPropertiesListedInLobby(PhotonNetwork.networkingPeer.enterRoomParamsCache.RoomOptions.CRPFL);
		}

		public void DSB(GameObject AD)
		{
			if (this.ai)
			{
				UnityEngine.Object.Destroy(AD.GetComponent<NavMeshAgent>());
			}
			if (this.ragdoll)
			{
				UnityEngine.Object.Destroy(AD.GetComponent<FPScontroller>());
				AD.AddComponent<Rigidbody>();
				AD.AddComponent<SphereCollider>();
			}
			if (this.collect)
			{
				AD.AddComponent<WaitForDestroy>();
			}
			if (this.controll)
			{
				PhotonNetwork.Destroy(this.PT.gameObject);
				UnityEngine.Object.Destroy(AD.GetComponent<BoxCollider>());
				AD.transform.gameObject.AddComponent<FPScontroller>();
				AD.transform.gameObject.AddComponent<FPSinput>();
				AD.transform.gameObject.AddComponent<LadderPlayer>();
				AD.transform.gameObject.AddComponent<Camera>();
				AD.transform.gameObject.AddComponent<MouseLook>();
				this.nmb = false;
			}
			if (this.sb)
			{
				AD.transform.gameObject.AddComponent<FPSMouseLook>();
			}
			if (this.mini)
			{
				AD.transform.eulerAngles = new Vector3(1f, 1f, 1f);
			}
		}

		public static string RandomCode(int length, bool useNum = true, bool useLow = false, bool useUpp = false)
		{
			byte[] array = new byte[4];
			new RNGCryptoServiceProvider().GetBytes(array);
			System.Random random = new System.Random(System.BitConverter.ToInt32(array, 0));
			string text = null;
			string text2 = "";
			if (useNum)
			{
				text2 = "0123456789";
			}
			if (useLow)
			{
				text2 = "abcdefghijklmnopqrstuvwxyz";
			}
			if (useUpp)
			{
				text2 = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";
			}
			for (int i = 0; i < length; i++)
			{
				text += text2.Substring(random.Next(0, text2.Length - 1), 1);
			}
			return text;
		}

		public bool Use;

		public int Maxs;

		public bool msa;

		public bool maa;

		public bool ai;

		public bool ragdoll;

		public bool collect;

		public bool controll;

		public bool saas;

		public bool sssa;

		public ExitGames.Client.Photon.Hashtable RoomProperties;

		public bool so;

		public bool sopp;

		public bool fork;

		public bool pk;

		public string Name = "Name";

		public bool KIck;

		private Vector2 Vector5;

		public bool sb;

		public bool nmb;

		public GameObject np1;

		public string NameC;

		public bool nc;

		public bool n;

		public bool mini;

		public bool mini2;

		public bool ts;

		public bool k;

		public string ty = "1";

		public bool fx;

		public float ss;

		public bool load;

		public bool MM;

		public bool Hp;

		public string dz1 = "0";

		public string dz2 = "0";

		public bool dz;

		public bool dzc;

		public string color;

		public bool wp;

		public bool lf;

		public bool gss;

		public bool ms;

		public bool pb;

		public string ON = "Name";

		public string MN = "GameObject Name";

		public string HS = "100";

		private bool Clip;

		private bool sl;

		private bool kz;

		private float speed = 25f;

		private bool Mouse;

		private bool dall;

		private float spe = 7f;

		private float pro;

		private float a = 4f;

		private bool god;

		private bool TinkyGod;
	}
	[HarmonyPatch(typeof(Application), "Quit", new System.Type[]
	{

	})]
	[System.Obsolete]
	public class Quit_Hook
	{
		[HarmonyPrefix]
		internal static bool Prefix()
		{
			return false;
		}
	}
}
