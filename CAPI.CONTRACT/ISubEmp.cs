using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using CAPI.ENTITIES;

namespace CAPI.CONTRACT
{
    public interface ISubEmp
    {
        List<SubEmpDTO> GetSubEmpList();
        //SubEmpDTO GetSubEmp(int eId);
        bool InsertSubEmp(SubEmpDTO data);
        //string UpdateSubEmp(SubEmpDTO data);
        //string DeleteSubEmp(int eId);
    }
}
