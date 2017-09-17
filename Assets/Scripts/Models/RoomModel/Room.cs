using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Common;

public class Room {
    public int roomId;
    public RoomStatus roomStatus;   //房间状态
    public int RoomOwnerId;   //房主ID
    public int rivavlId;    //竞赛者ID

    public Room(int roomid , int ownerid)
    {
        this.roomId = roomid;
        this.RoomOwnerId = ownerid;
    }
}
