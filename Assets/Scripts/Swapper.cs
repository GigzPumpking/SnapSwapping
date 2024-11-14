using UnityEngine;

public class Swapper : MonoBehaviour
{
    public ThrowableObject to;
    public GameObject player;
    public Vector3 object_coords;
    public Vector3 player_coords;
    /*void Update(){
        player_coords = player.transform.position;
        if(to.currentThrownObject != null){
            object_coords = to.currentThrownObject.transform.position;
        }
    }*/
    public void Swap()
    {
        object_coords = to.currentThrownObject.transform.position;
        player_coords = player.transform.position;
        if(to.currentThrownObject != null){
            player.transform.position = object_coords;
            to.currentThrownObject.transform.position = player_coords;
        }
    }
}