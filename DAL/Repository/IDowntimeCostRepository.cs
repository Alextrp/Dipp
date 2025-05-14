using DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Repository
{
    public interface IDowntimeCostRepository: IRepository<DowntimeCost>
    {
        Task<DowntimeCost?> GetByStationAndCargoTypeAsync(int stationId, int cargoTypeId);

    }
}
