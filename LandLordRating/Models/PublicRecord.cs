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
        [Display(Name = "Criminal")]
        Criminal,
        [Display(Name = "Civil")]
        Civil,
        [Display(Name = "Domestic")]
        Domestic
    }

    public enum CriminalTypes
    {
        [Display(Name = "Petty Theft")]
        PettyTheft,
        [Display(Name = "Prostitution")]
        Prostitution,
        [Display(Name = "Vandalism")]
        Vandalism,
        [Display(Name = "Drunk Driving")]
        DrunkDriving,
        [Display(Name = "Murder")]
        Murder,
        [Display(Name = "Robbery")]
        Robbery,
        [Display(Name = "Rape")]
        Rape,
        [Display(Name = "Drug Possession")]
        DrugPossession,
        [Display(Name = "Drug Trafficking/Distribution")]
        DrugTraffickingDistribution,
        [Display(Name = "Other Crime")]
        OtherCrime
    }

    public enum CivilTypes
    {
        [Display(Name = "Small Claims")]
        SmallClaims,
        [Display(Name = "Delinquent Tax")]
        DelinquentTax,
        [Display(Name = "Eviction")]
        Eviction,
        [Display(Name = "Unlawful Detainer")]
        UnlawfulDetainer,
        [Display(Name = "Breach Of Contract")]
        BreachOfContract,
        [Display(Name = "Foreclosure")]
        Foreclosure,
        [Display(Name = "LandLord Complaint")]
        LandLordComplaint,
        [Display(Name = "Will")]
        Will,
        [Display(Name = "Guardianship")]
        Guardianship,
        [Display(Name = "Traffic Violation")]
        TrafficViolation,
        [Display(Name = "Employment Discrimination")]
        EmploymentDiscrimination,
        [Display(Name = "Other Civil Case")]
        OtherCivilCase
    }

    public enum DomesticTypes
    {
        [Display(Name = "Divorce")]
        Divorce,
        [Display(Name = "Custody Hearing")]
        CustodyHearing,
        [Display(Name = "Adopton")]
        Adoption,
        [Display(Name = "Protection Orders")]
        ProtectionOrders,
        [Display(Name = "Abuse and Neglect")]
        AbuseAndNeglect,
        [Display(Name = "Child Support")]
        ChildSupport,
        [Display(Name = "Other Domestic Case")]
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
        [DataType(DataType.Date)]
        [Display(Name = "Date Filed")]
        public DateTime DateFiled { get; set; }
        [Required]
        [Display(Name = "Case URL")]
        [Url(ErrorMessage = "Please enter a valid url")]
        [StringLength(120, ErrorMessage = "Case URL cannot be longer than 120 characters.")]
        public string CaseURL { get; set; }
        [Required]
        [Display(Name = "Plaintiff Or Defendant")]
        public PlaintiffOrDefendant PlaintiffOrDefendant { get; set; }

        public bool IsApproved { get; set; }
        public bool IsDeclined { get; set; }

        public virtual LandLord LandLord { get; set; }

    }
}