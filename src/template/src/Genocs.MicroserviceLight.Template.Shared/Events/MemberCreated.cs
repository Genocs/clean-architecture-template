namespace UTU.TaxFree.DTO.Member.Bus.Events
{
    using Genocs.MicroserviceLight.Template.Shared.Interfaces;

    public class MemberCreated : IEvent
    {
        public string MemberId { get; set; }
        public string BalanceId { get; set; }
        public string CountryOfResidence { get; set; }
        public string WalletId { get; set; }
        public string UtuPlusBalanceId { get; set; }
        public string CurrencyId { get; set; }
        public int AccountCurrencyPrecision { get; set; }
        public string MerchantId { get; set; }
        public string RegistrationCountry { get; set; }
    }
}

