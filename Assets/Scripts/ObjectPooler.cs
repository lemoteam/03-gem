using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPooler : MonoBehaviour {

	[System.Serializable]
	public class Pool
	{
		public string tag;
		public GameObject prefab;
		public int radius;
		public int size; 
	}


	public List<Pool> pools;
	public Dictionary<string, Queue<GameObject>> poolDictionary;

	
	void Start()
	{
		poolDictionary = new Dictionary<string, Queue<GameObject>>();

		foreach (var pool in pools)
		{
			var objectPool = new Queue<GameObject>();

			for (var i = 0; i < pool.size; i++)
			{
				// Calc size
				var angle = (360 / pool.size) * i;
				var posX = Math.Sin((angle * Math.PI / 180)) * pool.radius;
				var posY = Math.Cos((angle * Math.PI / 180)) * pool.radius;
				
				GameObject obj = Instantiate(pool.prefab);
				obj.SetActive(true);
				obj.transform.position = new Vector3(ToSingle(posX), 0, ToSingle(posY));
				objectPool.Enqueue(obj);
			}

			poolDictionary.Add(pool.tag, objectPool);
		}
	}


	public void Levitate(string tag) {
		if (!poolDictionary.ContainsKey(tag)) return;
		foreach (var pool in pools) {
			for (var i = 0; i < pool.size; i++)
			{
				var objectToSpawn = poolDictionary[tag].Dequeue();
				var gemElement = objectToSpawn.GetComponent<GemElement>();
				gemElement.instance.LevitationOn(); 
				poolDictionary[tag].Enqueue(objectToSpawn);
			}
		}
	}



	public void Gravitate(string tag)
	{
		if (!poolDictionary.ContainsKey(tag)) return;
		foreach (var pool in pools) {
			for (var i = 0; i < pool.size; i++)
			{
				var objectToSpawn = poolDictionary[tag].Dequeue();
				var gemElement = objectToSpawn.GetComponent<GemElement>();
				gemElement.instance.LevitationOff(); 
				poolDictionary[tag].Enqueue(objectToSpawn);	
			}
		}
	}
	

	
	private static float ToSingle(double value)
	{
		return (float)value;
	}
}
