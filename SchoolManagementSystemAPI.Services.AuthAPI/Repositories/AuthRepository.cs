using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using SchoolManagementSystemAPI.Services.AuthAPI.Model.DTOs;
using SchoolManagementSystemAPI.Services.AuthAPI.Repositories.Data;
using SchoolManagementSystemAPI.Services.AuthAPI.Repositories.IRepositories;

namespace SchoolManagementSystemAPI.Services.AuthAPI.Repositories
{
    //public class AuthRepository<T> : IAuthRepository<T> where T : RegisterRequestDTO
    public class AuthRepository : IAuthRepository
    {
        private readonly AppDbContext _db;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly IMapper _mapper;

        public AuthRepository(AppDbContext db, UserManager<ApplicationUser> userManager, RoleManager<IdentityRole> roleManager, IMapper mapper)
        {
            _db = db;
            _userManager = userManager;
            _roleManager = roleManager;
            _mapper = mapper;
        }
        public async Task<bool> AssignRole(RegisterRequestDTO registerRequestDTO)
        {
            try
            {
                var user = _db.ApplicationUsers.First(u => u.Email.ToLower() == registerRequestDTO.Email);
                if (user != null)
                {
                    //; if (!_roleManager.RoleExistsAsync(registerRequestDTO.role).GetAwaiter().GetResult())
                    //  {
                    //      _roleManager.CreateAsync(new IdentityRole(registerRequestDTO.role)).GetAwaiter().GetResult();
                    //  }
                    await _userManager.AddToRoleAsync(user, registerRequestDTO.Role);
                    return true;
                }
                throw new Exception("unable to assign role");
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public async Task<bool> RegisterUser(RegisterRequestDTO registerRequestDTO) // Teacher, Parent, Student
        {
            ApplicationUser newUser = _mapper.Map<ApplicationUser>(registerRequestDTO);
            newUser.UserName = registerRequestDTO.Email;
            newUser.NormalizedEmail = registerRequestDTO.Email;

            /* ApplicationUser newUser = new ApplicationUser
             {
                 UserName = registerRequestDTO.Email,
                 Email = registerRequestDTO.Email,
                 NormalizedEmail = registerRequestDTO.Email,
                 PhoneNumber = registerRequestDTO.PhoneNumber,
                 BloodGroup = registerRequestDTO.BloodGroup,
                 Role = registerRequestDTO.Role,
                 Date_of_Birth = registerRequestDTO.Date_of_Birth,
                 FirstName = registerRequestDTO.FirstName,
                 LastName = registerRequestDTO.LastName,
                 OtherName = registerRequestDTO.OtherName,

             };*/


            try
            {
                var register = await _userManager.CreateAsync(newUser, registerRequestDTO.Password);

                if (register.Succeeded)
                {
                    return register.Succeeded;
                }
                else
                {
                    throw new BadHttpRequestException(register.Errors.First().Description);
                }
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public async Task<ApplicationUser> GetUserById(string id)
        {
            var user = await _db.ApplicationUsers.FirstOrDefaultAsync(u => u.Id.ToLower() == id.ToLower());
            if (user == null) return null;

            return user;
        }

        public async Task<ApplicationUser> GetUserByUsername(string username)
        {
            var user = await _db.ApplicationUsers.FirstOrDefaultAsync(u => u.UserName.ToLower() == username.ToLower());
            if (user == null) return null;

            return user;
        }

        public IEnumerable<ApplicationUser> GetUsersByRole(string role)
        {
          return _db.ApplicationUsers.Where(u => u.Role.ToLower() == role.ToLower());
        }

        public IEnumerable<ApplicationUser> GetAllUsers()
        {
            return _db.ApplicationUsers.ToList();
        }

        public async Task<bool> CheckPassword(ApplicationUser user, string password)
        {
            return await _userManager.CheckPasswordAsync(user, password);
        }

        public async Task<IList<string>> GetRoles(ApplicationUser user)
        {
            return await _userManager.GetRolesAsync(user);
        }

        public string CreateRole(string role)
        {
            if (!_roleManager.RoleExistsAsync(role).GetAwaiter().GetResult())
            {
                _roleManager.CreateAsync(new IdentityRole(role)).GetAwaiter().GetResult();
                return "Action created successfully";
            }
            throw new BadHttpRequestException("Role already exist");
        }

        public void Remove(ApplicationUser user)
        {
            _db.ApplicationUsers.Remove(user);
            _db.SaveChanges();
        }
    }
}




/*var filename = prod.ProductId + Path.GetExtension(product.Image.FileName);
var filePath = @"wwwroot\ProductImages\" + filename;
var fileDirectory = Path.Combine(Directory.GetCurrentDirectory(), filePath);
using (var fileStream = new FileStream(fileDirectory, FileMode.Create))
{
    product.Image.CopyTo(fileStream);
};
var baseUrl = $"{HttpContext.Request.Scheme}://{HttpContext.Request.Host.Value}{HttpContext.Request.PathBase.Value}";
prod.ImageUrl = baseUrl + "/ProductImages/" + filename;
_db.Products.Update(prod);
_db.SaveChanges();
*/