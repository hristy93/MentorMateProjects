namespace ImmigrantsInvasion
{
    interface IKillPeople
    {
        void KillPeople(int weaponsCount);
        bool TryToBuyWeapon();
        void BuyNeededWeapons(int weaponsCount);
        double GetVictimsPercentage();
    }
}
