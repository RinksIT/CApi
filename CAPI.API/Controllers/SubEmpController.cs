using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using CAPI.CONTRACT;
using CAPI.ENTITIES;

namespace CAPI.API.Controllers
{
    [Produces("application/json")]
    [Route("api/SubEmp")]
    [ApiController]
    public class SubEmpController : Controller
    {
        private readonly ISubEmp _subEmp;
        public SubEmpController(ISubEmp subEmp)
        {
            _subEmp = subEmp;
        }
        [HttpGet]
        [Route("GetSubEmpList")]
        public List<SubEmpDTO> GetSubEmpList()
        {
            List<SubEmpDTO> lst = new List<SubEmpDTO>();

            try
            {
                lst = _subEmp.GetSubEmpList();
                return lst;
            }
            catch (Exception)
            {
                return lst;
            }
        }

        //Add Person  
        [HttpPost("InsertSubEmp")]
        public bool InsertSubEmp([FromBody] SubEmpDTO subEmpDTO)
        {
            try
            {
                 _subEmp.InsertSubEmp(subEmpDTO);
                return true;
            }
            catch (Exception)
            {

                return false;
            }
        }
    }
}