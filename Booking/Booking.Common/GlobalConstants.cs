﻿namespace Booking.Common
{
    public static class GlobalConstants
    {
        public const string SystemName = "Booking";

        public const string AdministratorRoleName = "Administrator";

        public const string OwnerRoleName = "Owner";

        // Property
        public const int PropertyNameMinLength = 3;

        public const int PropertyNameMaxLength = 150;

        public const int PropertyAddressMinLength = 5;

        public const int PropertyAddressMaxLength = 200;

        public const int PropertyDescriptionMaxLength = 500;

        public const int PropertyFloorMin = 1;

        public const int PropertyFloorMax = 75;

        public const string PropertyRatingDisplay = "Property rating";

        public const string PropertyImagesPath = "/images/properties/";

        public const string DefaultImagePath = "/assets/images/defaults/default.png";

        public const string DateFormat = "dd/MM/yyyy";

        // Offers
        public const string OfferImagesPath = "/images/offers/";

        // PropertyCategory
        public const int PropertyCategoryNameMaxLength = 100;

        public const int PropertyCategoryMinId = 1;

        public const string PropertyCategoryDisplayName = "Property category";

        // FacilityCategory
        public const int FacilityCategoryNameMaxLength = 100;

        // Facility
        public const int FacilityNameMaxLength = 100;

        // Currency
        public const int CurrencyCodeMaxLength = 3;

        // Country
        public const int CountryNameMinLength = 2;

        public const int CountryNameMaxLength = 100;

        public const int MinCountryId = 1;

        public const string CountryDisplayName = "Country";

        // BedType
        public const int BedTypeNameMaxLength = 50;

        // PropertyType
        public const int PropertyTypeNameMaxLength = 80;

        // Rule
        public const int RuleNameMaxLength = 80;

        // Town
        public const int TownNameMaxLength = 80;

        public const int MinTownId = 1;

        public const string TownDisplayName = "Towns";

        public static class ErrorMessages
        {
            // Properies
            public const string PropertyName = "Name must be between 3 and 150 characters long.";

            public const string PropertyNameIsAlreadyUsed = "This property name is already used. Try different one!";

            public const string PropertyAddress = "Address must be between 5 and 200 characters long.";

            public const string PropertyFloors = "Floors must be between 1 and 75.";

            public const string PropertyDescription = "Description can't be more than 500 characters long.";

            // Properties Categories
            public const string PropertyCategories = "Choose property category.";

            // Town
            public const string Town = "Choose a town.";

            // Country
            public const string Country = "Choose a country.";

            // Images
            public const string ImageExtention = "Invalid image extension";

            // Consts
            public const string EditErrorKey = "EditError";

            public const string EditErrorValue = "You don't have permission to make any changes to this property or it doesn't exists.";

            public const string ByIdErrorKey = "ByIdError";

            public const string ByIdErrorValue = "The property was not found.";

            public const string OfferAccessKey = "AccessDenied";

            public const string OfferAccessValue = "Access denied. You don't have permission to edit/delete this offer or it doesn't exist.";

            public const string DeleteErrorKey = "DeleteError";

            public const string DeleteErrorValue = "Cannot delete someone else property!";
        }

        public static class SuccessMessages
        {
            // Property
            public const string AddKey = "Add";

            public const string AddValue = "Property was successfully created.";

            public const string EditKey = "Edit";

            public const string EditValue = "Property was successfully edited.";

            public const string DeleteKey = "Delete";

            public const string DeleteValue = "Property was successfully deleted.";
        }
    }
}
