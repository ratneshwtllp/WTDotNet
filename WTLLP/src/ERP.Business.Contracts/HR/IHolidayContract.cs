using ERP.Domain.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;
using System;

namespace ERP.Business.Contracts
{
    public interface IHolidayContract
    {
        Task<List<HR_HolidayMaster>> Get();
        Task<HR_HolidayMaster> Get(int id);
        Task<List<HR_HolidayMaster>> GetHolidayList(string search);
        Task<int> GetNewHolidayId();
        Task<int> CheckDuplicate(DateTime value);
        Task<string> Post(HR_HolidayMaster value);
        Task<string> Put(HR_HolidayMaster value);
        Task<int> Delete(int id);
    }
}