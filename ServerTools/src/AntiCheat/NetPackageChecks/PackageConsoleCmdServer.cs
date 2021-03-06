﻿using HarmonyLib;
using System;

namespace ServerTools
{
    class PackageConsoleCmdServer
    {
        static AccessTools.FieldRef<NetPackageConsoleCmdServer, int> _entityId = AccessTools.FieldRefAccess<NetPackageConsoleCmdServer, int>("playerEntityId");
        static AccessTools.FieldRef<NetPackageConsoleCmdServer, string> _command = AccessTools.FieldRefAccess<NetPackageConsoleCmdServer, string>("cmd");

        public static bool PackageConsoleCmdServer_ProcessPackage_Prefix(NetPackageConsoleCmdServer __instance, World _world)
        {
            try
            {
                if (Packages.IsEnabled && __instance.Sender != null)
                {
                    ClientInfo _cInfo = __instance.Sender;
                    if (_cInfo.entityId != _entityId(__instance))
                    {
                        Log.Out(string.Format("[SERVERTOOLS] Detected erroneous data NetPackageConsoleCmdServer uploaded by steam id = {0}, owner id = {1} entity id = {2} name = {3}. Attempted running console command {4} with mismatched entity id {5}", __instance.Sender.playerId, __instance.Sender.ownerId, __instance.Sender.entityId, __instance.Sender.playerName, _command(__instance), _entityId(__instance)));
                        Packages.Writer(_cInfo.ownerId, _cInfo.playerId, _cInfo.playerName, string.Format("Attempted running console command {0} with mismatched entity id {1}", _command(__instance), _entityId(__instance)));
                        Packages.Ban(_cInfo.ownerId, _cInfo.playerId, _cInfo.playerName);
                        return false;
                    }
                    //else if (!GameManager.Instance.adminTools.CommandAllowedFor(new string[] { _command(__instance) }, _cInfo) && !_command(__instance).ToLower().Contains("help"))
                    //{
                    //    Log.Out(string.Format("[SERVERTOOLS] Detected erroneous data NetPackageConsoleCmdServer uploaded by steam id = {0}, owner id = {1} entity id = {2} name = {3}. Attempted running console command {4} without permission", __instance.Sender.playerId, __instance.Sender.ownerId, __instance.Sender.entityId, __instance.Sender.playerName, _command(__instance)));
                    //    Packages.Kick(_cInfo.playerId, _cInfo.playerName);
                    //    Packages.Writer(_cInfo.ownerId, _cInfo.playerId, _cInfo.playerName, string.Format("Attempted running console command {0} without permission", _command(__instance)));
                    //    return false;
                    //}
                }
            }
            catch (Exception e)
            {
                Log.Out(string.Format("[SERVERTOOLS] Error in PackagePersistentPlayerState.PackagePersistentPlayerState_ProcessPackage_Prefix: {0}", e.Message));
            }
            return true;
        }
    }
}
