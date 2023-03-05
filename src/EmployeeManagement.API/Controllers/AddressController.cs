﻿using EmployeeManagement.Common.Dtos.Address;
using EmployeeManagement.Common.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Infrastructure;

namespace EmployeeManagement.API.Controllers;

[ApiController]
[Route("[controller]")]
public class AddressController : ControllerBase
{
    private IAddressService AddressService { get; }

    public AddressController(IAddressService addressService)
    {
        AddressService = addressService;
    }

    [HttpPost]
    [Route("Create")]
    public async Task<IActionResult> CreateAddress (AddressCreate addressCreate)
    {
        var id = await AddressService.CreateAddressAsync(addressCreate);
        return Ok(id);
    }

    [HttpPut]
    [Route("Update")]
    public async Task<IActionResult> UpdateAddress (AddressUpdate addressUpdate)
    {
        await AddressService.UpdateAddressAsync(addressUpdate);
        return Ok();
    }

    [HttpDelete]
    [Route("Delete")]
    public async Task<IActionResult> DeleteAddress (AddressDelete addressDelete)
    {
        await AddressService.DeleteAddressAsync(addressDelete);
        return Ok();
    }

    [HttpGet]
    [Route("Get/{id}")]
    public async Task<IActionResult> GetAddressById(int id)
    {
        var address = await AddressService.GetAddressByIdAsync(id);
        return Ok(address);
    }

    [HttpGet]
    [Route("Get")]
    public async Task<IActionResult> GetAddresses()
    {
        var addresses = await AddressService.GetAddressesAsync();
        return Ok(addresses);
    }
    
}
