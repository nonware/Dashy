using Api.Host.Controllers.Base;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Shared.GrainContracts;
using Shared.GrainContracts.GrainInterfaces;
using Shared.Silo;

namespace Api.Host.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TileController : ClusterAwareControllerBase
    {
        public TileController(IClusterClient clusterClient) : base(clusterClient)
        {
        }

        // GET api/<TileController>/5
        [HttpGet("{id}")]
        [AllowAnonymous]
        public async Task<IActionResult> GetTileAsync(string tileId)
        {
            var tile = await ClusterClient.GetGrainWithId<ITileGrain>(tileId).GetStateAsync();
            return Ok(tile);
        }

        // POST api/<TileController>
        [HttpPost]
        [AllowAnonymous]
        public async Task<IActionResult> CreateTileAsync([FromBody] TileState tileState)
        {
            var tileGrain = CreateNewGrain<ITileGrain>(tileState);
            await tileGrain.SetStateAsync(tileState);
            return Ok();
        }

        // PUT api/<TileControler/5
        [HttpPut("{tileId}")]
        [AllowAnonymous]
        public async Task<IActionResult> UpdateTileAsync([FromRoute] string tileId, [FromBody] TileState tileState)
        {
            var tileGrain = ClusterClient.GetGrainWithId<ITileGrain>(tileId);
            await tileGrain.SetStateAsync(tileState);
            return Ok();
        }

        // DELETE api/<TileController>/5
        [HttpDelete("{tileId}")]
        public async Task<IActionResult> Delete(string tileId)
        {
            var grain = ClusterClient.GetGrainWithId<ITileGrain>(tileId);
            await grain.ClearStateAsync();
            return Ok();
        }
    }
}
