using TAMService.Data.Repository;
using TAMService.Helpers;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using TAMService.Models.ResultDto;
using TAMService.Models;

namespace TAMService.Controllers
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

            var requiredAssets = new List<AssetDto>();

            foreach (var asset in assets)
            {
                UserResultDto owner = null;
                if (asset.OwnerId != Guid.Empty)
                {
                    var user = _dataStore.GetUser(asset.OwnerId);
                    if (user != null)
                    {
                        var teamDetails = _dataStore.GetTeam(user.Team);
                        owner = user.AsUserResultDto(teamDetails);
                    }
                }
                requiredAssets.Add(asset.ConvertToAssetDto());
            }

            if (!requiredAssets.Any())
            {
                return NotFound("No asset found.");
            }
            return Ok(requiredAssets);
        }

        [HttpPost]
        public IActionResult RegisterAsset([FromBody] AssetDto asset)
        {
            var assetEntity = asset.ToEntity();
            var isAssetAdded = _dataStore.TryRegisteringAsset(assetEntity);
            if (isAssetAdded)
            {
                _dataStore.SaveChanges();
                return StatusCode(201, "Asset registered successfully...");
            }

            return BadRequest("Asset is already registered.");
        }

        [HttpGet("{serialNumber}")]
        public IActionResult GetAsset(string serialNumber)
        {
            var asset = _dataStore.GetAsset(serialNumber);
            AssetDto resultAsset = null;
            if (asset.OwnerId != Guid.Empty)
            {
                var user = _dataStore.GetUser(asset.OwnerId);
                UserResultDto owner = null;
                if (user != null)
                {
                    var teamDetails = _dataStore.GetTeam(user.Team);
                    owner = user.AsUserResultDto(teamDetails);
                }

                resultAsset = asset.ConvertToAssetDto();
            }

            if (resultAsset == null)
            {
                return NotFound("Asset not exists.");
            }

            return Ok(resultAsset);
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
                var team = _dataStore.GetTeam(user.Team);
                return Ok(user.AsUserResultDto(team));
            }
            return NotFound("Asset allocated to user does not exists.");
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
        public IActionResult UpdateAssetDetails([FromBody] AssetDto asset)
        {
            var isAssetInfoUpdated = _dataStore.TryUpdatingAssetDetails(asset.ToEntity());
            _dataStore.SaveChanges();
            if (isAssetInfoUpdated)
            {
                return Ok("Asset details updated successfully.");
            }
            return BadRequest("Failed to update asset details.");
        }
    }


}