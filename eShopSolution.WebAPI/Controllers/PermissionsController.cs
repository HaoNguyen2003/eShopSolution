﻿using eShopSolution.BusinessLayer.Abstract;
using eShopSolution.BusinessLayer.Service;
using eShopSolution.DtoLayer.Model;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace eShopSolution.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PermissionsController : ControllerBase
    {
        private readonly IPermissionService _permissionService;

        public PermissionsController(IPermissionService permissionService) {
            _permissionService=permissionService;
        }
        [HttpGet]
        public async Task<IActionResult> GetAll() {
            var result= await _permissionService.GetAll();
            return Ok(result);
        }
        [HttpGet("ID")]
        public async Task<IActionResult> GetByID(int ID)
        {
            var result = await _permissionService.GetByID(ID);
            if (result.code != 200)
                return NotFound();
            return Ok(result);
        }
        [HttpPost]
        public async Task<IActionResult>Create(PermissionModel permission) {
            var result = await _permissionService.Create(permission);
            return StatusCode(result.code,result);
        }
        [HttpPut("ID")]
        public async Task<IActionResult> Update(PermissionModel permission)
        {
            var result = await _permissionService.Update(permission.ID,permission);
            return StatusCode(result.code, result);
        }
        [HttpDelete("ID")]
        public async Task<IActionResult> Delete(int ID)
        {
            var result = await _permissionService.Delete(ID);
            return StatusCode(result.code, result);
        }
    }
}