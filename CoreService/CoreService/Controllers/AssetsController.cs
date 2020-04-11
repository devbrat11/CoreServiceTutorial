using CoreService.Data.Repository;
using CoreService.Helpers;
using CoreService.Models.InputDto;
using Microsoft.AspNetCore.Mvc;
using System;

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
                if (asset.Owner == null)
                {
                    return NoContent();
                }
                return Ok(asset.Owner);
            }
            return NotFound("Asset allocated to user does not exists.");
        }

        [HttpPost]
        public IActionResult RegisterAsset([FromBody] AssetInputDto assetInput)
        {
            var assetEntity = assetInput.ToEntity();
            var isAssetAdded = _dataStore.TryRegisteringAsset(assetEntity);
            if (isAssetAdded)
            {
                _dataStore.SaveChanges();
                return StatusCode(201, "Asset registered successfully...");
            }

            return BadRequest("Asset is already registered.");
        }

        [HttpPut("{assetSerialNumber}/assign/{userId}")]
        public IActionResult AllocateAssetToUser(string assetSerialNumber, Guid userId)
        {
            var assetAllocationInfo = _dataStore.TryAllocatingAssetToUser(assetSerialNumber, userId);
            if (assetAllocationInfo.Item1)
            {
                _dataStore.SaveChanges();
                return Ok(assetAllocationInfo.Item2);
            }
            return BadRequest(assetAllocationInfo.Item2);
        }

        [HttpPut]
        public IActionResult UpdateAssetDetails([FromBody] AssetInputDto assetInput)
        {
            var isAssetInfoUpdated = _dataStore.TryUpdatingAssetDetails(assetInput.ToEntity());
            _dataStore.SaveChanges();
            if (isAssetInfoUpdated)
            {
                return Ok("Asset details updated successfully.");
            }
            return BadRequest("Failed to update asset details.");
        }
    }


}