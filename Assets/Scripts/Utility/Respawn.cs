using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Foundry
{
    public class Respawn : MonoBehaviour
    {
        public float floorPlaneHeight;
        public float maxDepth = 100;

        public Vector3 respawnPosition;
        
        private void Update()
        {
            float depth = floorPlaneHeight - transform.position.y;

            if (depth > maxDepth)
            {
                transform.position = respawnPosition;
                depth = floorPlaneHeight;
            }

            
        }

    }
}