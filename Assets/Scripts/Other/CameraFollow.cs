using UnityEngine;
using System.Collections;

public class CameraFollow : MonoBehaviour 
{
    public Transform player;
	public Transform boss;
    public float cameraSpeed;

	public float distance;

	private Vector3 target;
	
	void Update () // Calcule un point entre le boss et le joueur et place la caméra smoothly sur ce point
    {
		target = boss.position - player.position;
		target = target.normalized;
		target = player.position + (distance * target);

		if (player)
        {
			transform.position = Vector3.Lerp(transform.position, target, cameraSpeed) + new Vector3(0, 0, -20);
        }
	}
}
