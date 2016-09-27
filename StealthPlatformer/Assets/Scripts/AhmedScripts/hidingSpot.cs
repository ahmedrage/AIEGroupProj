using UnityEngine;
using System.Collections;

public class hidingSpot : MonoBehaviour {
	public int playerLayerIndex = 9;
	public int hidingLayerIndex = 10;
	public Color hidingColor;
	public SpriteRenderer m_spriteRenderer;
	public AudioSource hidingSound;
	Color initialColor;
	// Use this for initialization
	void Start () {
		m_spriteRenderer = GetComponent<SpriteRenderer> ();
		initialColor = m_spriteRenderer.color;
	}
	
	// Update is called once per frame
	void OnTriggerEnter2D (Collider2D other) {
		if (other.gameObject.layer == playerLayerIndex) {   
			m_spriteRenderer.color = hidingColor;
			other.gameObject.layer = hidingLayerIndex;
			hidingSound.Play ();
		}
	}
	void OnTriggerExit2D (Collider2D other) {
		if (other.gameObject.layer == hidingLayerIndex) {
			m_spriteRenderer.color = initialColor;
			other.gameObject.layer = playerLayerIndex;
		}
	}
}
