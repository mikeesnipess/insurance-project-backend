using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace insurance_project_backend.Models.FMCSA
{
    public class CarrierInfoResponseModel
    {
        [JsonPropertyName("content")]
        public List<Content> Content { get; set; }

        [JsonPropertyName("retrievalDate")]
        public string RetrievalDate { get; set; }
    }

    public class ContentModel
    {
        [JsonPropertyName("_links")]
        public Links Links { get; set; }

        [JsonPropertyName("carrier")]
        public Carrier Carrier { get; set; }
    }

    public class LinksModel
    {
        [JsonPropertyName("basics")]
        public Link Basics { get; set; }

        [JsonPropertyName("cargo carried")]
        public Link CargoCarried { get; set; }

        [JsonPropertyName("operation classification")]
        public Link OperationClassification { get; set; }

        [JsonPropertyName("docket numbers")]
        public Link DocketNumbers { get; set; }

        [JsonPropertyName("carrier active-For-hire authority")]
        public Link CarrierAuthority { get; set; }

        [JsonPropertyName("self")]
        public Link Self { get; set; }
    }

    public class LinkModel
    {
        [JsonPropertyName("href")]
        public string Href { get; set; }
    }

    public class CarrierModel
    {
        [JsonPropertyName("allowedToOperate")]
        public string AllowedToOperate { get; set; }

        [JsonPropertyName("bipdInsuranceOnFile")]
        public string BipdInsuranceOnFile { get; set; }

        [JsonPropertyName("bipdInsuranceRequired")]
        public string BipdInsuranceRequired { get; set; }

        [JsonPropertyName("bipdRequiredAmount")]
        public string BipdRequiredAmount { get; set; }

        [JsonPropertyName("bondInsuranceOnFile")]
        public string BondInsuranceOnFile { get; set; }

        [JsonPropertyName("bondInsuranceRequired")]
        public string BondInsuranceRequired { get; set; }

        [JsonPropertyName("brokerAuthorityStatus")]
        public string BrokerAuthorityStatus { get; set; }

        [JsonPropertyName("cargoInsuranceOnFile")]
        public string CargoInsuranceOnFile { get; set; }

        [JsonPropertyName("cargoInsuranceRequired")]
        public string CargoInsuranceRequired { get; set; }

        [JsonPropertyName("carrierOperation")]
        public CarrierOperation CarrierOperation { get; set; }

        [JsonPropertyName("censusTypeId")]
        public CensusTypeId CensusTypeId { get; set; }

        [JsonPropertyName("commonAuthorityStatus")]
        public string CommonAuthorityStatus { get; set; }

        [JsonPropertyName("contractAuthorityStatus")]
        public string ContractAuthorityStatus { get; set; }

        [JsonPropertyName("crashTotal")]
        public int? CrashTotal { get; set; }

        [JsonPropertyName("dbaName")]
        public string DbaName { get; set; }

        [JsonPropertyName("dotNumber")]
        public int? DotNumber { get; set; }

        [JsonPropertyName("driverInsp")]
        public int? DriverInsp { get; set; }

        [JsonPropertyName("driverOosInsp")]
        public decimal? DriverOosInsp { get; set; }

        [JsonPropertyName("driverOosRate")]
        public decimal? DriverOosRate { get; set; }

        [JsonPropertyName("driverOosRateNationalAverage")]
        public string DriverOosRateNationalAverage { get; set; }

        [JsonPropertyName("ein")]
        public long? Ein { get; set; }

        [JsonPropertyName("fatalCrash")]
        public int? FatalCrash { get; set; }

        [JsonPropertyName("hazmatInsp")]
        public int? HazmatInsp { get; set; }

        [JsonPropertyName("hazmatOosInsp")]
        public int? HazmatOosInsp { get; set; }

        [JsonPropertyName("hazmatOosRate")]
        public int? HazmatOosRate { get; set; }

        [JsonPropertyName("hazmatOosRateNationalAverage")]
        public string HazmatOosRateNationalAverage { get; set; }

        [JsonPropertyName("injCrash")]
        public int? InjCrash { get; set; }

        [JsonPropertyName("isPassengerCarrier")]
        public string IsPassengerCarrier { get; set; }

        [JsonPropertyName("issScore")]
        public string IssScore { get; set; }

        [JsonPropertyName("legalName")]
        public string LegalName { get; set; }

        [JsonPropertyName("mcs150Outdated")]
        public string Mcs150Outdated { get; set; }

        [JsonPropertyName("oosDate")]
        public string OosDate { get; set; }

        [JsonPropertyName("oosRateNationalAverageYear")]
        public string OosRateNationalAverageYear { get; set; }

        [JsonPropertyName("phyCity")]
        public string PhyCity { get; set; }

        [JsonPropertyName("phyCountry")]
        public string PhyCountry { get; set; }

        [JsonPropertyName("phyState")]
        public string PhyState { get; set; }

        [JsonPropertyName("phyStreet")]
        public string PhyStreet { get; set; }

        [JsonPropertyName("phyZipcode")]
        public string PhyZipcode { get; set; }

        [JsonPropertyName("reviewDate")]
        public string ReviewDate { get; set; }

        [JsonPropertyName("reviewType")]
        public string ReviewType { get; set; }

        [JsonPropertyName("safetyRating")]
        public string SafetyRating { get; set; }

        [JsonPropertyName("safetyRatingDate")]
        public string SafetyRatingDate { get; set; }

        [JsonPropertyName("safetyReviewDate")]
        public string SafetyReviewDate { get; set; }

        [JsonPropertyName("safetyReviewType")]
        public string SafetyReviewType { get; set; }

        [JsonPropertyName("snapshotDate")]
        public string SnapshotDate { get; set; }

        [JsonPropertyName("statusCode")]
        public string StatusCode { get; set; }

        [JsonPropertyName("totalDrivers")]
        public int? TotalDrivers { get; set; }

        [JsonPropertyName("totalPowerUnits")]
        public int? TotalPowerUnits { get; set; }

        [JsonPropertyName("towawayCrash")]
        public int? TowawayCrash { get; set; }

        [JsonPropertyName("vehicleInsp")]
        public int? VehicleInsp { get; set; }

        [JsonPropertyName("vehicleOosInsp")]
        public int? VehicleOosInsp { get; set; }

        [JsonPropertyName("vehicleOosRate")]
        public double? VehicleOosRate { get; set; }

        [JsonPropertyName("vehicleOosRateNationalAverage")]
        public string VehicleOosRateNationalAverage { get; set; }
    }

    public class CarrierOperationModel
    {
        [JsonPropertyName("carrierOperationCode")]
        public string CarrierOperationCode { get; set; }

        [JsonPropertyName("carrierOperationDesc")]
        public string CarrierOperationDesc { get; set; }
    }

    public class CensusTypeIdModel
    {
        [JsonPropertyName("id")]
        public int Id { get; set; }

        [JsonPropertyName("name")]
        public string Name { get; set; }
    }
}
