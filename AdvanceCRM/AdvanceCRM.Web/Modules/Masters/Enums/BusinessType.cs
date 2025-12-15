using Serenity.ComponentModel;
using System.ComponentModel;

namespace AdvanceCRM.Masters
{
    [EnumKey("Masters.BusinessType")]
    public enum BusinessTypeMaster
    {
        [Description("Central Government")]
        CentralGovernment = 1,
        [Description("State Government")]
        StateGovernment = 2,
        [Description("Statutory body (Central Govt.)")]
        StatutorybodyCentralGovt = 3,
        [Description("Statutory body (State Govt.)")]
        StatutorybodyStateGovt = 4,
        [Description("Autonomous body(Central Govt.)")]
        AutonomousbodyCentralGovt = 5,
        [Description("Autonomous body(State Govt.)")]
        AutonomousbodyStateGovt = 6,
        [Description("Local Authority(Central Govt.)")]
        LocalAuthorityCentralGovt = 7,
        [Description("Local Authority(State Govt.)")]
        LocalAuthorityStateGovt = 8,
        [Description("Company")]
        Company = 9,
        [Description("Branch/Division of Company")]
        BranchDivisionofCompany = 10,
        [Description("Association of Person (AOP)")]
        AssociationofPersonAOP = 11,
        [Description("Association of Person (Trust)")]
        AssociationofPersonTrust = 12,
        [Description("Artificial Juridical  Person")]
        ArtificialJuridicalPerson = 13,
        [Description("Body of Individuals")]
        BodyofIndividuals = 14,
        [Description("Individual/HUF")]
        IndividualHUF = 15,
        [Description("Firm")]
        Firm = 16
    }
}