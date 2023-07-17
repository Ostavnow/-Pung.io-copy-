using UnityEngine;

public static class AlgorithmsBot
{
    public static Vector3 AvoidingObstacles(Vector3 position,Vector2 direction)
    {
        Vector3 targetDirection = new Vector3(0,0,0);
        if(Physics2D.Raycast(position,Vector2.down,2))
        {
            
            if(direction.x > 0)
            targetDirection = new Vector3(position.x + 5,position.y + Random.value,0);
            else if(direction.x < 0)
            targetDirection = new Vector3(position.x - 5,position.y + Random.value,0);
            else if(Physics2D.Raycast(position,Vector2.left))
            targetDirection = new Vector3(position.x + 5,position.y + Random.value,0);
            else if(Physics2D.Raycast(position,Vector2.right))
            targetDirection = new Vector3(position.x - 5,position.y + Random.value,0);
        }
        else if(Physics2D.Raycast(position,Vector2.up,2))
        {
            if(direction.x > 0)
            targetDirection = new Vector3(position.x + 5,position.y + Random.value,0);
            else if(direction.x < 0)
            targetDirection = new Vector3(position.x - 5,position.y + Random.value,0);
            else if(Physics2D.Raycast(position,Vector2.left))
            targetDirection = new Vector3(position.x + 5,position.y + Random.value,0);
            else if(Physics2D.Raycast(position,Vector2.right))
            targetDirection = new Vector3(position.x - 5,position.y + Random.value,0);
        }
        else if(Physics2D.Raycast(position,Vector2.left,2))
        {
            if(direction.y > 0)
            targetDirection = new Vector3(position.x + Random.value,position.y + 5,0);
            else if(direction.y < 0)
            targetDirection = new Vector3(position.x + Random.value,position.y - 5,0);
            else if(Physics2D.Raycast(position,Vector2.down))
            targetDirection = new Vector3(position.x + Random.value,position.y + 5,0);
            else if(Physics2D.Raycast(position,Vector2.up))
            targetDirection = new Vector3(position.x + Random.value,position.y - 5,0);
        }
        else if(Physics2D.Raycast(position,Vector2.right,2))
        {
            if(direction.y > 0)
            targetDirection = new Vector3(position.x + Random.value,position.y + 5,0);
            else if(direction.y < 0)
            targetDirection = new Vector3(position.x + Random.value,position.y - 5,0);
            else if(Physics2D.Raycast(position,Vector2.down))
            targetDirection = new Vector3(position.x - Random.value,position.y + 5,0);
            else if(Physics2D.Raycast(position,Vector2.up))
            targetDirection = new Vector3(position.x - Random.value,position.y - 5,0);
        }
        return targetDirection.normalized;
    }
    public static Collider2D[] OverlapBoxAllPuchingBag(Vector3 position)
    {
        Collider2D[] colliders = Physics2D.OverlapBoxAll(position,new Vector2(16,10),0);
        Collider2D[] layerMaskColliders = new Collider2D[5];
        for(int i = 0,y = 0;i < colliders.Length && y < 5;i++)
        {
            if(colliders[i].gameObject.layer == 7)
            {
                layerMaskColliders[y] = colliders[i];
                y++;
            }
        }
        return layerMaskColliders;
    }
    public static Collider2D[] OverlapBoxAllPlayer(Vector3 position)
    {
        Collider2D[] colliders = Physics2D.OverlapBoxAll(position,new Vector2(16,10),0);
        int numberOfPlayerColliders = colliders.Length;
        int numberSortedItems = 0;
        for(int numberCheckedItems = 0;numberCheckedItems < colliders.Length;numberCheckedItems++)
        {
            if(colliders[numberCheckedItems].gameObject.layer != 3)
            {
                numberOfPlayerColliders--;
            }
            else if(colliders[numberCheckedItems].gameObject.layer == 3)
            {
                if(numberCheckedItems != numberSortedItems)
                {
                    colliders[numberSortedItems] = colliders[numberCheckedItems];
                    colliders[numberCheckedItems] = null;
                    numberSortedItems++;
                }
                else
                {
                    numberSortedItems++;
                }
            }
        }
        for(int i = numberOfPlayerColliders - 1;i > 0;)
        {
            if(Vector2.Distance(colliders[i - 1].transform.position,position) > Vector2.Distance(colliders[i].transform.position,position))
            {
                int y = i;
                while(y != 0 && Vector2.Distance(colliders[y - 1].transform.position,position) > Vector2.Distance(colliders[y].transform.position,position))
                {
                    Debug.LogWarning("Сработала сортировка");
                    Collider2D collider = colliders[y];
                    colliders[y] = colliders[y - 1];
                    colliders[y - 1] = collider;
                    y--;
                }
            }
            else
            {
                i--;
            }
        }
        Collider2D[] layerMaskColliders = new Collider2D[5];
        for(int i = 1,y = 0;i < numberOfPlayerColliders && y < 5;i++)
        {
                layerMaskColliders[y] = colliders[i];
                y++;
        }
        return layerMaskColliders;
    }
}