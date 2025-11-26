using ParkingManagementAPI.Exceptions;
using ParkingManagementAPI.Models.Database;

namespace ParkingManagementAPI.Services.Helpers
{
    public class ChargeCalculationHelper
    {
        //in minutes. Better if it was in settings.json Could be modified without redeploying solution
        private const int ADDITIONAL_CHARGE_INTERVAL = 5;

        //Assuming: wasn't sure about the requirement "Vehicles will be charged per minute they are parked". Example: Should 5min 10s parking time calculate charge for 6minutes? True = yes (6min). False = no (5min).
        private const bool STARTED_MINUTES_COUNT_AS_PARKED = false;

        public double CalculateFinalCharge(ParkingSpotAllocation allocation)
        {
            var chargeRate = allocation.ChargeRate.ChargePerMinute;
            var additionalCharge = allocation.ChargeRate.AdditionalCharge;

            var parkingTimeInSeconds = GetSecondsParked(allocation);

            if (parkingTimeInSeconds <= 0) throw new IncorrectParkingException("Can't calculate charge. Vehicle is still parked");

            var parkingTimeInMinutes = parkingTimeInSeconds / 60;

            var finalCharge = CalculatePerMinuteCharge(chargeRate, parkingTimeInMinutes) + CalculateAdditionalCharge(additionalCharge, parkingTimeInMinutes);

            return finalCharge;
        }

        private double GetSecondsParked(ParkingSpotAllocation allocation)
        {
            if (allocation.EndDateTime is null) return 0;

            var secondsParked = ((DateTime)allocation.EndDateTime - allocation.StartDateTime).TotalSeconds;

            return secondsParked;
        }

        private double CalculatePerMinuteCharge(double chargeRate, double minutes)
        {
            var countOfCharges = RoundMinutes(minutes);

            return countOfCharges * chargeRate;
        }

        private double CalculateAdditionalCharge(double additionalCharge, double minutes)
        {
            var countOfCharges = Math.Round(RoundMinutes(minutes) / ADDITIONAL_CHARGE_INTERVAL, 0, MidpointRounding.ToZero);

            return countOfCharges * additionalCharge;
        }

        private double RoundMinutes(double minutes) => STARTED_MINUTES_COUNT_AS_PARKED ? Math.Round(minutes, 0, MidpointRounding.AwayFromZero) : Math.Round(minutes, 0, MidpointRounding.ToZero);
    }
}
