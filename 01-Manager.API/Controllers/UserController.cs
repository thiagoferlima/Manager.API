using AutoMapper;
using Manager.API.ViewModels;
using Manager.Core.Exceptions;
using Manager.Services.DTO;
using Manager.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Runtime.InteropServices;
using System.Threading.Tasks;
using System.Diagnostics;
using Manager.API.Utilities;
using Manager.Services.Services;
using EscNet.Mails.Models;
using Manager.Domain.Entities;
using System.Xml.Linq;

namespace Manager.API.Controllers
{
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;
        private readonly IMapper _mapper;

        public UserController(IUserService userService, IMapper mapper)
        {

            _mapper=mapper;
            _userService= userService;
        }

        [HttpPost]
        [Authorize]
        [Route("/api/v1/users/create")]
        public async Task<IActionResult> Create([FromBody] CreateUserViewModel userViewModel)
        {
            try
            {   var userDTO = _mapper.Map<UserDTO>(userViewModel);

                var userCreated = await _userService.Create(userDTO);

                return Ok(new ResultViewModel
                {
                    Message = "Usuário criado com sucesso!",
                    Success = true,
                    Data = new 
                    {
                        Id = userCreated.Id,
                        Name = userCreated.Name,
                        Email = userCreated.Email,
                    }
                });
            }
            catch (DomainException ex)
            {
                return BadRequest(Responses.DomainErrorMessage(ex.Message, ex.Errors));
            }
            catch (Exception)
            {
                return StatusCode(500, Responses.ApplicationErrorMessage());
            }
        }
        [HttpPut]
        [Authorize]
        [Route("/api/v1/users/update")]
        public async Task<IActionResult> Update ([FromBody] UpdateUserViewModel userViewModel) 
        {
            try
            {   
                var userDTO = _mapper.Map<UserDTO>(userViewModel);
                var userUpdated = await _userService.Update(userDTO);
                return Ok(new ResultViewModel
                {
                    Message = "Usuário atulizado com sucesso!",
                    Success = true,
                    Data = new
                    {
                        Id = userUpdated.Id,
                        Name = userUpdated.Name,
                        Email = userUpdated.Email,
                    }
                });            
            }
            catch (DomainException ex) 
            {
                return BadRequest(Responses.DomainErrorMessage(ex.Message, ex.Errors));
            }
            catch (Exception) 
            {
                return StatusCode(500,Responses.ApplicationErrorMessage());
            }
            
        }
        [HttpDelete]
        [Authorize]
        [Route("/api/v1/remove/{id}")]
        public async Task<IActionResult> Remove(long id)
        {
            try
            {

                await _userService.Remove(id);

                return Ok(new ResultViewModel
                {
                    Message = "Usuário removido com sucesso!",
                    Success = true,
                    Data = null
                });
            }
            catch (DomainException ex)
            {
                return BadRequest(Responses.DomainErrorMessage(ex.Message));
            }
            catch (Exception)
            {
                return StatusCode(500, Responses.ApplicationErrorMessage());
            }

        }
        [HttpGet]
        [Authorize]
        [Route("/api/v1/get/{id}")]
        public async Task<IActionResult> GetById(long id)
        {
            try
            {

                await _userService.GetById(id);

                if(User == null) 
                return Ok(new ResultViewModel
                {
                    Message = "Nenhum usuário foi encontrado com o ID informado.",
                    Success = true,
                    Data = User
                });
                return Ok(new ResultViewModel
                {
                    Message = "Usuário encontrado com o Id Informado.",
                    Success = true,
                    Data = User

                });
            }
            catch (DomainException ex)
            {
                return BadRequest(Responses.DomainErrorMessage(ex.Message));
            }
            catch (Exception)
            {
                return StatusCode(500, Responses.ApplicationErrorMessage());
            }

        }
        [HttpGet]
        [Authorize]
        [Route("/api/v1/users/get-all")]
        public async Task<IActionResult> GetAll()
        {
            try
            {

                var allUsers = await _userService.GetAll();
              
                    return Ok(new ResultViewModel
                    {
                        Message = "Usuário encontrado com sucesso!",
                        Success = true,
                        Data = allUsers
                    });
            }
            catch (DomainException ex)
            {
                return BadRequest(Responses.DomainErrorMessage(ex.Message));
            }
            catch (Exception)
            {
                return StatusCode(500, Responses.ApplicationErrorMessage());
            }

        }
        [HttpGet]
        [Authorize]
        [Route("/api/v1/users/get-by-email")]
        public async Task<IActionResult> GetByEmail([FromQuery] string email)
        {
            try
            {

                var user = await _userService.GetByEmail(email);

                if (user == null)
                    return Ok(new ResultViewModel
                    {
                        Message = "Nenhum usuário foi encontrado com o email informado.",
                        Success = true,
                        Data = user
                    });
                return Ok(new ResultViewModel
                {
                    Message = "Usuário encontrado com sucesso!",
                    Success = true,
                    Data = user

                });
            }
            catch (DomainException ex)
            {
                return BadRequest(Responses.DomainErrorMessage(ex.Message));
            }
            catch (Exception)
            {
                return StatusCode(500, Responses.ApplicationErrorMessage());
            }

        }
        [HttpGet]
        [Authorize]
        [Route("/api/v1/users/search-by-name")]
        public async Task<IActionResult> SearchByName([FromQuery] string name)
        {
            try
            {

                var allUsers = await _userService.SearchByName(name);

                if (allUsers.Count == 0)
                    return Ok(new ResultViewModel
                    {
                        Message = "Nenhum usuário foi encontrado com o nome informado.",
                        Success = true,
                        Data = null
                    });
                return Ok(new ResultViewModel
                {
                    Message = "Usuário encontrado com sucesso!.",
                    Success = true,
                    Data = allUsers

                });
            }
            catch (DomainException ex)
            {
                return BadRequest(Responses.DomainErrorMessage(ex.Message));
            }
            catch (Exception)
            {
                return StatusCode(500, Responses.ApplicationErrorMessage());
            }

        }
        [HttpGet]
        [Authorize]
        [Route("/api/v1/users/search-by-email")]
        public async Task<IActionResult> SearchByEmail([FromQuery] string email)
        {
            try
            {

                var allUsers = await _userService.SearchByEmail(email);

                if (allUsers.Count == 0)
                    return Ok(new ResultViewModel
                    {
                        Message = "Nenhum usuário foi encontrado com o email informado.",
                        Success = true,
                        Data = null
                    });
                return Ok(new ResultViewModel
                {
                    Message = "Usuário encontrado com Sucesso!",
                    Success = true,
                    Data = allUsers

                });
            }
            catch (DomainException ex)
            {
                return BadRequest(Responses.DomainErrorMessage(ex.Message));
            }
            catch (Exception)
            {
                return StatusCode(500, Responses.ApplicationErrorMessage());
            }

        }


    }
}