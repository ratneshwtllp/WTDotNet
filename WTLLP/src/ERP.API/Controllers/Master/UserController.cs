using System.Linq;
using Microsoft.AspNetCore.Mvc;
using ERP.DataAccess;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Memory;
using System;
using ERP.Domain.Entities;

namespace ERP.Api
{
    [Route("api/[controller]")]
    public class UserController : ControllerBase
    {
        IMemoryCache _memoryCache;
        DBContext _context;
        public UserController(IMemoryCache memoryCache, DBContext context)
        {
            _memoryCache = memoryCache;
            _context = context;
        }

        // GET api/values
        [HttpGet]
        public IQueryable<UserDetails> Get()
        {
            return _context.UserDetails.AsQueryable();
        }

        [Route("{id}")]
        public IActionResult Get(int id)
        {
            try
            {
                return Ok(_context.UserDetails.Where(x => x.UserId == id).FirstOrDefault());
            }
            catch (Exception ex)
            {
                return Ok(ex.InnerException);
            }
        }

        [Route("GetId")]
        public IActionResult GetId(string name, string psd)
        {
            try
            {
                var record = _context.UserDetails.Where(x => x.LoginName == name && x.Password==psd).FirstOrDefault<UserDetails>();
                if (record != null)
                {
                    return Ok(record.UserId);
                }
                else
                {
                    return Ok(0);
                }
            }
            catch (Exception ex)
            {
                return Ok(ex.InnerException);
            }
        }

        [Route("GetUserList")]
        public IActionResult GetUserList(string search)
        {
            try
            {
                if (string.IsNullOrEmpty(search))
                    return Ok(_context.ViewUserDetails.OrderByDescending(x => x.UserId));
                else
                    return Ok(_context.ViewUserDetails
                        .Where(x => x.UserName.Contains(search) || x.DepartmentName.Contains(search) || x.LoginName.Contains(search) || x.Address.Contains(search) || x.Email.Contains(search) || x.PhoneNo.Contains(search) || x.MobileNo.Contains(search))
                        .OrderByDescending(x => x.DepartmentName));
            }
            catch (Exception ex)
            {
                return Ok(ex.InnerException);
            }
        }     

        [Route("GetNewUserId")]
        public IActionResult GetNewUserId()
        {
            try
            {
                long UserId = 0;
                var lastUser = _context.UserDetails.OrderBy(x => x.UserId).LastOrDefault();
                UserId = (lastUser == null ? 0 : lastUser.UserId) + 1;
                return Ok(UserId);
            }
            catch (Exception ex)
            {
                return Ok(ex.InnerException);
            }
        }

        [Route("CheckDuplicate")]
        public IActionResult CheckDuplicate(string value)
        {
            try
            {
                var existingUser = _context.UserDetails.Where(x => x.LoginName == value).FirstOrDefault<UserDetails>();
                if (existingUser != null)
                {
                    return Ok(1);
                }
                else
                {
                    return Ok(0);
                }
            }
            catch (Exception ex)
            {
                return Ok(ex.InnerException);
            }
        }

        [HttpPost]
        public IActionResult Post([FromBody]UserDetails value)
        {
            try
            {
                _context.UserDetails.Add(value);
                _context.SaveChanges();
                return Ok("Record Save");
            }
            catch (Exception ex)
            {
                return Ok(ex.InnerException);
            }
        }

        public IActionResult Put([FromBody]UserDetails value)
        {
            try
            {
                var existingUser = _context.UserDetails.Where(x => x.UserId == value.UserId).FirstOrDefault<UserDetails>();
                if (existingUser == null)
                {
                    return Ok("Not Found");
                }
                existingUser.UserName = value.UserName;
                existingUser.LoginName = value.LoginName;
                existingUser.Password = value.Password;
                existingUser.ConfirmPassword = value.ConfirmPassword;
                existingUser.Address = value.Address;
                existingUser.Email = value.Email;
                existingUser.PhoneNo = value.PhoneNo;
                existingUser.MobileNo = value.MobileNo;
                existingUser.DepartmentId = value.DepartmentId;
                existingUser.DepartmentHead = value.DepartmentHead;
                existingUser.UserTypeId  = value.UserTypeId;
                existingUser.IsActive = value.IsActive;
                _context.UserDetails.Update(existingUser);
                _context.SaveChanges();
                return Ok("Record Updated");
            }
            catch (Exception ex)
            {
                return Ok(ex.InnerException);
            }
        }

        [Route("Delete/{id}")]
        public IActionResult Delete(int id)
        {
            try
            {
                var existingUser = _context.UserDetails.Where(x => x.UserId == id).FirstOrDefault<UserDetails>();
                if (existingUser != null)
                {
                    _context.UserDetails.Remove(existingUser);
                    _context.SaveChanges();
                    return Ok("Record Deleted");
                }
                else
                {
                    return Ok("Not Found");
                }
            }
            catch (Exception ex)
            {
                return Ok(ex.InnerException);
            }
        }
        [Route("GetUserTypeList")]
        public IActionResult GetUserTypeList()
        {
            try
            {
                object usertype;
                usertype = _context.UserType 
                                        .OrderBy(c => c.UserTypeId )
                                        .Select(x => new { Id = x.UserTypeId, Value = x.UserTypeName }).ToList();
                return Ok(usertype);
            }
            catch (Exception ex)
            {
                return Ok(ex.InnerException);
            }
        }


        [Route("GetUserDetails/{userid}")]
        public IActionResult GetUserDetails(int userid)
        {
            try
            {
                var result = _context.ViewUserDetails.Where(x => x.UserId == userid).FirstOrDefault();
                return Ok(result);
            }
            catch (Exception ex)
            {
                return Ok(ex.InnerException);
            }
        }
    }
}
