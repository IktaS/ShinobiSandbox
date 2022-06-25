import enum

from numpy import isin


class Hand(enum.IntEnum):
    Right = 0
    Left = 1
    Both = 2


HandDictionary = {"RightHand": Hand.Right, "LeftHand": Hand.Left, "Both": Hand.Both}
InverseHandDictionary = {v: k for k, v in HandDictionary.items()}


class GestureInputA(enum.IntEnum):
    Aim = 0
    Fire = 1
    Lightning = 2
    Gust = 3
    EarthPrison = 4
    Recover = 5
    Shield = 6


class GestureInputB(enum.IntEnum):
    ActivateFire = 0
    ThrowFire = 1
    Recover = 2
    AimLightning = 3
    ActivateLightning = 4
    ActivateThrowEarthPrison = 5
    ThrowEarthPrison = 6
    ActivateActivationEarthPrison = 7
    ThrowActivationEarthPrison = 8
    ActivateGust = 9
    ThrowGust = 10
    Shield = 11


GestureDictionary = {
    "Aim_V1": GestureInputA.Aim,
    "Fire_V1": GestureInputA.Fire,
    "Lightning_V1": GestureInputA.Lightning,
    "Gust_V1": GestureInputA.Gust,
    "EarthPrison_V1": GestureInputA.EarthPrison,
    "Recover_V1": GestureInputA.Recover,
    "Shield_V1": GestureInputA.Shield,
    "ActivateFire_V2": GestureInputB.ActivateFire,
    "ThrowFire_V2": GestureInputB.ThrowFire,
    "Recover_V2": GestureInputB.Recover,
    "AimLightning_V2": GestureInputB.AimLightning,
    "ActivateLightning_V2": GestureInputB.ActivateLightning,
    "ActivateThrowEarthPrison_V2": GestureInputB.ActivateThrowEarthPrison,
    "ThrowEarthPrison_V2": GestureInputB.ThrowEarthPrison,
    "ActivateActivationEarthPrison_V2": GestureInputB.ActivateActivationEarthPrison,
    "ThrowActivationEarthPrison_V2": GestureInputB.ThrowActivationEarthPrison,
    "ActivateGust_V2": GestureInputB.ActivateGust,
    "ThrowGust_V2": GestureInputB.ThrowGust,
    "Shield_V2": GestureInputB.Shield,
}
InverseGestureDictionary = {v: k for k, v in GestureDictionary.items()}


class ActionA(enum.IntEnum):
    Fire = 0
    Lightning = 1
    Gust = 2
    ShootEarthPrison = 3
    ActivateEarthPrison = 4
    Shield = 5
    Recover = 6


class ActionB(enum.IntEnum):
    Fire = 0
    Lightning = 1
    Gust = 2
    ShootEarthPrison = 3
    ActivateEarthPrison = 4
    Shield = 5
    Recover = 6


ActionDictionary = {
    "ShootFire_V1": ActionA.Fire,
    "ShootLightning_V1": ActionA.Lightning,
    "ShootGust_V1": ActionA.Gust,
    "ShootEarthPrison_V1": ActionA.ShootEarthPrison,
    "ActivateEarthPrison_V1": ActionA.ActivateEarthPrison,
    "ActivateShield_V1": ActionA.Shield,
    "ActivateRecover_V1": ActionA.Recover,
    "ShootFire_V2": ActionB.Fire,
    "ShootLightning_V2": ActionB.Lightning,
    "ShootGust_V2": ActionB.Gust,
    "ShootEarthPrison_V2": ActionB.ShootEarthPrison,
    "ActivateEarthPrison_V2": ActionB.ActivateEarthPrison,
    "ActivateShield_V2": ActionB.Shield,
    "ActivateRecover_V2": ActionB.Recover,
}
InverseActionDictionary = {v: k for k, v in ActionDictionary.items()}


def GetActionType(Action):
    if isinstance(Action, ActionA):
        return "A"
    if isinstance(Action, ActionB):
        return "B"
    return "Unknown"
