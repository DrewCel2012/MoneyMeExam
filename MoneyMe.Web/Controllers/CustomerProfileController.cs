using Mapster;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Extensions.Options;
using MoneyMe.Model;
using MoneyMe.Model.DataTransferObjects;
using MoneyMe.Model.Enums;
using MoneyMe.Model.ViewModels;
using MoneyMe.Service.Factory;
using MoneyMe.Service.Interface;

namespace MoneyMe.Web.Controllers
{
    public class CustomerProfileController : Controller
    {
        private readonly ICustomerProfileService _customerProfileService;
        private readonly IProductsService _productsService;
        private readonly ICustomerProfileLoanAppService _customerProfileLoanAppService;
        private readonly LoanApplicationDetails _loanApplicationDetails;


        public CustomerProfileController(
            ICustomerProfileService customerProfileService,
            IProductsService productsService,
            ICustomerProfileLoanAppService customerProfileLoanAppService,
            IOptions<LoanApplicationDetails> loanApplicationDetails)
        {
            _customerProfileService = customerProfileService;
            _productsService = productsService;
            _customerProfileLoanAppService = customerProfileLoanAppService;
            _loanApplicationDetails = loanApplicationDetails.Value;
        }


        // GET: CustomerProfile
        public async Task<IActionResult> Index()
        {
            using (_customerProfileService)
            {
                var customers = await _customerProfileService.GetAllAsync();
                return View(customers);
            }
        }

        // GET: CustomerProfile/LoanDetails/1
        public async Task<IActionResult> LoanDetails(int? id)
        {
            if (id == null)
                return NotFound();

            using (_customerProfileService)
            {
                var customer = await _customerProfileService.GetByIDAsync<CustomerProfileViewModel>(id.Value);
                if (customer == null)
                    return NotFound();

                return View(customer);
            }
        }

        // GET: CustomerProfile/ApplyLoan/
        public async Task<IActionResult> ApplyLoan(CustomerProfileDto dto)
        {
            var loanApplicationDto = dto.Adapt<LoanApplicationDto>();
            var type = (ProductType)loanApplicationDto.ProductId;
            IRepaymentTypeService repaymentTypeService = RepaymentTypeFactory.GetRepaymentType(type);

            loanApplicationDto.Repayment = repaymentTypeService.ComputePMT(loanApplicationDto.Term, (double)loanApplicationDto.AmountRequired);            
            loanApplicationDto.TotalRepayment = loanApplicationDto.Repayment * loanApplicationDto.Term;

            return View(loanApplicationDto);
        }

        // POST: CustomerProfile/ApplyLoan/
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ApplyLoan(int id, LoanApplicationDto dto)
        {
            if (id != dto.CustomerProfileId)
                return NotFound();

            if (ModelState.IsValid)
            {
                ServiceResponse response = new();
                using (_customerProfileLoanAppService)
                    response = await _customerProfileLoanAppService.CreateLoanApplication(dto);

                if (response.HasError || !string.IsNullOrWhiteSpace(response.Output))
                {
                    string? message = (response.HasError) ? response.Message : response.Output;  
                    ModelState.AddModelError("ApplyLoanErrorMessage", $"Error: {message}");
                    return View(dto);
                }
            }
            
            return RedirectToAction(nameof(Index));
        }

        // GET: CustomerProfile/QuoteCalculator/
        public async Task<IActionResult> QuoteCalculator(int? id)
        {
            if (id == null)
                return NotFound();

            var customerProfile = await _customerProfileService.GetByIDAsync<CustomerProfileDto>(id.Value);
            if (customerProfile == null)
                return NotFound();

            customerProfile.LoanApplicationDetails = _loanApplicationDetails;
            using (_productsService)
                ViewData["ProductId"] = new SelectList(await _productsService.GetAllAsync(), "Id", "Name", customerProfile.ProductId);

            return View(customerProfile);
        }

        // POST: CustomerProfile/QuoteCalculator/
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> QuoteCalculator(int id, [Bind("Id,AmountRequired,Term,Title,FirstName,LastName,DateOfBirth,Mobile,Email,ProductId")] CustomerProfileDto dto)
        {
            if (id != dto.Id)
                return NotFound();
            
            if (ModelState.IsValid)
                return RedirectToAction(nameof(ApplyLoan), dto);
            
            return View(dto);
        }
    }
}
