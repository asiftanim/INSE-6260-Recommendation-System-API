﻿using Microsoft.EntityFrameworkCore;
using RRS.Core;
using RRS.Data.Entities;
using RRS.Data.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RRS.Data.Implementation
{
    public class UserRepository : IUserRepository
    {
        private readonly RRSDBContext _dbContext;

        public UserRepository(RRSDBContext context) 
        {
            _dbContext = context;
        }

        public List<User> GetUsers()
        {
            return _dbContext.Users.ToList<User>();
        }

        public User GetUserByUserId(string id)
        {
            return _dbContext.Users.Where(u => u.UserId == id).FirstOrDefault();
        }

        public User UpdateUserByUserId(string id)
        {
            var user = _dbContext.Users.SingleOrDefault(x => x.UserId == id);
            if (user != null)
            {
                user.IsNew = false;
                _dbContext.SaveChanges();
            }
            return user;
        }

        public User CreateUser(string email, string password)
        {
            User newUser = new User() { UserId = email, Password = password, IsNew = true };
            _dbContext.Users.Add(newUser);
            _dbContext.SaveChanges();
            return newUser;
        }

        public List<String> GetDistinctUsers()
        {
            return _dbContext.RestaurantsVisited.Select(x => x.UserId).Distinct().ToList(); 
        }
    }
}
