using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using MoneyMe.Model;
using MoneyMe.Model.DataTransferObjects;
using MoneyMe.Service.Interface;

namespace MoneyMe.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(Roles = "Admin")]
    public class CustomerProfileController : ControllerBase
    {
        private readonly ICustomerProfileService _customerProfileService;
        private readonly UserRedirectDetails _userRedirectDetails;

        public CustomerProfileController(ICustomerProfileService customerProfileService, IOptions<UserRedirectDetails> userRedirectDetails)
        {
            _customerProfileService = customerProfileService;
            _userRedirectDetails = userRedirectDetails.Value;
        }


        [HttpPost]
        [Route("Create")]
        public async Task<IActionResult> Create(CustomerProfileDto dto)
        {
            using (_customerProfileService)
            {
                bool result = await _customerProfileService.AddAsync(dto);
                if (!result)
                    return BadRequest();

                int id = _customerProfileService.CustomerProfileId;
                string _userRedirectUrl = $"{_userRedirectDetails.BaseUrl}{_userRedirectDetails.QuoteCalculatorPage}{id}";

                return Ok(_userRedirectUrl);
            }
        }
    }
}
