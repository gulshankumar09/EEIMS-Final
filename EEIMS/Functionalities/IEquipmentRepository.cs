using EEIMS.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EEIMS.Functionalities
{
    public interface IEquipmentRepository
    {
        IEnumerable<ViewEquipmentViewModel> GetAllEquipments();
        UpdateEquipmentViewModel GetEquipment(int id);
        int AddEquipment(AddEquipmentViewModel item);
        int RemoveEquipment(int id);
        int UpdateEquipment(UpdateEquipmentViewModel item);
        IEnumerable<ViewEquipmentViewModel> GetNotAssinedEquipments();

    }
}
