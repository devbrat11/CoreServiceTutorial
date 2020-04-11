﻿using System;
using CoreService.Data.Repository;
using CoreService.Helpers;
using CoreService.Models;
using Microsoft.AspNetCore.Mvc;

namespace CoreService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AssetsController : ControllerBase
    {
        private readonly IDataStore _dataStore;

        public AssetsController(IDataStore dataStore)
        {
            _dataStore = dataStore;
        }

        [HttpGet]
        public IActionResult GetAssets()
        {
            var assets = _dataStore.GetAllAssets();
            if (assets == null)
            {
                return NotFound("No asset found.");
            }
            return Ok(assets);
        }

        [HttpGet("{serialNumber}")]
        public IActionResult GetAssetDetails(string serialNumber)
        {
            var asset = _dataStore.GetAsset(serialNumber);
            if (asset != null)
            {
                return Ok(asset);
            }
            return NotFound("Asset not exists.");
        }

        [HttpGet("{serialNumber}/user")]
        public IActionResult GetAssetAllocatedUserInfo(string serialNumber)
        {
            var asset = _dataStore.GetAsset(serialNumber);
            if (asset != null)
            {
                if (asset.OwnerId.Equals(Guid.Empty))
                {
                    return NoContent();
                }
                var user = _dataStore.GetUser(asset.OwnerId);
                if (user != null)
                {
                    return Ok(user);
                }
            }
            return NotFound("Asset allocated to user does not exists.");
        }

        [HttpPost]
        public IActionResult RegisterAsset([FromBody] AssetDto asset)
        {
            var assetEntity = asset.ConvertToEntity();
            var isAssetAdded = _dataStore.TryRegisteringAsset(assetEntity);
            if (isAssetAdded)
            {
                _dataStore.SaveChanges();
                return StatusCode(201, "Asset registered successfully...");
            }

            return BadRequest("Asset is already registered.");
        }

        [HttpPut("{assetSerialNumber}/assign/{userId}")]
        public IActionResult AllocateAssetToUser(string assetSerialNumber,Guid userId)
        {
            var assetAllocationInfo = _dataStore.TryAllocatingAssetToUser(userId, assetSerialNumber);
            if (assetAllocationInfo.Item1)
            {
                _dataStore.SaveChanges();
                return Ok(assetAllocationInfo.Item2);
            }
            return BadRequest(assetAllocationInfo.Item2);
        }

        [HttpPut]
        public IActionResult UpdateAssetDetails([FromBody] AssetDto asset)
        {
            var isAssetInfoUpdated = _dataStore.TryUpdatingAssetDetails(asset.ConvertToEntity());
            _dataStore.SaveChanges();
            if (isAssetInfoUpdated)
            {
                return Ok("Asset details updated successfully.");
            }
            return BadRequest("Failed to update asset details.");
        }
    }

    
}