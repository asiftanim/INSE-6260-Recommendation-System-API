using RRS.Core;
using RRS.Data.Entities;
using RRS.Data.Interface;
using RRS.Service.DataModel;
using RRS.Service.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace RRS.Service.Implementation
{
    public class UserService: IUserService
    {
        private readonly IUserRepository _userRepository;
        public UserService(IUserRepository userRepository) 
        {
            _userRepository = userRepository;
        }

        public UserModel GetUserByUserId(string id)
        {
            var user = _userRepository.GetUserByUserId(id);
            
            return user != null ? new UserModel() { Id = user.Id, UserId = user.UserId, IsNew = user.IsNew } : null;
        }

        public UserModel UpdateUserByUserId(string id)
        {
            var user = _userRepository.UpdateUserByUserId(id);

            return user != null ? new UserModel() { Id = user.Id, UserId = user.UserId, IsNew = user.IsNew } : null;
        }

        public List<UserModel> GetAllUsers()
        {
            var users = _userRepository.GetUsers();
            List<UserModel> userModels = new List<UserModel>();

            foreach (var user in users)
            {
                userModels.Add(new UserModel() { Id = user.Id, UserId = user.UserId, IsNew = user.IsNew});
            }
            return userModels;
        }

        public UserCreateModel CreateUser(UserCreateModel userCreateModel)
        {
            var user = _userRepository.CreateUser(userCreateModel.UserId, EncryptString(userCreateModel.Password));

            return new UserCreateModel() { Id = user.Id, UserId = user.UserId, IsNew = user.IsNew};
        }

        public void ImportUsers()
        {
            List<String> userIds = _userRepository.GetDistinctUsers();
            foreach(var userId in userIds)
            {
                _userRepository.CreateUser(userId, EncryptString("12345"));
            }
        }

        public UserModel Login(UserCreateModel userModel)
        {
            var user = _userRepository.GetUserByUserId(userModel.UserId);
            if (user != null)
            {
                if (userModel.Password == DecryptString(user.Password))
                {
                    return new UserModel() { Id = user.Id, UserId = user.UserId, IsNew = user.IsNew };
                }
            }

            return new UserModel();
        }

        private string EncryptString(string plainText)
        {
            string key = "b14ca5898a4e4133bbce2ea2315a1916";
            byte[] iv = new byte[16];
            byte[] array;

            using (Aes aes = Aes.Create())
            {
                aes.Key = Encoding.UTF8.GetBytes(key);
                aes.IV = iv;

                ICryptoTransform encryptor = aes.CreateEncryptor(aes.Key, aes.IV);

                using (MemoryStream memoryStream = new MemoryStream())
                {
                    using (CryptoStream cryptoStream = new CryptoStream((Stream)memoryStream, encryptor, CryptoStreamMode.Write))
                    {
                        using (StreamWriter streamWriter = new StreamWriter((Stream)cryptoStream))
                        {
                            streamWriter.Write(plainText);
                        }

                        array = memoryStream.ToArray();
                    }
                }
            }

            return Convert.ToBase64String(array);
        }

        private string DecryptString(string cipherText)
        {
            string key = "b14ca5898a4e4133bbce2ea2315a1916";
            byte[] iv = new byte[16];
            byte[] buffer = Convert.FromBase64String(cipherText);

            using (Aes aes = Aes.Create())
            {
                aes.Key = Encoding.UTF8.GetBytes(key);
                aes.IV = iv;
                ICryptoTransform decryptor = aes.CreateDecryptor(aes.Key, aes.IV);

                using (MemoryStream memoryStream = new MemoryStream(buffer))
                {
                    using (CryptoStream cryptoStream = new CryptoStream((Stream)memoryStream, decryptor, CryptoStreamMode.Read))
                    {
                        using (StreamReader streamReader = new StreamReader((Stream)cryptoStream))
                        {
                            return streamReader.ReadToEnd();
                        }
                    }
                }
            }
        }
    }
}
