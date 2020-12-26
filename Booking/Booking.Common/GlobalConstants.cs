namespace Booking.Common
{
    public static class GlobalConstants
    {
        public const string SystemName = "Booking";

        public const string AdministratorRoleName = "Administrator";

        public const string OwnerRoleName = "Owner";

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

        public const int BookingMinMembers = 1;

        public const int BookingMaxMembers = 30;

        public const string OfferImagesPath = "/images/offers/";

        public const int PropertyCategoryNameMaxLength = 100;

        public const int PropertyCategoryMinId = 1;

        public const string PropertyCategoryDisplayName = "Property category";

        public const int FacilityCategoryNameMaxLength = 100;

        public const int FacilityNameMaxLength = 100;

        public const int CurrencyCodeMaxLength = 3;

        public const int CountryNameMinLength = 2;

        public const int CountryNameMaxLength = 100;

        public const int MinCountryId = 1;

        public const string CountryDisplayName = "Country";

        public const int BedTypeNameMaxLength = 50;

        public const int PropertyTypeNameMaxLength = 80;

        public const int RuleNameMaxLength = 80;

        public const int TownNameMaxLength = 80;

        public const int MinTownId = 1;

        public const string TownDisplayName = "Towns";

        public const string ValidFromDisplayName = "Valid from";

        public const string ValidToDisplayName = "Valid to";

        public const int MinOfferCount = 1;

        public const int MaxOfferCount = 20;

        public static class ErrorMessages
        {
            public const string PropertyName = "Name must be between 3 and 150 characters long.";

            public const string PropertyNameIsAlreadyUsed = "This property name is already used. Try different one!";

            public const string PropertyAddress = "Address must be between 5 and 200 characters long.";

            public const string PropertyFloors = "Floors must be between 1 and 75.";

            public const string PropertyDescription = "Description can't be more than 500 characters long.";

            public const string PropertyCategories = "Choose property category.";

            public const string Town = "Choose a town.";

            public const string Country = "Choose a country.";

            public const string ImageExtention = "Invalid image extension";

            public const string EditErrorKey = "EditError";

            public const string EditErrorValue = "You don't have permission to make any changes to this property or it doesn't exists.";

            public const string BookingErrorKey = "BookingError";

            public const string BookingErrorValue = "Something went wrong. Try again!";

            public const string ByIdErrorKey = "ByIdError";

            public const string ByIdErrorValue = "The property was not found.";

            public const string OfferAccessKey = "OfferAccessDenied";

            public const string OfferAccessValue = "You don't have permission to edit/delete this offer.";

            public const string PropertyAccessKey = "PropertyAccessDenied";

            public const string PropertyAccessValue = "Property doesn't exists or you don't have permission to access it.";

            public const string DeleteErrorKey = "DeleteError";

            public const string DeleteErrorValue = "Cannot delete someone else property!";

            public const string OfferPrice = "Price is required. It must be more than 1.00.";

            public const string OfferValidFrom = "The date cannot be before today.";

            public const string OfferValidFromRequired = "Valid from field is required.";

            public const string OfferValidToRequired = "Valid to field is required.";

            public const string OfferValidTo = "The date must be at least 2 days after valid from date.";

            public const string MembersCount = "At least one sleeping place is required. Cannot have more than 30 members per offer.";

            public const string OffersCount = "Offers must be between 1 and 20.";
        }

        public static class SuccessMessages
        {
            public const string AddKey = "Add";

            public const string AddValue = "Property was successfully created.";

            public const string EditKey = "Edit";

            public const string EditValue = "Property was successfully edited.";

            public const string DeleteKey = "Delete";

            public const string DeleteValue = "Property was successfully deleted.";

            public const string AddOfferKey = "AddOffer";

            public const string AddOfferValue = "The offer was successfully added.";

            public const string EditOfferKey = "EditOffer";

            public const string EditOfferValue = "The offer was successfully edited.";

            public const string DeleteOfferKey = "DeleteOffer";

            public const string DeleteOfferValue = "The offer was successfully deleted.";

            public const string BookingKey = "Booking";

            public const string BookingValue = "Booking was successfully canceled!";
        }
    }
}
