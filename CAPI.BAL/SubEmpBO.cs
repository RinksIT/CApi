using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using System.Threading.Tasks;
using CAPI.CONTRACT;
using CAPI.ENTITIES;

namespace CAPI.BAL
{
    public class SubEmpBO : ISubEmp
    {
        public int eid;
        public string eName;
        public string eDe;
        private readonly IDapperHelper _db;

        public SubEmpBO(IDapperHelper db)
        {
            _db = db;
        }
        public List<SubEmpDTO> GetSubEmpList()
        {
            List<SubEmpDTO> lst = new List<SubEmpDTO>();
            try
            {
                lst = _db.GetDataList<SubEmpDTO>("GetSubEmps", CommandType.StoredProcedure);
                return lst;
            }
            catch (Exception ex)
            {
                return lst;
            }
        }


        //public async Task<Person> InsertSubEmp(SubEmpDTO subEmp)
        //{
        //    var obj = await _dbContext.Persons.AddAsync(_object);
        //    _dbContext.SaveChanges();
        //    return obj.Entity;
        //}

        public bool InsertSubEmp(SubEmpDTO subEmp)
        {
            _db.AddParameter("@eName", subEmp.eName);
            _db.AddParameter("@eDesignation", subEmp.eDesignation);
            string ans = _db.ExecuteNonQuery("InsertSubEmp", CommandType.StoredProcedure);
            return true;
        }
    }
}
