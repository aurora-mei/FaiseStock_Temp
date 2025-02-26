﻿using AutoMapper;
using FaiseStock.API.Services;
using FaiseStock.Data.Models;
using FaiseStock.Data.Models.Dtos;
using FaiseStock.Data.Models.ViewModels;
using FaiseStock.Data.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace FaiseStock.API.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class AdminController : Controller
    {
      
        private readonly IMapper _mapper;
        private readonly ILogger<AdminController> _logger;
        private readonly IAdminService _adminService;
        public AdminController(IAdminService adminService, ILogger<AdminController> logger
            ,IMapper mapper)
        {
            _adminService = adminService;
            _logger = logger;
            _mapper = mapper;
        }

        [HttpPost]
        [Route("create-contest")]
        public async Task<IActionResult> CreateContest([FromBody] ContestDto contestDto)
        {
            //time input format should be 2025-02-26T21:56:00
            _logger.LogInformation("Do CreateContest");
               Contest contest =  await _adminService.CreateContestAsync(_mapper.Map<Contest>(contestDto));
              ContestVM finalContestVm = _mapper.Map<ContestVM>(contest);
               return Ok(finalContestVm);
        }
       
     
    }
}
