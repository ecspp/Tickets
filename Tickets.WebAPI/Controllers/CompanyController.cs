using System.Runtime.InteropServices;
using System.Runtime.Intrinsics.X86;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Tickets.Domain;
using Tickets.Domain.Identity;
using Tickets.Repository;
using Tickets.WebAPI.DTOs;

namespace Tickets.WebAPI.Controllers
{
    [Route("api/companies")]
    [ApiController]
    public class CompanyController : ControllerBase
    {
        private readonly ICompanyRepository _companyRepository;
        private readonly IMapper _mapper;
        private readonly IConfiguration _config;
        private readonly UserManager<User> _userManager;

        public CompanyController(
            ICompanyRepository companyREpository, 
            IMapper mapper,
            IConfiguration config,
            UserManager<User> userManager)
        {
            _companyRepository = companyREpository;
            _mapper = mapper;
            _config = config;
            _userManager = userManager;
        }

        [HttpPost("register")]
        [AllowAnonymous]
        public async Task<IActionResult> Register(CompanyRegistrationDTO companyDto)
        {
            try
            {
                var company = _mapper.Map<Company>(companyDto);
                var user = _mapper.Map<User>(companyDto.User);
                _companyRepository.Add(company);
                if (await _companyRepository.SaveChangesAsync()) 
                {
                    user.CompanyId = company.Id;
                    IdentityResult result = await _userManager.CreateAsync(user, companyDto.User.Password);
                    FirstUserDTO userToReturn = _mapper.Map<FirstUserDTO>(user);
                    if (result.Succeeded) {
                        CompanyRegistrationDTO companyToReturn = _mapper.Map<CompanyRegistrationDTO>(company);
                        companyToReturn.User = userToReturn;
                        return Ok(companyToReturn);
                    } else {
                        _companyRepository.Delete(company);
                        await _companyRepository.SaveChangesAsync();
                        return BadRequest();
                    }
                } else
                {
                    return BadRequest();
                }
            }
            catch (System.Exception ex)
            {
                
                return BadRequest(ex.Message);
            }
        }

    }
}