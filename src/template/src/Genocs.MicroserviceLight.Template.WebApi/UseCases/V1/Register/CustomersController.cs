namespace Genocs.MicroserviceLight.Template.WebApi.UseCases.V1.Register
{
    using Application.Boundaries.Register;
    using Domain.ValueObjects;
    using Genocs.MicroserviceLight.Template.WebApi.ApiClient;
    using Microsoft.AspNetCore.Http;
    using Microsoft.AspNetCore.Mvc;
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.Net.Http.Json;
    using System.Threading.Tasks;

    [ApiVersion("1.0")]
    [Route("api/v1/[controller]")]
    [ApiController]
    public sealed class CustomersController : ControllerBase
    {
        private readonly IUseCase _registerUseCase;
        private readonly RegisterPresenter _presenter;
        private readonly IMemberApi _memberApiClient;

        public CustomersController(
            IMemberApi memberApiClient,
            IUseCase registerUseCase,
            RegisterPresenter presenter)
        {
            _memberApiClient = memberApiClient;
            _registerUseCase = registerUseCase;
            _presenter = presenter;
        }

        /// <summary>
        /// Register a customer
        /// </summary>
        /// <response code="200">The registered customer was create successfully.</response>
        /// <response code="400">Bad request.</response>
        /// <response code="500">Error.</response>
        /// <param name="request">The request to register a customer</param>
        /// <returns>The newly registered customer</returns>
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(RegisterResponse))]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Post([FromBody][Required] RegisterRequest request)
        {
            var registerInput = new RegisterInput(
                new SSN(request.SSN),
                new Name(request.Name),
                new PositiveMoney(request.InitialAmount)
            );

            var response = await _memberApiClient.GetMemberAsync(true, "LU4U8TBWS");

            response.EnsureSuccessStatusCode();
            Root root = await response.Content.ReadFromJsonAsync<Root>();

            await _registerUseCase.Execute(registerInput);

            return _presenter.ViewModel;
        }
    }



    public class AutoRefundValue
    {
        public bool Value { get; set; }
        public string ProductId { get; set; }
    }

    public class IdentityDocument
    {
        public string PrimaryName { get; set; }
        public string SecondaryName { get; set; }
        public string DocumentType { get; set; }
        public string EmissionCountry { get; set; }
        public string Number { get; set; }
        public DateTime DateOfBirth { get; set; }
        public string Nationality { get; set; }
        public DateTime EmissionDate { get; set; }
        public DateTime ExpirationDate { get; set; }
        public string MRZ { get; set; }
        public string ImageURL { get; set; }
        public bool Validated { get; set; }
        public bool IsValidCheckSum { get; set; }
        public object IsValidManualCheck { get; set; }
        public bool IsValid { get; set; }
    }

    public class DefaultPaymentMethod
    {
        public string Id { get; set; }
        public string Type { get; set; }
        public string ProductId { get; set; }
    }

    public class CreditCard
    {
        public string Id { get; set; }
        public string Type { get; set; }
        public string Description { get; set; }
        public string CardNumber { get; set; }
        public DateTime ExpirationDate { get; set; }
        public string CardHolder { get; set; }
        public string BIN { get; set; }
        public bool Expired { get; set; }
        public string PanHash { get; set; }
        public string PanToken { get; set; }
        public string ExpirationDateStr { get; set; }
        public string MaskedLast4 { get; set; }
        public bool Verified { get; set; }
        public string Label { get; set; }
        public string CurrencyId { get; set; }
        public string TypeOfCard { get; set; }
        public string Kind { get; set; }
        public object AuthorizationURL { get; set; }
        public string AuthorizationType { get; set; }
        public string AuthorizationRequestType { get; set; }
        public bool GuaranteedCard { get; set; }
        public string Schema { get; set; }
    }

    public class PaymentMethods
    {
        public List<DefaultPaymentMethod> DefaultPaymentMethods { get; set; }
        public List<CreditCard> CreditCards { get; set; }
    }

    public class TempPaymentMethods
    {
        public List<object> DefaultPaymentMethods { get; set; }
        public List<object> CreditCards { get; set; }
    }

    public class Created
    {
        public string UserId { get; set; }
        public DateTime Date { get; set; }
    }

    public class Root
    {
        public string Id { get; set; }
        public object MemberId { get; set; }
        public object SourceId { get; set; }
        public string Nickname { get; set; }
        public object PersonalTitle { get; set; }
        public string CountryOfResidence { get; set; }
        public string CountryOfResidenceLabel { get; set; }
        public string Mobile { get; set; }
        public string Email { get; set; }
        public object PreferredLanguage { get; set; }
        public bool HeadOfFamily { get; set; }
        public List<AutoRefundValue> AutoRefundValues { get; set; }
        public IdentityDocument IdentityDocument { get; set; }
        public object PSDocument { get; set; }
        public int Bonus { get; set; }
        public int Earning { get; set; }
        public bool MobileVerified { get; set; }
        public bool Blacklisted { get; set; }
        public string MobileAppLanguage { get; set; }
        public string SMSLanguage { get; set; }
        public PaymentMethods PaymentMethods { get; set; }
        public object ProfileImage { get; set; }
        public bool Deleted { get; set; }
        public object DeletedAt { get; set; }
        public bool PrivacyAccepted { get; set; }
        public bool TermsAndConditionsAccepted { get; set; }
        public bool MarketingAccepted { get; set; }
        public DateTime TermsAndConditionsAcceptanceDate { get; set; }
        public int DataProtectionAcceptedId { get; set; }
        public int PrivacyPolicyAcceptedId { get; set; }
        public int TermOfUseAcceptedId { get; set; }
        public bool IsRegistered { get; set; }
        public bool HasProfileImage { get; set; }
        public bool HasPassportScan { get; set; }
        public TempPaymentMethods TempPaymentMethods { get; set; }
        public object LegalHoldEmail { get; set; }
        public object LegalHoldMobile { get; set; }
        public object ProfileImageUrl { get; set; }
        public bool ToBeVerified { get; set; }
        public Created Created { get; set; }
        public int UnreadCount { get; set; }
        public object OpenId { get; set; }
        public object SessionKey { get; set; }
    }

}