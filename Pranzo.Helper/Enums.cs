namespace Lazeez.Helper
{
    public class Enums
    {
        public enum Culture : short
        {
            En = 0,
            Ar = 1
        }

        public enum UserType
        {
            None = 0,
            LazeezAdmin = 1,
            RestaurantAdmin = 2,
            Meter = 3,
            Cashier = 4,
            Chef = 5,
            Normal = 6
        }

        public enum RestaurantUserType
        {
            Meter = 3,
            Cashier = 4,
            Chef = 5,
        }

        public enum WeekDays
        {
            Saturday = 1,
            Sunday = 2,
            Monday = 3,
            Tuesday = 4,
            Wednesday = 5,
            Thursday = 6,
            Friday = 7
        }

        public enum RestaurantType
        {
            Restaurant = 1,
            Coffee = 2
        }

        public enum SourceType
        {
            Restaurant = 1,
            RestaurantMenu = 2,
            RestaurantMeal = 3
        }

        public enum RestaurantOrderStatus
        {
            Draft = 1,
            Received = 2,
            Ongoing = 3,
            Delivered = 4
        }

        // Enum ForLookup Data
        public enum LookupData
        {
            FoodType = 1,
            MealException = 2,
            Country = 3,
            City = 4,
            District = 5
        }
    }
}