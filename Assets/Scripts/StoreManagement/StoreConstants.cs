using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public enum StoreArea
{
    FRONT,
    BACK,
    IN_AISLE
}


public static class StoreConstants
{

    //At what Z position do the aisles start
    public const float AisleStartZ = 5.0f;

    //At what Z position do the aisles end
    public const float AisleEndZ = 40.0f;

    //At what X position is the first aisle
    // Vector3(AisleStartX, 0, AisleStartZ) should be bottom left of all aisles
    public const float AisleStartX = 6f;

    public const float AisleWidth = 10f;
    public const float AisleLength = AisleEndZ - AisleStartZ;


    public static StoreArea GetStoreArea(Vector3 position)
    {
        if (position.z >= AisleEndZ)
        {
            return StoreArea.BACK;
        }
        else if (position.z >= AisleStartZ)
        {
            return StoreArea.IN_AISLE;
        }
        else
        {
            return StoreArea.FRONT;
        }
    }

}
    
