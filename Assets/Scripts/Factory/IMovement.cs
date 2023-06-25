using Assignment.Core;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace Assignment.Factory
{

    public interface IMovement 
    {
    /// <summary>
    /// IMovement to implement state based pattern for the movement control
    /// </summary>
    /// <param name="rb"></param>
        void Execute(Rigidbody2D rb);
    }
}
