﻿using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using EscNet.Cryptography.Interfaces;
using Manager.Core.Exceptions;
using Manager.Domain.Entities;
using Manager.Infra.Interfaces;
using Manager.Services.DTO;
using Manager.Services.Interfaces;

namespace Manager.Services.Services
{
    public class UserService : IUserService
    {
        private readonly IMapper _mapper;

        private readonly IRijndaelCryptography _rijndaelCryptography;

        private readonly IUserRepository _userRepository;

        public UserService(IMapper mapper, IUserRepository userRepository, IRijndaelCryptography rijndaelCryptography)
        {
            _mapper=mapper;
            _userRepository=userRepository;
            _rijndaelCryptography = rijndaelCryptography;
        }

        public async Task<UserDTO> Create(UserDTO userDTO)
        {
            var userExits = await _userRepository.GetByEmail(userDTO.Email);
            if (userExits != null)
                throw new DomainException("Já existe um usuário cadastrado com o email informado.");
            var user = _mapper.Map<User>(userDTO);
            user.Validate();
            user.ChangePassword(_rijndaelCryptography.Encrypt(user.Password));

            var userCreated = await _userRepository.Create(user);

            return _mapper.Map<UserDTO>(userCreated);
        }
        public async Task<UserDTO> Update(UserDTO userDTO)
        {
            var userExits = await _userRepository.GetById(userDTO.Id);

            if (userExits == null)
                throw new DomainException("Não existe nenhum usuário com o id informado.");

            var user = _mapper.Map<User>(userDTO);
            user.Validate();
            user.ChangePassword(_rijndaelCryptography.Encrypt(user.Password));

            var userUpdated = await _userRepository.Update(user);

            return _mapper.Map<UserDTO>(userUpdated);
        }
        public async Task Remove(long id)
        {
            await _userRepository.Remove(id);

        }

        public async Task<UserDTO> GetById(long id)
        {
            var user = await _userRepository.GetById(id);
            return _mapper.Map<UserDTO>(user);
        }

        public async Task<List<UserDTO>> GetAll()
        {
            var allUsers = await _userRepository.GetAll();

            return _mapper.Map<List<UserDTO>>(allUsers);
        }

        public async Task<List<UserDTO>> SearchByName(string name)
        {
            var allUsers = await _userRepository.SearchByName(name);
            return _mapper.Map<List<UserDTO>>(allUsers);
        }

        public async Task<List<UserDTO>> SearchByEmail(string email)
        {
            var allUsers = await _userRepository.SearchByEmail(email);
            return _mapper.Map<List<UserDTO>>(allUsers);

        }

        public async Task<UserDTO> GetByEmail(string email)
        {
            var user = await _userRepository.GetByEmail(email);
            return _mapper.Map<UserDTO>(user);
        }



    }
}