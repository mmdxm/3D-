using UnityEngine;
using System.Collections;

namespace MySpace
{
    public class Move : MonoBehaviour
    {
        private const float speed = 20;
        private int status;
        //2:在船上  1:从船上岸  0:从岸下船
        private Vector3 destination;
        private Vector3 middle;

        public void SetDestination(Vector3 _pos)
        {
            destination = _pos;
            middle = _pos;
            if (_pos.y == transform.position.y)
            {          // Boat moving
                status = 2;
            }
            else if (_pos.y < transform.position.y)
            {    // Character from coast to boat
                middle.y = transform.position.y;
            }
            else
            {                                        // Character from boat to coast
                middle.x = transform.position.x;
            }
            status = 1;
        }

        private void Update()
        {
            if (status == 1)
            {
                transform.position = Vector3.MoveTowards(transform.position, middle, speed * Time.deltaTime);
                if (transform.position == middle)
                {
                    status = 2;
                }
            }
            else if (status == 2)
            {
                transform.position = Vector3.MoveTowards(transform.position, destination, speed * Time.deltaTime);
                if (transform.position == destination)
                {
                    status = 0;
                }
            }
        }
        public void Reset()
        {
            status = 0;
        }
    }
}
