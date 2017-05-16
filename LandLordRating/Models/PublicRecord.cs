using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace LandLordRating.Models
{
    public enum RecordTypes
    {
        [Description("Criminal")]
        Criminal,
        [Description("Civil")]
        Civil,
        [Description("Domestic")]
        Domestic
    }

    public enum CriminalTypes
    {
        [Description("Petty Theft")]
        PettyTheft,
        [Description("Prostitution")]
        Prostitution,
        [Description("Vandalism")]
        Vandalism,
        [Description("Drunk Driving")]
        DrunkDriving,
        [Description("Murder")]
        Murder,
        [Description("Robbery")]
        Robbery,
        [Description("Rape")]
        Rape,
        [Description("Drug Possession")]
        DrugPossession,
        [Description("Drug Trafficking/Distribution")]
        DrugTraffickingDistribution,
        [Description("Other Crime")]
        OtherCrime
    }

    public enum CivilTypes
    {
        [Description("Small Claims")]
        SmallClaims,
        [Description("Delinquent Tax")]
        DelinquentTax,
        [Description("Eviction")]
        Eviction,
        [Description("Unlawful Detainer")]
        UnlawfulDetainer,
        [Description("Breach Of Contract")]
        BreachOfContract,
        [Description("Foreclosure")]
        Foreclosure,
        [Description("LandLord Complaint")]
        LandLordComplaint,
        [Description("Will")]
        Will,
        [Description("Guardianship")]
        Guardianship,
        [Description("Traffic Violation")]
        TrafficViolation,
        [Description("Employment Discrimination")]
        EmploymentDiscrimination,
        [Description("Other Civil Case")]
        OtherCivilCase
    }

    public enum DomesticTypes
    {
        [Description("Divorce")]
        Divorce,
        [Description("Custody Hearing")]
        CustodyHearing,
        [Description("Adopton")]
        Adoption,
        [Description("Protection Orders")]
        ProtectionOrders,
        [Description("Abuse and Neglect")]
        AbuseAndNeglect,
        [Description("Child Support")]
        ChildSupport,
        [Description("Other Domestic Case")]
        OtherDomesticCase
    }

    public enum PlaintiffOrDefendant
    {
        Plaintiff,
        Defendant
    }

    public class PublicRecord
    {
        [Key]
        public int PublicRecordId { get; set; }
        [Required]
        [Display(Name = "Record Type")]
        public RecordTypes RecordType { get; set; }
        [Display(Name = "Criminal Case Type")]
        public CriminalTypes? CriminalType { get; set; }
        [Display(Name = "Civil Case Type")]
        public CivilTypes? CivilType { get; set; }
        [Display(Name = "Domestic Case Type")]
        public DomesticTypes? DomesticType { get; set; }
        [Required]
        [Display(Name = "Case Name")]
        [StringLength(40, ErrorMessage = "Case Name cannot be longer than 40 characters.")]
        public string CaseName { get; set; }
        [Required]
        [Display(Name = "Case Number")]
        [StringLength(25, ErrorMessage = "Case Number cannot be longer than 25 characters.")]
        public string CaseNumber { get; set; }
        [Required]
        [Display(Name = "Date Filed")]
        public DateTime DateFiled { get; set; }
        [Required]
        [Display(Name = "Case URL")]
        [StringLength(100, ErrorMessage = "Case URL cannot be longer than 100 characters.")]
        public string CaseURL { get; set; }
        [Required]
        [Display(Name = "Plaintiff Or Defendant")]
        public PlaintiffOrDefendant PlaintiffOrDefendant { get; set; }

        public bool IsApproved { get; set; }
        public bool IsDeclined { get; set; }

        public virtual LandLord LandLord { get; set; }

    }
}