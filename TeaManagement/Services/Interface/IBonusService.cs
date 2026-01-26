using System.Threading.Tasks;
using TeaManagement.Dtos;
using TeaManagement.Entities;

namespace TeaManagement.Interface ;

public interface IBonusService
{
    public Task<AddBonus>  AddBonusAsync(AddBonusDto dto);
}