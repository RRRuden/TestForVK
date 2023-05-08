using API.Models;
using AutoMapper;
using Domain.Entities;
using Domain.Enums;
using Domain.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly IUserRepository _repository;
        private readonly IMapper _mapper;

        public UsersController(IUserRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody]UserCreateDto request)
        {
            if (!ModelState.IsValid)
                return UnprocessableEntity(ModelState);

            if (await _repository.LoginIsUsed(request.Login))
                return BadRequest("Login already used");

            if (request.UserGroupId == 2 && await _repository.AdminIsExist())
                return BadRequest("Admin exist");

            var user = _mapper.Map<User>(request);

            await _repository.Create(user);

            return Ok();
        }

        [HttpGet]
        public async Task<IActionResult> Users()
        {
            return Ok(await _repository.GetUsersAsync());
        }

        [HttpGet("Pagination")]
        public async Task<IActionResult> Users([FromQuery] PaginationParameters parameters)
        {
            return Ok(await _repository.GetUsersAsync(parameters));
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetUser(Guid id)
        {
            var user = await _repository.GetByIdAsync(id);
            if (user == null)
                return NotFound("User does not exist");

            return Ok(user);
        }

        [HttpPut]
        public async Task<IActionResult> Delete(Guid id)
        {
            var user = await _repository.GetByIdAsync(id);
            if(user == null)
                return NotFound("User does not exist");

            user.UserStateId = (int)State.Blocked;
            await _repository.Update(user);
            return Ok($"User {id} deleted");
        }
    }
}
