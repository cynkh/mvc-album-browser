using System.Collections.Generic;
using System.Linq;

using mvc_album_browser.Abstract;
using mvc_album_browser.Entities;
using mvc_album_browser.Models;

namespace mvc_album_browser.Services
{
    public class UsersService
    {
        private readonly IRepository<user> _repository;
        public UsersService(IRepository<user> repository)
        {
            _repository = repository;
        }
        public IEnumerable<User> GetAllUsers()
        {
            return _repository.GetAll().Select(u => _getUserModelFromEntity(u));
        }
        public User GetFirstUserById(int userId)
        {
            var user = _repository.FirstOrDefault(u => u.id == userId);

            return _getUserModelFromEntity(user);
        }
        public IEnumerable<User> GetUsersByIds(IEnumerable<int> userIds)
        {
            return _repository.Where(u => userIds.Contains(u.id)).Select(u => _getUserModelFromEntity(u));
        }
        public User FindFirstUserByExactName(string name)
        {
            var user = _repository.FirstOrDefault(u => u.name == name);

            return _getUserModelFromEntity(user);
        }
        public IEnumerable<User> FindUsersWhoseNamesContain(string name, bool caseSensitive = false)
        {
            return _repository.Where(u => 
                u.name.Trim().ToUpper().Contains(name.Trim().ToUpper()))
                .Select(u => _getUserModelFromEntity(u));
        }
        public IEnumerable<User> FindUsersByExactName(string name)
        {
            return _repository.Where(u => u.name == name).Select(u => _getUserModelFromEntity(u));
        }
        private User _getUserModelFromEntity(user user)
        {
            var address = user.address;

            return new User
            {
                Id = user.id,
                Name = user.name,
                UserName = user.username,
                PhoneNumber = user.phone,
                EmailAddress = user.email,
                Address = new Address
                {
                    Street = address.street,
                    Suite = address.suite,
                    City = address.city,
                    ZipCode = address.zipcode,
                    Geo = new GeoLocation
                    {
                        Latitude = address.geo.lat,
                        Longitude = address.geo.lng
                    }
                }
            };
        }
    }
}