using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using Friender.API.Data;
using Friender.API.Dtos;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Friender.API.Controllers
{
  [Authorize]
  [Route("api/[controller]")]
  [ApiController]
  public class UsersController : ControllerBase
  {
    private readonly IFrienderRepository _repo;
    private readonly IMapper _mapper;
    public UsersController(IFrienderRepository repo, IMapper mapper)
    {
      _mapper = mapper;
      _repo = repo;
    }

    [HttpGet]
    public async Task<IActionResult> GetUsers()
    {
      var users = await _repo.GetUsers();
      return Ok(_mapper.Map<IEnumerable<UserForListDto>>(users));
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetUser(int id)
    {
      var user = await _repo.GetUser(id);

      if (user != null) return Ok(_mapper.Map<UserForDetailedDto>(user));

      return NotFound();
    }
  }
}