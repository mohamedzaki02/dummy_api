using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using DatingApp.Dtos.User;
using DatingApp.Models;
using DatingApp.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace DatingApp.Controllers
{
    [Authorize]
    [Route("api/users")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IDatingRepository<User> _userRepository;
        private readonly IMapper _mapper;

        public UserController(IDatingRepository<User> userRepository, IMapper mapper)
        {
            _userRepository = userRepository;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<IActionResult> GetUsers()
        {
            return Ok(_mapper.Map<ICollection<UserForListDto>>(await _userRepository.GetAll()));
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetUserById(int id)
        {
            return Ok(_mapper.Map<UserForDetailsDto>(await _userRepository.GetById(id)));
        }
    }
}