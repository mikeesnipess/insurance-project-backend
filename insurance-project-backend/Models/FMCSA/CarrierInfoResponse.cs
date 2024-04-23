using System.Text.Json.Serialization;

namespace insurance_project_backend.Models.FMCSA
{
    public record CarrierInfoResponse(
        [property: JsonPropertyName("content")] List<Content> Content,
        [property: JsonPropertyName("retrievalDate")] string RetrievalDate
    );

    public record Content(
        [property: JsonPropertyName("_links")] Links Links,
        [property: JsonPropertyName("carrier")] Carrier Carrier
    );

    public record Links(
        [property: JsonPropertyName("basics")] Link Basics,
        [property: JsonPropertyName("cargo carried")] Link CargoCarried,
        [property: JsonPropertyName("operation classification")] Link OperationClassification,
        [property: JsonPropertyName("docket numbers")] Link DocketNumbers,
        [property: JsonPropertyName("carrier active-For-hire authority")] Link CarrierAuthority,
        [property: JsonPropertyName("self")] Link Self
    );

    public record Link(
        [property: JsonPropertyName("href")] string Href
    );

    public record Carrier(
        [property: JsonPropertyName("allowedToOperate")] string AllowedToOperate,
        [property: JsonPropertyName("bipdInsuranceOnFile")] string? BipdInsuranceOnFile,
        [property: JsonPropertyName("bipdInsuranceRequired")] string? BipdInsuranceRequired,
        [property: JsonPropertyName("bipdRequiredAmount")] string? BipdRequiredAmount,
        [property: JsonPropertyName("bondInsuranceOnFile")] string? BondInsuranceOnFile,
        [property: JsonPropertyName("bondInsuranceRequired")] string? BondInsuranceRequired,
        [property: JsonPropertyName("brokerAuthorityStatus")] string? BrokerAuthorityStatus,
        [property: JsonPropertyName("cargoInsuranceOnFile")] string? CargoInsuranceOnFile,
        [property: JsonPropertyName("cargoInsuranceRequired")] string? CargoInsuranceRequired,
        [property: JsonPropertyName("carrierOperation")] CarrierOperation? CarrierOperation,
        [property: JsonPropertyName("censusTypeId")] CensusTypeId? CensusTypeId,
        [property: JsonPropertyName("commonAuthorityStatus")] string? CommonAuthorityStatus,
        [property: JsonPropertyName("contractAuthorityStatus")] string? ContractAuthorityStatus,
        [property: JsonPropertyName("crashTotal")] int? CrashTotal,
        [property: JsonPropertyName("dbaName")] string DbaName,
        [property: JsonPropertyName("dotNumber")] int? DotNumber,
        [property: JsonPropertyName("driverInsp")] int? DriverInsp,
        [property: JsonPropertyName("driverOosInsp")] int? DriverOosInsp,
        [property: JsonPropertyName("driverOosRate")] int? DriverOosRate,
        [property: JsonPropertyName("driverOosRateNationalAverage")] string? DriverOosRateNationalAverage,
        [property: JsonPropertyName("ein")] long? Ein,
        [property: JsonPropertyName("fatalCrash")] int? FatalCrash,
        [property: JsonPropertyName("hazmatInsp")] int? HazmatInsp,
        [property: JsonPropertyName("hazmatOosInsp")] int? HazmatOosInsp,
        [property: JsonPropertyName("hazmatOosRate")] int? HazmatOosRate,
        [property: JsonPropertyName("hazmatOosRateNationalAverage")] string? HazmatOosRateNationalAverage,
        [property: JsonPropertyName("injCrash")] int? InjCrash,
        [property: JsonPropertyName("isPassengerCarrier")] string? IsPassengerCarrier,
        [property: JsonPropertyName("issScore")] string? IssScore,
        [property: JsonPropertyName("legalName")] string LegalName,
        [property: JsonPropertyName("mcs150Outdated")] string? Mcs150Outdated,
        [property: JsonPropertyName("oosDate")] string? OosDate,
        [property: JsonPropertyName("oosRateNationalAverageYear")] string? OosRateNationalAverageYear,
        [property: JsonPropertyName("phyCity")] string PhyCity,
        [property: JsonPropertyName("phyCountry")] string PhyCountry,
        [property: JsonPropertyName("phyState")] string PhyState,
        [property: JsonPropertyName("phyStreet")] string PhyStreet,
        [property: JsonPropertyName("phyZipcode")] string PhyZipcode,
        [property: JsonPropertyName("reviewDate")] string? ReviewDate,
        [property: JsonPropertyName("reviewType")] string? ReviewType,
        [property: JsonPropertyName("safetyRating")] string? SafetyRating,
        [property: JsonPropertyName("safetyRatingDate")] string? SafetyRatingDate,
        [property: JsonPropertyName("safetyReviewDate")] string? SafetyReviewDate,
        [property: JsonPropertyName("safetyReviewType")] string? SafetyReviewType,
        [property: JsonPropertyName("snapshotDate")] string? SnapshotDate,
        [property: JsonPropertyName("statusCode")] string StatusCode,
        [property: JsonPropertyName("totalDrivers")] int? TotalDrivers,
        [property: JsonPropertyName("totalPowerUnits")] int? TotalPowerUnits,
        [property: JsonPropertyName("towawayCrash")] int? TowawayCrash,
        [property: JsonPropertyName("vehicleInsp")] int? VehicleInsp,
        [property: JsonPropertyName("vehicleOosInsp")] int? VehicleOosInsp,
        [property: JsonPropertyName("vehicleOosRate")] double? VehicleOosRate,
        [property: JsonPropertyName("vehicleOosRateNationalAverage")] string? VehicleOosRateNationalAverage
    );

    public record CarrierOperation(
        [property: JsonPropertyName("carrierOperationCode")] string CarrierOperationCode,
        [property: JsonPropertyName("carrierOperationDesc")] string CarrierOperationDesc
    );

    public record CensusTypeId(
        [property: JsonPropertyName("id")] int Id,
        [property: JsonPropertyName("name")] string Name
    );
}
