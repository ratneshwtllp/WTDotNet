
using ERP.Domain.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ERP.Business.Contracts
{
    public interface IRMPropertiesContract
    {     
        Task<int>GetNewRMPId();
        Task<string> Post(RMProperties value);
        Task<string> Put(RMProperties value);       
        Task<List<RMProperties>> RMPropertiesList(string search);
        Task<RMProperties> GetValue(long fitemid);
        Task<int> Delete(int id);
        Task<string> GetRMPValueList();
        Task<string> PostValue(RMPropertiesValue value);      
        Task<string> PutValue(RMPropertiesValue value);
        Task<int> GetNewRMPValueId();
        //Task<List<ViewRMPropertiesDetails>> RMProValueList(RMProSearch PS);
        Task<List<ViewRMPropertiesDetails>> RMProValueList(string search);
        Task<int> DeleteVal(int id);
        Task<RMPropertiesValue> GetRMValue(long fitemid);
        // For Mapping
        Task<string> GetCategoryList();
        Task<List<RMProperties>> GetRMproList();
        Task<long> GetNewMapId();
        Task<long> GetNewRMProId();

        Task<string> PostMapping(RMPropertiesMapping value);
        Task<List<ViewRMPropertiesMapping>> RMMappingList(RMProSearch RMS);
        Task<int> DeleteMapping(int id);
        Task<RMPropertiesMapping> GetMapping(long fitemid);
        Task<List<RMPropertiesMapping>> GetMapList();
        Task<List<RMPropertiesMapping>> GetRMMapProperties(long catid);
        Task<int> DeleteRMmapping(long id);
    
        Task<int> CheckRMPValue(long id,string value,long rmpvalueid);
        Task<int> CheckRMP(string value, long rmpid);

    }
}