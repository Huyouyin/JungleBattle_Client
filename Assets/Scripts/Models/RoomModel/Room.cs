using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Common;

public class Room {
    public int roomId;
    public RoomStatus roomStatus;   //房间状态
    public string ownerName;   //房主ID
    public string rivavlName;    //竞赛者ID

    public Room(int roomid , string ownername)
    {
        this.roomId = roomid;
        this.ownerName = ownername;
    }
}
